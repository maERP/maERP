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

namespace maERP.Server.Tests.Features.Order.Commands;

public class OrderUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public OrderUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_OrderUpdateCommandTests_{uniqueId}";
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
    }

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
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

    private async Task<int> SeedOrderTestDataAndCreateOrderAsync()
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
                    Id = 1,
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "john.doe@test.com",
                    TenantId = 1
                };

                var customer2Tenant1 = new Domain.Entities.Customer
                {
                    Id = 2,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    Email = "jane.smith@test.com",
                    TenantId = 1
                };

                var customer1Tenant2 = new Domain.Entities.Customer
                {
                    Id = 3,
                    Firstname = "Bob",
                    Lastname = "Johnson",
                    Email = "bob.johnson@test.com",
                    TenantId = 2
                };

                DbContext.Customer.AddRange(customer1Tenant1, customer2Tenant1, customer1Tenant2);

                // Create test orders
                var order1 = new Domain.Entities.Order
                {
                    Id = 1,
                    CustomerId = 1,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.Invoiced,
                    PaymentMethod = "Credit Card",
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    InvoiceAddressCity = "Test City",
                    InvoiceAddressCountry = "Germany",
                    Total = 100.00m,
                    DateOrdered = DateTime.UtcNow.AddDays(-1),
                    TenantId = 1
                };

                var order2 = new Domain.Entities.Order
                {
                    Id = 2,
                    CustomerId = 2,
                    Status = OrderStatus.Pending,
                    PaymentStatus = PaymentStatus.Unknown,
                    PaymentMethod = "PayPal",
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    InvoiceAddressCity = "Another City",
                    InvoiceAddressCountry = "Germany",
                    Total = 200.00m,
                    DateOrdered = DateTime.UtcNow.AddDays(-2),
                    TenantId = 1
                };

                var order3 = new Domain.Entities.Order
                {
                    Id = 3,
                    CustomerId = 3,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    PaymentMethod = "Bank Transfer",
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    InvoiceAddressCity = "Third City",
                    InvoiceAddressCountry = "Austria",
                    Total = 300.00m,
                    DateOrdered = DateTime.UtcNow.AddDays(-3),
                    TenantId = 2
                };

                DbContext.Order.AddRange(order1, order2, order3);
                await DbContext.SaveChangesAsync();

                return 1; // Return ID of first order for tenant 1
            }

            // If data already exists, return existing order ID
            var existingOrder = await DbContext.Order.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.TenantId == TenantConstants.TestTenant1Id);
            return existingOrder?.Id ?? 1;
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private OrderInputDto CreateOrderUpdateDto(int id)
    {
        return new OrderInputDto
        {
            Id = id,
            CustomerId = 1,
            Status = OrderStatus.ReadyForDelivery,
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
            DeliverAddressZip = "54321",
            DeliveryAddressCountry = "Germany",
            CustomerNote = "Updated customer note",
            InternalNote = "Updated internal note",
            DateOrdered = DateTime.UtcNow.AddDays(-1)
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateOrder_WithValidData_ShouldReturnOk()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(orderId, result.Data);
    }

    [Fact]
    public async Task UpdateOrder_WithValidData_ShouldPersistChangesInDatabase()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        
        // Verify through API that changes were persisted
        var getResponse = await Client.GetAsync($"/api/v1/Orders/{orderId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual(orderDto.InvoiceAddressFirstName, orderDetail!.Data.InvoiceAddressFirstName);
        TestAssertions.AssertEqual(orderDto.Total, orderDetail.Data.Total);
        TestAssertions.AssertEqual(OrderStatus.ReadyForDelivery, orderDetail.Data.Status);
        TestAssertions.AssertEqual(PaymentStatus.CompletelyPaid, orderDetail.Data.PaymentStatus);
    }

    [Fact]
    public async Task UpdateOrder_WithNonExistentOrder_ShouldReturnNotFound()
    {
        await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(999);

        var response = await PutAsJsonAsync("/api/v1/Orders/999", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WithMismatchedIds_ShouldReturnBadRequest()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.Id = orderId + 100; // Mismatched ID

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WithoutTenantHeader_ShouldReturnBadRequestOrNotFound()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        var orderDto = CreateOrderUpdateDto(orderId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest || 
                                 response.StatusCode == HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateOrder_CrossTenantAccess_ShouldReturnNotFound()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(2); // Try to access tenant 1 order from tenant 2
        var orderDto = CreateOrderUpdateDto(orderId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WithInvalidCustomerId_ShouldReturnBadRequest()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.CustomerId = 999; // Non-existent customer

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateOrder_WithNegativeTotal_ShouldReturnBadRequest()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.Total = -50.00m;

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateOrder_StatusChange_ShouldUpdateCorrectly()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.Status = OrderStatus.Completed;

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        
        // Verify status change
        var getResponse = await Client.GetAsync($"/api/v1/Orders/{orderId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual(OrderStatus.Completed, orderDetail!.Data.Status);
    }

    [Fact]
    public async Task UpdateOrder_PaymentStatusChange_ShouldUpdateCorrectly()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.PaymentStatus = PaymentStatus.FirstReminder;

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        
        // Verify payment status change
        var getResponse = await Client.GetAsync($"/api/v1/Orders/{orderId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual(PaymentStatus.FirstReminder, orderDetail!.Data.PaymentStatus);
    }

    [Fact]
    public async Task UpdateOrder_AddressUpdate_ShouldUpdateCorrectly()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.InvoiceAddressStreet = "789 New Address St";
        orderDto.InvoiceAddressCity = "New City";
        orderDto.DeliveryAddressStreet = "789 New Address St";
        orderDto.DeliveryAddressCity = "New City";

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        
        // Verify address updates
        var getResponse = await Client.GetAsync($"/api/v1/Orders/{orderId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual("789 New Address St", orderDetail!.Data.InvoiceAddressStreet);
        TestAssertions.AssertEqual("New City", orderDetail.Data.InvoiceAddressCity);
    }

    [Fact]
    public async Task UpdateOrder_TenantIsolation_ShouldNotUpdateCrossTenantOrders()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        
        // Verify order exists for tenant 1
        SetTenantHeader(1);
        var getResponse1 = await Client.GetAsync($"/api/v1/Orders/{orderId}");
        TestAssertions.AssertHttpSuccess(getResponse1);
        
        // Try to update from tenant 2
        SetTenantHeader(2);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.InvoiceAddressFirstName = "Hacked Name";
        
        var updateResponse = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, updateResponse.StatusCode);
        
        // Verify order was not changed when accessed from original tenant
        SetTenantHeader(1);
        var getResponse2 = await Client.GetAsync($"/api/v1/Orders/{orderId}");
        TestAssertions.AssertHttpSuccess(getResponse2);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse2);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertNotEqual("Hacked Name", orderDetail!.Data.InvoiceAddressFirstName);
    }

    [Fact]
    public async Task UpdateOrder_WithCrossTenantCustomer_ShouldReturnBadRequest()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.CustomerId = 3; // Customer belongs to tenant 2

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateOrder_WithNotesUpdate_ShouldUpdateCorrectly()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.CustomerNote = "Special delivery instructions";
        orderDto.InternalNote = "Customer is VIP";

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        
        // Verify notes update
        var getResponse = await Client.GetAsync($"/api/v1/Orders/{orderId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual("Special delivery instructions", orderDetail!.Data.Note);
    }

    [Fact]
    public async Task UpdateOrder_ResponseStructure_ShouldHaveCorrectFormat()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(orderId, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task UpdateOrder_WithInvalidJson_ShouldReturnBadRequest()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/Orders/{orderId}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WithEmptyBody_ShouldReturnBadRequest()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/Orders/{orderId}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_PaymentInformationUpdate_ShouldUpdateCorrectly()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(1);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.PaymentProvider = "PayPal";
        orderDto.PaymentTransactionId = "PAYPAL-TXN-456";
        orderDto.PaymentMethod = "PayPal Payment";

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        
        // Verify payment information update
        var getResponse = await Client.GetAsync($"/api/v1/Orders/{orderId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual("PayPal", orderDetail!.Data.PaymentProvider);
        TestAssertions.AssertEqual("PAYPAL-TXN-456", orderDetail.Data.PaymentTransactionId);
        TestAssertions.AssertEqual("PayPal Payment", orderDetail.Data.PaymentMethod);
    }
}