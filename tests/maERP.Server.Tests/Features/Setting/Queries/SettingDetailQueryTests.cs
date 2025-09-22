using System.Net;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Setting.Queries;

public class SettingDetailQueryTests : GlobalTestBase
{
    private async Task SeedSettingTestDataAsync()
    {
        var existingSettings = await DbContext.Setting.ToListAsync();
        if (!existingSettings.Any(s => s.Key == "test.detail.global"))
        {
            var setting1 = new maERP.Domain.Entities.Setting
            {
                Key = "test.detail.global",
                Value = "detail_value1"
            };

            var setting2 = new maERP.Domain.Entities.Setting
            {
                Key = "test.config.global",
                Value = "config_value1"
            };

            var setting3 = new maERP.Domain.Entities.Setting
            {
                Key = "test.boolean.global",
                Value = "true"
            };

            DbContext.Setting.AddRange(setting1, setting2, setting3);
            await DbContext.SaveChangesAsync();
        }
    }

    private async Task<Guid> CreateTestSettingAsync(string key = "test.detail.key", string value = "test_value")
    {
        var settingDto = new SettingInputDto
        {
            Key = key,
            Value = value
        };

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        return result!.Data;
    }

    [Fact]
    public async Task GetSettingDetail_WithValidId_ShouldReturnSettingDetail()
    {
        var settingId = await CreateTestSettingAsync("test.valid.detail", "valid_value");

        var response = await Client.GetAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result!.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(settingId, result.Data!.Id);
        TestAssertions.AssertEqual("test.valid.detail", result.Data.Key);
        TestAssertions.AssertEqual("valid_value", result.Data.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        var response = await Client.GetAsync($"/api/v1/Settings/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result!.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetSettingDetail_WithInvalidId_ShouldReturnBadRequest()
    {
        var response = await Client.GetAsync("/api/v1/Settings/invalid-id");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSettingDetail_ResponseStructure_ShouldHaveCorrectFormat()
    {
        var settingId = await CreateTestSettingAsync("test.response.structure", "structure_value");

        var response = await Client.GetAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result!.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);

        // Verify all required fields are present
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data!.Id);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(result.Data.Key));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(result.Data.Value));
        // TestAssertions.AssertTrue(result.Data.DateCreated > DateTime.MinValue);
        // TestAssertions.AssertTrue(result.Data.DateModified > DateTime.MinValue);
    }

    [Fact]
    public async Task GetSettingDetail_WithSpecialCharacters_ShouldPreserveCharacters()
    {
        var specialValue = "Special chars: @#$%^&*()_+-={}[]|\\:;\"'<>?,./ äöü";
        var settingId = await CreateTestSettingAsync("test.special.chars", specialValue);

        var response = await Client.GetAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertEqual(specialValue, result!.Data!.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithJsonValue_ShouldPreserveJsonStructure()
    {
        var jsonValue = "{\"name\":\"test\",\"enabled\":true,\"count\":42,\"items\":[\"a\",\"b\"]}";
        var settingId = await CreateTestSettingAsync("test.json.value", jsonValue);

        var response = await Client.GetAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertEqual(jsonValue, result!.Data!.Value);
    }

    [Fact]
    public async Task GetSettingDetail_WithLongValue_ShouldHandleLargeData()
    {
        var longValue = new string('x', 5000); // 5000 character string
        var settingId = await CreateTestSettingAsync("test.long.value", longValue);

        var response = await Client.GetAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertEqual(longValue, result!.Data!.Value);
        TestAssertions.AssertEqual(5000, result.Data.Value.Length);
    }

    [Fact]
    public async Task GetSettingDetail_WithEmptyValue_ShouldReturnEmptyString()
    {
        var settingId = await CreateTestSettingAsync("test.empty.value", "");

        var response = await Client.GetAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<SettingDetailDto>>(response);
        TestAssertions.AssertEqual("", result!.Data!.Value);
    }

    [Fact(Skip = "Settings are now global entities, no tenant isolation")]
    public Task GetSettingDetail_TenantIsolation_ShouldOnlyReturnOwnTenantSettings()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }

    [Fact(Skip = "Settings are now global entities, no tenant header required")]
    public Task GetSettingDetail_WithoutTenantHeader_ShouldReturnSystemSettings()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }

    [Fact(Skip = "Settings are now global entities, no tenant header required")]
    public Task GetSettingDetail_WithInvalidTenantHeader_ShouldReturnUnauthorized()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }
}