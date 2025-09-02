using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Order.Queries;

public class OrderDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public OrderDetailQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_OrderDetailQueryTests_{uniqueId}";
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
                    Id = 1,
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "john.doe@test.com",
                    TenantId = 1
                };

                var customer1Tenant2 = new Domain.Entities.Customer
                {
                    Id = 2,
                    Firstname = "Bob",
                    Lastname = "Johnson",
                    Email = "bob.johnson@test.com",
                    TenantId = 2
                };

                DbContext.Customer.AddRange(customer1Tenant1, customer1Tenant2);

                // Create detailed order for tenant 1
                var order1Tenant1 = new Domain.Entities.Order
                {
                    Id = 1,
                    CustomerId = 1,
                    SalesChannelId = 1,
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
                    DeliverAddressZip = "12345",
                    DeliveryAddressCountry = "USA",
                    Subtotal = 150.00m,
                    ShippingCost = 10.00m,
                    TotalTax = 15.00m,
                    Total = 175.00m,
                    CustomerNote = "Please deliver to front door",
                    DateOrdered = DateTime.UtcNow.AddDays(-5),
                    TenantId = 1
                };

                // Create detailed order for tenant 2
                var order1Tenant2 = new Domain.Entities.Order
                {
                    Id = 2,
                    CustomerId = 2,
                    SalesChannelId = 2,
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
                    DeliverAddressZip = "67890",
                    DeliveryAddressCountry = "Canada",
                    Subtotal = 200.00m,
                    ShippingCost = 20.00m,
                    TotalTax = 22.00m,
                    Total = 242.00m,
                    CustomerNote = "Call before delivery",
                    DateOrdered = DateTime.UtcNow.AddDays(-3),
                    TenantId = 2
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

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task GetOrderDetail_WithValidIdAndTenant_ShouldReturnOrderDetail()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<OrderDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data!.Id);
        TestAssertions.AssertEqual("John", result.Data.InvoiceAddressFirstName);
    }

    [Fact]
    public async Task GetOrderDetail_WithValidIdButDifferentTenant_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/2");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithInvalidId_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/999");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Orders/1");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Orders/1");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_ResponseStructure_ShouldContainAllRequiredFields()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<OrderDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var order = result.Data!;
        TestAssertions.AssertEqual(1, order.Id);
        TestAssertions.AssertEqual(1, order.CustomerId);
        TestAssertions.AssertEqual(OrderStatus.Processing, order.Status);
        TestAssertions.AssertEqual(PaymentStatus.CompletelyPaid, order.PaymentStatus);
        TestAssertions.AssertEqual("Credit Card", order.PaymentMethod);
        TestAssertions.AssertEqual(175.00m, order.Total);
    }

    [Fact]
    public async Task GetOrderDetail_WithPaymentInformation_ShouldReturnPaymentDetails()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/1");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/1");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/1");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/1");

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
        SetTenantHeader(1);
        var responseTenant1 = await Client.GetAsync("/api/v1/Orders/1");
        TestAssertions.AssertHttpSuccess(responseTenant1);
        
        // Verify tenant 1 cannot access tenant 2's order
        var responseTenant1ToTenant2 = await Client.GetAsync("/api/v1/Orders/2");
        TestAssertions.AssertHttpStatusCode(responseTenant1ToTenant2, HttpStatusCode.NotFound);
        
        // Verify tenant 2 can access its order
        SetTenantHeader(2);
        var responseTenant2 = await Client.GetAsync("/api/v1/Orders/2");
        TestAssertions.AssertHttpSuccess(responseTenant2);
        
        // Verify tenant 2 cannot access tenant 1's order
        var responseTenant2ToTenant1 = await Client.GetAsync("/api/v1/Orders/1");
        TestAssertions.AssertHttpStatusCode(responseTenant2ToTenant1, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithRemoteOrderId_ShouldReturnRemoteId()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<OrderDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var order = result.Data!;
        TestAssertions.AssertEqual("ORDER-001", order.RemoteOrderId);
        TestAssertions.AssertEqual(1, order.SalesChannelId);
    }

    [Fact]
    public async Task GetOrderDetail_WithZeroId_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/0");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedOrderDetailTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/-1");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrderDetail_WithDifferentTenantOrders_ShouldReturnCorrectOrderData()
    {
        await SeedOrderDetailTestDataAsync();
        
        // Get tenant 1 order
        SetTenantHeader(1);
        var responseTenant1 = await Client.GetAsync("/api/v1/Orders/1");
        TestAssertions.AssertHttpSuccess(responseTenant1);
        var resultTenant1 = await ReadResponseAsync<Result<OrderDetailDto>>(responseTenant1);
        
        // Get tenant 2 order
        SetTenantHeader(2);
        var responseTenant2 = await Client.GetAsync("/api/v1/Orders/2");
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