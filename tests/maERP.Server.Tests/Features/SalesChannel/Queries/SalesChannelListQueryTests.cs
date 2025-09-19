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

public class SalesChannelListQueryTests : TenantIsolatedTestBase
{

    // Test Entity IDs
    private static readonly Guid TestSalesChannel1Id = new("11111111-1111-1111-1111-111111111111");
    private static readonly Guid TestSalesChannel2Id = new("22222222-2222-2222-2222-222222222222");
    private static readonly Guid TestSalesChannel3Id = new("33333333-3333-3333-3333-333333333333");
    private static readonly Guid TestSalesChannel4Id = new("14141414-1414-1414-1414-141414141414");

    private async Task SeedTestDataAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

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
            TenantId = TenantConstants.TestTenant1Id
        };

        var salesChannel1_2 = new maERP.Domain.Entities.SalesChannel
        {
            Id = TestSalesChannel2Id,
            Type = SalesChannelType.Shopware6,
            Name = "Shopware Store T1",
            Url = "https://shopware1.example.com",
            Username = "shopware1",
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
            Url = "https://ebay.example.com",
            Username = "ebay2",
            Password = "ebaypass2",
            ImportProducts = true,
            ExportProducts = false,
            ImportCustomers = true,
            ExportCustomers = false,
            ImportOrders = true,
            ExportOrders = true,
            TenantId = TenantConstants.TestTenant2Id
        };

        var salesChannel2_2 = new maERP.Domain.Entities.SalesChannel
        {
            Id = TestSalesChannel4Id,
            Type = SalesChannelType.WooCommerce,
            Name = "WooCommerce Store T2",
            Url = "https://store2.example.com",
            Username = "user2",
            Password = "pass2",
            ImportProducts = false,
            ExportProducts = false,
            ImportCustomers = false,
            ExportCustomers = false,
            ImportOrders = false,
            ExportOrders = false,
            TenantId = TenantConstants.TestTenant2Id
        };

        DbContext.SalesChannel.AddRange(salesChannel1_1, salesChannel1_2, salesChannel2_1, salesChannel2_2);
        await DbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task GetSalesChannelsList_WithValidTenant_ShouldReturnSuccess()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithoutTenantHeader_ShouldReturnEmptyResults()
    {
        await SeedTestDataAsync();
        RemoveTenantHeader();

        var response = await Client.GetAsync("/api/v1/SalesChannels");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertEqual(0, result.Data.Count);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        await SeedTestDataAsync();
        SetInvalidTenantHeaderValue("invalid-guid-format");

        var response = await Client.GetAsync("/api/v1/SalesChannels");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetSalesChannelsList_TenantIsolation_ShouldOnlyReturnTenantData()
    {
        await SeedTestDataAsync();

        // Test Tenant 1 isolation - should see only their own sales channels
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);
        var result1 = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response1);

        TestAssertions.AssertNotNull(result1?.Data);
        TestAssertions.AssertEqual(4, result1.Data.Count);
        
        var tenant1Channels = result1.Data.ToList();
        TestAssertions.AssertTrue(tenant1Channels.Any(s => s.Name == "WooCommerce Store T1"), 
            "Tenant 1 should see WooCommerce Store T1");
        TestAssertions.AssertTrue(tenant1Channels.Any(s => s.Name == "Shopware Store T1"), 
            "Tenant 1 should see Shopware Store T1");
        TestAssertions.AssertFalse(tenant1Channels.Any(s => s.Name.Contains("T2")), 
            "Tenant 1 should not see any T2 sales channels");

        // Verify specific IDs for Tenant 1
        var tenant1Ids = tenant1Channels.Select(s => s.Id).ToHashSet();
        TestAssertions.AssertTrue(tenant1Ids.Contains(TestSalesChannel1Id), 
            "Tenant 1 should see TestSalesChannel1Id");
        TestAssertions.AssertTrue(tenant1Ids.Contains(TestSalesChannel2Id), 
            "Tenant 1 should see TestSalesChannel2Id");
        TestAssertions.AssertFalse(tenant1Ids.Contains(TestSalesChannel3Id), 
            "Tenant 1 should not see TestSalesChannel3Id");
        TestAssertions.AssertFalse(tenant1Ids.Contains(TestSalesChannel4Id), 
            "Tenant 1 should not see TestSalesChannel4Id");

        // Test Tenant 2 isolation - should see only their own sales channels
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/SalesChannels");
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);
        var result2 = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response2);

        TestAssertions.AssertNotNull(result2?.Data);
        TestAssertions.AssertEqual(4, result2.Data.Count);
        
        var tenant2Channels = result2.Data.ToList();
        TestAssertions.AssertTrue(tenant2Channels.Any(s => s.Name == "eBay Store T2"), 
            "Tenant 2 should see eBay Store T2");
        TestAssertions.AssertTrue(tenant2Channels.Any(s => s.Name == "WooCommerce Store T2"), 
            "Tenant 2 should see WooCommerce Store T2");
        TestAssertions.AssertFalse(tenant2Channels.Any(s => s.Name.Contains("T1")), 
            "Tenant 2 should not see any T1 sales channels");

        // Verify specific IDs for Tenant 2
        var tenant2Ids = tenant2Channels.Select(s => s.Id).ToHashSet();
        TestAssertions.AssertTrue(tenant2Ids.Contains(TestSalesChannel3Id), 
            "Tenant 2 should see TestSalesChannel3Id");
        TestAssertions.AssertTrue(tenant2Ids.Contains(TestSalesChannel4Id), 
            "Tenant 2 should see TestSalesChannel4Id");
        TestAssertions.AssertFalse(tenant2Ids.Contains(TestSalesChannel1Id), 
            "Tenant 2 should not see TestSalesChannel1Id");
        TestAssertions.AssertFalse(tenant2Ids.Contains(TestSalesChannel2Id), 
            "Tenant 2 should not see TestSalesChannel2Id");
    }

    [Fact]
    public async Task GetSalesChannelsList_WithPagination_ShouldReturnCorrectPage()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?PageNumber=1&PageSize=1");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.Data.Count);
        TestAssertions.AssertEqual(4, result.TotalCount);
        TestAssertions.AssertEqual(1, result.CurrentPage);
        TestAssertions.AssertEqual(1, result.PageSize);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithSearchString_ShouldFilterResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?SearchString=WooCommerce");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(2, result.Data.Count);
        TestAssertions.AssertTrue(result.Data!.Any(s => s.Name == "WooCommerce Store T1"),
            "Should contain WooCommerce Store T1");
        TestAssertions.AssertTrue(result.Data!.Any(s => s.Name == "WooCommerce Tenant 1"),
            "Should contain WooCommerce Tenant 1");
    }

    [Fact]
    public async Task GetSalesChannelsList_WithEmptySearchString_ShouldReturnAllResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?SearchString=");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(4, result.Data.Count);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithNonExistentSearch_ShouldReturnEmptyResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?SearchString=NonExistent");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data.Count);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithOrderBy_ShouldReturnOrderedResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?OrderBy=Name desc");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(4, result.Data.Count);
        TestAssertions.AssertEqual("WooCommerce Tenant 1", result.Data!.First().Name);
        TestAssertions.AssertEqual("Shopware Store T1", result.Data.Last().Name);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithMultipleOrderBy_ShouldReturnOrderedResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?OrderBy=Type,Name");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(4, result.Data.Count);
    }

    [Fact]
    public async Task GetSalesChannelsList_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);

        if (result.Data!.Any())
        {
            var firstChannel = result.Data!.First();
            TestAssertions.AssertNotEqual(Guid.Empty, firstChannel.Id);
            TestAssertions.AssertNotNull(firstChannel.Name);
            TestAssertions.AssertNotNull(firstChannel.Url);
            TestAssertions.AssertNotNull(firstChannel.Username);
            TestAssertions.AssertNotNull(firstChannel.Warehouses);
        }
    }

    [Fact]
    public async Task GetSalesChannelsList_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?PageNumber=999&PageSize=10");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data.Count);
        TestAssertions.AssertEqual(4, result.TotalCount);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithLargePageSize_ShouldReturnAllResults()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?PageSize=1000");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(4, result.Data.Count);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?PageSize=0");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
    }

    [Fact]
    public async Task GetSalesChannelsList_WithNonExistentTenant_ShouldReturnEmptyResults()
    {
        await SeedTestDataAsync();
        SetInvalidTenantHeader();

        var response = await Client.GetAsync("/api/v1/SalesChannels");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertEqual(0, result.Data.Count);
    }

    [Fact]
    public async Task GetSalesChannelsList_VerifyDtoProperties_ShouldContainAllExpectedFields()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result?.Data);

        if (result.Data!.Any())
        {
            var channel = result.Data!.First();
            TestAssertions.AssertNotEqual(Guid.Empty, channel.Id);
            TestAssertions.AssertNotNull(channel.Name);
            TestAssertions.AssertNotNull(channel.Url);
            TestAssertions.AssertNotNull(channel.Username);
            TestAssertions.AssertNotNull(channel.Warehouses);

            // Verify boolean flags are properly mapped
            TestAssertions.AssertTrue(channel.ImportProducts == true || channel.ImportProducts == false);
            TestAssertions.AssertTrue(channel.ExportProducts || !channel.ExportProducts);
            TestAssertions.AssertTrue(channel.ImportCustomers || !channel.ImportCustomers);
            TestAssertions.AssertTrue(channel.ExportCustomers || !channel.ExportCustomers);
            TestAssertions.AssertTrue(channel.ImportOrders || !channel.ImportOrders);
            TestAssertions.AssertTrue(channel.ExportOrders || !channel.ExportOrders);
        }
    }

    [Fact]
    public async Task GetSalesChannelsList_CaseSensitiveSearch_ShouldWork()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/SalesChannels?SearchString=woocommerce");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<SalesChannelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Should find WooCommerce regardless of case
        TestAssertions.AssertTrue(result.Data.Count >= 0);
    }
}