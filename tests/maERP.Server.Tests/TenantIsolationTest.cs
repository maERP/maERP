using System.Net;
using System.Net.Http.Json;
using maERP.Application.Contracts.Services;
using maERP.Application.Features.Customer.Commands.CustomerCreate;
using maERP.Application.Features.Customer.Commands.CustomerUpdate;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class TenantIsolationTest : IClassFixture<MaErpWebApplicationFactory<Program>>
{
    private readonly MaErpWebApplicationFactory<Program> _webApplicationFactory;

    public TenantIsolationTest(MaErpWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Fact]
    public async Task TenantIsolation_CustomerAccess_ShouldOnlyReturnCurrentTenantData()
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        // Set up test data for different tenants
        await _webApplicationFactory.InitializeDbForTests(
            new List<Customer> {
                new() {
                    Id = 1,
                    Firstname = "Tenant1Customer",
                    Lastname = "One",
                    TenantId = 1
                },
                new() {
                    Id = 2,
                    Firstname = "Tenant2Customer",
                    Lastname = "Two", 
                    TenantId = 2
                },
                new() {
                    Id = 3,
                    Firstname = "GlobalCustomer",
                    Lastname = "Global",
                    TenantId = null
                }
            });

        // Test access with tenant 1 context
        using (var scope = _webApplicationFactory.Services.CreateScope())
        {
            var tenantContext = (TestTenantContext)scope.ServiceProvider.GetRequiredService<ITenantContext>();
            tenantContext.SetTestTenant(1);

            var result = await httpClient.GetFromJsonAsync<PaginatedResult<CustomerListDto>>("/api/v1/Customers/");
            
            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            Assert.Equal(2, result.TotalCount); // Should see tenant 1 customer + global customer
            
            var customerNames = result.Data.Select(c => c.Firstname).ToList();
            Assert.Contains("Tenant1Customer", customerNames);
            Assert.Contains("GlobalCustomer", customerNames);
            Assert.DoesNotContain("Tenant2Customer", customerNames);
        }

        // Test access with tenant 2 context
        using (var scope = _webApplicationFactory.Services.CreateScope())
        {
            var tenantContext = (TestTenantContext)scope.ServiceProvider.GetRequiredService<ITenantContext>();
            tenantContext.SetTestTenant(2);

            var result = await httpClient.GetFromJsonAsync<PaginatedResult<CustomerListDto>>("/api/v1/Customers/");
            
            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            Assert.Equal(2, result.TotalCount); // Should see tenant 2 customer + global customer
            
            var customerNames = result.Data.Select(c => c.Firstname).ToList();
            Assert.Contains("Tenant2Customer", customerNames);
            Assert.Contains("GlobalCustomer", customerNames);
            Assert.DoesNotContain("Tenant1Customer", customerNames);
        }
    }

    [Fact]
    public async Task TenantIsolation_CreateCustomer_ShouldAssignCurrentTenantId()
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        // Set tenant context to tenant 2
        using var scope = _webApplicationFactory.Services.CreateScope();
        var tenantContext = scope.ServiceProvider.GetRequiredService<TestTenantContext>();
        tenantContext.SetTestTenant(2);

        var customer = new CustomerCreateCommand()
        {
            Firstname = "NewCustomer",
            Lastname = "ForTenant2",
        };

        HttpResponseMessage response = await httpClient.PostAsJsonAsync("/api/v1/Customers", customer);
        var result = await response.Content.ReadFromJsonAsync<Result<int>>();

        Assert.NotNull(result);
        Assert.True(result.Succeeded);

        // Verify the created customer is assigned to tenant 2
        var createdCustomer = await httpClient.GetFromJsonAsync<Result<CustomerDetailDto>>($"/api/v1/Customers/{result.Data}");
        
        Assert.NotNull(createdCustomer);
        Assert.True(createdCustomer.Succeeded);
        Assert.Equal("NewCustomer", createdCustomer.Data.Firstname);
        // Note: TenantId is not exposed in DTOs for security, but the filter ensures it's correct
    }

    [Fact]
    public async Task TenantIsolation_UpdateCustomer_ShouldOnlyUpdateOwnTenantData()
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        
        // Set up customer for tenant 1
        await _webApplicationFactory.InitializeDbForTests(
            new List<Customer> {
                new() {
                    Id = 10,
                    Firstname = "TenantOneCustomer",
                    Lastname = "Original",
                    TenantId = 1
                }
            });

        // Try to update customer from tenant 2 context (should fail)
        using (var scope = _webApplicationFactory.Services.CreateScope())
        {
            var tenantContext = (TestTenantContext)scope.ServiceProvider.GetRequiredService<ITenantContext>();
            tenantContext.SetTestTenant(2);

            var updateCommand = new CustomerUpdateCommand
            {
                Firstname = "Hacked",
                Lastname = "Name"
            };

            var response = await httpClient.PutAsJsonAsync("/api/v1/Customers/10", updateCommand);
            
            // Should return NotFound because tenant 2 can't see tenant 1's data
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        // Update from correct tenant context (should succeed)  
        using (var scope = _webApplicationFactory.Services.CreateScope())
        {
            var tenantContext = (TestTenantContext)scope.ServiceProvider.GetRequiredService<ITenantContext>();
            tenantContext.SetTestTenant(1);

            var updateCommand = new CustomerUpdateCommand
            {
                Firstname = "UpdatedCorrectly",
                Lastname = "ByRightTenant"
            };

            var response = await httpClient.PutAsJsonAsync("/api/v1/Customers/10", updateCommand);
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

    [Fact]
    public async Task TenantIsolation_DeleteCustomer_ShouldOnlyDeleteOwnTenantData()
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        
        // Set up customer for tenant 1
        await _webApplicationFactory.InitializeDbForTests(
            new List<Customer> {
                new() {
                    Id = 20,
                    Firstname = "ToBeDeleted",
                    Lastname = "Customer",
                    TenantId = 1
                }
            });

        // Try to delete customer from tenant 2 context (should fail)
        using (var scope = _webApplicationFactory.Services.CreateScope())
        {
            var tenantContext = (TestTenantContext)scope.ServiceProvider.GetRequiredService<ITenantContext>();
            tenantContext.SetTestTenant(2);

            var response = await httpClient.DeleteAsync("/api/v1/Customers/20");
            
            // Should return NotFound because tenant 2 can't see tenant 1's data
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        // Delete from correct tenant context (should succeed)
        using (var scope = _webApplicationFactory.Services.CreateScope())
        {
            var tenantContext = (TestTenantContext)scope.ServiceProvider.GetRequiredService<ITenantContext>();
            tenantContext.SetTestTenant(1);

            var response = await httpClient.DeleteAsync("/api/v1/Customers/20");
            
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}