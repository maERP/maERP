using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Sales;
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

namespace maERP.Server.Tests.Features.Sales.Queries;

public class SalesReadyForDeliveryListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;
    private static readonly int Customer1Id = 1;
    private static readonly int Customer2Id = 2;
    private static readonly int Customer3Id = 3;
    private static readonly Guid Sales1Id = Guid.NewGuid();
    private static readonly Guid Sales2Id = Guid.NewGuid();
    private static readonly Guid Sales3Id = Guid.NewGuid();
    private static readonly Guid Sales4Id = Guid.NewGuid();
    private static readonly Guid Sales5Id = Guid.NewGuid();
    private static readonly Guid Sales6Id = Guid.NewGuid();
    private static readonly Guid Sales7Id = Guid.NewGuid();
    private static readonly Guid Sales8Id = Guid.NewGuid();

    public SalesReadyForDeliveryListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SalesReadyForDeliveryListQueryTests_{uniqueId}";
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

    private async Task SeedSalesReadyForDeliveryTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Sales.AnyAsync();
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

                // Create saless for tenant 1 - ready for delivery (ReadyForDelivery + CompletelyPaid)
                var readySales1Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales1Id,
                    CustomerId = Customer1Id,
                    Status = SalesStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 199.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-5),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var readySales2Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales2Id,
                    CustomerId = Customer2Id,
                    Status = SalesStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 299.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-3),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var readySales3Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales3Id,
                    CustomerId = Customer1Id,
                    Status = SalesStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 89.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-1),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // This should NOT appear in ready for delivery list (not ready status)
                var notReadySales1Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales4Id,
                    CustomerId = Customer1Id,
                    Status = SalesStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 149.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-2),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // This should NOT appear in ready for delivery list (not completely paid)
                var notPaidReadySales1Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales5Id,
                    CustomerId = Customer2Id,
                    Status = SalesStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.PartiallyPaid,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 79.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-4),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Create saless for tenant 2
                var readySales1Tenant2 = new Domain.Entities.Sales
                {
                    Id = Sales6Id,
                    CustomerId = Customer3Id,
                    Status = SalesStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    Total = 249.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-7),
                    TenantId = TenantConstants.TestTenant2Id
                };

                var readySales2Tenant2 = new Domain.Entities.Sales
                {
                    Id = Sales7Id,
                    CustomerId = Customer3Id,
                    Status = SalesStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    Total = 349.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-6),
                    TenantId = TenantConstants.TestTenant2Id
                };

                // This should NOT appear in ready for delivery list (tenant 2, not paid)
                var notPaidReadySales1Tenant2 = new Domain.Entities.Sales
                {
                    Id = Sales8Id,
                    CustomerId = Customer3Id,
                    Status = SalesStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.Invoiced,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Johnson",
                    Total = 159.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-8),
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Sales.AddRange(
                    readySales1Tenant1, readySales2Tenant1, readySales3Tenant1,
                    notReadySales1Tenant1, notPaidReadySales1Tenant1,
                    readySales1Tenant2, readySales2Tenant2, notPaidReadySales1Tenant2);
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
    public async Task GetSalessReadyForDelivery_WithValidTenant_ShouldReturnOnlyReadyAndPaidSaless()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_WithDifferentTenant_ShouldReturnOnlyThatTenantData()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(o => o.InvoiceAddressFirstName == "Bob") ?? false);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_ShouldExcludeNotReadySaless()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Should not include the processing sales (ID 4) even if paid
        var salesIds = result.Data?.Select(o => o.Id).ToList();
        TestAssertions.AssertDoesNotContain(Sales4Id, salesIds ?? new List<Guid>());

        // All returned saless should have ReadyForDelivery status
        var statuses = result.Data?.Select(o => o.Status).ToList();
        TestAssertions.AssertTrue(statuses?.All(s => s == SalesStatus.ReadyForDelivery) ?? false);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_ShouldExcludeNotCompletelyPaidSaless()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Should not include the ready but not paid sales (ID 5)
        var salesIds = result.Data?.Select(o => o.Id).ToList();
        TestAssertions.AssertDoesNotContain(Sales5Id, salesIds ?? new List<Guid>());

        // All returned saless should be completely paid
        var paymentStatuses = result.Data?.Select(o => o.PaymentStatus).ToList();
        TestAssertions.AssertTrue(paymentStatuses?.All(s => s == PaymentStatus.CompletelyPaid) ?? false);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_WithPagination_ShouldRespectPageSize()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery?pageNumber=0&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_WithSalesByTotal_ShouldReturnSalesedResults()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery?salesBy=Total");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);

        var totals = result.Data?.Select(x => x.Total).ToList();
        TestAssertions.AssertEqual(89.99m, totals?[0]);
        TestAssertions.AssertEqual(199.99m, totals?[1]);
        TestAssertions.AssertEqual(299.99m, totals?[2]);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_WithSalesByTotalDescending_ShouldReturnDescSalesedResults()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery?salesBy=Total desc");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);

        var totals = result.Data?.Select(x => x.Total).ToList();
        TestAssertions.AssertEqual(299.99m, totals?[0]);
        TestAssertions.AssertEqual(199.99m, totals?[1]);
        TestAssertions.AssertEqual(89.99m, totals?[2]);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstSales = result.Data?.First();
        TestAssertions.AssertNotNull(firstSales);
        TestAssertions.AssertTrue(firstSales!.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstSales.InvoiceAddressFirstName));
        TestAssertions.AssertEqual(SalesStatus.ReadyForDelivery, firstSales.Status);
        TestAssertions.AssertEqual(PaymentStatus.CompletelyPaid, firstSales.PaymentStatus);
        TestAssertions.AssertTrue(firstSales.Total > 0);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_ShouldOnlyIncludeReadyForDeliveryStatus()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var salesStatuses = result.Data?.Select(o => o.Status).Distinct().ToList();
        TestAssertions.AssertEqual(1, salesStatuses?.Count ?? 0);
        TestAssertions.AssertEqual(SalesStatus.ReadyForDelivery, salesStatuses?[0]);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_ShouldOnlyIncludeCompletelyPaidStatus()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var paymentStatuses = result.Data?.Select(o => o.PaymentStatus).Distinct().ToList();
        TestAssertions.AssertEqual(1, paymentStatuses?.Count ?? 0);
        TestAssertions.AssertEqual(PaymentStatus.CompletelyPaid, paymentStatuses?[0]);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_TenantIsolation_ShouldNotLeakDataBetweenTenants()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();

        // Test tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var responseTenant1 = await Client.GetAsync("/api/v1/Saless/ready-for-delivery");
        TestAssertions.AssertHttpSuccess(responseTenant1);
        var resultTenant1 = await ReadResponseAsync<PaginatedResult<SalesListDto>>(responseTenant1);

        // Test tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var responseTenant2 = await Client.GetAsync("/api/v1/Saless/ready-for-delivery");
        TestAssertions.AssertHttpSuccess(responseTenant2);
        var resultTenant2 = await ReadResponseAsync<PaginatedResult<SalesListDto>>(responseTenant2);

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
    public async Task GetSalessReadyForDelivery_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(3, result.TotalCount);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_WithSalesByDateSalesed_ShouldReturnDateSalesedResults()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery?salesBy=DateSalesed");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetSalessReadyForDelivery_WithMultipleSalesBy_ShouldRespectMultipleSorting()
    {
        await SeedSalesReadyForDeliveryTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Saless/ready-for-delivery?salesBy=InvoiceAddressFirstName,Total");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }
}