using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Sales;
using maERP.Domain.Entities;
using maERP.Domain.Constants;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Sales.Commands;

public class SalesUpdateCommandTests : TenantIsolatedTestBase
{
    private static readonly int Customer1Id = 1;
    private static readonly int Customer2Id = 2;
    private static readonly int Customer3Id = 3;
    private static readonly Guid Sales1Id = Guid.NewGuid();
    private static readonly Guid Sales2Id = Guid.NewGuid();
    private static readonly Guid Sales3Id = Guid.NewGuid();
    private static readonly Guid SalesChannel1Id = Guid.NewGuid();

    private async Task<Guid> SeedSalesTestDataAndCreateSalesAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Customer.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                // Create customers for both tenants
                var customer1Tenant1 = new Domain.Entities.Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Customer1Id,
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "john.doe@test.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer2Tenant1 = new Domain.Entities.Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Customer2Id,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    Email = "jane.smith@test.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer1Tenant2 = new Domain.Entities.Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Customer3Id,
                    Firstname = "Bob",
                    Lastname = "Johnson",
                    Email = "bob.johnson@test.com",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Customer.AddRange(customer1Tenant1, customer2Tenant1, customer1Tenant2);

                // Create test saless
                var sales1 = new Domain.Entities.Sales
                {
                    Id = Sales1Id,
                    SalesId = 10001,
                    SalesChannelId = SalesChannel1Id,
                    CustomerId = Customer1Id,
                    Status = SalesStatus.Processing,
                    PaymentStatus = PaymentStatus.Invoiced,
                    PaymentMethod = "Credit Card",
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    InvoiceAddressCity = "Test City",
                    InvoiceAddressCountry = "Germany",
                    Total = 100.00m,
                    DateSalesed = DateTime.UtcNow.AddDays(-1),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var sales2 = new Domain.Entities.Sales
                {
                    Id = Sales2Id,
                    SalesId = 10002,
                    SalesChannelId = SalesChannel1Id,
                    CustomerId = Customer2Id,
                    Status = SalesStatus.Pending,
                    PaymentStatus = PaymentStatus.Unknown,
                    PaymentMethod = "PayPal",
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    InvoiceAddressCity = "Another City",
                    InvoiceAddressCountry = "Germany",
                    Total = 200.00m,
                    DateSalesed = DateTime.UtcNow.AddDays(-2),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var sales3 = new Domain.Entities.Sales
                {
                    Id = Sales3Id,
                    SalesId = 10003,
                    SalesChannelId = SalesChannel1Id,
                    CustomerId = Customer3Id,
                    Status = SalesStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    PaymentMethod = "Bank Transfer",
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    InvoiceAddressCity = "Third City",
                    InvoiceAddressCountry = "Austria",
                    Total = 300.00m,
                    DateSalesed = DateTime.UtcNow.AddDays(-3),
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Sales.AddRange(sales1, sales2, sales3);
                await DbContext.SaveChangesAsync();

                return Sales1Id; // Return ID of first sales for tenant 1
            }

            // If data already exists, return existing sales ID
            var existingSales = await DbContext.Sales.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.TenantId == TenantConstants.TestTenant1Id);
            return existingSales?.Id ?? Sales1Id;
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private SalesInputDto CreateSalesUpdateDto(Guid id)
    {
        return new SalesInputDto
        {
            Id = id,
            SalesId = 12345,
            SalesChannelId = SalesChannel1Id,
            CustomerId = Customer1Id,
            Status = SalesStatus.ReadyForDelivery,
            PaymentStatus = PaymentStatus.CompletelyPaid,
            PaymentMethod = "Credit Card Updated",
            PaymentProvider = "Stripe",
            PaymentTransactionId = "TXN-UPDATED",
            Subtotal = 150.00m,
            ShippingCost = 15.00m,
            TotalTax = 31.35m,
            Total = 196.35m,
            InvoiceAddressFirstName = "John Updated",
            InvoiceAddressLastName = "Doe Updated",
            InvoiceAddressStreet = "456 Updated St",
            InvoiceAddressCity = "Updated City",
            InvoiceAddressZip = "54321",
            InvoiceAddressCountry = "Germany",
            DeliveryAddressFirstName = "John Updated",
            DeliveryAddressLastName = "Doe Updated",
            DeliveryAddressStreet = "456 Updated St",
            DeliveryAddressCity = "Updated City",
            DeliveryAddressZip = "54321",
            DeliveryAddressCountry = "Germany",
            CustomerNote = "Updated customer note",
            InternalNote = "Updated internal note",
            DateSalesed = DateTime.UtcNow.AddDays(-1)
        };
    }



    [Fact]
    public async Task UpdateSales_WithValidData_ShouldReturnOk()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(salesId, result.Data);
    }

    [Fact]
    public async Task UpdateSales_WithValidData_ShouldPersistChangesInDatabase()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify through API that changes were persisted
        var getResponse = await Client.GetAsync($"/api/v1/Saless/{salesId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual(salesDto.InvoiceAddressFirstName, salesDetail!.Data.InvoiceAddressFirstName);
        TestAssertions.AssertEqual(salesDto.Total, salesDetail.Data.Total);
        TestAssertions.AssertEqual(SalesStatus.ReadyForDelivery, salesDetail.Data.Status);
        TestAssertions.AssertEqual(PaymentStatus.CompletelyPaid, salesDetail.Data.PaymentStatus);
    }

    [Fact]
    public async Task UpdateSales_WithNonExistentSales_ShouldReturnNotFound()
    {
        await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var nonExistentId = Guid.NewGuid();
        var salesDto = CreateSalesUpdateDto(nonExistentId);

        var response = await PutAsJsonAsync($"/api/v1/Saless/{nonExistentId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSales_WithMismatchedIds_ShouldReturnBadRequest()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);
        salesDto.Id = Guid.NewGuid(); // Mismatched ID

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSales_WithoutTenantHeader_ShouldReturnNotFound()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        RemoveTenantHeader();
        var salesDto = CreateSalesUpdateDto(salesId);

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSales_CrossTenantAccess_ShouldReturnNotFound()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id); // Try to access tenant 1 sales from tenant 2
        var salesDto = CreateSalesUpdateDto(salesId);

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSales_WithNonExistentTenant_ShouldReturnNotFound()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetInvalidTenantHeader();
        var salesDto = CreateSalesUpdateDto(salesId);

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSales_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetInvalidTenantHeaderValue("invalid-guid-format");
        var salesDto = CreateSalesUpdateDto(salesId);

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSales_WithInvalidCustomerId_ShouldReturnBadRequest()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);
        salesDto.CustomerId = 99999; // Non-existent customer

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSales_WithNegativeTotal_ShouldReturnBadRequest()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);
        salesDto.Total = -50.00m;

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSales_StatusChange_ShouldUpdateCorrectly()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);
        salesDto.Status = SalesStatus.Completed;

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify status change
        var getResponse = await Client.GetAsync($"/api/v1/Saless/{salesId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual(SalesStatus.Completed, salesDetail!.Data.Status);
    }

    [Fact]
    public async Task UpdateSales_PaymentStatusChange_ShouldUpdateCorrectly()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);
        salesDto.PaymentStatus = PaymentStatus.FirstReminder;

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify payment status change
        var getResponse = await Client.GetAsync($"/api/v1/Saless/{salesId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual(PaymentStatus.FirstReminder, salesDetail!.Data.PaymentStatus);
    }

    [Fact]
    public async Task UpdateSales_AddressUpdate_ShouldUpdateCorrectly()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);
        salesDto.InvoiceAddressStreet = "789 New Address St";
        salesDto.InvoiceAddressCity = "New City";
        salesDto.DeliveryAddressStreet = "789 New Address St";
        salesDto.DeliveryAddressCity = "New City";

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify address updates
        var getResponse = await Client.GetAsync($"/api/v1/Saless/{salesId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual("789 New Address St", salesDetail!.Data.InvoiceAddressStreet);
        TestAssertions.AssertEqual("New City", salesDetail.Data.InvoiceAddressCity);
    }

    [Fact]
    public async Task UpdateSales_TenantIsolation_ShouldNotUpdateCrossTenantSaless()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();

        // Verify sales exists for tenant 1 and get original data
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse1 = await Client.GetAsync($"/api/v1/Saless/{salesId}");
        TestAssertions.AssertHttpSuccess(getResponse1);
        var originalSales = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse1);
        var originalFirstName = originalSales.Data.InvoiceAddressFirstName;

        // Try to update from tenant 2 - should fail because sales doesn't exist in tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var salesDto = CreateSalesUpdateDto(salesId);
        // Use valid customer for tenant 2 to avoid validation issues
        salesDto.CustomerId = Customer3Id;
        salesDto.InvoiceAddressFirstName = "Hacked Name";
        salesDto.Total = 999.99m;

        var updateResponse = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, updateResponse.StatusCode);

        // Verify sales was not changed when accessed from original tenant
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse2 = await Client.GetAsync($"/api/v1/Saless/{salesId}");
        TestAssertions.AssertHttpSuccess(getResponse2);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse2);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual(originalFirstName, salesDetail!.Data.InvoiceAddressFirstName);
        TestAssertions.AssertNotEqual(999.99m, salesDetail.Data.Total);
    }

    [Fact]
    public async Task UpdateSales_WithCrossTenantCustomer_ShouldReturnBadRequest()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);
        salesDto.CustomerId = Customer3Id; // Customer belongs to tenant 2

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSales_WithNotesUpdate_ShouldUpdateCorrectly()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);
        salesDto.CustomerNote = "Special delivery instructions";
        salesDto.InternalNote = "Customer is VIP";

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify notes update
        var getResponse = await Client.GetAsync($"/api/v1/Saless/{salesId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual("Special delivery instructions", salesDetail!.Data.Note);
    }

    [Fact]
    public async Task UpdateSales_ResponseStructure_ShouldHaveCorrectFormat()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(salesId, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task UpdateSales_WithInvalidJson_ShouldReturnBadRequest()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/Saless/{salesId}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSales_WithEmptyBody_ShouldReturnBadRequest()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/Saless/{salesId}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSales_PaymentInformationUpdate_ShouldUpdateCorrectly()
    {
        var salesId = await SeedSalesTestDataAndCreateSalesAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateSalesUpdateDto(salesId);
        salesDto.PaymentProvider = "PayPal";
        salesDto.PaymentTransactionId = "PAYPAL-TXN-456";
        salesDto.PaymentMethod = "PayPal Payment";

        var response = await PutAsJsonAsync($"/api/v1/Saless/{salesId}", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify payment information update
        var getResponse = await Client.GetAsync($"/api/v1/Saless/{salesId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual("PayPal", salesDetail!.Data.PaymentProvider);
        TestAssertions.AssertEqual("PAYPAL-TXN-456", salesDetail.Data.PaymentTransactionId);
        TestAssertions.AssertEqual("PayPal Payment", salesDetail.Data.PaymentMethod);
    }
}