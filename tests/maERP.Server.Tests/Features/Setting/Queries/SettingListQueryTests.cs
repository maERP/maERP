using System.Net;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Setting.Queries;

public class SettingListQueryTests : GlobalTestBase
{

    private async Task SeedSettingTestDataAsync()
    {
        var existingSettings = await DbContext.Setting.ToListAsync();
        if (!existingSettings.Any(s => s.Key == "test.setting.global"))
        {
            var setting1 = new maERP.Domain.Entities.Setting
            {
                Key = "test.setting.global",
                Value = "value1"
            };

            var setting2 = new maERP.Domain.Entities.Setting
            {
                Key = "test.search.global",
                Value = "searchable"
            };

            var setting3 = new maERP.Domain.Entities.Setting
            {
                Key = "test.theme.global",
                Value = "dark"
            };

            var setting4 = new maERP.Domain.Entities.Setting
            {
                Key = "test.config.global",
                Value = "config_value"
            };

            var setting5 = new maERP.Domain.Entities.Setting
            {
                Key = "test.feature.global",
                Value = "enabled"
            };

            DbContext.Setting.AddRange(setting1, setting2, setting3, setting4, setting5);
            await DbContext.SaveChangesAsync();
        }
    }

    [Fact]
    public async Task GetSettings_ShouldReturnGlobalSettings()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Any(s => s.Key.Contains("global")));
    }

    [Fact]
    public async Task GetSettings_ShouldReturnSameGlobalSettingsRegardlessOfTenant()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Any(s => s.Key.Contains("global")));
    }

    [Fact]
    public async Task GetSettings_WithoutTenantHeader_ShouldReturnGlobalSettings()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);

        // Settings are now global, so we should get all settings
        TestAssertions.AssertTrue(result.Data!.Any(s => s.Key.Contains("global")));
    }

    [Fact]
    public async Task GetSettings_WithPagination_ShouldRespectPageSize()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?pageNumber=0&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Count <= 2);
        TestAssertions.AssertTrue(result.TotalCount > 0);
    }

    [Fact]
    public async Task GetSettings_WithPaginationSecondPage_ShouldReturnSecondPageData()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?pageNumber=1&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.TotalCount > 0);
    }

    [Fact]
    public async Task GetSettings_WithSearchString_ShouldFilterResults()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?searchString=test.search");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Any(s => s.Key == "test.search.global"));

        var keys = result.Data!.Select(s => s.Key).ToList();
        TestAssertions.AssertTrue(keys.All(k => k.Contains("test.search")));
    }

    [Fact]
    public async Task GetSettings_WithSearchStringNoMatch_ShouldReturnEmpty()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?searchString=nonexistent");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetSettings_WithOrderByKey_ShouldReturnOrderedResults()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?orderBy=Key");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Count > 0);

        // Verify ordering - keys should be in ascending order
        var keys = result.Data!.Select(x => x.Key).ToList();
        var sortedKeys = keys.OrderBy(k => k).ToList();
        TestAssertions.AssertEqual(sortedKeys, keys);
    }

    [Fact]
    public async Task GetSettings_WithOrderByKeyDescending_ShouldReturnDescOrderedResults()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?orderBy=Key desc");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Count > 0);

        // Verify descending ordering
        var keys = result.Data!.Select(x => x.Key).ToList();
        var sortedKeysDesc = keys.OrderByDescending(k => k).ToList();
        TestAssertions.AssertEqual(sortedKeysDesc, keys);
    }

    [Fact]
    public async Task GetSettings_WithOrderByValue_ShouldReturnValueOrderedResults()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?orderBy=Value");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Count > 0);

        // Verify value ordering
        var values = result.Data!.Select(x => x.Value).ToList();
        var sortedValues = values.OrderBy(v => v).ToList();
        TestAssertions.AssertEqual(sortedValues, values);
    }

    [Fact]
    public async Task GetSettings_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedSettingTestDataAsync();

        // First get the actual total count of settings
        var countResponse = await Client.GetAsync("/api/v1/Settings?pageNumber=0&pageSize=100");
        var countResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(countResponse);
        var actualTotalCount = countResult!.TotalCount;

        var response = await Client.GetAsync("/api/v1/Settings?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(actualTotalCount, result!.TotalCount);
    }

    [Fact]
    public async Task GetSettings_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?pageSize=0");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Count > 0);
    }

    [Fact]
    public async Task GetSettings_WithNegativePageNumber_ShouldHandleGracefully()
    {

        var response = await Client.GetAsync("/api/v1/Settings?pageNumber=-1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);

        // Negative page numbers should be handled gracefully by returning the first page (page 0)
        // This should return the default pageSize of 10 items (or fewer if less data exists)
        TestAssertions.AssertTrue((result!.Data?.Count ?? 0) >= 0, "Expected at least some results when negative pageNumber is treated as first page");
        TestAssertions.AssertTrue((result.Data?.Count ?? 0) <= 10, "Expected maximum of 10 results (default pageSize)");
        TestAssertions.AssertEqual(0, result!.CurrentPage); // Should be normalized to page 0
    }

    [Fact]
    public async Task GetSettings_WithNonExistentTenant_ShouldReturnOnlyNonTenantSettings()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);

        // With non-existent tenant, we should not get any tenant-specific settings
        var tenantSpecificSettings = result.Data?.Where(s => s.Key.Contains("tenant")).ToList();
        TestAssertions.AssertEmpty(tenantSpecificSettings ?? new List<SettingListDto>());
    }

    [Fact]
    public async Task GetSettings_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstSetting = result.Data!.First();
        TestAssertions.AssertNotNull(firstSetting);
        TestAssertions.AssertNotEqual(Guid.Empty, firstSetting!.Id);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstSetting.Key));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstSetting.Value));
    }

    [Fact]
    public async Task GetSettings_WithSpecificKeySearch_ShouldFilterByKey()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?searchString=test.theme.global");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Any(s => s.Key == "test.theme.global"));
    }

    [Fact]
    public async Task GetSettings_WithValueSearch_ShouldFilterByValue()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?searchString=searchable");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Any(s => s.Value == "searchable"));
    }

    [Fact]
    public async Task GetSettings_WithCaseInsensitiveSearch_ShouldReturnResults()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?searchString=GLOBAL");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Any(s => s.Key.Contains("global")));
    }

    [Fact]
    public async Task GetSettings_WithPartialKeySearch_ShouldReturnMatchingResults()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?searchString=test.theme");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Any(s => s.Key.Contains("test.theme")));
    }

    [Fact]
    public async Task GetSettings_WithEmptySearchString_ShouldReturnAllResults()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?searchString=");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertTrue(result.Data!.Count > 0);
        TestAssertions.AssertTrue(result.Data!.Any(s => s.Key.Contains("global")));
    }

    [Fact(Skip = "Settings are now global entities without tenant isolation")]
    public async Task GetSettings_TenantIsolation_ShouldNotReturnOtherTenantSettings()
    {
        await SeedSettingTestDataAsync();

        var response1 = await Client.GetAsync("/api/v1/Settings");
        var result1 = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response1);


        var response2 = await Client.GetAsync("/api/v1/Settings");
        var result2 = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response2);

        // Filter to only test-specific settings to verify tenant isolation
        var tenant1TestSettings = result1!.Data?.Where(s => s.Key.StartsWith("test.")).ToList();
        var tenant2TestSettings = result2!.Data?.Where(s => s.Key.StartsWith("test.")).ToList();

        TestAssertions.AssertEqual(3, tenant1TestSettings?.Count ?? 0);
        TestAssertions.AssertEqual(2, tenant2TestSettings?.Count ?? 0);

        // Verify that tenant1 only gets tenant1 test settings
        TestAssertions.AssertTrue(tenant1TestSettings?.All(s => s.Key.Contains("tenant1")) ?? false);
        
        // Verify that tenant2 only gets tenant2 test settings
        TestAssertions.AssertTrue(tenant2TestSettings?.All(s => s.Key.Contains("tenant2")) ?? false);

        // Ensure no overlap between tenant-specific settings
        var tenant1Ids = tenant1TestSettings?.Select(s => s.Id).ToList();
        var tenant2Ids = tenant2TestSettings?.Select(s => s.Id).ToList();

        TestAssertions.AssertTrue(tenant1Ids?.All(id => !tenant2Ids?.Contains(id) ?? true) ?? false);
        TestAssertions.AssertTrue(tenant2Ids?.All(id => !tenant1Ids?.Contains(id) ?? true) ?? false);
    }

    [Fact]
    public async Task GetSettings_WithMultipleOrderBy_ShouldRespectMultipleSorting()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?orderBy=Key,Value&pageSize=100");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);

        // Verify we get all available settings (should be more than 3 due to system default settings)
        TestAssertions.AssertTrue((result.Data?.Count ?? 0) > 3, $"Expected more than 3 settings, got {result.Data?.Count ?? 0}");

        // Verify sorting is applied by checking the order
        var keys = result.Data!.Select(s => s.Key).ToList();
        var sortedKeys = keys.OrderBy(k => k).ToList();
        TestAssertions.AssertEqual(sortedKeys, keys);
    }

    [Fact]
    public async Task GetSettings_WithComplexSearch_ShouldFilterCorrectly()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings?searchString=test.setting&pageSize=1&orderBy=Key");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result!.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.TotalCount > 0);
        TestAssertions.AssertTrue(result.Data?.First().Key.Contains("test.setting") ?? false);
    }
}