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

public class OrderUpdateCommandTests : TenantIsolatedTestBase
{
    private static readonly int Customer1Id = 1;
    private static readonly int Customer2Id = 2;
    private static readonly int Customer3Id = 3;
    private static readonly Guid Order1Id = Guid.NewGuid();
    private static readonly Guid Order2Id = Guid.NewGuid();
    private static readonly Guid Order3Id = Guid.NewGuid();
    private static readonly Guid SalesChannel1Id = Guid.NewGuid();

    private async Task<Guid> SeedOrderTestDataAndCreateOrderAsync()
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

                // Create test orders
                var order1 = new Domain.Entities.Order
                {
                    Id = Order1Id,
                    OrderId = 10001,
                    SalesChannelId = SalesChannel1Id,
                    CustomerId = Customer1Id,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.Invoiced,
                    PaymentMethod = "Credit Card",
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    InvoiceAddressCity = "Test City",
                    InvoiceAddressCountry = "Germany",
                    Total = 100.00m,
                    DateOrdered = DateTime.UtcNow.AddDays(-1),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var order2 = new Domain.Entities.Order
                {
                    Id = Order2Id,
                    OrderId = 10002,
                    SalesChannelId = SalesChannel1Id,
                    CustomerId = Customer2Id,
                    Status = OrderStatus.Pending,
                    PaymentStatus = PaymentStatus.Unknown,
                    PaymentMethod = "PayPal",
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    InvoiceAddressCity = "Another City",
                    InvoiceAddressCountry = "Germany",
                    Total = 200.00m,
                    DateOrdered = DateTime.UtcNow.AddDays(-2),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var order3 = new Domain.Entities.Order
                {
                    Id = Order3Id,
                    OrderId = 10003,
                    SalesChannelId = SalesChannel1Id,
                    CustomerId = Customer3Id,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    PaymentMethod = "Bank Transfer",
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    InvoiceAddressCity = "Third City",
                    InvoiceAddressCountry = "Austria",
                    Total = 300.00m,
                    DateOrdered = DateTime.UtcNow.AddDays(-3),
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Order.AddRange(order1, order2, order3);
                await DbContext.SaveChangesAsync();

                return Order1Id; // Return ID of first order for tenant 1
            }

            // If data already exists, return existing order ID
            var existingOrder = await DbContext.Order.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.TenantId == TenantConstants.TestTenant1Id);
            return existingOrder?.Id ?? Order1Id;
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private OrderInputDto CreateOrderUpdateDto(Guid id)
    {
        return new OrderInputDto
        {
            Id = id,
            OrderId = 12345,
            SalesChannelId = SalesChannel1Id,
            CustomerId = Customer1Id,
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
            DeliveryAddressZip = "54321",
            DeliveryAddressCountry = "Germany",
            CustomerNote = "Updated customer note",
            InternalNote = "Updated internal note",
            DateOrdered = DateTime.UtcNow.AddDays(-1)
        };
    }



    [Fact]
    public async Task UpdateOrder_WithValidData_ShouldReturnOk()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var orderDto = CreateOrderUpdateDto(orderId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(orderId, result.Data);
    }

    [Fact]
    public async Task UpdateOrder_WithValidData_ShouldPersistChangesInDatabase()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var nonExistentId = Guid.NewGuid();
        var orderDto = CreateOrderUpdateDto(nonExistentId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{nonExistentId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WithMismatchedIds_ShouldReturnBadRequest()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.Id = Guid.NewGuid(); // Mismatched ID

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WithoutTenantHeader_ShouldReturnNotFound()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        RemoveTenantHeader();
        var orderDto = CreateOrderUpdateDto(orderId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_CrossTenantAccess_ShouldReturnNotFound()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id); // Try to access tenant 1 order from tenant 2
        var orderDto = CreateOrderUpdateDto(orderId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WithNonExistentTenant_ShouldReturnNotFound()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetInvalidTenantHeader();
        var orderDto = CreateOrderUpdateDto(orderId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetInvalidTenantHeaderValue("invalid-guid-format");
        var orderDto = CreateOrderUpdateDto(orderId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WithInvalidCustomerId_ShouldReturnBadRequest()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.CustomerId = 99999; // Non-existent customer

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateOrder_WithNegativeTotal_ShouldReturnBadRequest()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.Total = -50.00m;

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateOrder_StatusChange_ShouldUpdateCorrectly()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        SetTenantHeader(TenantConstants.TestTenant1Id);
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

        // Verify order exists for tenant 1 and get original data
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse1 = await Client.GetAsync($"/api/v1/Orders/{orderId}");
        TestAssertions.AssertHttpSuccess(getResponse1);
        var originalOrder = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse1);
        var originalFirstName = originalOrder.Data.InvoiceAddressFirstName;

        // Try to update from tenant 2 - should fail because order doesn't exist in tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var orderDto = CreateOrderUpdateDto(orderId);
        // Use valid customer for tenant 2 to avoid validation issues
        orderDto.CustomerId = Customer3Id;
        orderDto.InvoiceAddressFirstName = "Hacked Name";
        orderDto.Total = 999.99m;

        var updateResponse = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, updateResponse.StatusCode);

        // Verify order was not changed when accessed from original tenant
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse2 = await Client.GetAsync($"/api/v1/Orders/{orderId}");
        TestAssertions.AssertHttpSuccess(getResponse2);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse2);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual(originalFirstName, orderDetail!.Data.InvoiceAddressFirstName);
        TestAssertions.AssertNotEqual(999.99m, orderDetail.Data.Total);
    }

    [Fact]
    public async Task UpdateOrder_WithCrossTenantCustomer_ShouldReturnBadRequest()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var orderDto = CreateOrderUpdateDto(orderId);
        orderDto.CustomerId = Customer3Id; // Customer belongs to tenant 2

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateOrder_WithNotesUpdate_ShouldUpdateCorrectly()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var orderDto = CreateOrderUpdateDto(orderId);

        var response = await PutAsJsonAsync($"/api/v1/Orders/{orderId}", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/Orders/{orderId}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WithEmptyBody_ShouldReturnBadRequest()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/Orders/{orderId}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_PaymentInformationUpdate_ShouldUpdateCorrectly()
    {
        var orderId = await SeedOrderTestDataAndCreateOrderAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
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