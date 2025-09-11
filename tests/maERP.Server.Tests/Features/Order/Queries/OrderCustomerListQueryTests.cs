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

public class OrderCustomerListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;
    private static readonly Guid Customer1Id = Guid.NewGuid();
    private static readonly Guid Customer2Id = Guid.NewGuid();
    private static readonly Guid Customer3Id = Guid.NewGuid();
    private static readonly Guid Customer4Id = Guid.NewGuid();
    private static readonly Guid Customer5Id = Guid.NewGuid();
    private static readonly Guid Order1Id = Guid.NewGuid();
    private static readonly Guid Order2Id = Guid.NewGuid();
    private static readonly Guid Order3Id = Guid.NewGuid();
    private static readonly Guid Order4Id = Guid.NewGuid();
    private static readonly Guid Order5Id = Guid.NewGuid();
    private static readonly Guid Order6Id = Guid.NewGuid();
    private static readonly Guid Order7Id = Guid.NewGuid();
    private static readonly Guid Order8Id = Guid.NewGuid();
    private static readonly Guid Order9Id = Guid.NewGuid();

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

        TenantContext.SetAssignedTenantIds(new[] { TenantConstants.TestTenant1Id, TenantConstants.TestTenant2Id });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(Guid tenantId)
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
                    Id = Customer1Id,
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "john.doe@test.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer2Tenant1 = new Domain.Entities.Customer
                {
                    Id = Customer2Id,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    Email = "jane.smith@test.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer3Tenant1 = new Domain.Entities.Customer
                {
                    Id = Customer3Id,
                    Firstname = "Alice",
                    Lastname = "Johnson",
                    Email = "alice.johnson@test.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer1Tenant2 = new Domain.Entities.Customer
                {
                    Id = Customer4Id,
                    Firstname = "Bob",
                    Lastname = "Wilson",
                    Email = "bob.wilson@test.com",
                    TenantId = TenantConstants.TestTenant2Id
                };

                var customer2Tenant2 = new Domain.Entities.Customer
                {
                    Id = Customer5Id,
                    Firstname = "Carol",
                    Lastname = "Brown",
                    Email = "carol.brown@test.com",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Customer.AddRange(customer1Tenant1, customer2Tenant1, customer3Tenant1, customer1Tenant2, customer2Tenant2);

                // Create multiple orders for customer 1 (tenant 1)
                var order1Customer1Tenant1 = new Domain.Entities.Order
                {
                    Id = Order1Id,
                    CustomerId = Customer1Id,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 199.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-5),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var order2Customer1Tenant1 = new Domain.Entities.Order
                {
                    Id = Order2Id,
                    CustomerId = Customer1Id,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.PartiallyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 299.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-3),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var order3Customer1Tenant1 = new Domain.Entities.Order
                {
                    Id = Order3Id,
                    CustomerId = Customer1Id,
                    Status = OrderStatus.Completed,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 89.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-1),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Create orders for customer 2 (tenant 1)
                var order1Customer2Tenant1 = new Domain.Entities.Order
                {
                    Id = Order4Id,
                    CustomerId = Customer2Id,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.Invoiced,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 149.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-4),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var order2Customer2Tenant1 = new Domain.Entities.Order
                {
                    Id = Order5Id,
                    CustomerId = Customer2Id,
                    Status = OrderStatus.OnHold,
                    PaymentStatus = PaymentStatus.FirstReminder,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 79.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-2),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Create order for customer 3 (tenant 1)
                var order1Customer3Tenant1 = new Domain.Entities.Order
                {
                    Id = Order6Id,
                    CustomerId = Customer3Id,
                    Status = OrderStatus.Pending,
                    PaymentStatus = PaymentStatus.Unknown,
                    InvoiceAddressFirstName = "Alice",
                    InvoiceAddressLastName = "Johnson",
                    Total = 249.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-6),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Create orders for customer 1 (tenant 2)
                var order1Customer1Tenant2 = new Domain.Entities.Order
                {
                    Id = Order7Id,
                    CustomerId = Customer4Id,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Wilson",
                    Total = 349.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-7),
                    TenantId = TenantConstants.TestTenant2Id
                };

                var order2Customer1Tenant2 = new Domain.Entities.Order
                {
                    Id = Order8Id,
                    CustomerId = Customer4Id,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.PartiallyPaid,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Wilson",
                    Total = 449.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-8),
                    TenantId = TenantConstants.TestTenant2Id
                };

                // Create order for customer 2 (tenant 2)
                var order1Customer2Tenant2 = new Domain.Entities.Order
                {
                    Id = Order9Id,
                    CustomerId = Customer5Id,
                    Status = OrderStatus.Completed,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Carol",
                    InvoiceAddressLastName = "Brown",
                    Total = 159.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-9),
                    TenantId = TenantConstants.TestTenant2Id
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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(o => o.CustomerId == Customer1Id) ?? false);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithDifferentCustomer_ShouldReturnOnlyThatCustomerOrders()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer2Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(o => o.CustomerId == Customer2Id) ?? false);
        TestAssertions.AssertTrue(result.Data?.All(o => o.InvoiceAddressFirstName == "Jane") ?? false);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithCrossTenantCustomerId_ShouldReturnEmptyResult()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Try to access customer 4 which belongs to tenant 2
        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer4Id}");

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

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Guid.NewGuid()}");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}?pageNumber=0&pageSize=2");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}?searchString=CompletelyPaid");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}?searchString=NonexistentStatus");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}?orderBy=Total");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}?orderBy=Total desc");

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
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstOrder = result.Data?.First();
        TestAssertions.AssertNotNull(firstOrder);
        TestAssertions.AssertEqual<Guid>(Customer1Id, firstOrder!.CustomerId);
        TestAssertions.AssertTrue(firstOrder.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstOrder.InvoiceAddressFirstName));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstOrder.Status));
        TestAssertions.AssertTrue(firstOrder.Total > 0);
    }

    [Fact]
    public async Task GetOrdersByCustomer_ShouldIncludeVariousOrderStatuses()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var responseTenant1Customer1 = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}");
        TestAssertions.AssertHttpSuccess(responseTenant1Customer1);
        var resultTenant1Customer1 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseTenant1Customer1);

        // Test tenant 2 customer 4 (2 orders)
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var responseTenant2Customer4 = await Client.GetAsync($"/api/v1/Orders/customer/{Customer4Id}");
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
        TestAssertions.AssertEqual<Guid>(Customer1Id, tenant1CustomerIds?[0] ?? Guid.Empty);
        TestAssertions.AssertEqual(1, tenant2CustomerIds?.Count ?? 0);
        TestAssertions.AssertEqual<Guid>(Customer4Id, tenant2CustomerIds?[0] ?? Guid.Empty);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithDifferentCustomersInSameTenant_ShouldReturnCorrectOrders()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get orders for customer 1
        var responseCustomer1 = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}");
        TestAssertions.AssertHttpSuccess(responseCustomer1);
        var resultCustomer1 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseCustomer1);

        // Get orders for customer 2
        var responseCustomer2 = await Client.GetAsync($"/api/v1/Orders/customer/{Customer2Id}");
        TestAssertions.AssertHttpSuccess(responseCustomer2);
        var resultCustomer2 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseCustomer2);

        // Verify correct order counts per customer
        TestAssertions.AssertNotNull(resultCustomer1?.Data);
        TestAssertions.AssertNotNull(resultCustomer2?.Data);
        TestAssertions.AssertEqual(3, resultCustomer1?.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, resultCustomer2?.Data?.Count ?? 0);

        // Verify customer IDs are correct
        TestAssertions.AssertTrue(resultCustomer1?.Data?.All(o => o.CustomerId == Customer1Id) ?? false);
        TestAssertions.AssertTrue(resultCustomer2?.Data?.All(o => o.CustomerId == Customer2Id) ?? false);

        // Verify customer names are correct
        TestAssertions.AssertTrue(resultCustomer1?.Data?.All(o => o.InvoiceAddressFirstName == "John") ?? false);
        TestAssertions.AssertTrue(resultCustomer2?.Data?.All(o => o.InvoiceAddressFirstName == "Jane") ?? false);
    }

    [Fact]
    public async Task GetOrdersByCustomer_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedOrderCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}?pageNumber=10&pageSize=10");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Guid.Empty}");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Orders/customer/{Customer1Id}?orderBy=DateOrdered");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }
}