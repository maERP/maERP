using System.Net;
using System.Text.Json;
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

        TenantContext.SetAssignedTenantIds(new[] { 1, 2 });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(int tenantId)
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
                    Id = 1,
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "john.doe@test.com",
                    TenantId = 1
                };

                var customer2 = new maERP.Domain.Entities.Customer
                {
                    Id = 2,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    Email = "jane.smith@test.com",
                    TenantId = 2
                };

                DbContext.Customer.AddRange(customer1, customer2);

                var invoice1Tenant1 = new maERP.Domain.Entities.Invoice
                {
                    Id = 1,
                    InvoiceNumber = "INV-001",
                    InvoiceDate = DateTime.Now.AddDays(-10),
                    CustomerId = 1,
                    OrderId = 1001,
                    Subtotal = 100.00m,
                    ShippingCost = 10.00m,
                    TotalTax = 19.00m,
                    Total = 129.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Created,
                    TenantId = 1
                };

                var invoice2Tenant1 = new maERP.Domain.Entities.Invoice
                {
                    Id = 2,
                    InvoiceNumber = "INV-002",
                    InvoiceDate = DateTime.Now.AddDays(-8),
                    CustomerId = 1,
                    Subtotal = 200.00m,
                    TotalTax = 38.00m,
                    Total = 238.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.CompletelyPaid,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Sent,
                    TenantId = 1
                };

                var invoice3Tenant2 = new maERP.Domain.Entities.Invoice
                {
                    Id = 3,
                    InvoiceNumber = "INV-T2-001",
                    InvoiceDate = DateTime.Now.AddDays(-5),
                    CustomerId = 2,
                    Subtotal = 150.00m,
                    TotalTax = 28.50m,
                    Total = 178.50m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Created,
                    TenantId = 2
                };

                var invoice4WithItems = new maERP.Domain.Entities.Invoice
                {
                    Id = 4,
                    InvoiceNumber = "INV-003",
                    InvoiceDate = DateTime.Now.AddDays(-3),
                    CustomerId = 1,
                    Subtotal = 300.00m,
                    TotalTax = 57.00m,
                    Total = 357.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Created,
                    TenantId = 1
                };

                DbContext.Invoice.AddRange(invoice1Tenant1, invoice2Tenant1, invoice3Tenant2, invoice4WithItems);

                var invoiceItem1 = new maERP.Domain.Entities.InvoiceItem
                {
                    Id = 1,
                    InvoiceId = 4,
                    ProductId = 1,
                    Name = "Test Product",
                    UnitPrice = 100.00m,
                    TenantId = 1
                };

                var invoiceItem2 = new maERP.Domain.Entities.InvoiceItem
                {
                    Id = 2,
                    InvoiceId = 4,
                    ProductId = 1,
                    Name = "Another Product",
                    UnitPrice = 200.00m,
                    TenantId = 1
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
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Invoices/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data);

        var deletedInvoice = await DbContext.Invoice.FindAsync(1);
        Assert.Null(deletedInvoice);
    }

    [Fact]
    public async Task DeleteInvoice_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Invoices/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data);
    }

    [Fact]
    public async Task DeleteInvoice_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.DeleteAsync("/api/v1/Invoices/1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteInvoice_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();

        var response = await Client.DeleteAsync("/api/v1/Invoices/1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteInvoice_TenantIsolation_ShouldNotDeleteOtherTenantInvoices()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Invoices/3");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);

        var invoice3 = await DbContext.Invoice.FindAsync(3);
        TestAssertions.AssertNotNull(invoice3);
        TestAssertions.AssertEqual(2, invoice3!.TenantId);
    }

    [Fact]
    public async Task DeleteInvoice_WithInvoiceItems_ShouldDeleteInvoiceAndItems()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var itemsBeforeDelete = await DbContext.InvoiceItem.Where(i => i.InvoiceId == 4).CountAsync();
        TestAssertions.AssertEqual(2, itemsBeforeDelete);

        var response = await Client.DeleteAsync("/api/v1/Invoices/4");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(4, result.Data);

        var deletedInvoice = await DbContext.Invoice.FindAsync(4);
        Assert.Null(deletedInvoice);

        var itemsAfterDelete = await DbContext.InvoiceItem.Where(i => i.InvoiceId == 4).CountAsync();
        TestAssertions.AssertEqual(0, itemsAfterDelete);
    }

    [Fact]
    public async Task DeleteInvoice_WithPaidStatus_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Invoices/2");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);

        var invoice2 = await DbContext.Invoice.FindAsync(2);
        TestAssertions.AssertNotNull(invoice2);
    }

    [Fact]
    public async Task DeleteInvoice_WithInvalidId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Invoices/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteInvoice_WithZeroId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Invoices/0");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteInvoice_WithNegativeId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Invoices/-1");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteInvoice_WithNonExistentTenant_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.DeleteAsync("/api/v1/Invoices/1");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest || 
                                 response.StatusCode == HttpStatusCode.NotFound ||
                                 response.StatusCode == HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task DeleteInvoice_ShouldNotAffectOtherTenantInvoices()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var invoicesBeforeDelete = await DbContext.Invoice.Where(i => i.TenantId == 2).CountAsync();
        TestAssertions.AssertEqual(1, invoicesBeforeDelete);

        var response = await Client.DeleteAsync("/api/v1/Invoices/1");
        TestAssertions.AssertHttpSuccess(response);

        var invoicesAfterDelete = await DbContext.Invoice.Where(i => i.TenantId == 2).CountAsync();
        TestAssertions.AssertEqual(1, invoicesAfterDelete);

        var tenant2Invoice = await DbContext.Invoice.FindAsync(3);
        TestAssertions.AssertNotNull(tenant2Invoice);
        TestAssertions.AssertEqual(2, tenant2Invoice!.TenantId);
    }

    [Fact]
    public async Task DeleteInvoice_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Invoices/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task DeleteInvoice_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Invoices/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteInvoice_MultipleDeletionAttempts_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var firstDeleteResponse = await Client.DeleteAsync("/api/v1/Invoices/1");
        TestAssertions.AssertHttpSuccess(firstDeleteResponse);

        var secondDeleteResponse = await Client.DeleteAsync("/api/v1/Invoices/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, secondDeleteResponse.StatusCode);

        var result = await ReadResponseAsync<Result<int>>(secondDeleteResponse);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteInvoice_WithLargeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Invoices/2147483647");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task DeleteInvoice_ShouldOnlyDeleteFromCorrectTenant()
    {
        await SeedTestDataAsync();

        var tenant1InvoicesBeforeDelete = await DbContext.Invoice.Where(i => i.TenantId == 1).CountAsync();
        var tenant2InvoicesBeforeDelete = await DbContext.Invoice.Where(i => i.TenantId == 2).CountAsync();

        SetTenantHeader(1);
        var response = await Client.DeleteAsync("/api/v1/Invoices/1");
        TestAssertions.AssertHttpSuccess(response);

        var tenant1InvoicesAfterDelete = await DbContext.Invoice.Where(i => i.TenantId == 1).CountAsync();
        var tenant2InvoicesAfterDelete = await DbContext.Invoice.Where(i => i.TenantId == 2).CountAsync();

        TestAssertions.AssertEqual(tenant1InvoicesBeforeDelete - 1, tenant1InvoicesAfterDelete);
        TestAssertions.AssertEqual(tenant2InvoicesBeforeDelete, tenant2InvoicesAfterDelete);
    }

    [Fact]
    public async Task DeleteInvoice_WithDraftStatus_ShouldDeleteSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.DeleteAsync("/api/v1/Invoices/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data);

        var deletedInvoice = await DbContext.Invoice.FindAsync(1);
        Assert.Null(deletedInvoice);
    }

    [Fact]
    public async Task DeleteInvoice_CascadeDelete_ShouldRemoveRelatedEntities()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var invoiceItemsCount = await DbContext.InvoiceItem.Where(i => i.InvoiceId == 4).CountAsync();
        TestAssertions.AssertTrue(invoiceItemsCount > 0);

        var response = await Client.DeleteAsync("/api/v1/Invoices/4");

        TestAssertions.AssertHttpSuccess(response);

        var remainingItemsCount = await DbContext.InvoiceItem.Where(i => i.InvoiceId == 4).CountAsync();
        TestAssertions.AssertEqual(0, remainingItemsCount);
    }

    [Fact]
    public async Task DeleteInvoice_ShouldNotAffectOtherInvoicesInSameTenant()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var tenant1InvoicesBeforeDelete = await DbContext.Invoice
            .Where(i => i.TenantId == 1 && i.Id != 1)
            .CountAsync();

        var response = await Client.DeleteAsync("/api/v1/Invoices/1");
        TestAssertions.AssertHttpSuccess(response);

        var tenant1InvoicesAfterDelete = await DbContext.Invoice
            .Where(i => i.TenantId == 1 && i.Id != 1)
            .CountAsync();

        TestAssertions.AssertEqual(tenant1InvoicesBeforeDelete, tenant1InvoicesAfterDelete);

        var invoice2 = await DbContext.Invoice.FindAsync(2);
        TestAssertions.AssertNotNull(invoice2);
        TestAssertions.AssertEqual(1, invoice2!.TenantId);
    }
}