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
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Customer.Commands;

public class CustomerUpdateCommandTests : TenantIsolatedTestBase
{
    private static readonly Guid Customer1Id = new("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    private static readonly Guid Customer2Id = new("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
    private static readonly Guid Customer3Id = new("cccccccc-cccc-cccc-cccc-cccccccccccc");
    private static readonly Guid NonExistentId = new("99999999-9999-9999-9999-999999999999");
    private static readonly Guid ZeroId = new("00000000-0000-0000-0000-000000000000");
    private static readonly Guid LargeId = new("ffffffff-ffff-ffff-ffff-ffffffffffff");


    private async Task SeedCustomerUpdateTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

            var customer1Tenant1 = new maERP.Domain.Entities.Customer
            {
                Id = Customer1Id,
                CustomerId = 1,
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
                TenantId = TenantConstants.TestTenant1Id
            };

            var customer2Tenant1 = new maERP.Domain.Entities.Customer
            {
                Id = Customer2Id,
                CustomerId = 2,
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
                TenantId = TenantConstants.TestTenant1Id
            };

            var customer3Tenant2 = new maERP.Domain.Entities.Customer
            {
                Id = Customer3Id,
                CustomerId = 3,
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
                TenantId = TenantConstants.TestTenant2Id
            };

            DbContext.Customer.AddRange(customer1Tenant1, customer2Tenant1, customer3Tenant2);
            await DbContext.SaveChangesAsync();
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private CustomerInputDto CreateValidUpdateDto(Guid? id = null)
    {
        return new CustomerInputDto
        {
            Id = id ?? Customer1Id,
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


    [Fact]
    public async Task UpdateCustomer_WithValidData_ShouldReturnNoContent()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(Customer1Id);
        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithValidData_ShouldUpdateDatabase()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Clear change tracker to avoid conflicts with entity tracking
        DbContext.ChangeTracker.Clear();

        var updateData = CreateValidUpdateDto(Customer1Id);
        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var updatedCustomer = await DbContext.Customer.FindAsync(Customer1Id);
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
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(NonExistentId);
        var response = await PutAsJsonAsync($"/api/v1/Customers/{NonExistentId}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var updateData = CreateValidUpdateDto(Customer1Id);
        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateCustomer_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedCustomerUpdateTestDataAsync();
        RemoveTenantHeader();

        var updateData = CreateValidUpdateDto(Customer1Id);
        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        // Without tenant header, the customer cannot be found due to tenant isolation
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithMismatchedIds_ShouldReturnBadRequest()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(Customer2Id); // Body has ID 2
        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData); // URL has ID 1

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithEmptyFirstname_ShouldReturnBadRequest()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(Customer1Id);
        updateData.Firstname = "";

        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_WithEmptyLastname_ShouldReturnBadRequest()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(Customer1Id);
        updateData.Lastname = "";

        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_WithInvalidEmail_ShouldReturnBadRequest()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(Customer1Id);
        updateData.Email = "invalid-email";

        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_WithInvalidWebsite_ShouldReturnBadRequest()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(Customer1Id);
        updateData.Website = "invalid-url";

        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_WithDuplicateNameData_ShouldReturnBadRequest()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(Customer1Id);
        updateData.Firstname = "Jane"; // Same as existing customer with ID 2
        updateData.Lastname = "Smith";

        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_ChangeStatusToInactive_ShouldUpdateSuccessfully()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(Customer1Id);
        updateData.CustomerStatus = CustomerStatus.Inactive;

        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Clear change tracker to ensure fresh database read
        DbContext.ChangeTracker.Clear();
        
        // Use IgnoreQueryFilters to read the updated customer directly from database
        var updatedCustomer = await DbContext.Customer.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == Customer1Id);
        TestAssertions.AssertEqual(CustomerStatus.Inactive, updatedCustomer!.CustomerStatus);
    }

    [Fact]
    public async Task UpdateCustomer_PartialUpdate_ShouldUpdateOnlyChangedFields()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var originalCustomer = await DbContext.Customer.FindAsync(Customer1Id);
        var originalCompany = originalCustomer!.CompanyName;

        // Clear change tracker to avoid conflicts with entity tracking in the update handler
        DbContext.ChangeTracker.Clear();

        var updateData = CreateValidUpdateDto(Customer1Id);
        updateData.CompanyName = originalCompany; // Keep original
        updateData.Firstname = "Only Updated Firstname";

        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var updatedCustomer = await DbContext.Customer.FindAsync(Customer1Id);
        TestAssertions.AssertEqual("Only Updated Firstname", updatedCustomer!.Firstname);
        TestAssertions.AssertEqual(originalCompany, updatedCustomer.CompanyName);
    }

    [Fact]
    public async Task UpdateCustomer_TenantIsolation_ShouldOnlyUpdateOwnTenantData()
    {
        await SeedCustomerUpdateTestDataAsync();

        // Try to update customer from tenant 1 while being tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var updateData = CreateValidUpdateDto(Customer1Id);
        updateData.Firstname = "Hacked Name";

        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);

        // Verify the customer from tenant 1 was not modified
        var customer1 = await DbContext.Customer.FindAsync(Customer1Id);
        TestAssertions.AssertNotEqual("Hacked Name", customer1!.Firstname);
        TestAssertions.AssertEqual("John", customer1.Firstname); // Original name

        // Clear change tracker to avoid conflicts with entity tracking in the update handler
        DbContext.ChangeTracker.Clear();

        // Verify that tenant 2 can update its own customer
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var updateDataT2 = CreateValidUpdateDto(Customer3Id);
        updateDataT2.Firstname = "Updated Bob";

        var responseT2 = await PutAsJsonAsync($"/api/v1/Customers/{Customer3Id}", updateDataT2);
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, responseT2.StatusCode);

        var customer3 = await DbContext.Customer.FindAsync(Customer3Id);
        TestAssertions.AssertEqual("Updated Bob", customer3!.Firstname);
    }

    [Fact]
    public async Task UpdateCustomer_WithZeroId_ShouldReturnBadRequest()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(ZeroId);
        var response = await PutAsJsonAsync($"/api/v1/Customers/{ZeroId}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithNonExistentSpecialId_ShouldReturnNotFound()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var nonExistentId = new Guid("ffffffff-ffff-ffff-ffff-fffffffffff0"); // Non-existent ID
        var updateData = CreateValidUpdateDto(nonExistentId);
        var response = await PutAsJsonAsync($"/api/v1/Customers/{nonExistentId}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithLargeId_ShouldReturnNotFound()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(LargeId);
        var response = await PutAsJsonAsync($"/api/v1/Customers/{LargeId}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithNullJson_ShouldReturnBadRequest()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", (CustomerInputDto)null!);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithMalformedJson_ShouldReturnBadRequest()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var malformedJson = "{id: 1, firstname: 'Updated'}"; // Missing quotes
        var content = new StringContent(malformedJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/Customers/{Customer1Id}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WithUnicodeCharacters_ShouldUpdateSuccessfully()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Clear change tracker to avoid conflicts with entity tracking
        DbContext.ChangeTracker.Clear();

        var updateData = CreateValidUpdateDto(Customer1Id);
        updateData.Firstname = "José";
        updateData.Lastname = "García";
        updateData.CompanyName = "Ñueva Compañía";
        updateData.Note = "Müller & Søn";

        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var updatedCustomer = await DbContext.Customer.FindAsync(Customer1Id);
        TestAssertions.AssertEqual("José", updatedCustomer!.Firstname);
        TestAssertions.AssertEqual("García", updatedCustomer.Lastname);
        TestAssertions.AssertEqual("Ñueva Compañía", updatedCustomer.CompanyName);
    }

    [Fact]
    public async Task UpdateCustomer_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData = CreateValidUpdateDto(NonExistentId); // Non-existent ID
        var response = await PutAsJsonAsync($"/api/v1/Customers/{NonExistentId}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateCustomer_ConcurrentUpdate_ShouldHandleCorrectly()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateData1 = CreateValidUpdateDto(Customer1Id);
        updateData1.Firstname = "First Update";

        var updateData2 = CreateValidUpdateDto(Customer1Id);
        updateData2.Firstname = "Second Update";

        var response1Task = PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData1);
        var response2Task = PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData2);

        await Task.WhenAll(response1Task, response2Task);

        var response1 = await response1Task;
        var response2 = await response2Task;

        // Both should succeed (no concurrency control implemented)
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response1.StatusCode);
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response2.StatusCode);

        // Clear change tracker to ensure fresh database read
        DbContext.ChangeTracker.Clear();

        var finalCustomer = await DbContext.Customer.FindAsync(Customer1Id);
        TestAssertions.AssertNotNull(finalCustomer);

        // The final result should be one of the expected values, but since CreateValidUpdateDto
        // sets other fields to fixed values, we need to check all the fields that were set
        TestAssertions.AssertTrue(finalCustomer!.Firstname == "First Update" ||
                                 finalCustomer.Firstname == "Second Update");
        TestAssertions.AssertEqual("Updated Doe", finalCustomer.Lastname);
        TestAssertions.AssertEqual("Updated Company", finalCustomer.CompanyName);
    }

    [Fact]
    public async Task UpdateCustomer_WithSameData_ShouldReturnNoContent()
    {
        await SeedCustomerUpdateTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var originalCustomer = await DbContext.Customer.AsNoTracking().FirstAsync(c => c.Id == Customer1Id);

        var updateData = new CustomerInputDto
        {
            Id = Customer1Id,
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

        var response = await PutAsJsonAsync($"/api/v1/Customers/{Customer1Id}", updateData);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }
}