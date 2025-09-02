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

namespace maERP.Server.Tests.Features.Order.Commands;

public class OrderCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public OrderCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_OrderCreateCommandTests_{uniqueId}";
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

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PostAsync(requestUri, content);
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

    private async Task SeedOrderTestDataAsync()
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
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private OrderInputDto CreateValidOrderDto()
    {
        return new OrderInputDto
        {
            CustomerId = 1,
            Status = OrderStatus.Pending,
            PaymentStatus = PaymentStatus.Unknown,
            PaymentMethod = "Credit Card",
            Subtotal = 100.00m,
            ShippingCost = 10.00m,
            TotalTax = 19.00m,
            Total = 129.00m,
            InvoiceAddressFirstName = "John",
            InvoiceAddressLastName = "Doe",
            InvoiceAddressStreet = "123 Test St",
            InvoiceAddressCity = "Test City",
            InvoiceAddressZip = "12345",
            InvoiceAddressCountry = "Germany",
            DeliveryAddressFirstName = "John",
            DeliveryAddressLastName = "Doe",
            DeliveryAddressStreet = "123 Test St",
            DeliveryAddressCity = "Test City",
            DeliverAddressZip = "12345",
            DeliveryAddressCountry = "Germany",
            DateOrdered = DateTime.UtcNow
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateOrder_WithValidData_ShouldReturnCreated()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = CreateValidOrderDto();

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
    }

    [Fact]
    public async Task CreateOrder_WithValidData_ShouldPersistInDatabase()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = CreateValidOrderDto();

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
        
        // Verify through API that order exists
        var getResponse = await Client.GetAsync($"/api/v1/Orders/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual(orderDto.CustomerId, orderDetail!.Data.CustomerId);
        TestAssertions.AssertEqual(orderDto.Total, orderDetail.Data.Total);
        TestAssertions.AssertEqual(orderDto.InvoiceAddressFirstName, orderDetail.Data.InvoiceAddressFirstName);
    }

    [Fact]
    public async Task CreateOrder_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = new OrderInputDto
        {
            // Missing required fields like CustomerId
            Status = OrderStatus.Pending,
            Total = 100.00m
        };

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateOrder_WithInvalidCustomerId_ShouldReturnBadRequest()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = CreateValidOrderDto();
        orderDto.CustomerId = 999; // Non-existent customer

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateOrder_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedOrderTestDataAsync();
        var orderDto = CreateValidOrderDto();

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_WithNonExistentTenant_ShouldHandleGracefully()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(999);
        var orderDto = CreateValidOrderDto();

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest || 
                                 response.StatusCode == HttpStatusCode.NotFound ||
                                 response.StatusCode == HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CreateOrder_WithNegativeTotal_ShouldReturnBadRequest()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = CreateValidOrderDto();
        orderDto.Total = -10.00m;

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateOrder_WithInvalidOrderStatus_ShouldHandleGracefully()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = CreateValidOrderDto();
        
        // Create JSON with invalid enum value
        var json = JsonSerializer.Serialize(orderDto);
        var jsonObject = JsonSerializer.Deserialize<JsonElement>(json);
        var modifiedJson = json.Replace($"\"status\":{(int)orderDto.Status}", "\"status\":999");
        
        var content = new StringContent(modifiedJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Orders", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_TenantIsolation_ShouldOnlyCreateInCorrectTenant()
    {
        await SeedOrderTestDataAsync();

        var order1Dto = CreateValidOrderDto();
        order1Dto.CustomerId = 1; // Customer from tenant 1
        order1Dto.InvoiceAddressFirstName = "Tenant1Order";
        
        var order2Dto = CreateValidOrderDto();
        order2Dto.CustomerId = 3; // Customer from tenant 2
        order2Dto.InvoiceAddressFirstName = "Tenant2Order";

        // Create order in tenant 1
        SetTenantHeader(1);
        var createResponse1 = await PostAsJsonAsync("/api/v1/Orders", order1Dto);
        TestAssertions.AssertEqual(HttpStatusCode.Created, createResponse1.StatusCode);
        var result1 = await ReadResponseAsync<Result<int>>(createResponse1);
        
        // Create order in tenant 2
        SetTenantHeader(2);
        var createResponse2 = await PostAsJsonAsync("/api/v1/Orders", order2Dto);
        TestAssertions.AssertEqual(HttpStatusCode.Created, createResponse2.StatusCode);
        var result2 = await ReadResponseAsync<Result<int>>(createResponse2);
        
        // Verify orders exist in database with correct tenant IDs
        TenantContext.SetCurrentTenantId(null);
        var order1InDb = await DbContext.Order.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.Id == result1.Data);
        var order2InDb = await DbContext.Order.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.Id == result2.Data);
        
        TestAssertions.AssertNotNull(order1InDb);
        TestAssertions.AssertNotNull(order2InDb);
        TestAssertions.AssertEqual(1, order1InDb!.TenantId);
        TestAssertions.AssertEqual(2, order2InDb!.TenantId);

        // Verify tenant isolation in API
        using var tenant1Client = Factory.CreateClient();
        tenant1Client.DefaultRequestHeaders.Add("X-Tenant-Id", "1");
        
        var listResponse1 = await tenant1Client.GetAsync("/api/v1/Orders");
        TestAssertions.AssertHttpSuccess(listResponse1);
        var list1 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(listResponse1);
        var tenant1HasOrder = list1.Data?.Any(o => o.InvoiceAddressFirstName == "Tenant1Order") ?? false;
        var tenant1SeesOtherOrder = list1.Data?.Any(o => o.InvoiceAddressFirstName == "Tenant2Order") ?? false;
        
        TestAssertions.AssertTrue(tenant1HasOrder, "Tenant 1 should see its own order");
        TestAssertions.AssertFalse(tenant1SeesOtherOrder, "Tenant 1 should not see Tenant 2's orders");
    }

    [Fact]
    public async Task CreateOrder_WithCompleteAddressData_ShouldCreateSuccessfully()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = CreateValidOrderDto();
        orderDto.InvoiceAddressCompanyName = "Test Company";
        orderDto.InvoiceAddressPhone = "+49123456789";
        orderDto.DeliveryAddressCompanyName = "Test Company";
        orderDto.DeliveryAddressPhone = "+49123456789";

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
        
        // Verify address data persisted correctly
        var getResponse = await Client.GetAsync($"/api/v1/Orders/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual("Test Company", orderDetail!.Data.InvoiceAddressCompanyName);
        TestAssertions.AssertEqual("+49123456789", orderDetail.Data.InvoiceAddressPhone);
    }

    [Fact]
    public async Task CreateOrder_WithPaymentInformation_ShouldCreateSuccessfully()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = CreateValidOrderDto();
        orderDto.PaymentProvider = "Stripe";
        orderDto.PaymentTransactionId = "TXN-12345";
        orderDto.PaymentStatus = PaymentStatus.CompletelyPaid;

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        // Verify payment data persisted correctly
        var getResponse = await Client.GetAsync($"/api/v1/Orders/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual("Stripe", orderDetail!.Data.PaymentProvider);
        TestAssertions.AssertEqual("TXN-12345", orderDetail.Data.PaymentTransactionId);
        TestAssertions.AssertEqual(PaymentStatus.CompletelyPaid, orderDetail.Data.PaymentStatus);
    }

    [Fact]
    public async Task CreateOrder_WithNotes_ShouldCreateSuccessfully()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = CreateValidOrderDto();
        orderDto.CustomerNote = "Please deliver after 5 PM";
        orderDto.InternalNote = "VIP customer";

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        // Verify notes persisted correctly
        var getResponse = await Client.GetAsync($"/api/v1/Orders/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual("Please deliver after 5 PM", orderDetail!.Data.Note);
    }

    [Fact]
    public async Task CreateOrder_WithSalesChannelData_ShouldCreateSuccessfully()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = CreateValidOrderDto();
        orderDto.SalesChannelId = 1;
        orderDto.RemoteOrderId = "EXT-ORDER-123";

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        // Verify sales channel data persisted correctly
        var getResponse = await Client.GetAsync($"/api/v1/Orders/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var orderDetail = await ReadResponseAsync<Result<OrderDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(orderDetail?.Data);
        TestAssertions.AssertEqual(1, orderDetail!.Data.SalesChannelId);
        TestAssertions.AssertEqual("EXT-ORDER-123", orderDetail.Data.RemoteOrderId);
    }

    [Fact]
    public async Task CreateOrder_WithMinimalRequiredData_ShouldCreateSuccessfully()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = new OrderInputDto
        {
            CustomerId = 1,
            Status = OrderStatus.Pending,
            PaymentStatus = PaymentStatus.Unknown,
            Total = 50.00m,
            InvoiceAddressFirstName = "John",
            InvoiceAddressLastName = "Doe",
            InvoiceAddressCity = "Test City",
            InvoiceAddressCountry = "Germany",
            DateOrdered = DateTime.UtcNow
        };

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
    }

    [Fact]
    public async Task CreateOrder_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = CreateValidOrderDto();

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data > 0);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task CreateOrder_WithInvalidJson_ShouldReturnBadRequest()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Orders", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_WithEmptyBody_ShouldReturnBadRequest()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Orders", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_WithCrossTenantCustomer_ShouldReturnBadRequest()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);
        var orderDto = CreateValidOrderDto();
        orderDto.CustomerId = 3; // Customer belongs to tenant 2, but we're on tenant 1

        var response = await PostAsJsonAsync("/api/v1/Orders", orderDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }
}