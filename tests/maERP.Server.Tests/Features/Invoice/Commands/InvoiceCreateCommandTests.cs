using System.Net;
using System.Text.Json;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Invoice.Commands;

public class InvoiceCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public InvoiceCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_InvoiceCreateCommandTests_{uniqueId}";
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

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PostAsync(requestUri, content);
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
            var hasData = await DbContext.Customer.AnyAsync();
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

                var order1 = new maERP.Domain.Entities.Order
                {
                    Id = 1001,
                    CustomerId = 1,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var order2 = new maERP.Domain.Entities.Order
                {
                    Id = 1002,
                    CustomerId = 2,
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.Order.AddRange(order1, order2);

                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private InvoiceInputDto CreateValidInvoiceDto()
    {
        return new InvoiceInputDto
        {
            InvoiceNumber = $"INV-{DateTime.Now.Ticks}",
            InvoiceDate = DateTime.Now,
            CustomerId = 1,
            OrderId = 1001,
            Subtotal = 100.00m,
            ShippingCost = 10.00m,
            TotalTax = 19.00m,
            Total = 129.00m,
            PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
            InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Created,
            Notes = "Test invoice",
            InvoiceAddressFirstName = "John",
            InvoiceAddressLastName = "Doe",
            InvoiceAddressStreet = "123 Test St",
            InvoiceAddressCity = "Test City",
            InvoiceAddressZip = "12345",
            InvoiceAddressCountry = "Test Country"
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateInvoice_WithValidData_ShouldReturnCreatedInvoiceId()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);

        var createdInvoice = await DbContext.Invoice.FindAsync(result.Data);
        TestAssertions.AssertNotNull(createdInvoice);
        TestAssertions.AssertEqual(invoiceDto.InvoiceNumber, createdInvoice!.InvoiceNumber);
        TestAssertions.AssertEqual(1, createdInvoice.TenantId);
    }

    [Fact]
    public async Task CreateInvoice_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = new InvoiceInputDto
        {
            // Missing required fields like InvoiceNumber, CustomerId, etc.
            Notes = "Incomplete invoice"
        };

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateInvoice_WithDuplicateInvoiceNumber_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        
        var firstInvoice = CreateValidInvoiceDto();
        await PostAsJsonAsync("/api/v1/Invoices", firstInvoice);

        var duplicateInvoice = CreateValidInvoiceDto();
        duplicateInvoice.InvoiceNumber = firstInvoice.InvoiceNumber;

        var response = await PostAsJsonAsync("/api/v1/Invoices", duplicateInvoice);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateInvoice_WithNonExistentCustomerId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();
        invoiceDto.CustomerId = 999; // Non-existent customer

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateInvoice_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var invoiceDto = CreateValidInvoiceDto();

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateInvoice_WithNonExistentTenant_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(999);
        var invoiceDto = CreateValidInvoiceDto();

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest || 
                                 response.StatusCode == HttpStatusCode.NotFound ||
                                 response.StatusCode == HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CreateInvoice_TenantIsolation_ShouldNotAccessOtherTenantCustomers()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();
        invoiceDto.CustomerId = 2; // Customer from tenant 2

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task CreateInvoice_WithInvalidInvoiceNumber_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();
        invoiceDto.InvoiceNumber = ""; // Empty invoice number

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateInvoice_WithNegativeAmounts_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();
        invoiceDto.Total = -100.00m; // Negative total

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateInvoice_WithFutureInvoiceDate_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();
        invoiceDto.InvoiceDate = DateTime.Now.AddDays(30); // Future date

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateInvoice_WithCompleteAddressDetails_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();
        invoiceDto.InvoiceAddressCompanyName = "Test Company";
        invoiceDto.InvoiceAddressPhone = "+1234567890";
        invoiceDto.DeliveryAddressFirstName = "Jane";
        invoiceDto.DeliveryAddressLastName = "Doe";
        invoiceDto.DeliveryAddressStreet = "456 Delivery St";
        invoiceDto.DeliveryAddressCity = "Delivery City";
        invoiceDto.DeliveryAddressZip = "54321";
        invoiceDto.DeliveryAddressCountry = "Delivery Country";

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var createdInvoice = await DbContext.Invoice.FindAsync(result.Data);
        TestAssertions.AssertNotNull(createdInvoice);
        TestAssertions.AssertEqual("Test Company", createdInvoice!.InvoiceAddressCompanyName);
        TestAssertions.AssertEqual("Delivery City", createdInvoice.DeliveryAddressCity);
    }

    [Fact]
    public async Task CreateInvoice_WithDifferentTenants_ShouldMaintainStrictIsolation()
    {
        await SeedTestDataAsync();

        SetTenantHeader(1);
        var invoice1 = CreateValidInvoiceDto();
        invoice1.InvoiceNumber = "INV-T1-001";
        invoice1.CustomerId = 1;
        var response1 = await PostAsJsonAsync("/api/v1/Invoices", invoice1);
        TestAssertions.AssertEqual(HttpStatusCode.Created, response1.StatusCode);

        SetTenantHeader(2);
        var invoice2 = CreateValidInvoiceDto();
        invoice2.InvoiceNumber = "INV-T2-001";
        invoice2.CustomerId = 2;
        var response2 = await PostAsJsonAsync("/api/v1/Invoices", invoice2);
        TestAssertions.AssertEqual(HttpStatusCode.Created, response2.StatusCode);

        var result1 = await ReadResponseAsync<Result<int>>(response1);
        var result2 = await ReadResponseAsync<Result<int>>(response2);

        var createdInvoice1 = await DbContext.Invoice.FindAsync(result1.Data);
        var createdInvoice2 = await DbContext.Invoice.FindAsync(result2.Data);

        TestAssertions.AssertNotNull(createdInvoice1);
        TestAssertions.AssertNotNull(createdInvoice2);
        TestAssertions.AssertEqual(1, createdInvoice1!.TenantId);
        TestAssertions.AssertEqual(2, createdInvoice2!.TenantId);
        TestAssertions.AssertNotEqual(createdInvoice1.Id, createdInvoice2.Id);
    }

    [Fact]
    public async Task CreateInvoice_WithInvalidOrderId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();
        invoiceDto.OrderId = 999; // Non-existent order

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateInvoice_WithValidOrderFromDifferentTenant_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();
        invoiceDto.OrderId = 1002; // Order from tenant 2

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task CreateInvoice_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task CreateInvoice_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = new InvoiceInputDto(); // Invalid/empty invoice

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task CreateInvoice_WithLongInvoiceNumber_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();
        invoiceDto.InvoiceNumber = new string('A', 1000); // Very long invoice number

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateInvoice_WithSpecialCharactersInInvoiceNumber_ShouldHandleCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var invoiceDto = CreateValidInvoiceDto();
        invoiceDto.InvoiceNumber = "INV-#@$%-001";

        var response = await PostAsJsonAsync("/api/v1/Invoices", invoiceDto);

        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        
        if (result.Succeeded)
        {
            TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
            var createdInvoice = await DbContext.Invoice.FindAsync(result.Data);
            TestAssertions.AssertNotNull(createdInvoice);
            TestAssertions.AssertEqual("INV-#@$%-001", createdInvoice!.InvoiceNumber);
        }
        else
        {
            TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}