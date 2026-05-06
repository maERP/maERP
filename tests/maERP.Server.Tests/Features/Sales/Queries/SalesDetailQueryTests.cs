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

namespace maERP.Server.Tests.Features.Sales.Queries;

public class SalesDetailQueryTests : TenantIsolatedTestBase
{
    private static readonly int Customer1Id = 1;
    private static readonly int Customer2Id = 2;
    private static readonly Guid Sales1Id = Guid.NewGuid();
    private static readonly Guid Sales2Id = Guid.NewGuid();
    private static readonly Guid SalesChannel1Id = Guid.NewGuid();
    private static readonly Guid SalesChannel2Id = Guid.NewGuid();

    private async Task SeedSalesDetailTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Sales.AnyAsync();
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

                var customer1Tenant2 = new Domain.Entities.Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Customer2Id,
                    Firstname = "Bob",
                    Lastname = "Johnson",
                    Email = "bob.johnson@test.com",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Customer.AddRange(customer1Tenant1, customer1Tenant2);

                // Create detailed sales for tenant 1
                var sales1Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales1Id,
                    SalesId = 20001,
                    CustomerId = Customer1Id,
                    SalesChannelId = SalesChannel1Id,
                    RemoteSalesId = "SALES-001",
                    Status = SalesStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    PaymentMethod = "Credit Card",
                    PaymentProvider = "Stripe",
                    PaymentTransactionId = "TXN-001",
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    InvoiceAddressStreet = "123 Main St",
                    InvoiceAddressCity = "Anytown",
                    InvoiceAddressZip = "12345",
                    InvoiceAddressCountry = "USA",
                    DeliveryAddressFirstName = "John",
                    DeliveryAddressLastName = "Doe",
                    DeliveryAddressStreet = "123 Main St",
                    DeliveryAddressCity = "Anytown",
                    DeliveryAddressZip = "12345",
                    DeliveryAddressCountry = "USA",
                    Subtotal = 150.00m,
                    ShippingCost = 10.00m,
                    TotalTax = 15.00m,
                    Total = 175.00m,
                    CustomerNote = "Please deliver to front door",
                    DateSalesed = DateTime.UtcNow.AddDays(-5),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Create detailed sales for tenant 2
                var sales1Tenant2 = new Domain.Entities.Sales
                {
                    Id = Sales2Id,
                    SalesId = 20002,
                    CustomerId = Customer2Id,
                    SalesChannelId = SalesChannel2Id,
                    RemoteSalesId = "SALES-002",
                    Status = SalesStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.Invoiced,
                    PaymentMethod = "Bank Transfer",
                    PaymentProvider = "Bank",
                    PaymentTransactionId = "TXN-002",
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    InvoiceAddressStreet = "456 Oak St",
                    InvoiceAddressCity = "Another Town",
                    InvoiceAddressZip = "67890",
                    InvoiceAddressCountry = "Canada",
                    DeliveryAddressFirstName = "Bob",
                    DeliveryAddressLastName = "Johnson",
                    DeliveryAddressStreet = "456 Oak St",
                    DeliveryAddressCity = "Another Town",
                    DeliveryAddressZip = "67890",
                    DeliveryAddressCountry = "Canada",
                    Subtotal = 200.00m,
                    ShippingCost = 20.00m,
                    TotalTax = 22.00m,
                    Total = 242.00m,
                    CustomerNote = "Call before delivery",
                    DateSalesed = DateTime.UtcNow.AddDays(-3),
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Sales.AddRange(sales1Tenant1, sales1Tenant2);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }



    [Fact]
    public async Task GetSalesDetail_WithValidIdAndTenant_ShouldReturnSalesDetail()
    {
        await SeedSalesDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SalesDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual<Guid>(Sales1Id, result.Data!.Id);
        TestAssertions.AssertEqual("John", result.Data.InvoiceAddressFirstName);
    }

    [Fact]
    public async Task GetSalesDetail_WithValidIdButDifferentTenant_ShouldReturnNotFound()
    {
        await SeedSalesDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/{Sales2Id}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSalesDetail_WithInvalidId_ShouldReturnNotFound()
    {
        await SeedSalesDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/{Guid.NewGuid()}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSalesDetail_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedSalesDetailTestDataAsync();
        RemoveTenantHeader();

        var response = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSalesDetail_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedSalesDetailTestDataAsync();
        SetInvalidTenantHeader();

        var response = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSalesDetail_ResponseStructure_ShouldContainAllRequiredFields()
    {
        await SeedSalesDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SalesDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var sales = result.Data!;
        TestAssertions.AssertEqual<Guid>(Sales1Id, sales.Id);
        TestAssertions.AssertEqual(Customer1Id, sales.CustomerId);
        TestAssertions.AssertEqual(SalesStatus.Processing, sales.Status);
        TestAssertions.AssertEqual(PaymentStatus.CompletelyPaid, sales.PaymentStatus);
        TestAssertions.AssertEqual("Credit Card", sales.PaymentMethod);
        TestAssertions.AssertEqual(175.00m, sales.Total);
    }

    [Fact]
    public async Task GetSalesDetail_WithPaymentInformation_ShouldReturnPaymentDetails()
    {
        await SeedSalesDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SalesDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var sales = result.Data!;
        TestAssertions.AssertEqual("Credit Card", sales.PaymentMethod);
        TestAssertions.AssertEqual("Stripe", sales.PaymentProvider);
        TestAssertions.AssertEqual("TXN-001", sales.PaymentTransactionId);
        TestAssertions.AssertEqual(PaymentStatus.CompletelyPaid, sales.PaymentStatus);
    }

    [Fact]
    public async Task GetSalesDetail_WithAddressInformation_ShouldReturnAddressDetails()
    {
        await SeedSalesDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SalesDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var sales = result.Data!;
        TestAssertions.AssertEqual("John", sales.InvoiceAddressFirstName);
        TestAssertions.AssertEqual("Doe", sales.InvoiceAddressLastName);
        TestAssertions.AssertEqual("123 Main St", sales.InvoiceAddressStreet);
        TestAssertions.AssertEqual("Anytown", sales.InvoiceAddressCity);
        TestAssertions.AssertEqual("12345", sales.InvoiceAddressZip);
    }

    [Fact]
    public async Task GetSalesDetail_WithPriceBreakdown_ShouldReturnPriceDetails()
    {
        await SeedSalesDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SalesDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var sales = result.Data!;
        TestAssertions.AssertEqual(150.00m, sales.Subtotal);
        TestAssertions.AssertEqual(10.00m, sales.ShippingCost);
        TestAssertions.AssertEqual(15.00m, sales.TotalTax);
        TestAssertions.AssertEqual(175.00m, sales.Total);
    }

    [Fact]
    public async Task GetSalesDetail_WithCustomerNote_ShouldReturnNote()
    {
        await SeedSalesDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SalesDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var sales = result.Data!;
        TestAssertions.AssertEqual("Please deliver to front door", sales.Note);
    }

    [Fact]
    public async Task GetSalesDetail_TenantIsolation_ShouldNotAccessCrossTenantData()
    {
        await SeedSalesDetailTestDataAsync();

        // Verify tenant 1 can access its sales
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var responseTenant1 = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");
        TestAssertions.AssertHttpSuccess(responseTenant1);

        // Verify tenant 1 cannot access tenant 2's sales
        var responseTenant1ToTenant2 = await Client.GetAsync($"/api/v1/Saless/{Sales2Id}");
        TestAssertions.AssertHttpStatusCode(responseTenant1ToTenant2, HttpStatusCode.NotFound);

        // Verify tenant 2 can access its sales
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var responseTenant2 = await Client.GetAsync($"/api/v1/Saless/{Sales2Id}");
        TestAssertions.AssertHttpSuccess(responseTenant2);

        // Verify tenant 2 cannot access tenant 1's sales
        var responseTenant2ToTenant1 = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");
        TestAssertions.AssertHttpStatusCode(responseTenant2ToTenant1, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSalesDetail_WithRemoteSalesId_ShouldReturnRemoteId()
    {
        await SeedSalesDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SalesDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var sales = result.Data!;
        TestAssertions.AssertEqual("SALES-001", sales.RemoteSalesId);
        TestAssertions.AssertEqual<Guid?>(SalesChannel1Id, sales.SalesChannelId);
    }

    [Fact]
    public async Task GetSalesDetail_WithZeroId_ShouldReturnNotFound()
    {
        await SeedSalesDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/{Guid.Empty}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSalesDetail_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedSalesDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/{Guid.NewGuid()}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSalesDetail_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await SeedSalesDetailTestDataAsync();
        SetInvalidTenantHeaderValue("invalid-tenant-id");

        var response = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesDetail_WithDifferentTenantSaless_ShouldReturnCorrectSalesData()
    {
        await SeedSalesDetailTestDataAsync();

        // Get tenant 1 sales
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var responseTenant1 = await Client.GetAsync($"/api/v1/Saless/{Sales1Id}");
        TestAssertions.AssertHttpSuccess(responseTenant1);
        var resultTenant1 = await ReadResponseAsync<Result<SalesDetailDto>>(responseTenant1);

        // Get tenant 2 sales
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var responseTenant2 = await Client.GetAsync($"/api/v1/Saless/{Sales2Id}");
        TestAssertions.AssertHttpSuccess(responseTenant2);
        var resultTenant2 = await ReadResponseAsync<Result<SalesDetailDto>>(responseTenant2);

        // Verify data is different and correct for each tenant
        TestAssertions.AssertNotNull(resultTenant1?.Data);
        TestAssertions.AssertNotNull(resultTenant2?.Data);
        TestAssertions.AssertEqual("John", resultTenant1?.Data?.InvoiceAddressFirstName);
        TestAssertions.AssertEqual("Bob", resultTenant2?.Data?.InvoiceAddressFirstName);
        TestAssertions.AssertEqual(175.00m, resultTenant1?.Data?.Total);
        TestAssertions.AssertEqual(242.00m, resultTenant2?.Data?.Total);
    }
}