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

public class OrderNotPaidListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public OrderNotPaidListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_OrderNotPaidListQueryTests_{uniqueId}";
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

    private async Task SeedOrderNotPaidTestDataAsync()
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

                // Create orders for tenant 1 - mix of paid and unpaid, different statuses
                var notPaidOrder1Tenant1 = new Domain.Entities.Order
                {
                    Id = 1,
                    CustomerId = 1,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.Invoiced,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 199.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-5),
                    TenantId = 1
                };

                var notPaidOrder2Tenant1 = new Domain.Entities.Order
                {
                    Id = 2,
                    CustomerId = 2,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.PartiallyPaid,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 299.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-3),
                    TenantId = 1
                };

                var notPaidOrder3Tenant1 = new Domain.Entities.Order
                {
                    Id = 3,
                    CustomerId = 1,
                    Status = OrderStatus.Pending,
                    PaymentStatus = PaymentStatus.FirstReminder,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 89.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-10),
                    TenantId = 1
                };

                // This should NOT appear in not paid list (already paid)
                var paidOrderTenant1 = new Domain.Entities.Order
                {
                    Id = 4,
                    CustomerId = 1,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 149.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-1),
                    TenantId = 1
                };

                // This should NOT appear in not paid list (already shipped/completed)
                var completedOrderTenant1 = new Domain.Entities.Order
                {
                    Id = 5,
                    CustomerId = 2,
                    Status = OrderStatus.Completed,
                    PaymentStatus = PaymentStatus.Invoiced,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 79.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-2),
                    TenantId = 1
                };

                // Create orders for tenant 2
                var notPaidOrder1Tenant2 = new Domain.Entities.Order
                {
                    Id = 6,
                    CustomerId = 3,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.SecondReminder,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    Total = 249.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-7),
                    TenantId = 2
                };

                var notPaidOrder2Tenant2 = new Domain.Entities.Order
                {
                    Id = 7,
                    CustomerId = 3,
                    Status = OrderStatus.OnHold,
                    PaymentStatus = PaymentStatus.ReviewNecessary,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    Total = 349.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-4),
                    TenantId = 2
                };

                DbContext.Order.AddRange(
                    notPaidOrder1Tenant1, notPaidOrder2Tenant1, notPaidOrder3Tenant1, 
                    paidOrderTenant1, completedOrderTenant1,
                    notPaidOrder1Tenant2, notPaidOrder2Tenant2);
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
    public async Task GetOrdersNotPaid_WithValidTenant_ShouldReturnOnlyNotPaidShippableOrders()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetOrdersNotPaid_WithDifferentTenant_ShouldReturnOnlyThatTenantData()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(o => o.InvoiceAddressFirstName == "Bob") ?? false);
    }

    [Fact]
    public async Task GetOrdersNotPaid_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        await SeedOrderNotPaidTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Orders/not-paid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetOrdersNotPaid_ShouldExcludeCompletelyPaidOrders()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Should not include the completely paid order (ID 4)
        var orderIds = result.Data?.Select(o => o.Id).ToList();
        TestAssertions.AssertDoesNotContain(4, orderIds ?? new List<int>());
    }

    [Fact]
    public async Task GetOrdersNotPaid_ShouldExcludeCompletedOrders()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Should not include the completed order (ID 5) even if not paid
        var orderIds = result.Data?.Select(o => o.Id).ToList();
        TestAssertions.AssertDoesNotContain(5, orderIds ?? new List<int>());
    }

    [Fact]
    public async Task GetOrdersNotPaid_WithPagination_ShouldRespectPageSize()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid?pageNumber=0&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetOrdersNotPaid_WithOrderByTotal_ShouldReturnOrderedResults()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid?orderBy=Total");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
        
        var totals = result.Data?.Select(x => x.Total).ToList();
        TestAssertions.AssertEqual(89.99m, totals?[0]);
        TestAssertions.AssertEqual(199.99m, totals?[1]);
        TestAssertions.AssertEqual(299.99m, totals?[2]);
    }

    [Fact]
    public async Task GetOrdersNotPaid_WithOrderByTotalDescending_ShouldReturnDescOrderedResults()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid?orderBy=Total desc");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
        
        var totals = result.Data?.Select(x => x.Total).ToList();
        TestAssertions.AssertEqual(299.99m, totals?[0]);
        TestAssertions.AssertEqual(199.99m, totals?[1]);
        TestAssertions.AssertEqual(89.99m, totals?[2]);
    }

    [Fact]
    public async Task GetOrdersNotPaid_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
    }

    [Fact]
    public async Task GetOrdersNotPaid_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstOrder = result.Data?.First();
        TestAssertions.AssertNotNull(firstOrder);
        TestAssertions.AssertTrue(firstOrder!.Id > 0);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstOrder.InvoiceAddressFirstName));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstOrder.Status));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstOrder.PaymentStatus));
        TestAssertions.AssertTrue(firstOrder.Total > 0);
    }

    [Fact]
    public async Task GetOrdersNotPaid_ShouldIncludeVariousNotPaidStatuses()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var paymentStatuses = result.Data?.Select(o => o.PaymentStatus).ToList();
        TestAssertions.AssertContains("Invoiced", paymentStatuses ?? new List<string>());
        TestAssertions.AssertContains("PartiallyPaid", paymentStatuses ?? new List<string>());
        TestAssertions.AssertContains("FirstReminder", paymentStatuses ?? new List<string>());
    }

    [Fact]
    public async Task GetOrdersNotPaid_ShouldIncludeOnlyShippableOrderStatuses()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var orderStatuses = result.Data?.Select(o => o.Status).ToList();
        TestAssertions.AssertContains("Processing", orderStatuses ?? new List<string>());
        TestAssertions.AssertContains("ReadyForDelivery", orderStatuses ?? new List<string>());
        TestAssertions.AssertContains("Pending", orderStatuses ?? new List<string>());
        
        // Should not contain completed orders
        TestAssertions.AssertDoesNotContain("Completed", orderStatuses ?? new List<string>());
    }

    [Fact]
    public async Task GetOrdersNotPaid_TenantIsolation_ShouldNotLeakDataBetweenTenants()
    {
        await SeedOrderNotPaidTestDataAsync();
        
        // Test tenant 1
        SetTenantHeader(1);
        var responseTenant1 = await Client.GetAsync("/api/v1/Orders/not-paid");
        TestAssertions.AssertHttpSuccess(responseTenant1);
        var resultTenant1 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseTenant1);
        
        // Test tenant 2
        SetTenantHeader(2);
        var responseTenant2 = await Client.GetAsync("/api/v1/Orders/not-paid");
        TestAssertions.AssertHttpSuccess(responseTenant2);
        var resultTenant2 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseTenant2);

        // Verify data isolation
        TestAssertions.AssertNotNull(resultTenant1?.Data);
        TestAssertions.AssertNotNull(resultTenant2?.Data);
        TestAssertions.AssertEqual(3, resultTenant1?.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, resultTenant2?.Data?.Count ?? 0);
        
        // Ensure no data overlap
        var tenant1Names = resultTenant1?.Data?.Select(o => o.InvoiceAddressFirstName).Distinct().ToList();
        var tenant2Names = resultTenant2?.Data?.Select(o => o.InvoiceAddressFirstName).Distinct().ToList();
        TestAssertions.AssertContains("John", tenant1Names ?? new List<string>());
        TestAssertions.AssertContains("Jane", tenant1Names ?? new List<string>());
        TestAssertions.AssertDoesNotContain("Bob", tenant1Names ?? new List<string>());
        TestAssertions.AssertContains("Bob", tenant2Names ?? new List<string>());
    }

    [Fact]
    public async Task GetOrdersNotPaid_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(3, result.TotalCount);
    }

    [Fact]
    public async Task GetOrdersNotPaid_WithOrderByDateOrdered_ShouldReturnDateOrderedResults()
    {
        await SeedOrderNotPaidTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/not-paid?orderBy=DateOrdered");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }
}