using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Order;
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

namespace maERP.Server.Tests.Features.Order.Queries;

public class OrderDetailQueryTests : TenantIsolatedTestBase
{
    private static readonly Guid Customer1Id = Guid.NewGuid();
    private static readonly Guid Customer2Id = Guid.NewGuid();
    private static readonly Guid Order1Id = Guid.NewGuid();
    private static readonly Guid Order2Id = Guid.NewGuid();
    private static readonly Guid SalesChannel1Id = Guid.NewGuid();
    private static readonly Guid SalesChannel2Id = Guid.NewGuid();

    private async Task SeedOrderDetailTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Order.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                // Create customers for both tenants
                var customer1Tenant1 = new Domain.Entities.Customer
                {
                    Id = Customer1Id,
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "john.doe@test.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer1Tenant2 = new Domain.Entities.Customer
                {
                    Id = Customer2Id,
                    Firstname = "Bob",
                    Lastname = "Johnson",
                    Email = "bob.johnson@test.com",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Customer.AddRange(customer1Tenant1, customer1Tenant2);

                // Create detailed order for tenant 1
                var order1Tenant1 = new Domain.Entities.Order
                {
                    Id = Order1Id,
                    OrderId = 20001,
                    CustomerId = Customer1Id,
                    SalesChannelId = SalesChannel1Id,
                    RemoteOrderId = "ORDER-001",
                    Status = OrderStatus.Processing,
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
                    DateOrdered = DateTime.UtcNow.AddDays(-5),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Create detailed order for tenant 2
                var order1Tenant2 = new Domain.Entities.Order
                {
                    Id = Order2Id,
                    OrderId = 20002,
                    CustomerId = Customer2Id,
                    SalesChannelId = SalesChannel2Id,
                    RemoteOrderId = "ORDER-002",
                    Status = OrderStatus.ReadyForDelivery,
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
                    DateOrdered = DateTime.UtcNow.AddDays(-3),
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Order.AddRange(order1Tenant1, order1Tenant2);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }



    [Fact]
    public async Task GetOrderDetail_WithValidIdAndTenant_ShouldReturnOrderDetail()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<OrderDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual<Guid>(Order1Id, result.Data!.Id);
        TestAssertions.AssertEqual("John", result.Data.InvoiceAddressFirstName);
    }

    [Fact]
    public async Task GetOrderDetail_WithValidIdButDifferentTenant_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/{Order2Id}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithInvalidId_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/{Guid.NewGuid()}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();
        RemoveTenantHeader();

        var response = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();
        SetInvalidTenantHeader();

        var response = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_ResponseStructure_ShouldContainAllRequiredFields()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<OrderDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var order = result.Data!;
        TestAssertions.AssertEqual<Guid>(Order1Id, order.Id);
        TestAssertions.AssertEqual<Guid>(Customer1Id, order.CustomerId);
        TestAssertions.AssertEqual(OrderStatus.Processing, order.Status);
        TestAssertions.AssertEqual(PaymentStatus.CompletelyPaid, order.PaymentStatus);
        TestAssertions.AssertEqual("Credit Card", order.PaymentMethod);
        TestAssertions.AssertEqual(175.00m, order.Total);
    }

    [Fact]
    public async Task GetOrderDetail_WithPaymentInformation_ShouldReturnPaymentDetails()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<OrderDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var order = result.Data!;
        TestAssertions.AssertEqual("Credit Card", order.PaymentMethod);
        TestAssertions.AssertEqual("Stripe", order.PaymentProvider);
        TestAssertions.AssertEqual("TXN-001", order.PaymentTransactionId);
        TestAssertions.AssertEqual(PaymentStatus.CompletelyPaid, order.PaymentStatus);
    }

    [Fact]
    public async Task GetOrderDetail_WithAddressInformation_ShouldReturnAddressDetails()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<OrderDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var order = result.Data!;
        TestAssertions.AssertEqual("John", order.InvoiceAddressFirstName);
        TestAssertions.AssertEqual("Doe", order.InvoiceAddressLastName);
        TestAssertions.AssertEqual("123 Main St", order.InvoiceAddressStreet);
        TestAssertions.AssertEqual("Anytown", order.InvoiceAddressCity);
        TestAssertions.AssertEqual("12345", order.InvoiceAddressZip);
    }

    [Fact]
    public async Task GetOrderDetail_WithPriceBreakdown_ShouldReturnPriceDetails()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<OrderDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var order = result.Data!;
        TestAssertions.AssertEqual(150.00m, order.Subtotal);
        TestAssertions.AssertEqual(10.00m, order.ShippingCost);
        TestAssertions.AssertEqual(15.00m, order.TotalTax);
        TestAssertions.AssertEqual(175.00m, order.Total);
    }

    [Fact]
    public async Task GetOrderDetail_WithCustomerNote_ShouldReturnNote()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<OrderDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var order = result.Data!;
        TestAssertions.AssertEqual("Please deliver to front door", order.Note);
    }

    [Fact]
    public async Task GetOrderDetail_TenantIsolation_ShouldNotAccessCrossTenantData()
    {
        await SeedOrderDetailTestDataAsync();

        // Verify tenant 1 can access its order
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var responseTenant1 = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");
        TestAssertions.AssertHttpSuccess(responseTenant1);

        // Verify tenant 1 cannot access tenant 2's order
        var responseTenant1ToTenant2 = await Client.GetAsync($"/api/v1/Orders/{Order2Id}");
        TestAssertions.AssertHttpStatusCode(responseTenant1ToTenant2, HttpStatusCode.NotFound);

        // Verify tenant 2 can access its order
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var responseTenant2 = await Client.GetAsync($"/api/v1/Orders/{Order2Id}");
        TestAssertions.AssertHttpSuccess(responseTenant2);

        // Verify tenant 2 cannot access tenant 1's order
        var responseTenant2ToTenant1 = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");
        TestAssertions.AssertHttpStatusCode(responseTenant2ToTenant1, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithRemoteOrderId_ShouldReturnRemoteId()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<OrderDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var order = result.Data!;
        TestAssertions.AssertEqual("ORDER-001", order.RemoteOrderId);
        TestAssertions.AssertEqual<Guid?>(SalesChannel1Id, order.SalesChannelId);
    }

    [Fact]
    public async Task GetOrderDetail_WithZeroId_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/{Guid.Empty}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/{Guid.NewGuid()}");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await SeedOrderDetailTestDataAsync();
        SetInvalidTenantHeaderValue("invalid-tenant-id");

        var response = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetOrderDetail_WithDifferentTenantOrders_ShouldReturnCorrectOrderData()
    {
        await SeedOrderDetailTestDataAsync();

        // Get tenant 1 order
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var responseTenant1 = await Client.GetAsync($"/api/v1/Orders/{Order1Id}");
        TestAssertions.AssertHttpSuccess(responseTenant1);
        var resultTenant1 = await ReadResponseAsync<Result<OrderDetailDto>>(responseTenant1);

        // Get tenant 2 order
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var responseTenant2 = await Client.GetAsync($"/api/v1/Orders/{Order2Id}");
        TestAssertions.AssertHttpSuccess(responseTenant2);
        var resultTenant2 = await ReadResponseAsync<Result<OrderDetailDto>>(responseTenant2);

        // Verify data is different and correct for each tenant
        TestAssertions.AssertNotNull(resultTenant1?.Data);
        TestAssertions.AssertNotNull(resultTenant2?.Data);
        TestAssertions.AssertEqual("John", resultTenant1?.Data?.InvoiceAddressFirstName);
        TestAssertions.AssertEqual("Bob", resultTenant2?.Data?.InvoiceAddressFirstName);
        TestAssertions.AssertEqual(175.00m, resultTenant1?.Data?.Total);
        TestAssertions.AssertEqual(242.00m, resultTenant2?.Data?.Total);
    }
}