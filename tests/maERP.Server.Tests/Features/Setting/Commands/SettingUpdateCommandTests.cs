using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Setting.Commands;

public class GuidJsonConverter : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return Guid.Empty;
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            var guidString = reader.GetString();
            return Guid.TryParse(guidString, out var guid) ? guid : Guid.Empty;
        }

        throw new JsonException($"Unexpected token type {reader.TokenType} when reading Guid");
    }

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public class SettingUpdateCommandTests : GlobalTestBase
{
    private new async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) where T : class
    {
        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            // Allow null values for Guid properties
            Converters = { new GuidJsonConverter() }
        });
        return result ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    private async Task<Guid> CreateTestSettingAsync(string key = "test.update.key", string value = "original_value")
    {
        var settingDto = new SettingInputDto
        {
            Key = key,
            Value = value
        };

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        return result.Data;
    }

    private static SettingInputDto CreateUpdateDto(Guid id, string key = "test.update.key", string value = "updated_value")
    {
        return new SettingInputDto
        {
            Id = id,
            Key = key,
            Value = value
        };
    }

    [Fact]
    public async Task UpdateSetting_WithValidData_ShouldReturnOk()
    {
        var settingId = await CreateTestSettingAsync();

        var updateDto = CreateUpdateDto(settingId, "test.update.key", "new_updated_value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(settingId, result.Data);
    }

    [Fact]
    public async Task UpdateSetting_WithValidData_ShouldPersistChanges()
    {
        var settingId = await CreateTestSettingAsync("test.persist.update", "original_persist_value");

        var updateDto = CreateUpdateDto(settingId, "test.persist.update", "updated_persist_value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify the changes persisted
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual("updated_persist_value", settingDetail.Data!.Value);
    }

    [Fact]
    public async Task UpdateSetting_WithNonExistentId_ShouldReturnNotFound()
    {

        var nonExistentId = Guid.NewGuid();
        var updateDto = CreateUpdateDto(nonExistentId, "nonexistent.key", "value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{nonExistentId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateSetting_WithMismatchedId_ShouldReturnBadRequest()
    {
        var settingId = await CreateTestSettingAsync();

        var wrongId = Guid.NewGuid();
        var updateDto = CreateUpdateDto(wrongId, "test.mismatched.key", "value"); // Wrong ID in DTO

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateSetting_WithMissingKey_ShouldReturnBadRequest()
    {
        var settingId = await CreateTestSettingAsync();

        var updateDto = new SettingInputDto
        {
            Id = settingId,
            Key = "", // Missing required key
            Value = "test value"
        };

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSetting_WithEmptyValue_ShouldSucceed()
    {
        var settingId = await CreateTestSettingAsync();

        var updateDto = new SettingInputDto
        {
            Id = settingId,
            Key = "test.empty.value.update",
            Value = "" // Empty value should be allowed
        };

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(settingId, result.Data);
    }

    [Fact(Skip = "Settings are now global entities without tenant isolation")]
    public async Task UpdateSetting_WithWrongTenant_ShouldReturnNotFound()
    {
        var settingId = await CreateTestSettingAsync();

        // Try to update from tenant 2
        var updateDto = CreateUpdateDto(settingId, "test.wrong.tenant", "value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact(Skip = "Settings are now global entities without tenant isolation")]
    public Task UpdateSetting_TenantIsolation_ShouldMaintainSeparation()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }

    [Fact]
    public async Task UpdateSetting_WithSpecialCharacters_ShouldAcceptValidCharacters()
    {
        var settingId = await CreateTestSettingAsync();

        var updateDto = CreateUpdateDto(settingId, "test.special-chars_key.123", "special value: @#$% & more!");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify special characters are preserved
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual("special value: @#$% & more!", settingDetail.Data!.Value);
    }

    [Fact]
    public async Task UpdateSetting_WithLongValue_ShouldHandleLargeData()
    {
        var settingId = await CreateTestSettingAsync();

        var longValue = new string('y', 2000); // 2000 character string
        var updateDto = CreateUpdateDto(settingId, "test.long.update", longValue);

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify the long value is preserved
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual(longValue, settingDetail.Data!.Value);
    }

    [Fact]
    public async Task UpdateSetting_ChangeKeyValue_ShouldUpdateBothFields()
    {
        var settingId = await CreateTestSettingAsync("original.key", "original.value");

        var updateDto = CreateUpdateDto(settingId, "updated.key", "updated.value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify both key and value changed
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual("updated.key", settingDetail.Data!.Key);
        TestAssertions.AssertEqual("updated.value", settingDetail.Data.Value);
    }

    [Fact]
    public async Task UpdateSetting_WithDuplicateKey_ShouldReturnBadRequest()
    {

        // Create two settings
        var settingId1 = await CreateTestSettingAsync("first.key", "first.value");
        var settingId2 = await CreateTestSettingAsync("second.key", "second.value");


        // Try to update second setting with first setting's key
        var updateDto = CreateUpdateDto(settingId2, "first.key", "conflicting.value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId2}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateSetting_WithJsonValue_ShouldStoreJsonAsString()
    {
        var settingId = await CreateTestSettingAsync();

        var jsonValue = "{\"updated\":true,\"version\":2,\"items\":[\"a\",\"b\",\"c\"]}";
        var updateDto = CreateUpdateDto(settingId, "test.json.update", jsonValue);

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify the JSON is preserved as string
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual(jsonValue, settingDetail.Data!.Value);
    }

    [Fact(Skip = "Settings are now global entities, no tenant header required")]
    public async Task UpdateSetting_WithoutTenantHeader_ShouldReturnNotFoundForTenantSpecificSetting()
    {
        var settingId = await CreateTestSettingAsync();

        // Settings are now global, no tenant header needed

        var updateDto = CreateUpdateDto(settingId, "test.no.tenant", "value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateSetting_ResponseStructure_ShouldHaveCorrectFormat()
    {
        var settingId = await CreateTestSettingAsync();

        var updateDto = CreateUpdateDto(settingId, "test.response.structure", "response_value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertEqual(settingId, result.Data);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task UpdateSetting_ConcurrentUpdates_ShouldHandleRaceConditions()
    {
        var settingId = await CreateTestSettingAsync();

        // Create multiple tasks that try to update the same setting simultaneously
        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 0; i < 3; i++)
        {
            var updateDto = CreateUpdateDto(settingId, "concurrent.update.key", $"concurrent_value_{i}");
            tasks.Add(PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto));
        }

        var responses = await Task.WhenAll(tasks);

        // At least one should succeed (optimistic concurrency)
        var successfulResponses = responses.Where(r => r.StatusCode == HttpStatusCode.OK).ToList();
        TestAssertions.AssertTrue(successfulResponses.Count >= 1);
    }

    [Fact]
    public async Task UpdateSetting_PreservesDateModified_ShouldUpdateTimestamp()
    {
        var settingId = await CreateTestSettingAsync();

        // Get original timestamp by checking database directly
        var originalSetting = await DbContext.Setting.FindAsync(settingId);
        var originalModified = originalSetting?.DateModified;

        // Wait a small amount to ensure timestamp difference
        await Task.Delay(100);

        var updateDto = CreateUpdateDto(settingId, "timestamp.test", "updated_value");
        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify timestamp was updated - use AsNoTracking to get fresh data from database
        var updatedSetting = await DbContext.Setting.AsNoTracking().FirstOrDefaultAsync(s => s.Id == settingId);
        TestAssertions.AssertTrue(updatedSetting?.DateModified > originalModified);
    }

    [Fact]
    public async Task UpdateSetting_WithInvalidId_ShouldReturnBadRequest()
    {

        var updateDto = CreateUpdateDto(Guid.Empty, "invalid.id.test", "value"); // Invalid ID

        var response = await PutAsJsonAsync("/api/v1/Settings/invalid", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact(Skip = "Settings are now global entities without tenant isolation")]
    public async Task UpdateSetting_CrossTenantKeyConflict_ShouldAllowSameKeysAcrossTenants()
    {

        // Create setting for tenant 1
        var setting1Id = await CreateTestSettingAsync("cross.tenant.key", "tenant1_value");

        // Create setting for tenant 2 with same key
        var setting2Id = await CreateTestSettingAsync("cross.tenant.key.2", "tenant2_value");

        // Update tenant 1 setting - should succeed even though tenant 2 has same key
        var updateDto = CreateUpdateDto(setting1Id, "cross.tenant.key", "updated_tenant1_value");
        var response = await PutAsJsonAsync($"/api/v1/Settings/{setting1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify tenant 2 setting is unaffected
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{setting2Id}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual("tenant2_value", settingDetail.Data!.Value);
    }

    [Fact(Skip = "Settings are now global entities, no tenant header required")]
    public async Task UpdateSetting_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized()
    {
        var settingId = await CreateTestSettingAsync();

        // Settings are now global, no tenant header needed

        var updateDto = CreateUpdateDto(settingId, "test.invalid.tenant", "value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact(Skip = "Settings are now global entities, no tenant header required")]
    public async Task UpdateSetting_WithNonExistentValidTenant_ShouldReturnNotFound()
    {
        var settingId = await CreateTestSettingAsync();

        // Settings are now global, no tenant header needed

        var updateDto = CreateUpdateDto(settingId, "test.nonexistent.tenant", "value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}