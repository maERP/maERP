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

public class OrderListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public OrderListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_OrderListQueryTests_{uniqueId}";
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

    private async Task SeedOrderTestDataAsync()
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

                // Create orders for tenant 1
                var order1Tenant1 = new Domain.Entities.Order
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

                var order2Tenant1 = new Domain.Entities.Order
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

                var order3Tenant1 = new Domain.Entities.Order
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

                // Create order for tenant 2
                var order1Tenant2 = new Domain.Entities.Order
                {
                    Id = 4,
                    CustomerId = 3,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.Invoiced,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    Total = 149.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-2),
                    TenantId = 2
                };

                DbContext.Order.AddRange(order1Tenant1, order2Tenant1, order3Tenant1, order1Tenant2);
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
    public async Task GetOrders_WithValidTenant_ShouldReturnTenantData()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetOrders_WithDifferentTenant_ShouldReturnOnlyThatTenantData()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Orders");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("Bob", result.Data?.First().InvoiceAddressFirstName);
    }

    [Fact]
    public async Task GetOrders_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        await SeedOrderTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Orders");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetOrders_WithPagination_ShouldRespectPageSize()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders?pageNumber=0&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetOrders_WithPaginationSecondPage_ShouldReturnSecondPageData()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders?pageNumber=1&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetOrders_WithSearchString_ShouldFilterResults()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders?searchString=Jane");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.First().InvoiceAddressFirstName.Contains("Jane") ?? false);
    }

    [Fact]
    public async Task GetOrders_WithSearchStringNoMatch_ShouldReturnEmpty()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders?searchString=NonexistentName");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetOrders_WithOrderByTotal_ShouldReturnOrderedResults()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders?orderBy=Total");

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
    public async Task GetOrders_WithOrderByTotalDescending_ShouldReturnDescOrderedResults()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders?orderBy=Total desc");

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
    public async Task GetOrders_WithOrderByDateOrdered_ShouldReturnDateOrderedResults()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders?orderBy=DateOrdered");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetOrders_WithMultipleOrderBy_ShouldRespectMultipleSorting()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders?orderBy=Status,Total");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetOrders_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(3, result.TotalCount);
    }

    [Fact]
    public async Task GetOrders_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders?pageSize=0");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetOrders_WithNegativePageNumber_ShouldHandleGracefully()
    {
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Orders?pageNumber=-1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetOrders_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Orders");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
        TestAssertions.AssertEqual(0, result.TotalPages);
    }

    [Fact]
    public async Task GetOrders_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders");

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
        TestAssertions.AssertTrue(firstOrder.Total > 0);
    }

    [Fact]
    public async Task GetOrders_WithPaymentStatusFilter_ShouldFilterByPaymentStatus()
    {
        await SeedOrderTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Orders?searchString=CompletelyPaid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetOrders_TenantIsolation_ShouldNotLeakDataBetweenTenants()
    {
        await SeedOrderTestDataAsync();
        
        // Test tenant 1
        SetTenantHeader(1);
        var responseTenant1 = await Client.GetAsync("/api/v1/Orders");
        TestAssertions.AssertHttpSuccess(responseTenant1);
        var resultTenant1 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseTenant1);
        
        // Test tenant 2
        SetTenantHeader(2);
        var responseTenant2 = await Client.GetAsync("/api/v1/Orders");
        TestAssertions.AssertHttpSuccess(responseTenant2);
        var resultTenant2 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseTenant2);

        // Verify data isolation
        TestAssertions.AssertNotNull(resultTenant1?.Data);
        TestAssertions.AssertNotNull(resultTenant2?.Data);
        TestAssertions.AssertEqual(3, resultTenant1?.Data?.Count ?? 0);
        TestAssertions.AssertEqual(1, resultTenant2?.Data?.Count ?? 0);
        
        // Ensure no data overlap
        var tenant1Names = resultTenant1?.Data?.Select(o => o.InvoiceAddressFirstName).ToList();
        var tenant2Names = resultTenant2?.Data?.Select(o => o.InvoiceAddressFirstName).ToList();
        TestAssertions.AssertDoesNotContain("Bob", tenant1Names ?? new List<string>());
        TestAssertions.AssertContains("Bob", tenant2Names ?? new List<string>());
    }
}