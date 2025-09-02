using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Enums;

namespace maERP.Server.Tests.Features.Customer.Commands;

public class CustomerUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public CustomerUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_CustomerUpdateCommandTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        DbContext.Database.EnsureCreated();

        TenantContext.SetAssignedTenantIds(new[] { 1, 2 });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(int tenantId)
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantId.ToString());
        
        Task.Delay(10).Wait();
    }

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PutAsync(requestUri, content);
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
                    CompanyName = "Original Company",
                    Email = "john.doe@original.com",
                    Phone = "+1111111111",
                    Website = "https://original.com",
                    VatNumber = "VAT111111111",
                    Note = "Original customer",
                    CustomerStatus = CustomerStatus.Active,
                    DateEnrollment = DateTimeOffset.UtcNow.AddDays(-30),
                    TenantId = 1
                };

                var customer2Tenant1 = new maERP.Domain.Entities.Customer
                {
                    Id = 2,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    CompanyName = "Another Company",
                    Email = "jane.smith@another.com",
                    Phone = "+2222222222",
                    Website = "https://another.com",
                    VatNumber = "VAT222222222",
                    Note = "Another customer",
                    CustomerStatus = CustomerStatus.Active,
                    DateEnrollment = DateTimeOffset.UtcNow.AddDays(-20),
                    TenantId = 1
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
                    Note = "Tenant 2 customer",
                    CustomerStatus = CustomerStatus.Active,
                    DateEnrollment = DateTimeOffset.UtcNow.AddDays(-15),
                    TenantId = 2
                };

                DbContext.Customer.AddRange(customer1Tenant1, customer2Tenant1, customer3Tenant2);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private CustomerInputDto CreateValidUpdateDto(int id = 1)
    {
        return new CustomerInputDto
        {
            Id = id,
            Firstname = "Updated John",
            Lastname = "Updated Doe",
            CompanyName = "Updated Company",
            Email = "updated.john.doe@updated.com",
            Phone = "+9999999999",
            Website = "https://updated.com",
            VatNumber = "VAT999999999",
            Note = "Updated customer note",
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTimeOffset.UtcNow.AddDays(-10)
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateCustomer_WithValidData_ShouldReturnNoContent()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(1);
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithValidData_ShouldUpdateDatabase()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(1);
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var updatedCustomer = await DbContext.Customer.FindAsync(1);
        TestAssertions.AssertNotNull(updatedCustomer);
        TestAssertions.AssertEqual("Updated John", updatedCustomer!.Firstname);
        TestAssertions.AssertEqual("Updated Doe", updatedCustomer.Lastname);
        TestAssertions.AssertEqual("Updated Company", updatedCustomer.CompanyName);
        TestAssertions.AssertEqual("updated.john.doe@updated.com", updatedCustomer.Email);
        TestAssertions.AssertEqual("+9999999999", updatedCustomer.Phone);
        TestAssertions.AssertEqual("Updated customer note", updatedCustomer.Note);
    }

    [Fact]
    public async Task UpdateCustomer_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(999);
        var response = await PutAsJsonAsync("/api/v1/Customers/999", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(2);

        var updateData = CreateValidUpdateDto(1);
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateCustomer_WithoutTenantHeader_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();

        var updateData = CreateValidUpdateDto(1);
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithMismatchedIds_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(2); // Body has ID 2
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData); // URL has ID 1

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithEmptyFirstname_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(1);
        updateData.Firstname = "";
        
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_WithEmptyLastname_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(1);
        updateData.Lastname = "";
        
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_WithInvalidEmail_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(1);
        updateData.Email = "invalid-email";
        
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_WithInvalidWebsite_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(1);
        updateData.Website = "invalid-url";
        
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_WithDuplicateNameData_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(1);
        updateData.Firstname = "Jane"; // Same as existing customer with ID 2
        updateData.Lastname = "Smith";
        
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_ChangeStatusToInactive_ShouldUpdateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(1);
        updateData.CustomerStatus = CustomerStatus.Inactive;
        
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var updatedCustomer = await DbContext.Customer.FindAsync(1);
        TestAssertions.AssertEqual(CustomerStatus.Inactive, updatedCustomer!.CustomerStatus);
    }

    [Fact]
    public async Task UpdateCustomer_PartialUpdate_ShouldUpdateOnlyChangedFields()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var originalCustomer = await DbContext.Customer.FindAsync(1);
        var originalCompany = originalCustomer!.CompanyName;

        var updateData = CreateValidUpdateDto(1);
        updateData.CompanyName = originalCompany; // Keep original
        updateData.Firstname = "Only Updated Firstname";
        
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var updatedCustomer = await DbContext.Customer.FindAsync(1);
        TestAssertions.AssertEqual("Only Updated Firstname", updatedCustomer!.Firstname);
        TestAssertions.AssertEqual(originalCompany, updatedCustomer.CompanyName);
    }

    [Fact]
    public async Task UpdateCustomer_TenantIsolation_ShouldOnlyUpdateOwnTenantData()
    {
        await SeedTestDataAsync();
        
        // Try to update customer from tenant 1 while being tenant 2
        SetTenantHeader(2);
        var updateData = CreateValidUpdateDto(1);
        updateData.Firstname = "Hacked Name";
        
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);

        // Verify the customer from tenant 1 was not modified
        var customer1 = await DbContext.Customer.FindAsync(1);
        TestAssertions.AssertNotEqual("Hacked Name", customer1!.Firstname);
        TestAssertions.AssertEqual("John", customer1.Firstname); // Original name

        // Verify that tenant 2 can update its own customer
        SetTenantHeader(2);
        var updateDataT2 = CreateValidUpdateDto(3);
        updateDataT2.Firstname = "Updated Bob";
        
        var responseT2 = await PutAsJsonAsync("/api/v1/Customers/3", updateDataT2);
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, responseT2.StatusCode);

        var customer3 = await DbContext.Customer.FindAsync(3);
        TestAssertions.AssertEqual("Updated Bob", customer3!.Firstname);
    }

    [Fact]
    public async Task UpdateCustomer_WithZeroId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(0);
        var response = await PutAsJsonAsync("/api/v1/Customers/0", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithNegativeId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(-1);
        var response = await PutAsJsonAsync("/api/v1/Customers/-1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithLargeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(2147483647);
        var response = await PutAsJsonAsync("/api/v1/Customers/2147483647", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithNullJson_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await PutAsJsonAsync("/api/v1/Customers/1", (CustomerInputDto)null!);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithMalformedJson_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var malformedJson = "{id: 1, firstname: 'Updated'}"; // Missing quotes
        var content = new StringContent(malformedJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync("/api/v1/Customers/1", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithUnicodeCharacters_ShouldUpdateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(1);
        updateData.Firstname = "José";
        updateData.Lastname = "García";
        updateData.CompanyName = "Ñueva Compañía";
        updateData.Note = "Müller & Søn";
        
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var updatedCustomer = await DbContext.Customer.FindAsync(1);
        TestAssertions.AssertEqual("José", updatedCustomer!.Firstname);
        TestAssertions.AssertEqual("García", updatedCustomer.Lastname);
        TestAssertions.AssertEqual("Ñueva Compañía", updatedCustomer.CompanyName);
    }

    [Fact]
    public async Task UpdateCustomer_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData = CreateValidUpdateDto(999); // Non-existent ID
        var response = await PutAsJsonAsync("/api/v1/Customers/999", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_ConcurrentUpdate_ShouldHandleCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var updateData1 = CreateValidUpdateDto(1);
        updateData1.Firstname = "First Update";
        
        var updateData2 = CreateValidUpdateDto(1);
        updateData2.Firstname = "Second Update";

        var response1Task = PutAsJsonAsync("/api/v1/Customers/1", updateData1);
        var response2Task = PutAsJsonAsync("/api/v1/Customers/1", updateData2);

        await Task.WhenAll(response1Task, response2Task);

        var response1 = await response1Task;
        var response2 = await response2Task;

        // At least one should succeed
        var oneSucceeded = response1.StatusCode == HttpStatusCode.NoContent || 
                          response2.StatusCode == HttpStatusCode.NoContent;
        TestAssertions.AssertTrue(oneSucceeded);

        var finalCustomer = await DbContext.Customer.FindAsync(1);
        TestAssertions.AssertNotNull(finalCustomer);
        TestAssertions.AssertTrue(finalCustomer!.Firstname == "First Update" || 
                                 finalCustomer.Firstname == "Second Update");
    }

    [Fact]
    public async Task UpdateCustomer_WithSameData_ShouldReturnNoContent()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var originalCustomer = await DbContext.Customer.AsNoTracking().FirstAsync(c => c.Id == 1);
        
        var updateData = new CustomerInputDto
        {
            Id = 1,
            Firstname = originalCustomer.Firstname,
            Lastname = originalCustomer.Lastname,
            CompanyName = originalCustomer.CompanyName,
            Email = originalCustomer.Email,
            Phone = originalCustomer.Phone,
            Website = originalCustomer.Website,
            VatNumber = originalCustomer.VatNumber,
            Note = originalCustomer.Note,
            CustomerStatus = originalCustomer.CustomerStatus,
            DateEnrollment = originalCustomer.DateEnrollment
        };
        
        var response = await PutAsJsonAsync("/api/v1/Customers/1", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }
}