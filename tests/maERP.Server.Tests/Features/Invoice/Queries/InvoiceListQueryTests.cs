using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Invoice.Queries;

public class InvoiceListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;
    private static readonly Guid Customer1Id = Guid.NewGuid();
    private static readonly Guid Customer2Id = Guid.NewGuid();
    private static readonly Guid Invoice1Id = Guid.NewGuid();
    private static readonly Guid Invoice2Id = Guid.NewGuid();
    private static readonly Guid Invoice3Id = Guid.NewGuid();

    public InvoiceListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_InvoiceListQueryTests_{uniqueId}";
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

    private async Task SeedInvoiceTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Invoice.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                var customer1 = new maERP.Domain.Entities.Customer
                {
                    Id = Customer1Id,
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "john.doe@test.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer2 = new maERP.Domain.Entities.Customer
                {
                    Id = Customer2Id,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    Email = "jane.smith@test.com",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Customer.AddRange(customer1, customer2);

                var invoice1Tenant1 = new maERP.Domain.Entities.Invoice
                {
                    Id = Invoice1Id,
                    InvoiceNumber = "INV-001",
                    InvoiceDate = DateTime.Now.AddDays(-10),
                    CustomerId = Customer1Id,
                    Subtotal = 100.00m,
                    TotalTax = 19.00m,
                    Total = 119.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Created,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var invoice2Tenant1 = new maERP.Domain.Entities.Invoice
                {
                    Id = Invoice2Id,
                    InvoiceNumber = "INV-002",
                    InvoiceDate = DateTime.Now.AddDays(-5),
                    CustomerId = Customer1Id,
                    Subtotal = 200.00m,
                    TotalTax = 38.00m,
                    Total = 238.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.CompletelyPaid,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Sent,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var invoice3Tenant2 = new maERP.Domain.Entities.Invoice
                {
                    Id = Invoice3Id,
                    InvoiceNumber = "INV-T2-001",
                    InvoiceDate = DateTime.Now.AddDays(-3),
                    CustomerId = Customer2Id,
                    Subtotal = 150.00m,
                    TotalTax = 28.50m,
                    Total = 178.50m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Sent,
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Invoice.AddRange(invoice1Tenant1, invoice2Tenant1, invoice3Tenant2);
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
    public async Task GetInvoiceList_WithValidTenant_ShouldReturnInvoiceList()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Invoices");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);
        TestAssertions.AssertEqual(2, result.Data.Count);
    }

    [Fact]
    public async Task GetInvoiceList_WithoutTenantHeader_ShouldReturnEmptyList()
    {
        await SeedInvoiceTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Invoices");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetInvoiceList_WithWrongTenant_ShouldReturnEmptyList()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync("/api/v1/Invoices");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetInvoiceList_TenantIsolation_ShouldOnlyReturnOwnInvoices()
    {
        await SeedInvoiceTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/Invoices");
        var result1 = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response1);
        TestAssertions.AssertNotNull(result1);
        TestAssertions.AssertTrue(result1.Succeeded);
        TestAssertions.AssertEqual(2, result1.Data.Count);
        TestAssertions.AssertTrue(result1.Data.All(i => i.InvoiceNumber.StartsWith("INV-")));

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/Invoices");
        var result2 = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response2);
        TestAssertions.AssertNotNull(result2);
        TestAssertions.AssertTrue(result2.Succeeded);
        TestAssertions.AssertEqual(1, result2.Data.Count);
        TestAssertions.AssertEqual("INV-T2-001", result2.Data.First().InvoiceNumber);
    }

    [Fact]
    public async Task GetInvoiceList_WithPagination_ShouldReturnCorrectPage()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Invoices?pageNumber=1&pageSize=1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data.Count);
        TestAssertions.AssertEqual(1, result.CurrentPage);
        TestAssertions.AssertEqual(1, result.PageSize);
        TestAssertions.AssertEqual(2, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetInvoiceList_WithSearchString_ShouldFilterResults()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Invoices?searchString=INV-002");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data.Count);
        TestAssertions.AssertEqual("INV-002", result.Data.First().InvoiceNumber);
    }

    [Fact]
    public async Task GetInvoiceList_WithSorting_ShouldReturnSortedResults()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Invoices?orderBy=InvoiceNumber desc");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(2, result.Data.Count);
        TestAssertions.AssertEqual("INV-002", result.Data.First().InvoiceNumber);
        TestAssertions.AssertEqual("INV-001", result.Data.Last().InvoiceNumber);
    }

    [Fact]
    public async Task GetInvoiceList_WithInvalidPageNumber_ShouldReturnBadRequest()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Invoices?pageNumber=0");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetInvoiceList_WithInvalidPageSize_ShouldReturnBadRequest()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Invoices?pageSize=0");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetInvoiceList_WithLargePageSize_ShouldHandleGracefully()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Invoices?pageSize=1000");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(2, result.Data.Count);
    }

    [Fact]
    public async Task GetInvoiceList_ShouldIncludeAllRequiredFields()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Invoices");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var invoice = result.Data.First();
        TestAssertions.AssertTrue(invoice.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(invoice.InvoiceNumber));
        TestAssertions.AssertTrue(invoice.InvoiceDate != default);
        TestAssertions.AssertTrue(invoice.CustomerId != Guid.Empty);
        TestAssertions.AssertTrue(invoice.Total > 0);
        TestAssertions.AssertNotNull(invoice.PaymentStatus);
        TestAssertions.AssertNotNull(invoice.InvoiceStatus);
        // PaymentMethod enum was removed
    }

    [Fact]
    public async Task GetInvoiceList_WithMultipleTenants_ShouldMaintainStrictIsolation()
    {
        await SeedInvoiceTestDataAsync();

        var tenant1Invoices = new List<InvoiceListDto>();
        var tenant2Invoices = new List<InvoiceListDto>();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/Invoices");
        var result1 = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response1);
        if (result1.Succeeded) tenant1Invoices.AddRange(result1.Data);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/Invoices");
        var result2 = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response2);
        if (result2.Succeeded) tenant2Invoices.AddRange(result2.Data);

        var tenant1InvoiceNumbers = tenant1Invoices.Select(i => i.InvoiceNumber).ToList();
        var tenant2InvoiceNumbers = tenant2Invoices.Select(i => i.InvoiceNumber).ToList();

        TestAssertions.AssertFalse(tenant1InvoiceNumbers.Intersect(tenant2InvoiceNumbers).Any());
    }

    [Fact]
    public async Task GetInvoiceList_ResponseStructure_ShouldHaveCorrectPaginationMetadata()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Invoices?pageNumber=1&pageSize=1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<InvoiceListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.CurrentPage);
        TestAssertions.AssertEqual(1, result.PageSize);
        TestAssertions.AssertTrue(result.TotalCount > 0);
        TestAssertions.AssertTrue(result.TotalPages > 0);
        TestAssertions.AssertTrue(result.HasNextPage);
        TestAssertions.AssertFalse(result.HasPreviousPage);
    }
}