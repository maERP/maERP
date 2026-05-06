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

public class SalesCustomerListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;
    private static readonly int Customer1Id = 1;
    private static readonly int Customer2Id = 2;
    private static readonly int Customer3Id = 3;
    private static readonly int Customer4Id = 4;
    private static readonly int Customer5Id = 5;
    private static readonly Guid Sales1Id = Guid.NewGuid();
    private static readonly Guid Sales2Id = Guid.NewGuid();
    private static readonly Guid Sales3Id = Guid.NewGuid();
    private static readonly Guid Sales4Id = Guid.NewGuid();
    private static readonly Guid Sales5Id = Guid.NewGuid();
    private static readonly Guid Sales6Id = Guid.NewGuid();
    private static readonly Guid Sales7Id = Guid.NewGuid();
    private static readonly Guid Sales8Id = Guid.NewGuid();
    private static readonly Guid Sales9Id = Guid.NewGuid();

    public SalesCustomerListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SalesCustomerListQueryTests_{uniqueId}";
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

    private async Task SeedSalesCustomerTestDataAsync()
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

                var customer3Tenant1 = new Domain.Entities.Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Customer3Id,
                    Firstname = "Alice",
                    Lastname = "Johnson",
                    Email = "alice.johnson@test.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer1Tenant2 = new Domain.Entities.Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Customer4Id,
                    Firstname = "Bob",
                    Lastname = "Wilson",
                    Email = "bob.wilson@test.com",
                    TenantId = TenantConstants.TestTenant2Id
                };

                var customer2Tenant2 = new Domain.Entities.Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Customer5Id,
                    Firstname = "Carol",
                    Lastname = "Brown",
                    Email = "carol.brown@test.com",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Customer.AddRange(customer1Tenant1, customer2Tenant1, customer3Tenant1, customer1Tenant2, customer2Tenant2);

                // Create multiple saless for customer 1 (tenant 1)
                var sales1Customer1Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales1Id,
                    CustomerId = Customer1Id,
                    Status = SalesStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 199.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-5),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var sales2Customer1Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales2Id,
                    CustomerId = Customer1Id,
                    Status = SalesStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.PartiallyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 299.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-3),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var sales3Customer1Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales3Id,
                    CustomerId = Customer1Id,
                    Status = SalesStatus.Completed,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    Total = 89.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-1),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Create saless for customer 2 (tenant 1)
                var sales1Customer2Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales4Id,
                    CustomerId = Customer2Id,
                    Status = SalesStatus.Processing,
                    PaymentStatus = PaymentStatus.Invoiced,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 149.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-4),
                    TenantId = TenantConstants.TestTenant1Id
                };

                var sales2Customer2Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales5Id,
                    CustomerId = Customer2Id,
                    Status = SalesStatus.OnHold,
                    PaymentStatus = PaymentStatus.FirstReminder,
                    InvoiceAddressFirstName = "Jane",
                    InvoiceAddressLastName = "Smith",
                    Total = 79.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-2),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Create sales for customer 3 (tenant 1)
                var sales1Customer3Tenant1 = new Domain.Entities.Sales
                {
                    Id = Sales6Id,
                    CustomerId = Customer3Id,
                    Status = SalesStatus.Pending,
                    PaymentStatus = PaymentStatus.Unknown,
                    InvoiceAddressFirstName = "Alice",
                    InvoiceAddressLastName = "Johnson",
                    Total = 249.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-6),
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Create saless for customer 1 (tenant 2)
                var sales1Customer1Tenant2 = new Domain.Entities.Sales
                {
                    Id = Sales7Id,
                    CustomerId = Customer4Id,
                    Status = SalesStatus.Processing,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Wilson",
                    Total = 349.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-7),
                    TenantId = TenantConstants.TestTenant2Id
                };

                var sales2Customer1Tenant2 = new Domain.Entities.Sales
                {
                    Id = Sales8Id,
                    CustomerId = Customer4Id,
                    Status = SalesStatus.ReadyForDelivery,
                    PaymentStatus = PaymentStatus.PartiallyPaid,
                    InvoiceAddressFirstName = "Bob",
                    InvoiceAddressLastName = "Wilson",
                    Total = 449.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-8),
                    TenantId = TenantConstants.TestTenant2Id
                };

                // Create sales for customer 2 (tenant 2)
                var sales1Customer2Tenant2 = new Domain.Entities.Sales
                {
                    Id = Sales9Id,
                    CustomerId = Customer5Id,
                    Status = SalesStatus.Completed,
                    PaymentStatus = PaymentStatus.CompletelyPaid,
                    InvoiceAddressFirstName = "Carol",
                    InvoiceAddressLastName = "Brown",
                    Total = 159.99m,
                    DateSalesed = DateTime.UtcNow.AddDays(-9),
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Sales.AddRange(
                    sales1Customer1Tenant1, sales2Customer1Tenant1, sales3Customer1Tenant1,
                    sales1Customer2Tenant1, sales2Customer2Tenant1,
                    sales1Customer3Tenant1,
                    sales1Customer1Tenant2, sales2Customer1Tenant2,
                    sales1Customer2Tenant2);
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
    public async Task GetSalessByCustomer_WithValidCustomerAndTenant_ShouldReturnCustomerSaless()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(o => o.CustomerId == Customer1Id) ?? false);
    }

    [Fact]
    public async Task GetSalessByCustomer_WithDifferentCustomer_ShouldReturnOnlyThatCustomerSaless()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer2Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(o => o.CustomerId == Customer2Id) ?? false);
        TestAssertions.AssertTrue(result.Data?.All(o => o.InvoiceAddressFirstName == "Jane") ?? false);
    }

    [Fact]
    public async Task GetSalessByCustomer_WithCrossTenantCustomerId_ShouldReturnEmptyResult()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Try to access customer 4 which belongs to tenant 2
        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer4Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetSalessByCustomer_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        await SeedSalesCustomerTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetSalessByCustomer_WithNonExistentCustomer_ShouldReturnEmptyResult()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/99999");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetSalessByCustomer_WithPagination_ShouldRespectPageSize()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}?pageNumber=0&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetSalessByCustomer_WithSearchString_ShouldFilterResults()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}?searchString=CompletelyPaid");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.All(o => o.PaymentStatus == PaymentStatus.CompletelyPaid) ?? false);
    }

    [Fact]
    public async Task GetSalessByCustomer_WithSearchStringNoMatch_ShouldReturnEmpty()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}?searchString=NonexistentStatus");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetSalessByCustomer_WithSalesByTotal_ShouldReturnSalesedResults()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}?salesBy=Total");

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
    public async Task GetSalessByCustomer_WithSalesByTotalDescending_ShouldReturnDescSalesedResults()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}?salesBy=Total desc");

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
    public async Task GetSalessByCustomer_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
    }

    [Fact]
    public async Task GetSalessByCustomer_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstSales = result.Data?.First();
        TestAssertions.AssertNotNull(firstSales);
        TestAssertions.AssertEqual(Customer1Id, firstSales!.CustomerId);
        TestAssertions.AssertTrue(firstSales.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstSales.InvoiceAddressFirstName));
        TestAssertions.AssertTrue(Enum.IsDefined(firstSales.Status));
        TestAssertions.AssertTrue(firstSales.Total > 0);
    }

    [Fact]
    public async Task GetSalessByCustomer_ShouldIncludeVariousSalesStatuses()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        var salesStatuses = result.Data?.Select(o => o.Status).ToList();
        TestAssertions.AssertContains(SalesStatus.Processing, salesStatuses ?? new List<SalesStatus>());
        TestAssertions.AssertContains(SalesStatus.ReadyForDelivery, salesStatuses ?? new List<SalesStatus>());
        TestAssertions.AssertContains(SalesStatus.Completed, salesStatuses ?? new List<SalesStatus>());
    }

    [Fact]
    public async Task GetSalessByCustomer_TenantIsolation_ShouldNotLeakDataBetweenTenants()
    {
        await SeedSalesCustomerTestDataAsync();

        // Test tenant 1 customer 1 (3 saless)
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var responseTenant1Customer1 = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}");
        TestAssertions.AssertHttpSuccess(responseTenant1Customer1);
        var resultTenant1Customer1 = await ReadResponseAsync<PaginatedResult<SalesListDto>>(responseTenant1Customer1);

        // Test tenant 2 customer 4 (2 saless)
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var responseTenant2Customer4 = await Client.GetAsync($"/api/v1/Saless/customer/{Customer4Id}");
        TestAssertions.AssertHttpSuccess(responseTenant2Customer4);
        var resultTenant2Customer4 = await ReadResponseAsync<PaginatedResult<SalesListDto>>(responseTenant2Customer4);

        // Verify data isolation
        TestAssertions.AssertNotNull(resultTenant1Customer1?.Data);
        TestAssertions.AssertNotNull(resultTenant2Customer4?.Data);
        TestAssertions.AssertEqual(3, resultTenant1Customer1?.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, resultTenant2Customer4?.Data?.Count ?? 0);

        // Ensure no data overlap
        var tenant1CustomerIds = resultTenant1Customer1?.Data?.Select(o => o.CustomerId).Distinct().ToList();
        var tenant2CustomerIds = resultTenant2Customer4?.Data?.Select(o => o.CustomerId).Distinct().ToList();
        TestAssertions.AssertEqual(1, tenant1CustomerIds?.Count ?? 0);
        TestAssertions.AssertEqual(Customer1Id, tenant1CustomerIds?[0] ?? 0);
        TestAssertions.AssertEqual(1, tenant2CustomerIds?.Count ?? 0);
        TestAssertions.AssertEqual(Customer4Id, tenant2CustomerIds?[0] ?? 0);
    }

    [Fact]
    public async Task GetSalessByCustomer_WithDifferentCustomersInSameTenant_ShouldReturnCorrectSaless()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get saless for customer 1
        var responseCustomer1 = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}");
        TestAssertions.AssertHttpSuccess(responseCustomer1);
        var resultCustomer1 = await ReadResponseAsync<PaginatedResult<SalesListDto>>(responseCustomer1);

        // Get saless for customer 2
        var responseCustomer2 = await Client.GetAsync($"/api/v1/Saless/customer/{Customer2Id}");
        TestAssertions.AssertHttpSuccess(responseCustomer2);
        var resultCustomer2 = await ReadResponseAsync<PaginatedResult<SalesListDto>>(responseCustomer2);

        // Verify correct sales counts per customer
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
    public async Task GetSalessByCustomer_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(3, result.TotalCount);
    }

    [Fact]
    public async Task GetSalessByCustomer_WithZeroCustomerId_ShouldReturnEmptyResults()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/0");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetSalessByCustomer_WithSalesByDateSalesed_ShouldReturnDateSalesedResults()
    {
        await SeedSalesCustomerTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Saless/customer/{Customer1Id}?salesBy=DateSalesed");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SalesListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Count ?? 0);
    }
}