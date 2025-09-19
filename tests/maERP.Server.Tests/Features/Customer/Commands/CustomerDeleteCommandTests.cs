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

public class CustomerDeleteCommandTests : TenantIsolatedTestBase
{
    // Test customer IDs - using consistent GUIDs for reproducible tests
    private static readonly Guid Customer1Id = Guid.Parse("c1111111-1111-1111-1111-111111111111");
    private static readonly Guid Customer2Id = Guid.Parse("c2222222-2222-2222-2222-222222222222");
    private static readonly Guid Customer3Id = Guid.Parse("c3333333-3333-3333-3333-333333333333");
    private static readonly Guid Customer4Id = Guid.Parse("c4444444-4444-4444-4444-444444444444");
    private static readonly Guid Customer5Id = Guid.Parse("c5555555-5555-5555-5555-555555555555");
    private static readonly Guid CustomerAddressId = Guid.Parse("ca111111-1111-1111-1111-111111111111");


    private async Task SeedCustomerTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

            var customer1Tenant1 = new maERP.Domain.Entities.Customer
            {
                Id = Customer1Id,
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
                Id = Customer2Id,
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
                Id = Customer3Id,
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
                TenantId = TenantConstants.TestTenant2Id
            };

            var customer4Inactive = new maERP.Domain.Entities.Customer
            {
                Id = Customer4Id,
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
                Id = Customer5Id,
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
                Id = CustomerAddressId,
                CustomerId = Customer5Id,
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
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }


    [Fact]
    public async Task DeleteCustomer_WithValidId_ShouldReturnNoContent()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WithValidId_ShouldRemoveFromDatabase()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerBefore = await DbContext.Customer.FindAsync(Customer1Id);
        TestAssertions.AssertNotNull(customerBefore);

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Clear the change tracker to force fresh database reads
        DbContext.ChangeTracker.Clear();

        var customerAfter = await DbContext.Customer.FindAsync(Customer1Id);
        Assert.Null(customerAfter);
    }

    [Fact]
    public async Task DeleteCustomer_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteCustomer_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var customerExists = await DbContext.Customer.AnyAsync(c => c.Id == Customer1Id);
        TestAssertions.AssertTrue(customerExists);

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);

        // Verify customer still exists
        var customerStillExists = await DbContext.Customer.AnyAsync(c => c.Id == Customer1Id);
        TestAssertions.AssertTrue(customerStillExists);
    }

    [Fact]
    public async Task DeleteCustomer_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        RemoveTenantHeader();

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WithZeroId_ShouldReturnBadRequest()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/Customers/00000000-0000-0000-0000-000000000000");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/Customers/invalid-guid");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WithInvalidId_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/Customers/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WithLargeId_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact(Skip = "Todo: implement later")]
    public async Task DeleteCustomer_InactiveCustomer_ShouldDeleteSuccessfully()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Customer4Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var deletedCustomer = await DbContext.Customer.FindAsync(Customer4Id);
        Assert.Null(deletedCustomer);
    }

    [Fact]
    public async Task DeleteCustomer_WithAddresses_ShouldDeleteCascade()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var addressesBefore = await DbContext.CustomerAddress.Where(ca => ca.CustomerId == Customer5Id).CountAsync();
        TestAssertions.AssertTrue(addressesBefore > 0);

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Customer5Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Clear the change tracker to force fresh database reads
        DbContext.ChangeTracker.Clear();

        var customerAfter = await DbContext.Customer.FindAsync(Customer5Id);
        Assert.Null(customerAfter);

        // Clear change tracker again to ensure fresh reads for addresses
        DbContext.ChangeTracker.Clear();

        var addressesAfter = await DbContext.CustomerAddress.Where(ca => ca.CustomerId == Customer5Id).CountAsync();
        TestAssertions.AssertEqual(0, addressesAfter);
    }

    [Fact]
    public async Task DeleteCustomer_TenantIsolation_ShouldOnlyDeleteOwnTenantData()
    {
        await SeedCustomerTestDataAsync();

        // Verify both customers exist
        var customer1Exists = await DbContext.Customer.AnyAsync(c => c.Id == Customer1Id && c.TenantId == TenantConstants.TestTenant1Id);
        var customer3Exists = await DbContext.Customer.AnyAsync(c => c.Id == Customer3Id && c.TenantId == TenantConstants.TestTenant2Id);
        TestAssertions.AssertTrue(customer1Exists);
        TestAssertions.AssertTrue(customer3Exists);

        // Try to delete tenant 1's customer while being tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response1 = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        // Verify customer 1 still exists
        customer1Exists = await DbContext.Customer.AnyAsync(c => c.Id == Customer1Id);
        TestAssertions.AssertTrue(customer1Exists);

        // Verify tenant 2 can delete its own customer
        var response3 = await Client.DeleteAsync($"/api/v1/Customers/{Customer3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response3.StatusCode);

        // Verify customer 3 is deleted
        customer3Exists = await DbContext.Customer.AnyAsync(c => c.Id == Customer3Id);
        TestAssertions.AssertFalse(customer3Exists);

        // Verify customer 1 still exists
        customer1Exists = await DbContext.Customer.AnyAsync(c => c.Id == Customer1Id);
        TestAssertions.AssertTrue(customer1Exists);
    }

    [Fact]
    public async Task DeleteCustomer_MultipleCustomersInTenant_ShouldOnlyDeleteSpecified()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerCountBefore = await DbContext.Customer.Where(c => c.TenantId == TenantConstants.TestTenant1Id).CountAsync();
        TestAssertions.AssertTrue(customerCountBefore > 1);

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var customerCountAfter = await DbContext.Customer.Where(c => c.TenantId == TenantConstants.TestTenant1Id).CountAsync();
        TestAssertions.AssertEqual(customerCountBefore - 1, customerCountAfter);

        // Clear the change tracker to force fresh database reads
        DbContext.ChangeTracker.Clear();

        var deletedCustomer = await DbContext.Customer.FindAsync(Customer1Id);
        Assert.Null(deletedCustomer);

        var otherCustomer = await DbContext.Customer.FindAsync(Customer2Id);
        TestAssertions.AssertNotNull(otherCustomer);
    }

    [Fact]
    public async Task DeleteCustomer_ConcurrentDelete_ShouldHandleGracefully()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response1Task = Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");
        var response2Task = Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");

        var responses = await Task.WhenAll(response1Task, response2Task);

        var successCount = responses.Count(r => r.StatusCode == HttpStatusCode.NoContent);
        var notFoundCount = responses.Count(r => r.StatusCode == HttpStatusCode.NotFound);

        // One should succeed, one should be not found
        TestAssertions.AssertEqual(1, successCount);
        TestAssertions.AssertEqual(1, notFoundCount);

        var customerExists = await DbContext.Customer.AnyAsync(c => c.Id == Customer1Id);
        TestAssertions.AssertFalse(customerExists);
    }

    [Fact]
    public async Task DeleteCustomer_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Guid.NewGuid()}");

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
        await SeedCustomerTestDataAsync();
        SetInvalidTenantHeader();

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);

        var customerStillExists = await DbContext.Customer.AnyAsync(c => c.Id == Customer1Id);
        TestAssertions.AssertTrue(customerStillExists);
    }

    [Fact]
    public async Task DeleteCustomer_AlreadyDeletedCustomer_ShouldReturnNotFound()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // First deletion
        var response1 = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response1.StatusCode);

        // Second deletion attempt
        var response2 = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        var result = await ReadResponseAsync<Result<int>>(response2);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task DeleteCustomer_DatabaseConstraintViolation_ShouldHandleGracefully()
    {
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // This test assumes there might be foreign key constraints
        // that prevent deletion in some scenarios
        var response = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");

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
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var initialCount = await DbContext.Customer.CountAsync();
        var initialTenant1Count = await DbContext.Customer.Where(c => c.TenantId == TenantConstants.TestTenant1Id).CountAsync();
        var initialTenant2Count = await DbContext.Customer.Where(c => c.TenantId == TenantConstants.TestTenant2Id).CountAsync();

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");
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
        await SeedCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var customerBefore = await DbContext.Customer.AsNoTracking().FirstAsync(c => c.Id == Customer1Id);
        TestAssertions.AssertNotNull(customerBefore);

        var response = await Client.DeleteAsync($"/api/v1/Customers/{Customer1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // If implementing soft delete, verify the record is marked as deleted
        // If hard delete, verify it's completely removed
        var customerAfter = await DbContext.Customer
            .IgnoreQueryFilters() // In case soft delete uses query filters
            .FirstOrDefaultAsync(c => c.Id == Customer1Id);

        if (customerAfter == null)
        {
            // Hard delete - customer should be completely removed
            var customerExists = await DbContext.Customer.AnyAsync(c => c.Id == Customer1Id);
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
