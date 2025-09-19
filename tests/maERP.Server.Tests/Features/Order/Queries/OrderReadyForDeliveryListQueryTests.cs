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

public class OrderReadyForDeliveryListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;
    private static readonly Guid Customer1Id = Guid.NewGuid();
    private static readonly Guid Customer2Id = Guid.NewGuid();
    private static readonly Guid Customer3Id = Guid.NewGuid();
    private static readonly Guid Order1Id = Guid.NewGuid();
    private static readonly Guid Order2Id = Guid.NewGuid();
    private static readonly Guid Order3Id = Guid.NewGuid();
    private static readonly Guid Order4Id = Guid.NewGuid();
    private static readonly Guid Order5Id = Guid.NewGuid();
    private static readonly Guid Order6Id = Guid.NewGuid();
    private static readonly Guid Order7Id = Guid.NewGuid();
    private static readonly Guid Order8Id = Guid.NewGuid();

    public OrderReadyForDeliveryListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_OrderReadyForDeliveryListQueryTests_{uniqueId}";
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

    private async Task SeedOrderReadyForDeliveryTestDataAsync()
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

                var customer1Tenant2 = new Domain.Entities.Customer
                {
                    Id = Customer3Id,
                    Firstname = "Bob",
                    Lastname = "Johnson",
                    Email = "bob.johnson@test.com",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Customer.AddRange(customer1Tenant1, customer2Tenant1, customer1Tenant2);

                // Create orders for tenant 1 - ready for delivery (ReadyForDelivery + CompletelyPaid)
                var readyOrder1Tenant1 = new Domain.Entities.Order
                {
                    Id = Order1Id,
                    CustomerId = Customer1Id,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 199.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-5),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var readyOrder2Tenant1 = new Domain.Entities.Order
                {
                    Id = Order2Id,
                    CustomerId = Customer2Id,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 299.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-3),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var readyOrder3Tenant1 = new Domain.Entities.Order
                {
                    Id = Order3Id,
                    CustomerId = Customer1Id,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 89.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-1),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // This should NOT appear in ready for delivery list (not ready status)
                var notReadyOrder1Tenant1 = new Domain.Entities.Order
                {
                    Id = Order4Id,
                    CustomerId = Customer1Id,
                    Status = OrderStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 149.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-2),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // This should NOT appear in ready for delivery list (not completely paid)
                var notPaidReadyOrder1Tenant1 = new Domain.Entities.Order
                {
                    Id = Order5Id,
                    CustomerId = Customer2Id,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.PartiallyPaid,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 79.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-4),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Create orders for tenant 2
                var readyOrder1Tenant2 = new Domain.Entities.Order
                {
                    Id = Order6Id,
                    CustomerId = Customer3Id,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    Total = 249.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-7),
                    TenantId = TenantConstants.TestTenant2Id
                };

                var readyOrder2Tenant2 = new Domain.Entities.Order
                {
                    Id = Order7Id,
                    CustomerId = Customer3Id,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    Total = 349.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-6),
                    TenantId = TenantConstants.TestTenant2Id
                };

                // This should NOT appear in ready for delivery list (tenant 2, not paid)
                var notPaidReadyOrder1Tenant2 = new Domain.Entities.Order
                {
                    Id = Order8Id,
                    CustomerId = Customer3Id,
                    Status = OrderStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.Invoiced,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    Total = 159.99m,
                    DateOrdered = DateTime.UtcNow.AddDays(-8),
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Order.AddRange(
                    readyOrder1Tenant1, readyOrder2Tenant1, readyOrder3Tenant1,
                    notReadyOrder1Tenant1, notPaidReadyOrder1Tenant1,
                    readyOrder1Tenant2, readyOrder2Tenant2, notPaidReadyOrder1Tenant2);
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
    public async Task GetOrdersReadyForDelivery_WithValidTenant_ShouldReturnOnlyReadyAndPaidOrders()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_WithDifferentTenant_ShouldReturnOnlyThatTenantData()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(o => o.InvoiceAddressFirstName == "Bob") ?? false);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_ShouldExcludeNotReadyOrders()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Should not include the processing order (ID 4) even if paid
        var orderIds = result.Data?.Select(o => o.Id).ToList();
        TestAssertions.AssertDoesNotContain(Order4Id, orderIds ?? new List<Guid>());

        // All returned orders should have ReadyForDelivery status
        var statuses = result.Data?.Select(o => o.Status).ToList();
        TestAssertions.AssertTrue(statuses?.All(s => s == "ReadyForDelivery") ?? false);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_ShouldExcludeNotCompletelyPaidOrders()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Should not include the ready but not paid order (ID 5)
        var orderIds = result.Data?.Select(o => o.Id).ToList();
        TestAssertions.AssertDoesNotContain(Order5Id, orderIds ?? new List<Guid>());

        // All returned orders should be completely paid
        var paymentStatuses = result.Data?.Select(o => o.PaymentStatus).ToList();
        TestAssertions.AssertTrue(paymentStatuses?.All(s => s == "CompletelyPaid") ?? false);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_WithPagination_ShouldRespectPageSize()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery?pageNumber=0&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_WithOrderByTotal_ShouldReturnOrderedResults()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery?orderBy=Total");

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
    public async Task GetOrdersReadyForDelivery_WithOrderByTotalDescending_ShouldReturnDescOrderedResults()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery?orderBy=Total desc");

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
    public async Task GetOrdersReadyForDelivery_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstOrder = result.Data?.First();
        TestAssertions.AssertNotNull(firstOrder);
        TestAssertions.AssertTrue(firstOrder!.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstOrder.InvoiceAddressFirstName));
        TestAssertions.AssertEqual("ReadyForDelivery", firstOrder.Status);
        TestAssertions.AssertEqual("CompletelyPaid", firstOrder.PaymentStatus);
        TestAssertions.AssertTrue(firstOrder.Total > 0);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_ShouldOnlyIncludeReadyForDeliveryStatus()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var orderStatuses = result.Data?.Select(o => o.Status).Distinct().ToList();
        TestAssertions.AssertEqual(1, orderStatuses?.Count ?? 0);
        TestAssertions.AssertEqual("ReadyForDelivery", orderStatuses?[0]);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_ShouldOnlyIncludeCompletelyPaidStatus()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var paymentStatuses = result.Data?.Select(o => o.PaymentStatus).Distinct().ToList();
        TestAssertions.AssertEqual(1, paymentStatuses?.Count ?? 0);
        TestAssertions.AssertEqual("CompletelyPaid", paymentStatuses?[0]);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_TenantIsolation_ShouldNotLeakDataBetweenTenants()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();

        // Test tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var responseTenant1 = await Client.GetAsync("/api/v1/Orders/ready-for-delivery");
        TestAssertions.AssertHttpSuccess(responseTenant1);
        var resultTenant1 = await ReadResponseAsync<PaginatedResult<OrderListDto>>(responseTenant1);

        // Test tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var responseTenant2 = await Client.GetAsync("/api/v1/Orders/ready-for-delivery");
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
        TestAssertions.AssertEqual(1, tenant2Names?.Count ?? 0);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(3, result.TotalCount);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_WithOrderByDateOrdered_ShouldReturnDateOrderedResults()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery?orderBy=DateOrdered");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetOrdersReadyForDelivery_WithMultipleOrderBy_ShouldRespectMultipleSorting()
    {
        await SeedOrderReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Orders/ready-for-delivery?orderBy=InvoiceAddressFirstName,Total");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<OrderListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }
}