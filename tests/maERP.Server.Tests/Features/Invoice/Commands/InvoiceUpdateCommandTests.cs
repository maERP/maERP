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

public class InvoiceUpdateCommandTests : IDisposable
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
    private static readonly Guid InvoiceDuplicateId = Guid.NewGuid();
    private static readonly Guid Order1Id = Guid.NewGuid();
    private static readonly Guid Order2Id = Guid.NewGuid();

    public InvoiceUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_InvoiceUpdateCommandTests_{uniqueId}";
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

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PutAsync(requestUri, content);
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
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Customer.AddRange(customer1, customer2);

                var order1 = new maERP.Domain.Entities.Order
                {
                    Id = Order1Id,
                    CustomerId = Customer1Id,
                    TenantId = TenantConstants.TestTenant1Id
                };

                var order2 = new maERP.Domain.Entities.Order
                {
                    Id = Order2Id,
                    CustomerId = Customer2Id,
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Order.AddRange(order1, order2);

                var invoice1Tenant1 = new maERP.Domain.Entities.Invoice
                {
                    Id = Invoice1Id,
                    InvoiceNumber = "INV-001",
                    InvoiceDate = DateTime.Now.AddDays(-10),
                    CustomerId = Customer1Id,
                    OrderId = Order1Id,
                    Subtotal = 100.00m,
                    ShippingCost = 10.00m,
                    TotalTax = 19.00m,
                    Total = 129.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Created,
                    PaymentTransactionId = "TXN-123",
                    Notes = "Original notes",
                    InvoiceAddressFirstName = "John",
                    InvoiceAddressLastName = "Doe",
                    InvoiceAddressStreet = "123 Original St",
                    InvoiceAddressCity = "Original City",
                    InvoiceAddressZip = "12345",
                    InvoiceAddressCountry = "Original Country",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var invoice2Tenant2 = new maERP.Domain.Entities.Invoice
                {
                    Id = Invoice2Id,
                    InvoiceNumber = "INV-T2-001",
                    InvoiceDate = DateTime.Now.AddDays(-5),
                    CustomerId = Customer2Id,
                    OrderId = Order2Id,
                    Subtotal = 200.00m,
                    TotalTax = 38.00m,
                    Total = 238.00m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.CompletelyPaid,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Sent,
                    TenantId = TenantConstants.TestTenant2Id
                };

                var duplicateInvoiceTenant1 = new maERP.Domain.Entities.Invoice
                {
                    Id = InvoiceDuplicateId,
                    InvoiceNumber = "INV-DUPLICATE",
                    InvoiceDate = DateTime.Now.AddDays(-7),
                    CustomerId = Customer1Id,
                    Subtotal = 80.00m,
                    TotalTax = 15.20m,
                    Total = 95.20m,
                    PaymentStatus = maERP.Domain.Enums.PaymentStatus.Invoiced,
                    InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Created,
                    TenantId = TenantConstants.TestTenant1Id
                };

                DbContext.Invoice.AddRange(invoice1Tenant1, invoice2Tenant2, duplicateInvoiceTenant1);

                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private InvoiceInputDto CreateUpdateInvoiceDto()
    {
        return new InvoiceInputDto
        {
            Id = Invoice1Id,
            InvoiceNumber = "INV-001-UPDATED",
            InvoiceDate = DateTime.Now,
            CustomerId = Customer1Id,
            OrderId = Order1Id,
            Subtotal = 150.00m,
            ShippingCost = 15.00m,
            TotalTax = 28.50m,
            Total = 193.50m,
            PaymentStatus = maERP.Domain.Enums.PaymentStatus.CompletelyPaid,
            InvoiceStatus = maERP.Domain.Enums.InvoiceStatus.Sent,
            PaymentMethod = "Bank Transfer",
            PaymentTransactionId = "TXN-456",
            Notes = "Updated notes",
            InvoiceAddressFirstName = "John",
            InvoiceAddressLastName = "Doe",
            InvoiceAddressStreet = "456 Updated St",
            InvoiceAddressCity = "Updated City",
            InvoiceAddressZip = "54321",
            InvoiceAddressCountry = "Updated Country",
            DeliveryAddressFirstName = "John",
            DeliveryAddressLastName = "Doe",
            DeliveryAddressStreet = "456 Updated St",
            DeliveryAddressCity = "Updated City",
            DeliveryAddressZip = "54321",
            DeliveryAddressCountry = "Updated Country"
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateInvoice_WithValidData_ShouldReturnUpdatedInvoiceId()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(Invoice1Id, result.Data);

        var updatedInvoice = await DbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(i => i.Id == Invoice1Id);
        TestAssertions.AssertNotNull(updatedInvoice);
        TestAssertions.AssertEqual("INV-001-UPDATED", updatedInvoice!.InvoiceNumber);
        TestAssertions.AssertEqual(150.00m, updatedInvoice.Subtotal);
        TestAssertions.AssertEqual("Updated notes", updatedInvoice.Notes);
        TestAssertions.AssertEqual(maERP.Domain.Enums.PaymentStatus.CompletelyPaid, updatedInvoice.PaymentStatus);
    }

    [Fact]
    public async Task UpdateInvoice_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();
        var nonExistentId = Guid.NewGuid();
        updateDto.Id = nonExistentId;

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{nonExistentId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.False(result.Data.HasValue);
    }

    [Fact]
    public async Task UpdateInvoice_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var updateDto = CreateUpdateInvoiceDto();

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateInvoice_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var updateDto = CreateUpdateInvoiceDto();

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateInvoice_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new InvoiceInputDto
        {
            Id = Invoice1Id,
            // Missing required fields
            Notes = "Incomplete update"
        };

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateInvoice_WithDuplicateInvoiceNumber_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();
        updateDto.InvoiceNumber = "INV-DUPLICATE"; // Duplicate from same tenant

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateInvoice_TenantIsolation_ShouldNotAccessOtherTenantInvoices()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();
        updateDto.Id = Invoice2Id; // Invoice from tenant 2

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice2Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateInvoice_WithNonExistentCustomerId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();
        updateDto.CustomerId = Guid.NewGuid(); // Non-existent customer

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateInvoice_WithCustomerFromDifferentTenant_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();
        updateDto.CustomerId = Customer2Id; // Customer from tenant 2

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateInvoice_WithInvalidAmounts_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();
        updateDto.Total = -100.00m; // Negative total

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateInvoice_WithFutureInvoiceDate_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();
        updateDto.InvoiceDate = DateTime.Now.AddDays(30); // Future date

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateInvoice_WithUpdatedAddressDetails_ShouldUpdateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();
        updateDto.InvoiceAddressCompanyName = "Updated Company";
        updateDto.InvoiceAddressPhone = "+9876543210";
        updateDto.DeliveryAddressFirstName = "Jane";
        updateDto.DeliveryAddressLastName = "Updated";
        updateDto.DeliveryAddressStreet = "789 New Delivery St";
        updateDto.DeliveryAddressCity = "New Delivery City";

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var updatedInvoice = await DbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(i => i.Id == Invoice1Id);
        TestAssertions.AssertNotNull(updatedInvoice);
        TestAssertions.AssertEqual("Updated Company", updatedInvoice!.InvoiceAddressCompanyName);
        TestAssertions.AssertEqual("+9876543210", updatedInvoice.InvoiceAddressPhone);
        TestAssertions.AssertEqual("Jane", updatedInvoice.DeliveryAddressFirstName);
        TestAssertions.AssertEqual("New Delivery City", updatedInvoice.DeliveryAddressCity);
    }

    [Fact]
    public async Task UpdateInvoice_WithNonExistentTenant_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(Guid.NewGuid());
        var updateDto = CreateUpdateInvoiceDto();

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.BadRequest ||
                                 response.StatusCode == HttpStatusCode.NotFound ||
                                 response.StatusCode == HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task UpdateInvoice_WithMismatchedIdInUrlAndBody_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();
        updateDto.Id = Invoice2Id; // Different from URL

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateInvoice_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(Invoice1Id, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task UpdateInvoice_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new InvoiceInputDto { Id = Invoice1Id }; // Invalid/incomplete update

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task UpdateInvoice_WithEmptyInvoiceNumber_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();
        updateDto.InvoiceNumber = ""; // Empty invoice number

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateInvoice_ShouldPreserveOriginalCreationData()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var originalInvoice = await DbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(i => i.Id == Invoice1Id);
        var originalCreatedDate = originalInvoice!.DateCreated;
        var originalTenantId = originalInvoice.TenantId;

        var updateDto = CreateUpdateInvoiceDto();
        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertHttpSuccess(response);
        var updatedInvoice = await DbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(i => i.Id == Invoice1Id);
        TestAssertions.AssertNotNull(updatedInvoice);
        TestAssertions.AssertEqual(originalCreatedDate, updatedInvoice!.DateCreated);
        TestAssertions.AssertEqual(originalTenantId, updatedInvoice.TenantId);
        TestAssertions.AssertTrue(updatedInvoice.DateModified > originalCreatedDate);
    }

    [Fact]
    public async Task UpdateInvoice_WithLongInvoiceNumber_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();
        updateDto.InvoiceNumber = new string('B', 1000); // Very long invoice number

        var response = await PutAsJsonAsync($"/api/v1/Invoices/{Invoice1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid?>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateInvoice_WithInvalidId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateInvoiceDto();

        var response = await PutAsJsonAsync("/api/v1/Invoices/invalid", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
