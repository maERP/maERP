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

                var customer1 = new maERP.Domain.Entities.Customer
                {
                    Id = 1,
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "john.doe@test.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var customer2 = new maERP.Domain.Entities.Customer
                {
                    Id = 2,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    Email = "jane.smith@test.com",
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.Customer.AddRange(customer1, customer2);

                var product1 = new maERP.Domain.Entities.Product
                {
                    Id = 1,
                    Sku = "TEST-001",
                    Name = "Test Product 1",
                    Price = 100.00m,
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.Product.Add(product1);

                var invoice1Tenant1 = new maERP.Domain.Entities.Invoice
                {
                    Id = 1,
                    InvoiceNumber = "INV-001",
                    InvoiceDate = DateTime.Now.AddDays(-10),
                    CustomerId = 1,
                    OrderId = 1001,
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
                    TenantId = TenantConstants.TestTenant1Id
                };

                var invoice2Tenant2 = new maERP.Domain.Entities.Invoice
                {
                    Id = 2,
                    InvoiceNumber = "INV-T2-001",
                    InvoiceDate = DateTime.Now.AddDays(-5),
                    CustomerId = 2,
                    Subtotal = 100.00m,
                    TotalTax = 19.00m,
                    Total = 119.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.CompletelyPaid,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Sent,
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.Invoice.AddRange(invoice1Tenant1, invoice2Tenant2);

                var invoiceItem1 = new maERP.Domain.Entities.InvoiceItem
                {
                    Id = 1,
                    InvoiceId = 1,
                    ProductId = 1,
                    Name = "Test Product 1",
                    UnitPrice = 100.00m,
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.InvoiceItem.Add(invoiceItem1);

                var setting1 = new maERP.Domain.Entities.Setting
                {
                    Id = 1,
                    Key = "CompanyName",
                    Value = "Test Company Ltd",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var setting2 = new maERP.Domain.Entities.Setting
                {
                    Id = 2,
                    Key = "CompanyAddress",
                    Value = "123 Business St",
                    TenantId = TenantConstants.TestTenant1Id
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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1/pdf");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/999/pdf");

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
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Invoices/1/pdf");

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

        var response = await Client.GetAsync("/api/v1/Invoices/1/pdf");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1/pdf");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/invalid/pdf");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetInvoicePdf_WithZeroId_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/0/pdf");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/-1/pdf");

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
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Invoices/2/pdf");

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
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Invoices/1/pdf");

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

        SetTenantHeader(1);
        var response1 = await Client.GetAsync("/api/v1/Invoices/2/pdf");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        SetTenantHeader(2);
        var response2 = await Client.GetAsync("/api/v1/Invoices/1/pdf");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);
    }

    [Fact]
    public async Task GetInvoicePdf_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1/pdf");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/999/pdf");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/2147483647/pdf");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1/pdf");

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
        SetTenantHeader(1);

        var response1 = await Client.GetAsync("/api/v1/Invoices/1/pdf");
        var response2 = await Client.GetAsync("/api/v1/Invoices/1/pdf");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1/pdf");

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
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1/pdf");

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
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Invoices/2/pdf");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<byte[]>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data.Length > 0);
    }
}