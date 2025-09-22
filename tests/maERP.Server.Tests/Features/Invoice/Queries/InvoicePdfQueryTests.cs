using System.Net;
using System.Text.Json;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Invoice.Queries;

public class InvoicePdfQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    private Guid _invoice1Id;
    private Guid _invoice2Id;

    public InvoicePdfQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_InvoicePdfQueryTests_{uniqueId}";
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

                var customer1Id = Guid.NewGuid();
                var customer2Id = Guid.NewGuid();

                var customer1 = new maERP.Domain.Entities.Customer
                {
                    Id = customer1Id,
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "john.doe@test.com",
                    TenantId = TenantConstants.TestTenant1Id,

                };

                var customer2 = new maERP.Domain.Entities.Customer
                {
                    Id = customer2Id,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    Email = "jane.smith@test.com",
                    TenantId = TenantConstants.TestTenant2Id,

                };

                DbContext.Customer.AddRange(customer1, customer2);

                var product1Id = Guid.NewGuid();

                var product1 = new maERP.Domain.Entities.Product
                {
                    Id = product1Id,
                    Sku = "TEST-001",
                    Name = "Test Product 1",
                    Price = 100.00m,
                    TenantId = TenantConstants.TestTenant1Id,

                };

                DbContext.Product.Add(product1);

                _invoice1Id = Guid.NewGuid();
                var orderId = Guid.NewGuid();

                var invoice1Tenant1 = new maERP.Domain.Entities.Invoice
                {
                    Id = _invoice1Id,
                    InvoiceNumber = "INV-001",
                    InvoiceDate = DateTime.Now.AddDays(-10),
                    CustomerId = customer1Id,
                    OrderId = orderId,
                    Subtotal = 200.00m,
                    ShippingCost = 10.00m,
                    TotalTax = 38.00m,
                    Total = 248.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Created,
                    PaymentTransactionId = "TXN-123",
                    Notes = "Test invoice notes",
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    InvoiceAddressCompanyName = "Test Company",
                    InvoiceAddressPhone = "+1234567890",
                    InvoiceAddressStreet = "123 Test St",
                    InvoiceAddressCity = "Test City",
                    InvoiceAddressZip = "12345",
                    InvoiceAddressCountry = "Test Country",
                    TenantId = TenantConstants.TestTenant1Id,

                };

                _invoice2Id = Guid.NewGuid();

                var invoice2Tenant2 = new maERP.Domain.Entities.Invoice
                {
                    Id = _invoice2Id,
                    InvoiceNumber = "INV-T2-001",
                    InvoiceDate = DateTime.Now.AddDays(-5),
                    CustomerId = customer2Id,
                    Subtotal = 100.00m,
                    TotalTax = 19.00m,
                    Total = 119.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.CompletelyPaid,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Sent,
                    TenantId = TenantConstants.TestTenant2Id,

                };

                DbContext.Invoice.AddRange(invoice1Tenant1, invoice2Tenant2);

                var invoiceItem1 = new maERP.Domain.Entities.InvoiceItem
                {
                    Id = Guid.NewGuid(),
                    InvoiceId = _invoice1Id,
                    ProductId = product1Id,
                    Name = "Test Product 1",
                    UnitPrice = 100.00m,
                    TenantId = TenantConstants.TestTenant1Id,

                };

                DbContext.InvoiceItem.Add(invoiceItem1);

                var setting1 = new maERP.Domain.Entities.Setting
                {
                    Id = Guid.NewGuid(),
                    Key = "CompanyName",
                    Value = "Test Company Ltd",

                };

                var setting2 = new maERP.Domain.Entities.Setting
                {
                    Id = Guid.NewGuid(),
                    Key = "CompanyAddress",
                    Value = "123 Business St",

                };

                DbContext.Setting.AddRange(setting1, setting2);

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
    public async Task GetInvoicePdf_WithValidIdAndTenant_ShouldReturnPdfBytes()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);
    }

    [Fact]
    public async Task GetInvoicePdf_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{Guid.NewGuid()}/pdf");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoicePdf_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoicePdf_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoicePdf_WithValidPdf_ShouldReturnValidByteArray()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data.Length > 0);
    }

    [Fact]
    public async Task GetInvoicePdf_WithInvalidId_ShouldReturnBadRequest()
    {
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Invoices/invalid/pdf");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetInvoicePdf_WithZeroId_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{Guid.Empty}/pdf");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoicePdf_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{Guid.NewGuid()}/pdf");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoicePdf_WithTenant2Invoice_ShouldReturnCorrectPdf()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{_invoice2Id}/pdf");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data.Length > 0);
    }

    [Fact]
    public async Task GetInvoicePdf_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoicePdf_TenantIsolation_ShouldNotReturnOtherTenantInvoices()
    {
        await SeedInvoiceTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync($"/api/v1/Invoices/{_invoice2Id}/pdf");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);
    }

    [Fact]
    public async Task GetInvoicePdf_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task GetInvoicePdf_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{Guid.NewGuid()}/pdf");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetInvoicePdf_WithLargeId_ShouldHandleGracefully()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{Guid.NewGuid()}/pdf");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoicePdf_ShouldGenerateValidPdfHeader()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        var pdfBytes = result.Data;
        TestAssertions.AssertTrue(pdfBytes.Length >= 4);

        var header = System.Text.Encoding.ASCII.GetString(pdfBytes, 0, 4);
        TestAssertions.AssertEqual("%PDF", header);
    }

    [Fact]
    public async Task GetInvoicePdf_MultipleRequests_ShouldReturnConsistentResults()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response1 = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");
        var response2 = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");

        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpSuccess(response2);

        var result1 = await ReadResponseAsync<Result<byte[]>>(response1);
        var result2 = await ReadResponseAsync<Result<byte[]>>(response2);

        TestAssertions.AssertNotNull(result1);
        TestAssertions.AssertNotNull(result2);
        TestAssertions.AssertTrue(result1.Succeeded);
        TestAssertions.AssertTrue(result2.Succeeded);
        TestAssertions.AssertNotNull(result1.Data);
        TestAssertions.AssertNotNull(result2.Data);
    }

    [Fact]
    public async Task GetInvoicePdf_WithCompanySettings_ShouldUseSettingsInPdf()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data.Length > 1000);
    }

    [Fact]
    public async Task GetInvoicePdf_WithCompleteInvoiceData_ShouldGenerateCompletePdf()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{_invoice1Id}/pdf");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        var pdfSize = result.Data.Length;
        TestAssertions.AssertTrue(pdfSize > 0);
    }

    [Fact]
    public async Task GetInvoicePdf_WithMinimalInvoiceData_ShouldStillGeneratePdf()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/Invoices/{_invoice2Id}/pdf");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data.Length > 0);
    }
}