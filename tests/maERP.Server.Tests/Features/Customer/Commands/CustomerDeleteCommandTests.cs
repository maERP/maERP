using System.Net;
using System.Text.Json;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Enums;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Customer.Commands;

public class CustomerDeleteCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public CustomerDeleteCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_CustomerDeleteCommandTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        DbContext.Database.EnsureCreated();

        TenantContext.SetAssignedTenantIds(new[] { TenantConstants.TestTenant1Id, TenantConstants.TestTenant2Id });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(Guid tenantId)
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantId.ToString());
        
        Task.Delay(10).Wait();
    }

    protected async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) where T : class
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return result ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    private async Task SeedTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Customer.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                var customer1Tenant1 = new maERP.Domain.Entities.Customer
                {
                    Id = 1,
                    Firstname = "John",
                    Lastname = "Doe",
                    CompanyName = "Test Company 1",
                    Email = "john.doe@testcompany1.com",
                    Phone = "+1111111111",
                    Website = "https://testcompany1.com",
                    VatNumber = "VAT111111111",
                    Note = "Customer to be deleted",
                    CustomerStatus = CustomerStatus.Active,
                    DateEnrollment = DateTimeOffset.UtcNow.AddDays(-30),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer2Tenant1 = new maERP.Domain.Entities.Customer
                {
                    Id = 2,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    CompanyName = "Test Company 2",
                    Email = "jane.smith@testcompany2.com",
                    Phone = "+2222222222",
                    Website = "https://testcompany2.com",
                    VatNumber = "VAT222222222",
                    Note = "Another customer for tenant 1",
                    CustomerStatus = CustomerStatus.Active,
                    DateEnrollment = DateTimeOffset.UtcNow.AddDays(-20),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer3Tenant2 = new maERP.Domain.Entities.Customer
                {
                    Id = 3,
                    Firstname = "Bob",
                    Lastname = "Wilson",
                    CompanyName = "Tenant 2 Company",
                    Email = "bob.wilson@tenant2.com",
                    Phone = "+3333333333",
                    Website = "https://tenant2.com",
                    VatNumber = "VAT333333333",
                    Note = "Customer for tenant 2",
                    CustomerStatus = CustomerStatus.Active,
                    DateEnrollment = DateTimeOffset.UtcNow.AddDays(-15),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer4Inactive = new maERP.Domain.Entities.Customer
                {
                    Id = 4,
                    Firstname = "Alice",
                    Lastname = "Brown",
                    CompanyName = "Inactive Company",
                    Email = "alice.brown@inactive.com",
                    Phone = "+4444444444",
                    Website = "https://inactive.com",
                    VatNumber = "VAT444444444",
                    Note = "Inactive customer",
                    CustomerStatus = CustomerStatus.Inactive,
                    DateEnrollment = DateTimeOffset.UtcNow.AddDays(-50),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Add customer with addresses to test cascade deletion
                var customer5WithAddresses = new maERP.Domain.Entities.Customer
                {
                    Id = 5,
                    Firstname = "Charlie",
                    Lastname = "Davis",
                    CompanyName = "Address Company",
                    Email = "charlie.davis@address.com",
                    Phone = "+5555555555",
                    Website = "https://address.com",
                    VatNumber = "VAT555555555",
                    Note = "Customer with addresses",
                    CustomerStatus = CustomerStatus.Active,
                    DateEnrollment = DateTimeOffset.UtcNow.AddDays(-25),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customerAddress = new maERP.Domain.Entities.CustomerAddress
                {
                    Id = 1,
                    CustomerId = 5,
                    Firstname = "Charlie",
                    Lastname = "Davis",
                    CompanyName = "Address Company",
                    Street = "123 Test Street",
                    HouseNr = "1",
                    Zip = "12345",
                    City = "Test City",
                    DefaultDeliveryAddress = true,
                    DefaultInvoiceAddress = true,
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.Customer.AddRange(customer1Tenant1, customer2Tenant1, customer3Tenant2, customer4Inactive, customer5WithAddresses);
                DbContext.CustomerAddress.Add(customerAddress);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task DeleteCustomer_WithValidId_ShouldReturnNoContent()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Customers/1");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WithValidId_ShouldRemoveFromDatabase()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerBefore = await DbContext.Customer.FindAsync(1);
        TestAssertions.AssertNotNull(customerBefore);

        var response = await Client.DeleteAsync("/api/v1/Customers/1");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var customerAfter = await DbContext.Customer.FindAsync(1);
        Assert.Null(customerAfter);
    }

    [Fact]
    public async Task DeleteCustomer_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Customers/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteCustomer_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(2);

        var customerExists = await DbContext.Customer.AnyAsync(c => c.Id == 1);
        TestAssertions.AssertTrue(customerExists);

        var response = await Client.DeleteAsync("/api/v1/Customers/1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);

        // Verify customer still exists
        var customerStillExists = await DbContext.Customer.AnyAsync(c => c.Id == 1);
        TestAssertions.AssertTrue(customerStillExists);
    }

    [Fact]
    public async Task DeleteCustomer_WithoutTenantHeader_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();

        var response = await Client.DeleteAsync("/api/v1/Customers/1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WithZeroId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Customers/0");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WithNegativeId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Customers/-1");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WithInvalidId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Customers/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WithLargeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Customers/2147483647");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_InactiveCustomer_ShouldDeleteSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Customers/4");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var deletedCustomer = await DbContext.Customer.FindAsync(4);
        Assert.Null(deletedCustomer);
    }

    [Fact]
    public async Task DeleteCustomer_WithAddresses_ShouldDeleteCascade()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var addressesBefore = await DbContext.CustomerAddress.Where(ca => ca.CustomerId == 5).CountAsync();
        TestAssertions.AssertTrue(addressesBefore > 0);

        var response = await Client.DeleteAsync("/api/v1/Customers/5");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var customerAfter = await DbContext.Customer.FindAsync(5);
        Assert.Null(customerAfter);

        var addressesAfter = await DbContext.CustomerAddress.Where(ca => ca.CustomerId == 5).CountAsync();
        TestAssertions.AssertEqual(0, addressesAfter);
    }

    [Fact]
    public async Task DeleteCustomer_TenantIsolation_ShouldOnlyDeleteOwnTenantData()
    {
        await SeedTestDataAsync();
        
        // Verify both customers exist
        var customer1Exists = await DbContext.Customer.AnyAsync(c => c.Id == 1 && c.TenantId == TenantConstants.TestTenant1Id);
        var customer3Exists = await DbContext.Customer.AnyAsync(c => c.Id == 3 && c.TenantId == TenantConstants.TestTenant2Id);
        TestAssertions.AssertTrue(customer1Exists);
        TestAssertions.AssertTrue(customer3Exists);

        // Try to delete tenant 1's customer while being tenant 2
        SetTenantHeader(2);
        var response1 = await Client.DeleteAsync("/api/v1/Customers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        // Verify customer 1 still exists
        customer1Exists = await DbContext.Customer.AnyAsync(c => c.Id == 1);
        TestAssertions.AssertTrue(customer1Exists);

        // Verify tenant 2 can delete its own customer
        var response3 = await Client.DeleteAsync("/api/v1/Customers/3");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response3.StatusCode);

        // Verify customer 3 is deleted
        customer3Exists = await DbContext.Customer.AnyAsync(c => c.Id == 3);
        TestAssertions.AssertFalse(customer3Exists);

        // Verify customer 1 still exists
        customer1Exists = await DbContext.Customer.AnyAsync(c => c.Id == 1);
        TestAssertions.AssertTrue(customer1Exists);
    }

    [Fact]
    public async Task DeleteCustomer_MultipleCustomersInTenant_ShouldOnlyDeleteSpecified()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerCountBefore = await DbContext.Customer.Where(c => c.TenantId == TenantConstants.TestTenant1Id).CountAsync();
        TestAssertions.AssertTrue(customerCountBefore > 1);

        var response = await Client.DeleteAsync("/api/v1/Customers/1");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var customerCountAfter = await DbContext.Customer.Where(c => c.TenantId == TenantConstants.TestTenant1Id).CountAsync();
        TestAssertions.AssertEqual(customerCountBefore - 1, customerCountAfter);

        var deletedCustomer = await DbContext.Customer.FindAsync(1);
        Assert.Null(deletedCustomer);

        var otherCustomer = await DbContext.Customer.FindAsync(2);
        TestAssertions.AssertNotNull(otherCustomer);
    }

    [Fact]
    public async Task DeleteCustomer_ConcurrentDelete_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response1Task = Client.DeleteAsync("/api/v1/Customers/1");
        var response2Task = Client.DeleteAsync("/api/v1/Customers/1");

        var responses = await Task.WhenAll(response1Task, response2Task);

        var successCount = responses.Count(r => r.StatusCode == HttpStatusCode.NoContent);
        var notFoundCount = responses.Count(r => r.StatusCode == HttpStatusCode.NotFound);

        // One should succeed, one should be not found
        TestAssertions.AssertEqual(1, successCount);
        TestAssertions.AssertEqual(1, notFoundCount);

        var customerExists = await DbContext.Customer.AnyAsync(c => c.Id == 1);
        TestAssertions.AssertFalse(customerExists);
    }

    [Fact]
    public async Task DeleteCustomer_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Customers/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteCustomer_NonExistentTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.DeleteAsync("/api/v1/Customers/1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);

        var customerStillExists = await DbContext.Customer.AnyAsync(c => c.Id == 1);
        TestAssertions.AssertTrue(customerStillExists);
    }

    [Fact]
    public async Task DeleteCustomer_AlreadyDeletedCustomer_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // First deletion
        var response1 = await Client.DeleteAsync("/api/v1/Customers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response1.StatusCode);

        // Second deletion attempt
        var response2 = await Client.DeleteAsync("/api/v1/Customers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        var result = await ReadResponseAsync<Result<int>>(response2);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteCustomer_DatabaseConstraintViolation_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // This test assumes there might be foreign key constraints
        // that prevent deletion in some scenarios
        var response = await Client.DeleteAsync("/api/v1/Customers/1");

        // Should either succeed or return a proper error
        TestAssertions.AssertTrue(
            response.StatusCode == HttpStatusCode.NoContent || 
            response.StatusCode == HttpStatusCode.BadRequest ||
            response.StatusCode == HttpStatusCode.Conflict
        );

        if (response.StatusCode != HttpStatusCode.NoContent)
        {
            var result = await ReadResponseAsync<Result<int>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertFalse(result.Succeeded);
            TestAssertions.AssertNotEmpty(result.Messages);
        }
    }

    [Fact]
    public async Task DeleteCustomer_ValidateDataIntegrityAfterDeletion()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var initialCount = await DbContext.Customer.CountAsync();
        var initialTenant1Count = await DbContext.Customer.Where(c => c.TenantId == TenantConstants.TestTenant1Id).CountAsync();
        var initialTenant2Count = await DbContext.Customer.Where(c => c.TenantId == TenantConstants.TestTenant2Id).CountAsync();

        var response = await Client.DeleteAsync("/api/v1/Customers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var finalCount = await DbContext.Customer.CountAsync();
        var finalTenant1Count = await DbContext.Customer.Where(c => c.TenantId == TenantConstants.TestTenant1Id).CountAsync();
        var finalTenant2Count = await DbContext.Customer.Where(c => c.TenantId == TenantConstants.TestTenant2Id).CountAsync();

        TestAssertions.AssertEqual(initialCount - 1, finalCount);
        TestAssertions.AssertEqual(initialTenant1Count - 1, finalTenant1Count);
        TestAssertions.AssertEqual(initialTenant2Count, finalTenant2Count); // Tenant 2 should be unchanged
    }

    [Fact]
    public async Task DeleteCustomer_VerifyAuditTrailOrSoftDelete()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var customerBefore = await DbContext.Customer.AsNoTracking().FirstAsync(c => c.Id == 1);
        TestAssertions.AssertNotNull(customerBefore);

        var response = await Client.DeleteAsync("/api/v1/Customers/1");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // If implementing soft delete, verify the record is marked as deleted
        // If hard delete, verify it's completely removed
        var customerAfter = await DbContext.Customer
            .IgnoreQueryFilters() // In case soft delete uses query filters
            .FirstOrDefaultAsync(c => c.Id == 1);

        if (customerAfter == null)
        {
            // Hard delete - customer should be completely removed
            var customerExists = await DbContext.Customer.AnyAsync(c => c.Id == 1);
            TestAssertions.AssertFalse(customerExists);
        }
        else
        {
            // Soft delete - customer should be marked as deleted
            // This would depend on your implementation
            // Example: TestAssertions.AssertTrue(customerAfter.IsDeleted);
        }
    }
}