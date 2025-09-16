using System.Net;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Setting.Queries;

public class SettingDetailQueryTests : TenantIsolatedTestBase
{
    private async Task SeedSettingTestDataAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        var existingSettings = await DbContext.Setting.IgnoreQueryFilters().ToListAsync();
        if (!existingSettings.Any(s => s.Key == "test.detail.tenant1"))
        {
            var setting1Tenant1 = new maERP.Domain.Entities.Setting
            {
                Key = "test.detail.tenant1",
                Value = "detail_value1",
                TenantId = TenantConstants.TestTenant1Id
            };

            var setting2Tenant1 = new maERP.Domain.Entities.Setting
            {
                Key = "test.config.tenant1",
                Value = "config_value1",
                TenantId = TenantConstants.TestTenant1Id
            };

            var setting3Tenant1 = new maERP.Domain.Entities.Setting
            {
                Key = "test.boolean.tenant1",
                Value = "true",
                TenantId = TenantConstants.TestTenant1Id
            };

            var setting1Tenant2 = new maERP.Domain.Entities.Setting
            {
                Key = "test.detail.tenant2",
                Value = "detail_value2",
                TenantId = TenantConstants.TestTenant2Id
            };

            var setting2Tenant2 = new maERP.Domain.Entities.Setting
            {
                Key = "test.config.tenant2",
                Value = "config_value2",
                TenantId = TenantConstants.TestTenant2Id
            };

            var setting3Tenant2 = new maERP.Domain.Entities.Setting
            {
                Key = "test.special.tenant2",
                Value = "special_value",
                TenantId = TenantConstants.TestTenant2Id
            };

            DbContext.Setting.AddRange(setting1Tenant1, setting2Tenant1, setting3Tenant1,
                                     setting1Tenant2, setting2Tenant2, setting3Tenant2);
            await DbContext.SaveChangesAsync();
        }
    }

    [Fact]
    public async Task GetSettingDetail_WithValidIdAndTenant_ShouldReturnSettingDetail()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get tenant 1 settings to find an ID
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var tenant1Setting = listResult.Data?.FirstOrDefault(s => s.Key.Contains("tenant1"));
        TestAssertions.AssertNotNull(tenant1Setting);

        var response = await Client.GetAsync($"/api/v1/Settings/{tenant1Setting!.Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(tenant1Setting.Id, result.Data!.Id);
        TestAssertions.AssertEqual(tenant1Setting.Key, result.Data.Key);
        TestAssertions.AssertEqual(tenant1Setting.Value, result.Data.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get a tenant 1 setting ID
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var tenant1Setting = listResult.Data?.FirstOrDefault(s => s.Key.Contains("tenant1"));
        TestAssertions.AssertNotNull(tenant1Setting);

        // Try to access it with tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response = await Client.GetAsync($"/api/v1/Settings/{tenant1Setting!.Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get a tenant 1 setting ID
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var tenant1Setting = listResult.Data?.FirstOrDefault(s => s.Key.Contains("tenant1"));
        TestAssertions.AssertNotNull(tenant1Setting);

        // Remove tenant header and try to access tenant-specific setting
        RemoveTenantHeader();
        var response = await Client.GetAsync($"/api/v1/Settings/{tenant1Setting!.Id}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithValidId_ShouldIncludeAllSettingFields()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get a tenant 1 setting
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var tenant1Setting = listResult.Data?.FirstOrDefault(s => s.Key == "test.config.tenant1");
        TestAssertions.AssertNotNull(tenant1Setting);

        var response = await Client.GetAsync($"/api/v1/Settings/{tenant1Setting!.Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var setting = result.Data!;
        TestAssertions.AssertEqual(tenant1Setting.Id, setting.Id);
        TestAssertions.AssertEqual("test.config.tenant1", setting.Key);
        TestAssertions.AssertEqual("config_value1", setting.Value);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(setting.Key));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(setting.Value));
    }

    [Fact]
    public async Task GetSettingDetail_WithTenant2Setting_ShouldReturnCorrectSetting()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        // Get a tenant 2 setting
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var tenant2Setting = listResult.Data?.FirstOrDefault(s => s.Key == "test.detail.tenant2");
        TestAssertions.AssertNotNull(tenant2Setting);

        var response = await Client.GetAsync($"/api/v1/Settings/{tenant2Setting!.Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(tenant2Setting.Id, result.Data!.Id);
        TestAssertions.AssertEqual("test.detail.tenant2", result.Data.Key);
        TestAssertions.AssertEqual("detail_value2", result.Data.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithInvalidId_ShouldReturnBadRequest()
    {
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSettingDetail_WithZeroId_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Settings/{Guid.Empty}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithInvalidGuid_ShouldReturnBadRequest()
    {
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/Settings/invalid-guid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSettingDetail_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();
        SetInvalidTenantHeader(); // Uses valid GUID but non-existent tenant

        var response = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get an existing setting ID first
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var existingSetting = listResult.Data?.FirstOrDefault();
        TestAssertions.AssertNotNull(existingSetting);

        var response = await Client.GetAsync($"/api/v1/Settings/{existingSetting!.Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task GetSettingDetail_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetSettingDetail_WithLargeId_ShouldHandleGracefully()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_TenantIsolation_ShouldNotReturnOtherTenantSettings()
    {
        await SeedSettingTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        var response2 = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        var response3 = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response3.StatusCode);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response4 = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response4.StatusCode);

        var response5 = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response5.StatusCode);

        var response6 = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response6.StatusCode);
    }

    [Fact]
    public async Task GetSettingDetail_WithBooleanValue_ShouldReturnStringValue()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get a boolean setting dynamically
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var booleanSetting = listResult.Data?.FirstOrDefault(s => s.Key == "test.boolean.tenant1");
        TestAssertions.AssertNotNull(booleanSetting);

        var response = await Client.GetAsync($"/api/v1/Settings/{booleanSetting!.Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var setting = result.Data!;
        TestAssertions.AssertEqual(booleanSetting.Id, setting.Id);
        TestAssertions.AssertEqual("test.boolean.tenant1", setting.Key);
        TestAssertions.AssertEqual("true", setting.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithLanguageCodeValue_ShouldReturnCorrectValue()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        // Get a tenant 2 setting dynamically
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var configSetting = listResult.Data?.FirstOrDefault(s => s.Key == "test.config.tenant2");
        TestAssertions.AssertNotNull(configSetting);

        var response = await Client.GetAsync($"/api/v1/Settings/{configSetting!.Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var setting = result.Data!;
        TestAssertions.AssertEqual(configSetting.Id, setting.Id);
        TestAssertions.AssertEqual("test.config.tenant2", setting.Key);
        TestAssertions.AssertEqual("config_value2", setting.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithComplexKey_ShouldReturnCorrectSetting()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        // Get a special setting dynamically
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var specialSetting = listResult.Data?.FirstOrDefault(s => s.Key == "test.special.tenant2");
        TestAssertions.AssertNotNull(specialSetting);

        var response = await Client.GetAsync($"/api/v1/Settings/{specialSetting!.Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var setting = result.Data!;
        TestAssertions.AssertEqual(specialSetting.Id, setting.Id);
        TestAssertions.AssertEqual("test.special.tenant2", setting.Key);
        TestAssertions.AssertEqual("special_value", setting.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithCompleteSettingData_ShouldMapAllFields()
    {
        await SeedSettingTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get an existing setting ID first
        var listResponse = await Client.GetAsync("/api/v1/Settings");
        var listResult = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse);
        var existingSetting = listResult.Data?.FirstOrDefault();
        TestAssertions.AssertNotNull(existingSetting);

        var response = await Client.GetAsync($"/api/v1/Settings/{existingSetting!.Id}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        var setting = result.Data!;

        TestAssertions.AssertNotEqual(Guid.Empty, setting.Id);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(setting.Key));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(setting.Value));
    }

    [Fact]
    public async Task GetSettingDetail_CrossTenantAccess_ShouldBeCompletelyIsolated()
    {
        await SeedSettingTestDataAsync();

        // Get actual setting IDs for both tenants
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var listResponse1 = await Client.GetAsync("/api/v1/Settings");
        var listResult1 = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse1);
        var tenant1SettingId = listResult1.Data?.FirstOrDefault()?.Id ?? Guid.Empty;

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var listResponse2 = await Client.GetAsync("/api/v1/Settings");
        var listResult2 = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse2);
        var tenant2SettingId = listResult2.Data?.FirstOrDefault()?.Id ?? Guid.Empty;

        // Test that tenant 1 can access its own setting
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync($"/api/v1/Settings/{tenant1SettingId}");
        TestAssertions.AssertHttpSuccess(response1);

        // Test that tenant 2 cannot access tenant 1's setting
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync($"/api/v1/Settings/{tenant1SettingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        // Test that tenant 1 cannot access tenant 2's setting
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response3 = await Client.GetAsync($"/api/v1/Settings/{tenant2SettingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response3.StatusCode);

        // Test that tenant 2 can access its own setting
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response4 = await Client.GetAsync($"/api/v1/Settings/{tenant2SettingId}");
        TestAssertions.AssertHttpSuccess(response4);
    }

    [Fact]
    public async Task GetSettingDetail_WithNullTenant_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();

        RemoveTenantHeader();

        var response = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithEmptyTenant_ShouldReturnNotFound()
    {
        await SeedSettingTestDataAsync();

        SetInvalidTenantHeaderValue(string.Empty);

        var response = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetSettingDetail_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized()
    {
        await SeedSettingTestDataAsync();
        
        SetInvalidTenantHeaderValue("invalid_tenant_id");

        var response = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetSettingDetail_TenantIsolationStressTest_ShouldMaintainIsolation()
    {
        await SeedSettingTestDataAsync();

        // Get tenant 1 settings
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var listResponse1 = await Client.GetAsync("/api/v1/Settings");
        var listResult1 = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse1);
        var tenant1Settings = listResult1.Data ?? new List<SettingListDto>();

        // Get tenant 2 settings
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var listResponse2 = await Client.GetAsync("/api/v1/Settings");
        var listResult2 = await ReadResponseAsync<PaginatedResult<SettingListDto>>(listResponse2);
        var tenant2Settings = listResult2.Data ?? new List<SettingListDto>();

        // Ensure we have settings for both tenants
        TestAssertions.AssertTrue(tenant1Settings.Count > 0);
        TestAssertions.AssertTrue(tenant2Settings.Count > 0);

        // Get tenant-specific settings (those with "tenant1" or "tenant2" in the key)
        var tenant1SpecificSettings = tenant1Settings.Where(s => s.Key.Contains("tenant1")).ToList();
        var tenant2SpecificSettings = tenant2Settings.Where(s => s.Key.Contains("tenant2")).ToList();

        // Verify tenant 1 specific settings isolation
        foreach (var setting in tenant1SpecificSettings)
        {
            // Tenant 1 should be able to access its own tenant-specific settings
            SetTenantHeader(TenantConstants.TestTenant1Id);
            var response = await Client.GetAsync($"/api/v1/Settings/{setting.Id}");
            TestAssertions.AssertHttpSuccess(response);

            // Tenant 2 should NOT be able to access tenant 1's tenant-specific settings
            SetTenantHeader(TenantConstants.TestTenant2Id);
            response = await Client.GetAsync($"/api/v1/Settings/{setting.Id}");
            TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        // Verify tenant 2 specific settings isolation
        foreach (var setting in tenant2SpecificSettings)
        {
            // Tenant 2 should be able to access its own tenant-specific settings
            SetTenantHeader(TenantConstants.TestTenant2Id);
            var response = await Client.GetAsync($"/api/v1/Settings/{setting.Id}");
            TestAssertions.AssertHttpSuccess(response);

            // Tenant 1 should NOT be able to access tenant 2's tenant-specific settings
            SetTenantHeader(TenantConstants.TestTenant1Id);
            response = await Client.GetAsync($"/api/v1/Settings/{setting.Id}");
            TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}