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

namespace maERP.Server.Tests.Features.Invoice.Queries;

public class InvoiceDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public InvoiceDetailQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_InvoiceDetailQueryTests_{uniqueId}";
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

                var product1 = new maERP.Domain.Entities.Product
                {
                    Id = 1,
                    Sku = "TEST-001",
                    Name = "Test Product 1",
                    Price = 100.00m,
                    TenantId = 1
                };

                var product2 = new maERP.Domain.Entities.Product
                {
                    Id = 2,
                    Sku = "TEST-002", 
                    Name = "Test Product 2",
                    Price = 50.00m,
                    TenantId = 2
                };

                DbContext.Product.AddRange(product1, product2);

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
                    DeliveryAddressFirstName = "John",
                    DeliveryAddressLastName = "Doe",
                    DeliveryAddressStreet = "456 Delivery St",
                    DeliveryAddressCity = "Delivery City",
                    DeliveryAddressZip = "54321",
                    DeliveryAddressCountry = "Delivery Country",
                    TenantId = 1
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
                    TenantId = 2
                };

                DbContext.Invoice.AddRange(invoice1Tenant1, invoice2Tenant2);

                var invoiceItem1 = new maERP.Domain.Entities.InvoiceItem
                {
                    Id = 1,
                    InvoiceId = 1,
                    ProductId = 1,
                    Name = "Test Product 1",
                    UnitPrice = 100.00m,
                    TenantId = 1
                };

                var invoiceItem2 = new maERP.Domain.Entities.InvoiceItem
                {
                    Id = 2,
                    InvoiceId = 1,
                    ProductId = 1,
                    Name = "Test Product 1 (Second Item)",
                    UnitPrice = 100.00m,
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
    public async Task GetInvoiceDetail_WithValidIdAndTenant_ShouldReturnInvoiceDetail()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data!.Id);
        TestAssertions.AssertEqual("INV-001", result.Data.InvoiceNumber);
        TestAssertions.AssertEqual("John Doe", result.Data.CustomerName);
    }

    [Fact]
    public async Task GetInvoiceDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoiceDetail_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Invoices/1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoiceDetail_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Invoices/1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoiceDetail_ShouldIncludeAllBasicFields()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        var invoice = result.Data!;
        TestAssertions.AssertEqual(1, invoice.Id);
        TestAssertions.AssertEqual("INV-001", invoice.InvoiceNumber);
        TestAssertions.AssertTrue(invoice.InvoiceDate != default);
        TestAssertions.AssertEqual(1, invoice.CustomerId);
        TestAssertions.AssertEqual("John Doe", invoice.CustomerName);
        TestAssertions.AssertEqual(1001, invoice.OrderId);
        TestAssertions.AssertEqual("1001", invoice.OrderNumber);
        TestAssertions.AssertEqual(200.00m, invoice.Subtotal);
        TestAssertions.AssertEqual(10.00m, invoice.ShippingCost);
        TestAssertions.AssertEqual(38.00m, invoice.TotalTax);
        TestAssertions.AssertEqual(248.00m, invoice.Total);
        TestAssertions.AssertEqual(maERP.Domain.Enums.PaymentStatus.Invoiced, invoice.PaymentStatus);
        TestAssertions.AssertEqual(maERP.Domain.Enums.InvoiceStatus.Created, invoice.InvoiceStatus);
        TestAssertions.AssertEqual("TXN-123", invoice.PaymentTransactionId);
        TestAssertions.AssertEqual("Test invoice notes", invoice.Notes);
    }

    [Fact]
    public async Task GetInvoiceDetail_ShouldIncludeInvoiceItems()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        var invoice = result.Data!;
        TestAssertions.AssertNotNull(invoice.InvoiceItems);
        TestAssertions.AssertEqual(2, invoice.InvoiceItems.Count);
        
        var firstItem = invoice.InvoiceItems.First();
        TestAssertions.AssertTrue(firstItem.Id > 0);
        TestAssertions.AssertEqual(1, firstItem.InvoiceId);
        TestAssertions.AssertEqual(1, firstItem.ProductId);
        TestAssertions.AssertEqual("Test Product 1", firstItem.Name);
        TestAssertions.AssertEqual(100.00m, firstItem.UnitPrice);
    }

    [Fact]
    public async Task GetInvoiceDetail_ShouldIncludeInvoiceAddressDetails()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        var invoice = result.Data!;
        TestAssertions.AssertEqual("John", invoice.InvoiceAddressFirstName);
        TestAssertions.AssertEqual("Doe", invoice.InvoiceAddressLastName);
        TestAssertions.AssertEqual("Test Company", invoice.InvoiceAddressCompanyName);
        TestAssertions.AssertEqual("+1234567890", invoice.InvoiceAddressPhone);
        TestAssertions.AssertEqual("123 Test St", invoice.InvoiceAddressStreet);
        TestAssertions.AssertEqual("Test City", invoice.InvoiceAddressCity);
        TestAssertions.AssertEqual("12345", invoice.InvoiceAddressZip);
        TestAssertions.AssertEqual("Test Country", invoice.InvoiceAddressCountry);
    }

    [Fact]
    public async Task GetInvoiceDetail_ShouldIncludeDeliveryAddressDetails()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        var invoice = result.Data!;
        TestAssertions.AssertEqual("John", invoice.DeliveryAddressFirstName);
        TestAssertions.AssertEqual("Doe", invoice.DeliveryAddressLastName);
        TestAssertions.AssertEqual("456 Delivery St", invoice.DeliveryAddressStreet);
        TestAssertions.AssertEqual("Delivery City", invoice.DeliveryAddressCity);
        TestAssertions.AssertEqual("54321", invoice.DeliveryAddressZip);
        TestAssertions.AssertEqual("Delivery Country", invoice.DeliveryAddressCountry);
    }

    [Fact]
    public async Task GetInvoiceDetail_WithTenant2Invoice_ShouldReturnCorrectInvoice()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Invoices/2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data!.Id);
        TestAssertions.AssertEqual("INV-T2-001", result.Data.InvoiceNumber);
        TestAssertions.AssertEqual("Jane Smith", result.Data.CustomerName);
    }

    [Fact]
    public async Task GetInvoiceDetail_WithInvalidId_ShouldReturnBadRequest()
    {
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetInvoiceDetail_WithZeroId_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/0");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoiceDetail_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/-1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoiceDetail_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(999);

        var response = await Client.GetAsync("/api/v1/Invoices/1");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoiceDetail_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task GetInvoiceDetail_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/999");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetInvoiceDetail_WithLargeId_ShouldHandleGracefully()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/2147483647");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetInvoiceDetail_TenantIsolation_ShouldNotReturnOtherTenantInvoices()
    {
        await SeedInvoiceTestDataAsync();

        SetTenantHeader(1);
        var response1 = await Client.GetAsync("/api/v1/Invoices/2");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        SetTenantHeader(2);
        var response2 = await Client.GetAsync("/api/v1/Invoices/1");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);
    }

    [Fact]
    public async Task GetInvoiceDetail_WithMinimalInvoiceData_ShouldReturnCorrectly()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(2);

        var response = await Client.GetAsync("/api/v1/Invoices/2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        var invoice = result.Data!;
        TestAssertions.AssertEqual(2, invoice.Id);
        TestAssertions.AssertEqual("INV-T2-001", invoice.InvoiceNumber);
        TestAssertions.AssertNotNull(invoice.InvoiceItems);
        TestAssertions.AssertEmpty(invoice.InvoiceItems);
    }

    [Fact]
    public async Task GetInvoiceDetail_ShouldIncludeTimestamps()
    {
        await SeedInvoiceTestDataAsync();
        SetTenantHeader(1);

        var response = await Client.GetAsync("/api/v1/Invoices/1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<InvoiceDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        
        var invoice = result.Data!;
        TestAssertions.AssertTrue(invoice.CreatedDate != default);
        TestAssertions.AssertTrue(invoice.LastModifiedDate != default);
    }
}