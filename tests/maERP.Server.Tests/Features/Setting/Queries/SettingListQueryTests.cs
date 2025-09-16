using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Setting.Queries;

public class SettingListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public SettingListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SettingListQueryTests_{uniqueId}";
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

    private async Task SeedSettingTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

            var existingSettings = await DbContext.Setting.IgnoreQueryFilters().ToListAsync();
            if (!existingSettings.Any(s => s.Key == "test.setting.tenant1"))
            {
                var setting1Tenant1 = new maERP.Domain.Entities.Setting
                {
                    Key = "test.setting.tenant1",
                    Value = "value1",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var setting2Tenant1 = new maERP.Domain.Entities.Setting
                {
                    Key = "test.search.tenant1",
                    Value = "searchable",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var setting3Tenant1 = new maERP.Domain.Entities.Setting
                {
                    Key = "test.theme.tenant1",
                    Value = "dark",
                    TenantId = TenantConstants.TestTenant1Id
                };

                var setting1Tenant2 = new maERP.Domain.Entities.Setting
                {
                    Key = "test.setting.tenant2",
                    Value = "value2",
                    TenantId = TenantConstants.TestTenant2Id
                };

                var setting2Tenant2 = new maERP.Domain.Entities.Setting
                {
                    Key = "test.search.tenant2",
                    Value = "findable",
                    TenantId = TenantConstants.TestTenant2Id
                };

                DbContext.Setting.AddRange(setting1Tenant1, setting2Tenant1, setting3Tenant1,
                                         setting1Tenant2, setting2Tenant2);
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
    public async Task GetSettings_WithValidTenant_ShouldReturnTenantData()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Any(s => s.Key.Contains("tenant1")) ?? false);
    }

    [Fact]
    public async Task GetSettings_WithDifferentTenant_ShouldReturnOnlyThatTenantData()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync("/api/v1/Settings");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Any(s => s.Key.Contains("tenant2")) ?? false);

        var tenant1Settings = result.Data?.Where(s => s.Key.Contains("tenant1")).ToList();
        TestAssertions.AssertEmpty(tenant1Settings ?? new List<SettingListDto>());
    }

    [Fact]
    public async Task GetSettings_WithoutTenantHeader_ShouldReturnOnlyNonTenantSettings()
    {
        await SeedSettingTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Settings");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Without tenant header, we should get system settings (null TenantId)
        var tenantSpecificSettings = result.Data?.Where(s => s.Key.Contains("tenant")).ToList();
        TestAssertions.AssertEmpty(tenantSpecificSettings ?? new List<SettingListDto>());
    }

    [Fact]
    public async Task GetSettings_WithPagination_ShouldRespectPageSize()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?pageNumber=0&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Count <= 2);
        TestAssertions.AssertTrue(result.TotalCount > 0);
    }

    [Fact]
    public async Task GetSettings_WithPaginationSecondPage_ShouldReturnSecondPageData()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?pageNumber=1&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.TotalCount > 0);
    }

    [Fact]
    public async Task GetSettings_WithSearchString_ShouldFilterResults()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?searchString=test.search");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Any(s => s.Key == "test.search.tenant1") ?? false);

        var keys = result.Data?.Select(s => s.Key).ToList();
        TestAssertions.AssertTrue(keys?.All(k => k.Contains("test.search")) ?? false);
    }

    [Fact]
    public async Task GetSettings_WithSearchStringNoMatch_ShouldReturnEmpty()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?searchString=nonexistent");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetSettings_WithOrderByKey_ShouldReturnOrderedResults()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?orderBy=Key");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Count > 0);

        // Verify ordering - keys should be in ascending order
        var keys = result.Data?.Select(x => x.Key).ToList();
        var sortedKeys = keys?.OrderBy(k => k).ToList();
        TestAssertions.AssertEqual(sortedKeys, keys);
    }

    [Fact]
    public async Task GetSettings_WithOrderByKeyDescending_ShouldReturnDescOrderedResults()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?orderBy=Key desc");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Count > 0);

        // Verify descending ordering
        var keys = result.Data?.Select(x => x.Key).ToList();
        var sortedKeysDesc = keys?.OrderByDescending(k => k).ToList();
        TestAssertions.AssertEqual(sortedKeysDesc, keys);
    }

    [Fact]
    public async Task GetSettings_WithOrderByValue_ShouldReturnValueOrderedResults()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?orderBy=Value");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Count > 0);

        // Verify value ordering
        var values = result.Data?.Select(x => x.Value).ToList();
        var sortedValues = values?.OrderBy(v => v).ToList();
        TestAssertions.AssertEqual(sortedValues, values);
    }

    [Fact]
    public async Task GetSettings_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // First get the actual total count of settings
        var countResponse = await Client.GetAsync("/api/v1/Settings?pageNumber=0&pageSize=100");
        var countResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(countResponse);
        var actualTotalCount = countResult.TotalCount;

        var response = await Client.GetAsync("/api/v1/Settings?pageNumber=10&pageSize=10");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(actualTotalCount, result.TotalCount);
    }

    [Fact]
    public async Task GetSettings_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?pageSize=0");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Count > 0);
    }

    [Fact]
    public async Task GetSettings_WithNegativePageNumber_ShouldHandleGracefully()
    {
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync("/api/v1/Settings?pageNumber=-1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Negative page numbers should be handled gracefully by returning the first page (page 0)
        // This should return the default pageSize of 10 items (or fewer if less data exists)
        TestAssertions.AssertTrue((result.Data?.Count ?? 0) > 0, "Expected at least some results when negative pageNumber is treated as first page");
        TestAssertions.AssertTrue((result.Data?.Count ?? 0) <= 10, "Expected maximum of 10 results (default pageSize)");
        TestAssertions.AssertEqual(0, result.CurrentPage); // Should be normalized to page 0
    }

    [Fact]
    public async Task GetSettings_WithNonExistentTenant_ShouldReturnOnlyNonTenantSettings()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync("/api/v1/Settings");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // With non-existent tenant, we should not get any tenant-specific settings
        var tenantSpecificSettings = result.Data?.Where(s => s.Key.Contains("tenant")).ToList();
        TestAssertions.AssertEmpty(tenantSpecificSettings ?? new List<SettingListDto>());
    }

    [Fact]
    public async Task GetSettings_ResponseStructure_ShouldContainRequiredFields()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstSetting = result.Data?.First();
        TestAssertions.AssertNotNull(firstSetting);
        TestAssertions.AssertNotEqual(Guid.Empty, firstSetting!.Id);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstSetting.Key));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstSetting.Value));
    }

    [Fact]
    public async Task GetSettings_WithSpecificKeySearch_ShouldFilterByKey()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?searchString=test.theme.tenant1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Any(s => s.Key == "test.theme.tenant1") ?? false);
    }

    [Fact]
    public async Task GetSettings_WithValueSearch_ShouldFilterByValue()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?searchString=searchable");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Any(s => s.Value == "searchable") ?? false);
    }

    [Fact]
    public async Task GetSettings_WithCaseInsensitiveSearch_ShouldReturnResults()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?searchString=TENANT1");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Any(s => s.Key.Contains("tenant1")) ?? false);
    }

    [Fact]
    public async Task GetSettings_WithPartialKeySearch_ShouldReturnMatchingResults()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?searchString=test.theme");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Any(s => s.Key.Contains("test.theme")) ?? false);
    }

    [Fact]
    public async Task GetSettings_WithEmptySearchString_ShouldReturnAllResults()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?searchString=");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data?.Count > 0);
        TestAssertions.AssertTrue(result.Data?.Any(s => s.Key.Contains("tenant1")) ?? false);
    }

    [Fact]
    public async Task GetSettings_TenantIsolation_ShouldNotReturnOtherTenantSettings()
    {
        await SeedSettingTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/Settings");
        var result1 = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response1);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/Settings");
        var result2 = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response2);

        TestAssertions.AssertEqual(3, result1.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, result2.Data?.Count ?? 0);

        var tenant1Ids = result1.Data?.Select(s => s.Id).ToList();
        var tenant2Ids = result2.Data?.Select(s => s.Id).ToList();

        TestAssertions.AssertTrue(tenant1Ids?.All(id => !tenant2Ids?.Contains(id) ?? true) ?? false);
        TestAssertions.AssertTrue(tenant2Ids?.All(id => !tenant1Ids?.Contains(id) ?? true) ?? false);
    }

    [Fact]
    public async Task GetSettings_WithMultipleOrderBy_ShouldRespectMultipleSorting()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?orderBy=Key,Value&pageSize=100");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Verify we get all available settings (should be more than 3 due to system default settings)
        TestAssertions.AssertTrue((result.Data?.Count ?? 0) > 3, $"Expected more than 3 settings, got {result.Data?.Count ?? 0}");

        // Verify sorting is applied by checking the order
        var keys = result.Data?.Select(s => s.Key).ToList();
        var sortedKeys = keys?.OrderBy(k => k).ToList();
        TestAssertions.AssertEqual(sortedKeys, keys);
    }

    [Fact]
    public async Task GetSettings_WithComplexSearch_ShouldFilterCorrectly()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings?searchString=test.setting&pageSize=1&orderBy=Key");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<SettingListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.TotalCount > 0);
        TestAssertions.AssertTrue(result.Data?.First().Key.Contains("test.setting") ?? false);
    }
}