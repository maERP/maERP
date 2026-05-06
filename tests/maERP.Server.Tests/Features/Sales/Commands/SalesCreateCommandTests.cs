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

namespace maERP.Server.Tests.Features.Sales.Commands;

public class SalesCreateCommandTests : TenantIsolatedTestBase
{
    private static readonly int Customer1Id = 1;
    private static readonly int Customer2Id = 2;
    private static readonly int Customer3Id = 3;

    private async Task SeedSalesTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Customer.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                // Create additional customers for sales tests
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
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private SalesInputDto CreateValidSalesDto()
    {
        return new SalesInputDto
        {
            SalesId = 12345,
            CustomerId = Customer1Id,
            SalesChannelId = Guid.NewGuid(),
            Status = SalesStatus.Pending,
            PaymentStatus = PaymentStatus.Unknown,
            PaymentMethod = "Credit Card",
            Subtotal = 100.00m,
            ShippingCost = 10.00m,
            TotalTax = 19.00m,
            Total = 129.00m,
            InvoiceAddressFirstName = "John",
            InvoiceAddressLastName = "Doe",
            InvoiceAddressStreet = "123 Test St",
            InvoiceAddressCity = "Test City",
            InvoiceAddressZip = "12345",
            InvoiceAddressCountry = "Germany",
            DeliveryAddressFirstName = "John",
            DeliveryAddressLastName = "Doe",
            DeliveryAddressStreet = "123 Test St",
            DeliveryAddressCity = "Test City",
            DeliveryAddressZip = "12345",
            DeliveryAddressCountry = "Germany",
            DateSalesed = DateTime.UtcNow
        };
    }



    [Fact]
    public async Task CreateSales_WithValidData_ShouldReturnCreated()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateValidSalesDto();

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateSales_WithValidData_ShouldPersistInDatabase()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateValidSalesDto();

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);

        // Verify through API that sales exists
        var getResponse = await Client.GetAsync($"/api/v1/Saless/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual(salesDto.CustomerId, salesDetail!.Data.CustomerId);
        TestAssertions.AssertEqual(salesDto.Total, salesDetail.Data.Total);
        TestAssertions.AssertEqual(salesDto.InvoiceAddressFirstName, salesDetail.Data.InvoiceAddressFirstName);
    }

    [Fact]
    public async Task CreateSales_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = new SalesInputDto
        {
            // Missing required fields like CustomerId
            Status = SalesStatus.Pending,
            Total = 100.00m
        };

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSales_WithInvalidCustomerId_ShouldReturnBadRequest()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateValidSalesDto();
        salesDto.CustomerId = 99999; // Non-existent customer

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSales_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedSalesTestDataAsync();
        RemoveTenantHeader();
        var salesDto = CreateValidSalesDto();

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSales_WithNonExistentTenant_ShouldReturnBadRequest()
    {
        await SeedSalesTestDataAsync();
        SetInvalidTenantHeader();
        var salesDto = CreateValidSalesDto();

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSales_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await SeedSalesTestDataAsync();
        SetInvalidTenantHeaderValue("invalid-guid-format");
        var salesDto = CreateValidSalesDto();

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateSales_WithNegativeTotal_ShouldReturnBadRequest()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateValidSalesDto();
        salesDto.Total = -10.00m;

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSales_WithInvalidSalesStatus_ShouldHandleGracefully()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateValidSalesDto();

        // Create JSON with invalid enum value  
        var json = JsonSerializer.Serialize(salesDto);
        var modifiedJson = json.Replace($"\"Status\":{(int)salesDto.Status}", "\"Status\":999");
        if (modifiedJson == json)
        {
            // Try lowercase
            modifiedJson = json.Replace($"\"status\":{(int)salesDto.Status}", "\"status\":999");
        }

        var content = new StringContent(modifiedJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Saless", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSales_TenantIsolation_ShouldOnlyCreateInCorrectTenant()
    {
        await SeedSalesTestDataAsync();

        var sales1Dto = CreateValidSalesDto();
        sales1Dto.CustomerId = Customer1Id; // Customer from tenant 1
        sales1Dto.InvoiceAddressFirstName = "Tenant1Sales";

        var sales2Dto = CreateValidSalesDto();
        sales2Dto.CustomerId = Customer3Id; // Customer from tenant 2
        sales2Dto.InvoiceAddressFirstName = "Tenant2Sales";

        // Create sales in tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createResponse1 = await PostAsJsonAsync("/api/v1/Saless", sales1Dto);
        TestAssertions.AssertEqual(HttpStatusCode.Created, createResponse1.StatusCode);
        var result1 = await ReadResponseAsync<Result<Guid>>(createResponse1);

        // Create sales in tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var createResponse2 = await PostAsJsonAsync("/api/v1/Saless", sales2Dto);
        TestAssertions.AssertEqual(HttpStatusCode.Created, createResponse2.StatusCode);
        var result2 = await ReadResponseAsync<Result<Guid>>(createResponse2);

        // Verify saless exist in database with correct tenant IDs
        TenantContext.SetCurrentTenantId(null);
        var sales1InDb = await DbContext.Sales.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.Id == result1.Data);
        var sales2InDb = await DbContext.Sales.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.Id == result2.Data);

        TestAssertions.AssertNotNull(sales1InDb);
        TestAssertions.AssertNotNull(sales2InDb);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant1Id, sales1InDb!.TenantId!.Value);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant2Id, sales2InDb!.TenantId!.Value);

        // Verify tenant isolation in API
        using var tenant1Client = Factory.CreateClient();
        tenant1Client.DefaultRequestHeaders.Add("X-Tenant-Id", TenantConstants.TestTenant1Id.ToString());

        var listResponse1 = await tenant1Client.GetAsync("/api/v1/Saless");
        TestAssertions.AssertHttpSuccess(listResponse1);
        var list1 = await ReadResponseAsync<PaginatedResult<SalesListDto>>(listResponse1);
        var tenant1HasSales = list1.Data?.Any(o => o.InvoiceAddressFirstName == "Tenant1Sales") ?? false;
        var tenant1SeesOtherSales = list1.Data?.Any(o => o.InvoiceAddressFirstName == "Tenant2Sales") ?? false;

        TestAssertions.AssertTrue(tenant1HasSales, "Tenant 1 should see its own sales");
        TestAssertions.AssertFalse(tenant1SeesOtherSales, "Tenant 1 should not see Tenant 2's saless");
    }

    [Fact]
    public async Task CreateSales_WithCompleteAddressData_ShouldCreateSuccessfully()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateValidSalesDto();
        salesDto.InvoiceAddressCompanyName = "Test Company";
        salesDto.InvoiceAddressPhone = "+49123456789";
        salesDto.DeliveryAddressCompanyName = "Test Company";
        salesDto.DeliveryAddressPhone = "+49123456789";

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);

        // Verify address data persisted correctly
        var getResponse = await Client.GetAsync($"/api/v1/Saless/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual("Test Company", salesDetail!.Data.InvoiceAddressCompanyName);
        TestAssertions.AssertEqual("+49123456789", salesDetail.Data.InvoiceAddressPhone);
    }

    [Fact]
    public async Task CreateSales_WithPaymentInformation_ShouldCreateSuccessfully()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateValidSalesDto();
        salesDto.PaymentProvider = "Stripe";
        salesDto.PaymentTransactionId = "TXN-12345";
        salesDto.PaymentStatus = PaymentStatus.CompletelyPaid;

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify payment data persisted correctly
        var getResponse = await Client.GetAsync($"/api/v1/Saless/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual("Stripe", salesDetail!.Data.PaymentProvider);
        TestAssertions.AssertEqual("TXN-12345", salesDetail.Data.PaymentTransactionId);
        TestAssertions.AssertEqual(PaymentStatus.CompletelyPaid, salesDetail.Data.PaymentStatus);
    }

    [Fact]
    public async Task CreateSales_WithNotes_ShouldCreateSuccessfully()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateValidSalesDto();
        salesDto.CustomerNote = "Please deliver after 5 PM";
        salesDto.InternalNote = "VIP customer";

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify notes persisted correctly
        var getResponse = await Client.GetAsync($"/api/v1/Saless/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual("Please deliver after 5 PM", salesDetail!.Data.Note);
    }

    [Fact]
    public async Task CreateSales_WithSalesChannelData_ShouldCreateSuccessfully()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateValidSalesDto();
        salesDto.SalesChannelId = Guid.NewGuid();
        salesDto.RemoteSalesId = "EXT-SALES-123";

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify sales channel data persisted correctly
        var getResponse = await Client.GetAsync($"/api/v1/Saless/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesDetail = await ReadResponseAsync<Result<SalesDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesDetail?.Data);
        TestAssertions.AssertEqual<Guid?>(salesDto.SalesChannelId, salesDetail!.Data.SalesChannelId);
        TestAssertions.AssertEqual("EXT-SALES-123", salesDetail.Data.RemoteSalesId);
    }

    [Fact]
    public async Task CreateSales_WithMinimalRequiredData_ShouldCreateSuccessfully()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = new SalesInputDto
        {
            CustomerId = Customer1Id,
            SalesChannelId = Guid.NewGuid(),
            Status = SalesStatus.Pending,
            PaymentStatus = PaymentStatus.Unknown,
            Subtotal = 40.00m,
            ShippingCost = 5.00m,
            TotalTax = 5.00m,
            Total = 50.00m,
            InvoiceAddressFirstName = "John",
            InvoiceAddressLastName = "Doe",
            InvoiceAddressStreet = "123 Test St",
            InvoiceAddressCity = "Test City",
            InvoiceAddressZip = "12345",
            InvoiceAddressCountry = "Germany",
            DeliveryAddressFirstName = "John",
            DeliveryAddressLastName = "Doe",
            DeliveryAddressStreet = "123 Test St",
            DeliveryAddressCity = "Test City",
            DeliveryAddressZip = "12345",
            DeliveryAddressCountry = "Germany",
            DateSalesed = DateTime.UtcNow
        };

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateSales_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateValidSalesDto();

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task CreateSales_WithInvalidJson_ShouldReturnBadRequest()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Saless", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSales_WithEmptyBody_ShouldReturnBadRequest()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Saless", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSales_WithCrossTenantCustomer_ShouldReturnBadRequest()
    {
        await SeedSalesTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesDto = CreateValidSalesDto();
        salesDto.CustomerId = Customer3Id; // Customer belongs to tenant 2, but we're on tenant 1

        var response = await PostAsJsonAsync("/api/v1/Saless", salesDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }


}