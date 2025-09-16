#nullable disable
using System.Net;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Enums;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.SalesChannel.Queries;

public class SalesChannelDetailQueryTests : TenantIsolatedTestBase
{
    // Test Entity IDs
    private static readonly Guid TestSalesChannel1Id = new("11111111-1111-1111-1111-111111111111");
    private static readonly Guid TestSalesChannel2Id = new("22222222-2222-2222-2222-222222222222");
    private static readonly Guid TestSalesChannel3Id = new("33333333-3333-3333-3333-333333333333");

    private async Task SeedTestDataAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            // Check if test sales channels are already seeded to avoid duplicates
            var existingSalesChannel = await DbContext.SalesChannel.IgnoreQueryFilters()
                .FirstOrDefaultAsync(sc => sc.Id == TestSalesChannel1Id);

            if (existingSalesChannel == null)
            {

                // Get existing warehouses created by TestDataSeeder (they should exist)
                var warehouse1 = await DbContext.Warehouse.IgnoreQueryFilters()
                    .FirstAsync(w => w.TenantId == TenantConstants.TestTenant1Id);
                var warehouse2 = await DbContext.Warehouse.IgnoreQueryFilters()
                    .Skip(1).FirstAsync(w => w.TenantId == TenantConstants.TestTenant1Id);
                var warehouse3 = await DbContext.Warehouse.IgnoreQueryFilters()
                    .FirstAsync(w => w.TenantId == TenantConstants.TestTenant2Id);

                // Seed SalesChannels for Tenant 1
                var salesChannel1_1 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel1Id,
                    Type = SalesChannelType.WooCommerce,
                    Name = "WooCommerce Store T1",
                    Url = "https://store1.example.com",
                    Username = "user1",
                    Password = "pass1",
                    ImportProducts = true,
                    ExportProducts = true,
                    ImportCustomers = false,
                    ExportCustomers = false,
                    ImportOrders = true,
                    ExportOrders = false,
                    TenantId = TenantConstants.TestTenant1Id,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse1, warehouse2 }
                };

                var salesChannel1_2 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel2Id,
                    Type = SalesChannelType.Shopware6,
                    Name = "Shopify Store T1",
                    Url = "https://shopify1.example.com",
                    Username = "shopify1",
                    Password = "shoppass1",
                    ImportProducts = false,
                    ExportProducts = true,
                    ImportCustomers = true,
                    ExportCustomers = true,
                    ImportOrders = false,
                    ExportOrders = true,
                    TenantId = TenantConstants.TestTenant1Id
                };

                // Seed SalesChannels for Tenant 2
                var salesChannel2_1 = new maERP.Domain.Entities.SalesChannel
                {
                    Id = TestSalesChannel3Id,
                    Type = SalesChannelType.eBay,
                    Name = "eBay Store T2",
                    Url = "https://magento.example.com",
                    Username = "magento2",
                    Password = "magpass2",
                    ImportProducts = true,
                    ExportProducts = false,
                    ImportCustomers = true,
                    ExportCustomers = false,
                    ImportOrders = true,
                    ExportOrders = true,
                    TenantId = TenantConstants.TestTenant2Id,
                    Warehouses = new List<maERP.Domain.Entities.Warehouse> { warehouse3 }
                };

                DbContext.SalesChannel.AddRange(salesChannel1_1, salesChannel1_2, salesChannel2_1);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithValidIdAndTenant_ShouldReturnSuccess()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(TestSalesChannel1Id, result.Data.Id);
        TestAssertions.AssertEqual("WooCommerce Store T1", result.Data.Name);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        RemoveTenantHeader();

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();
        SetInvalidTenantHeaderValue("invalid-guid-format");

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_TenantIsolation_ShouldOnlyReturnOwnTenantData()
    {
        await SeedTestDataAsync();

        // Test Tenant 1 can access its own data
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var ownDataResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, ownDataResponse.StatusCode);
        var ownDataResult = await ReadResponseAsync<Result<SalesChannelDetailDto>>(ownDataResponse);
        TestAssertions.AssertNotNull(ownDataResult?.Data);
        TestAssertions.AssertEqual("WooCommerce Store T1", ownDataResult.Data.Name);
        TestAssertions.AssertEqual(TestSalesChannel1Id, ownDataResult.Data.Id);

        // Test Tenant 1 cannot access Tenant 2's data - should return NotFound
        var crossTenantResponse1 = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, crossTenantResponse1.StatusCode);

        // Test Tenant 2 can access its own data
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var ownDataResponse2 = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, ownDataResponse2.StatusCode);
        var ownDataResult2 = await ReadResponseAsync<Result<SalesChannelDetailDto>>(ownDataResponse2);
        TestAssertions.AssertNotNull(ownDataResult2?.Data);
        TestAssertions.AssertEqual("eBay Store T2", ownDataResult2.Data.Name);
        TestAssertions.AssertEqual(TestSalesChannel3Id, ownDataResult2.Data.Id);

        // Test Tenant 2 cannot access Tenant 1's data - should return NotFound
        var crossTenantResponse2 = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, crossTenantResponse2.StatusCode);
        
        // Additional verification: Tenant 2 cannot access Tenant 1's other sales channel
        var crossTenantResponse3 = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, crossTenantResponse3.StatusCode);

        // Verify tenant 1 can access their other sales channel
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var ownDataResponse3 = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, ownDataResponse3.StatusCode);
        var ownDataResult3 = await ReadResponseAsync<Result<SalesChannelDetailDto>>(ownDataResponse3);
        TestAssertions.AssertNotNull(ownDataResult3?.Data);
        TestAssertions.AssertEqual("Shopify Store T1", ownDataResult3.Data.Name);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithInvalidId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithZeroId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{Guid.Empty}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithNegativeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels/invalid-guid");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_VerifyDtoProperties_ShouldContainAllExpectedFields()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);

        var channel = result.Data!;
        TestAssertions.AssertEqual(TestSalesChannel1Id, channel.Id);
        TestAssertions.AssertEqual(SalesChannelType.WooCommerce, channel.SalesChannelType);
        TestAssertions.AssertEqual("WooCommerce Store T1", channel.Name);
        TestAssertions.AssertEqual("https://store1.example.com", channel.Url);
        TestAssertions.AssertEqual("user1", channel.Username);
        TestAssertions.AssertEqual("pass1", channel.Password);
        TestAssertions.AssertTrue(channel.ImportProducts);
        TestAssertions.AssertTrue(channel.ExportProducts);
        TestAssertions.AssertFalse(channel.ImportCustomers);
        TestAssertions.AssertFalse(channel.ExportCustomers);
        TestAssertions.AssertTrue(channel.ImportOrders);
        TestAssertions.AssertFalse(channel.ExportOrders);
        TestAssertions.AssertNotNull(channel.Warehouses);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithWarehouses_ShouldIncludeWarehouseData()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);

        var channel = result.Data!;
        TestAssertions.AssertNotNull(channel.Warehouses);
        TestAssertions.AssertEqual(2, channel.Warehouses.Count);
        TestAssertions.AssertTrue(channel.Warehouses.Any(w => w.Name == "Main Warehouse Tenant 1"));
        TestAssertions.AssertTrue(channel.Warehouses.Any(w => w.Name == "Secondary Warehouse Tenant 1"));
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithoutWarehouses_ShouldReturnEmptyWarehouseList()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel2Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);

        var channel = result.Data!;
        TestAssertions.AssertNotNull(channel.Warehouses);
        TestAssertions.AssertEqual(0, channel.Warehouses.Count);
    }

    [Fact]
    public async Task GetSalesChannelDetail_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetInvalidTenantHeader();

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelDetail_VerifyPasswordIncluded_ShouldContainSensitiveData()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);

        // DetailDto should include password (unlike ListDto)
        TestAssertions.AssertEqual("pass1", result.Data!.Password);
    }

    [Fact]
    public async Task GetSalesChannelDetail_VerifyAllSalesChannelTypes_ShouldHandleDifferentTypes()
    {
        await SeedTestDataAsync();

        // Test WooCommerce
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var wooResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, wooResponse.StatusCode);
        var wooResult = await ReadResponseAsync<Result<SalesChannelDetailDto>>(wooResponse);
        TestAssertions.AssertEqual(SalesChannelType.WooCommerce, wooResult?.Data?.SalesChannelType);

        // Test Shopware6
        var shopifyResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, shopifyResponse.StatusCode);
        var shopifyResult = await ReadResponseAsync<Result<SalesChannelDetailDto>>(shopifyResponse);
        TestAssertions.AssertEqual(SalesChannelType.Shopware6, shopifyResult?.Data?.SalesChannelType);

        // Test eBay
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var magentoResponse = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel3Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, magentoResponse.StatusCode);
        var magentoResult = await ReadResponseAsync<Result<SalesChannelDetailDto>>(magentoResponse);
        TestAssertions.AssertEqual(SalesChannelType.eBay, magentoResult?.Data?.SalesChannelType);
    }

    [Fact]
    public async Task GetSalesChannelDetail_VerifyBooleanFlags_ShouldHaveCorrectValues()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Test first sales channel with specific flag configuration
        var response = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel1Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);

        var channel = result.Data!;
        TestAssertions.AssertTrue(channel.ImportProducts);
        TestAssertions.AssertTrue(channel.ExportProducts);
        TestAssertions.AssertFalse(channel.ImportCustomers);
        TestAssertions.AssertFalse(channel.ExportCustomers);
        TestAssertions.AssertTrue(channel.ImportOrders);
        TestAssertions.AssertFalse(channel.ExportOrders);

        // Test second sales channel with different flag configuration
        var response2 = await Client.GetAsync($"/api/v1/SalesChannels/{TestSalesChannel2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);
        var result2 = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response2);

        var channel2 = result2.Data!;
        TestAssertions.AssertFalse(channel2.ImportProducts);
        TestAssertions.AssertTrue(channel2.ExportProducts);
        TestAssertions.AssertTrue(channel2.ImportCustomers);
        TestAssertions.AssertTrue(channel2.ExportCustomers);
        TestAssertions.AssertFalse(channel2.ImportOrders);
        TestAssertions.AssertTrue(channel2.ExportOrders);
    }

    [Fact]
    public async Task GetSalesChannelDetail_ErrorHandling_ShouldReturnProperErrorFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/SalesChannels/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SalesChannelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.NotFound, result.StatusCode);
        TestAssertions.AssertNotEmpty(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }
}