#nullable disable
using System.Net;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Enums;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.SalesChannel.Commands;

public class SalesChannelCreateCommandTests : TenantIsolatedTestBase
{
    // Test Warehouse IDs
    private static readonly Guid TestWarehouse1Id = new("11111111-1111-1111-1111-111111111111");
    private static readonly Guid TestWarehouse2Id = new("22222222-2222-2222-2222-222222222222");
    private static readonly Guid TestWarehouse3Id = new("33333333-3333-3333-3333-333333333333");

    private async Task SeedTestDataAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            // Check if warehouses are already seeded to avoid duplicates
            var existingWarehouse = await DbContext.Warehouse.IgnoreQueryFilters()
                .FirstOrDefaultAsync(w => w.Id == TestWarehouse1Id);

            if (existingWarehouse == null)
            {
                // Seed additional Warehouses for testing
                var warehouse1 = new maERP.Domain.Entities.Warehouse
                {
                    Id = TestWarehouse1Id,
                    Name = "Warehouse T1-1",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var warehouse2 = new maERP.Domain.Entities.Warehouse
                {
                    Id = TestWarehouse2Id,
                    Name = "Warehouse T1-2",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var warehouse3 = new maERP.Domain.Entities.Warehouse
                {
                    Id = TestWarehouse3Id,
                    Name = "Warehouse T2-1",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Warehouse.AddRange(warehouse1, warehouse2, warehouse3);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private SalesChannelInputDto CreateValidSalesChannelDto()
    {
        return new SalesChannelInputDto
        {
            SalesChannelType = SalesChannelType.WooCommerce,
            Name = "Test WooCommerce Store",
            Url = "https://test-store.example.com",
            Username = "testuser",
            Password = "testpassword123",
            ImportProducts = true,
            ExportProducts = false,
            ImportCustomers = true,
            ExportCustomers = false,
            ImportOrders = true,
            ExportOrders = true,
            WarehouseIds = new List<Guid> { TestWarehouse1Id, TestWarehouse2Id }
        };
    }



    [Fact]
    public async Task CreateSalesChannel_WithValidData_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateSalesChannel_WithValidData_ShouldPersistInDatabase()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);

        // Verify through API that sales channel exists
        var getResponse = await Client.GetAsync($"/api/v1/SalesChannels/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);
        TestAssertions.AssertEqual(salesChannelDto.Name, salesChannelDetail.Data.Name);
        TestAssertions.AssertEqual(salesChannelDto.Url, salesChannelDetail.Data.Url);
        TestAssertions.AssertEqual(salesChannelDto.Username, salesChannelDetail.Data.Username);
    }

    [Fact]
    public async Task CreateSalesChannel_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = new SalesChannelInputDto
        {
            // Missing required fields: Name, SalesChannelType
            Url = "https://incomplete.example.com"
        };

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSalesChannel_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        RemoveTenantHeader();
        var salesChannelDto = CreateValidSalesChannelDto();

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSalesChannel_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();
        SetInvalidTenantHeaderValue("invalid-guid-format");
        var salesChannelDto = CreateValidSalesChannelDto();

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateSalesChannel_WithNonExistentTenant_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetInvalidTenantHeader();
        var salesChannelDto = CreateValidSalesChannelDto();

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSalesChannel_WithInvalidWarehouseIds_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.WarehouseIds = new List<Guid> { Guid.NewGuid() }; // Non-existent warehouse

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSalesChannel_WithCrossTenantWarehouseIds_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.WarehouseIds = new List<Guid> { TestWarehouse3Id }; // Warehouse belongs to tenant 2

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSalesChannel_WithTooLongName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.Name = new string('A', 101); // Exceeds 100 character limit

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(responseContent.Contains("validation errors") || responseContent.Contains("Name"));
    }

    [Fact]
    public async Task CreateSalesChannel_WithInvalidUrl_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.Url = "invalid-url";

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSalesChannel_WithDuplicateName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Create first sales channel
        var firstSalesChannel = CreateValidSalesChannelDto();
        firstSalesChannel.Name = "Unique Store Name";
        await PostAsJsonAsync("/api/v1/SalesChannels", firstSalesChannel);

        // Try to create second sales channel with same name
        var duplicateSalesChannel = CreateValidSalesChannelDto();
        duplicateSalesChannel.Name = "Unique Store Name";
        duplicateSalesChannel.Url = "https://different-url.com";

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", duplicateSalesChannel);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSalesChannel_TenantIsolation_ShouldOnlyCreateInCorrectTenant()
    {
        await SeedTestDataAsync();

        var uniqueId1 = Guid.NewGuid().ToString("N")[..8];
        var uniqueId2 = Guid.NewGuid().ToString("N")[..8];

        // Create sales channels for each tenant with unique names
        var salesChannel1Dto = CreateValidSalesChannelDto();
        salesChannel1Dto.Name = $"Store-T1-{uniqueId1}";
        salesChannel1Dto.WarehouseIds = new List<Guid> { TestWarehouse1Id };

        var salesChannel2Dto = CreateValidSalesChannelDto();
        salesChannel2Dto.Name = $"Store-T2-{uniqueId2}";
        salesChannel2Dto.WarehouseIds = new List<Guid> { TestWarehouse3Id };

        // Create sales channel in tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createResponse1 = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannel1Dto);
        TestAssertions.AssertEqual(HttpStatusCode.Created, createResponse1.StatusCode);
        var result1 = await ReadResponseAsync<Result<Guid>>(createResponse1);
        var salesChannel1Id = result1.Data;

        // Create sales channel in tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var createResponse2 = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannel2Dto);
        TestAssertions.AssertEqual(HttpStatusCode.Created, createResponse2.StatusCode);
        var result2 = await ReadResponseAsync<Result<Guid>>(createResponse2);
        var salesChannel2Id = result2.Data;

        // Verify tenant isolation - Tenant 1 can only see their sales channel
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var listResponse1 = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertHttpSuccess(listResponse1);
        var list1 = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(listResponse1);
        
        var tenant1SalesChannels = list1.Data ?? new List<SalesChannelListDto>();
        var tenant1SeesTenant2Data = tenant1SalesChannels.Any(s => s.Name == salesChannel2Dto.Name);
        var tenant1SeesOwnData = tenant1SalesChannels.Any(s => s.Name == salesChannel1Dto.Name);

        TestAssertions.AssertFalse(tenant1SeesTenant2Data, "Tenant 1 should not see Tenant 2's sales channels");
        TestAssertions.AssertTrue(tenant1SeesOwnData, "Tenant 1 should see their own sales channels");

        // Verify tenant isolation - Tenant 2 can only see their sales channel
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var listResponse2 = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertHttpSuccess(listResponse2);
        var list2 = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(listResponse2);
        
        var tenant2SalesChannels = list2.Data ?? new List<SalesChannelListDto>();
        var tenant2SeesTenant1Data = tenant2SalesChannels.Any(s => s.Name == salesChannel1Dto.Name);
        var tenant2SeesOwnData = tenant2SalesChannels.Any(s => s.Name == salesChannel2Dto.Name);

        TestAssertions.AssertFalse(tenant2SeesTenant1Data, "Tenant 2 should not see Tenant 1's sales channels");
        TestAssertions.AssertTrue(tenant2SeesOwnData, "Tenant 2 should see their own sales channels");

        // Verify cross-tenant access prevention via direct API calls
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var crossTenantResponse = await Client.GetAsync($"/api/v1/SalesChannels/{salesChannel2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, crossTenantResponse.StatusCode);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var crossTenantResponse2 = await Client.GetAsync($"/api/v1/SalesChannels/{salesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, crossTenantResponse2.StatusCode);
    }

    [Fact]
    public async Task CreateSalesChannel_WithAllSalesChannelTypes_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var salesChannelTypes = new[]
        {
            SalesChannelType.WooCommerce,
            SalesChannelType.Shopware5,
            SalesChannelType.Shopware6,
            SalesChannelType.eBay,
            SalesChannelType.PointOfSale
        };

        foreach (var type in salesChannelTypes)
        {
            var salesChannelDto = CreateValidSalesChannelDto();
            salesChannelDto.SalesChannelType = type;
            salesChannelDto.Name = $"Test {type} Store";

            var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

            TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
            var result = await ReadResponseAsync<Result<Guid>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
        }
    }

    [Fact]
    public async Task CreateSalesChannel_WithMinimalRequiredData_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = new SalesChannelInputDto
        {
            SalesChannelType = SalesChannelType.PointOfSale,
            Name = "Minimal Sales Channel",
            Url = "https://minimal.example.com",
            Username = "minimal",
            Password = "password"
        };

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateSalesChannel_WithMaxValidStringLengths_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.Name = new string('N', 100); // Max allowed length (100 characters as per validator)
        salesChannelDto.Url = "https://test-max-length-store.example.com"; // Valid URL format
        salesChannelDto.Username = "testuser";
        salesChannelDto.Password = "testpassword123";

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateSalesChannel_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task CreateSalesChannel_WithInvalidJson_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/SalesChannels", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSalesChannel_WithEmptyBody_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/SalesChannels", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSalesChannel_WithEmptyWarehouseIds_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.WarehouseIds = new List<Guid>(); // Empty list should be allowed

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify no warehouses are associated
        var getResponse = await Client.GetAsync($"/api/v1/SalesChannels/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);
        TestAssertions.AssertEqual(0, salesChannelDetail.Data.Warehouses.Count);
    }

    [Fact]
    public async Task CreateSalesChannel_WithBooleanFlags_ShouldPersistCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var salesChannelDto = CreateValidSalesChannelDto();
        salesChannelDto.ImportProducts = false;
        salesChannelDto.ExportProducts = true;
        salesChannelDto.ImportCustomers = false;
        salesChannelDto.ExportCustomers = true;
        salesChannelDto.ImportOrders = false;
        salesChannelDto.ExportOrders = true;

        var response = await PostAsJsonAsync("/api/v1/SalesChannels", salesChannelDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify boolean flags are persisted correctly
        var getResponse = await Client.GetAsync($"/api/v1/SalesChannels/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var salesChannelDetail = await ReadResponseAsync<Result<SalesChannelDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(salesChannelDetail?.Data);

        TestAssertions.AssertFalse(salesChannelDetail.Data.ImportProducts);
        TestAssertions.AssertTrue(salesChannelDetail.Data.ExportProducts);
        TestAssertions.AssertFalse(salesChannelDetail.Data.ImportCustomers);
        TestAssertions.AssertTrue(salesChannelDetail.Data.ExportCustomers);
        TestAssertions.AssertFalse(salesChannelDetail.Data.ImportOrders);
        TestAssertions.AssertTrue(salesChannelDetail.Data.ExportOrders);
    }
}