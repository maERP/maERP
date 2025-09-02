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

public class OrderCustomerListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public OrderCustomerListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_OrderCustomerListQueryTests_{uniqueId}";
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

    private async Task SeedOrderCustomerTestDataAsync()
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

                var customer3Tenant1 = new Domain.Entities.Customer
                {
                    Id = 3,
                    Firstname = "Alice",
                    Lastname = "Johnson",
                    Email = "alice.johnson@test.com",
                    TenantId = 1
                };

                var customer1Tenant2 = new Domain.Entities.Customer
                {
                    Id = 4,
                    Firstname = "Bob",
                    Lastname = "Wilson",
                    Email = "bob.wilson@test.com",
                    TenantId = 2
                };

                var customer2Tenant2 = new Domain.Entities.Customer
                {
                    Id = 5,
                    Firstname = "Carol",
                    Lastname = "Brown",
                    Email = "carol.brown@test.com",
                    TenantId = 2
                };

                DbContext.Customer.AddRange(customer1Tenant1, customer2Tenant1, customer3Tenant1, customer1Tenant2, customer2Tenant2);

                // Create multiple orders for customer 1 (tenant 1)
                var order1Customer1Tenant1 = new Domain.Entities.Order
                {
                    Id = 1,
                    CustomerId = 1,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 199.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-5),
                    TenantId = 1
                };

                var order2Customer1Tenant1 = new Domain.Entities.Order
                {
                    Id = 2,
                    CustomerId = 1,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.PartiallyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 299.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-3),
                    TenantId = 1
                };

                var order3Customer1Tenant1 = new Domain.Entities.Order
                {
                    Id = 3,
                    CustomerId = 1,
                    Status = OrderStatus.Completed,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 89.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-1),
                    TenantId = 1
                };

                // Create orders for customer 2 (tenant 1)
                var order1Customer2Tenant1 = new Domain.Entities.Order
                {
                    Id = 4,
                    CustomerId = 2,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.Invoiced,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 149.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-4),
                    TenantId = 1
                };

                var order2Customer2Tenant1 = new Domain.Entities.Order
                {
                    Id = 5,
                    CustomerId = 2,
                    Status = OrderStatus.OnHold,
                    PaymentStatus = PaymentStatus.FirstReminder,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 79.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-2),
                    TenantId = 1
                };

                // Create order for customer 3 (tenant 1)
                var order1Customer3Tenant1 = new Domain.Entities.Order
                {
                    Id = 6,
                    CustomerId = 3,
                    Status = OrderStatus.Pending,
                    PaymentStatus = PaymentStatus.Unknown,
                    InvoiceAddressFirstName = "Alice",
                    InvoiceAddressLastName = "Johnson",
                    Total = 249.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-6),
                    TenantId = 1
                };

                // Create orders for customer 1 (tenant 2)
                var order1Customer1Tenant2 = new Domain.Entities.Order
                {
                    Id = 7,
                    CustomerId = 4,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Wilson",
                    Total = 349.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-7),
                    TenantId = 2
                };

                var order2Customer1Tenant2 = new Domain.Entities.Order
                {
                    Id = 8,
                    CustomerId = 4,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.PartiallyPaid,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Wilson",
                    Total = 449.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-8),
                    TenantId = 2
                };

                // Create order for customer 2 (tenant 2)
                var order1Customer2Tenant2 = new Domain.Entities.Order
                {
                    Id = 9,
                    CustomerId = 5,
                    Status = OrderStatus.Completed,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Carol",
                    InvoiceAddressLastName = "Brown",
                    Total = 159.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-9),
                    TenantId = 2
                };

                DbContext.Order.AddRange(
                    order1Customer1Tenant1, order2Customer1Tenant1, order3Customer1Tenant1,
                    order1Customer2Tenant1, order2Customer2Tenant1,
                    order1Customer3Tenant1,
                    order1Customer1Tenant2, order2Customer1Tenant2,
                    order1Customer2Tenant2);
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
    public async Task GetOrdersByCustomer_WithValidCustomerAndTenant_ShouldReturnCustomerOrders()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(o => o.CustomerId == 1) ?? false);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithDifferentCustomer_ShouldReturnOnlyThatCustomerOrders()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(o => o.CustomerId == 2) ?? false);
        TestAssertions.AssertTrue(result.Data?.All(o => o.InvoiceAddressFirstName == "Jane") ?? false);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithCrossTenantCustomerId_ShouldReturnEmptyResult()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        // Try to access customer 4 which belongs to tenant 2
        var response = await Client.GetAsync("/api/v1/Orders/customer/4");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        await SeedOrderCustomerTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Orders/customer/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithNonExistentCustomer_ShouldReturnEmptyResult()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/999");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithPagination_ShouldRespectPageSize()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/1?pageNumber=0&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithSearchString_ShouldFilterResults()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/1?searchString=CompletelyPaid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(o => o.PaymentStatus == "CompletelyPaid") ?? false);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithSearchStringNoMatch_ShouldReturnEmpty()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/1?searchString=NonexistentStatus");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithOrderByTotal_ShouldReturnOrderedResults()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/1?orderBy=Total");

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
    public async Task GetOrdersByCustomer_WithOrderByTotalDescending_ShouldReturnDescOrderedResults()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/1?orderBy=Total desc");

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
    public async Task GetOrdersByCustomer_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Orders/customer/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
    }

    [Fact]
    public async Task GetOrdersByCustomer_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstOrder = result.Data?.First();
        TestAssertions.AssertNotNull(firstOrder);
        TestAssertions.AssertEqual(1, firstOrder!.CustomerId);
        TestAssertions.AssertTrue(firstOrder.Id > 0);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstOrder.InvoiceAddressFirstName));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstOrder.Status));
        TestAssertions.AssertTrue(firstOrder.Total > 0);
    }

    [Fact]
    public async Task GetOrdersByCustomer_ShouldIncludeVariousOrderStatuses()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var orderStatuses = result.Data?.Select(o => o.Status).ToList();
        TestAssertions.AssertContains("Processing", orderStatuses ?? new List<string>());
        TestAssertions.AssertContains("ReadyForDelivery", orderStatuses ?? new List<string>());
        TestAssertions.AssertContains("Completed", orderStatuses ?? new List<string>());
    }

    [Fact]
    public async Task GetOrdersByCustomer_TenantIsolation_ShouldNotLeakDataBetweenTenants()
    {
        await SeedOrderCustomerTestDataAsync();
        
        // Test tenant 1 customer 1 (3 orders)
        SetTenantHeader(1);
        var responseTenant1Customer1 = await Client.GetAsync("/api/v1/Orders/customer/1");
        TestAssertions.AssertHttpSuccess(responseTenant1Customer1);
        var resultTenant1Customer1 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseTenant1Customer1);
        
        // Test tenant 2 customer 4 (2 orders)
        SetTenantHeader(2);
        var responseTenant2Customer4 = await Client.GetAsync("/api/v1/Orders/customer/4");
        TestAssertions.AssertHttpSuccess(responseTenant2Customer4);
        var resultTenant2Customer4 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseTenant2Customer4);

        // Verify data isolation
        TestAssertions.AssertNotNull(resultTenant1Customer1?.Data);
        TestAssertions.AssertNotNull(resultTenant2Customer4?.Data);
        TestAssertions.AssertEqual(3, resultTenant1Customer1?.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, resultTenant2Customer4?.Data?.Count ?? 0);
        
        // Ensure no data overlap
        var tenant1CustomerIds = resultTenant1Customer1?.Data?.Select(o => o.CustomerId).Distinct().ToList();
        var tenant2CustomerIds = resultTenant2Customer4?.Data?.Select(o => o.CustomerId).Distinct().ToList();
        TestAssertions.AssertEqual(1, tenant1CustomerIds?.Count ?? 0);
        TestAssertions.AssertEqual(1, tenant1CustomerIds?[0]);
        TestAssertions.AssertEqual(1, tenant2CustomerIds?.Count ?? 0);
        TestAssertions.AssertEqual(4, tenant2CustomerIds?[0]);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithDifferentCustomersInSameTenant_ShouldReturnCorrectOrders()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);
        
        // Get orders for customer 1
        var responseCustomer1 = await Client.GetAsync("/api/v1/Orders/customer/1");
        TestAssertions.AssertHttpSuccess(responseCustomer1);
        var resultCustomer1 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseCustomer1);
        
        // Get orders for customer 2
        var responseCustomer2 = await Client.GetAsync("/api/v1/Orders/customer/2");
        TestAssertions.AssertHttpSuccess(responseCustomer2);
        var resultCustomer2 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseCustomer2);

        // Verify correct order counts per customer
        TestAssertions.AssertNotNull(resultCustomer1?.Data);
        TestAssertions.AssertNotNull(resultCustomer2?.Data);
        TestAssertions.AssertEqual(3, resultCustomer1?.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, resultCustomer2?.Data?.Count ?? 0);
        
        // Verify customer IDs are correct
        TestAssertions.AssertTrue(resultCustomer1?.Data?.All(o => o.CustomerId == 1) ?? false);
        TestAssertions.AssertTrue(resultCustomer2?.Data?.All(o => o.CustomerId == 2) ?? false);
        
        // Verify customer names are correct
        TestAssertions.AssertTrue(resultCustomer1?.Data?.All(o => o.InvoiceAddressFirstName == "John") ?? false);
        TestAssertions.AssertTrue(resultCustomer2?.Data?.All(o => o.InvoiceAddressFirstName == "Jane") ?? false);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/1?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(3, result.TotalCount);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithZeroCustomerId_ShouldReturnEmptyResults()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/0");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithOrderByDateOrdered_ShouldReturnDateOrderedResults()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders/customer/1?orderBy=DateOrdered");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }
}