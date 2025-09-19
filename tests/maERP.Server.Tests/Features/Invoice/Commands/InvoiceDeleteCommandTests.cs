using System.Net;
using System.Text.Json;
using maERP.Domain.Constants;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Invoice.Commands;

public class InvoiceDeleteCommandTests : IDisposable
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
    private static readonly Guid Invoice4Id = Guid.NewGuid();
    private static readonly Guid InvoiceItem1Id = Guid.NewGuid();
    private static readonly Guid InvoiceItem2Id = Guid.NewGuid();
    private static readonly Guid Product1Id = Guid.NewGuid();

    public InvoiceDeleteCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_InvoiceDeleteCommandTests_{uniqueId}";
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

        Task.Delay(10).Wait();
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

    private async Task SeedTestDataAsync()
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
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.Customer.AddRange(customer1, customer2);

                var invoice1Tenant1 = new maERP.Domain.Entities.Invoice
                {
                    Id = Invoice1Id,
                    InvoiceNumber = "INV-001",
                    InvoiceDate = DateTime.Now.AddDays(-10),
                    CustomerId = Customer1Id,
                    OrderId = Guid.NewGuid(),
                    Subtotal = 100.00m,
                    ShippingCost = 10.00m,
                    TotalTax = 19.00m,
                    Total = 129.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Created,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var invoice2Tenant1 = new maERP.Domain.Entities.Invoice
                {
                    Id = Invoice2Id,
                    InvoiceNumber = "INV-002",
                    InvoiceDate = DateTime.Now.AddDays(-8),
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
                    InvoiceDate = DateTime.Now.AddDays(-5),
                    CustomerId = Customer2Id,
                    Subtotal = 150.00m,
                    TotalTax = 28.50m,
                    Total = 178.50m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Created,
                    TenantId = TenantConstants.TestTenant2Id
                };

                var invoice4WithItems = new maERP.Domain.Entities.Invoice
                {
                    Id = Invoice4Id,
                    InvoiceNumber = "INV-003",
                    InvoiceDate = DateTime.Now.AddDays(-3),
                    CustomerId = Customer1Id,
                    Subtotal = 300.00m,
                    TotalTax = 57.00m,
                    Total = 357.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Created,
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.Invoice.AddRange(invoice1Tenant1, invoice2Tenant1, invoice3Tenant2, invoice4WithItems);

                var invoiceItem1 = new maERP.Domain.Entities.InvoiceItem
                {
                    Id = InvoiceItem1Id,
                    InvoiceId = Invoice4Id,
                    ProductId = Product1Id,
                    Name = "Test Product",
                    UnitPrice = 100.00m,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var invoiceItem2 = new maERP.Domain.Entities.InvoiceItem
                {
                    Id = InvoiceItem2Id,
                    InvoiceId = Invoice4Id,
                    ProductId = Product1Id,
                    Name = "Another Product",
                    UnitPrice = 200.00m,
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.InvoiceItem.AddRange(invoiceItem1, invoiceItem2);

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
    public async Task DeleteInvoice_WithValidIdAndTenant_ShouldReturnDeletedInvoiceId()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(Invoice1Id, result.Data);

        var deletedInvoice = await DbContext.Invoice
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == Invoice1Id);
        Assert.Null(deletedInvoice);
    }

    [Fact]
    public async Task DeleteInvoice_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var nonExistentId = Guid.NewGuid();
        var response = await Client.DeleteAsync($"/api/v1/Invoices/{nonExistentId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.False(result.Data.HasValue);
    }

    [Fact]
    public async Task DeleteInvoice_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteInvoice_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteInvoice_TenantIsolation_ShouldNotDeleteOtherTenantInvoices()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice3Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);

        var invoice3 = await DbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(i => i.Id == Invoice3Id);
        TestAssertions.AssertNotNull(invoice3);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, invoice3!.TenantId);
    }

    [Fact]
    public async Task DeleteInvoice_WithInvoiceItems_ShouldDeleteInvoiceAndItems()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var itemsBeforeDelete = await DbContext.InvoiceItem.Where(i => i.InvoiceId == Invoice4Id).CountAsync();
        TestAssertions.AssertEqual(2, itemsBeforeDelete);

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice4Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(Invoice4Id, result.Data);

        var deletedInvoice = await DbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(i => i.Id == Invoice4Id);
        Assert.Null(deletedInvoice);

        var itemsAfterDelete = await DbContext.InvoiceItem.Where(i => i.InvoiceId == Invoice4Id).CountAsync();
        TestAssertions.AssertEqual(0, itemsAfterDelete);
    }

    [Fact]
    public async Task DeleteInvoice_WithPaidStatus_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice2Id}");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);

        var invoice2 = await DbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(i => i.Id == Invoice2Id);
        TestAssertions.AssertNotNull(invoice2);
    }

    [Fact]
    public async Task DeleteInvoice_WithInvalidId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/Invoices/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteInvoice_WithZeroId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/Invoices/0");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteInvoice_WithNegativeId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/Invoices/-1");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteInvoice_WithNonExistentTenant_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice1Id}");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest ||
                                 response.StatusCode == HttpStatusCode.NotFound ||
                                 response.StatusCode == HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task DeleteInvoice_ShouldNotAffectOtherTenantInvoices()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var invoicesBeforeDelete = await DbContext.Invoice.Where(i => i.TenantId == TenantConstants.TestTenant2Id).CountAsync();
        TestAssertions.AssertEqual(1, invoicesBeforeDelete);

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice1Id}");
        TestAssertions.AssertHttpSuccess(response);

        var invoicesAfterDelete = await DbContext.Invoice.Where(i => i.TenantId == TenantConstants.TestTenant2Id).CountAsync();
        TestAssertions.AssertEqual(1, invoicesAfterDelete);

        var tenant2Invoice = await DbContext.Invoice.FindAsync(Invoice3Id);
        TestAssertions.AssertNotNull(tenant2Invoice);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, tenant2Invoice!.TenantId);
    }

    [Fact]
    public async Task DeleteInvoice_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(Invoice1Id, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task DeleteInvoice_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var nonExistentId = Guid.NewGuid();
        var response = await Client.DeleteAsync($"/api/v1/Invoices/{nonExistentId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.False(result.Data.HasValue);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteInvoice_MultipleDeletionAttempts_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var firstDeleteResponse = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice1Id}");
        TestAssertions.AssertHttpSuccess(firstDeleteResponse);

        var secondDeleteResponse = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, secondDeleteResponse.StatusCode);

        var result = await ReadResponseAsync<Result<Guid?>>(secondDeleteResponse);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteInvoice_WithLargeId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/Invoices/2147483647");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteInvoice_ShouldOnlyDeleteFromCorrectTenant()
    {
        await SeedTestDataAsync();

        var tenant1InvoicesBeforeDelete = await DbContext.Invoice.Where(i => i.TenantId == TenantConstants.TestTenant1Id).CountAsync();
        var tenant2InvoicesBeforeDelete = await DbContext.Invoice.Where(i => i.TenantId == TenantConstants.TestTenant2Id).CountAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice1Id}");
        TestAssertions.AssertHttpSuccess(response);

        var tenant1InvoicesAfterDelete = await DbContext.Invoice.Where(i => i.TenantId == TenantConstants.TestTenant1Id).CountAsync();
        var tenant2InvoicesAfterDelete = await DbContext.Invoice.Where(i => i.TenantId == TenantConstants.TestTenant2Id).CountAsync();

        TestAssertions.AssertEqual(tenant1InvoicesBeforeDelete - 1, tenant1InvoicesAfterDelete);
        TestAssertions.AssertEqual(tenant2InvoicesBeforeDelete, tenant2InvoicesAfterDelete);
    }

    [Fact]
    public async Task DeleteInvoice_WithDraftStatus_ShouldDeleteSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice1Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(Invoice1Id, result.Data);

        var deletedInvoice = await DbContext.Invoice
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == Invoice1Id);
        Assert.Null(deletedInvoice);
    }

    [Fact]
    public async Task DeleteInvoice_CascadeDelete_ShouldRemoveRelatedEntities()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var invoiceItemsCount = await DbContext.InvoiceItem.Where(i => i.InvoiceId == Invoice4Id).CountAsync();
        TestAssertions.AssertTrue(invoiceItemsCount > 0);

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice4Id}");

        TestAssertions.AssertHttpSuccess(response);

        var remainingItemsCount = await DbContext.InvoiceItem.Where(i => i.InvoiceId == Invoice4Id).CountAsync();
        TestAssertions.AssertEqual(0, remainingItemsCount);
    }

    [Fact]
    public async Task DeleteInvoice_ShouldNotAffectOtherInvoicesInSameTenant()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var tenant1InvoicesBeforeDelete = await DbContext.Invoice
            .Where(i => i.TenantId == TenantConstants.TestTenant1Id && i.Id != Invoice1Id)
            .CountAsync();

        var response = await Client.DeleteAsync($"/api/v1/Invoices/{Invoice1Id}");
        TestAssertions.AssertHttpSuccess(response);

        var tenant1InvoicesAfterDelete = await DbContext.Invoice
            .Where(i => i.TenantId == TenantConstants.TestTenant1Id && i.Id != Invoice1Id)
            .CountAsync();

        TestAssertions.AssertEqual(tenant1InvoicesBeforeDelete, tenant1InvoicesAfterDelete);

        var invoice2 = await DbContext.Invoice.FindAsync(Invoice2Id);
        TestAssertions.AssertNotNull(invoice2);
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, invoice2!.TenantId);
    }
}
