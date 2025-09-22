using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;

namespace maERP.Server.Tests.Features.Setting.Commands;

public class SettingCreateCommandTests : GlobalTestBase
{

    private static SettingInputDto CreateValidSettingDto(string key = "test.create.key", string value = "test_value")
    {
        return new SettingInputDto
        {
            Key = key,
            Value = value
        };
    }

    [Fact]
    public async Task CreateSetting_WithValidData_ShouldReturnCreated()
    {
        var settingDto = CreateValidSettingDto();

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result!.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result!.Data);
    }

    [Fact]
    public async Task CreateSetting_WithValidData_ShouldPersistInDatabase()
    {
        var settingDto = CreateValidSettingDto("test.persist.key", "persist_value");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result!.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result!.Data);

        // Verify through API that setting exists
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(settingDetail?.Data);
        TestAssertions.AssertEqual(settingDto.Key, settingDetail!.Data!.Key);
        TestAssertions.AssertEqual(settingDto.Value, settingDetail.Data.Value);
    }

    [Fact]
    public async Task CreateSetting_WithNullKey_ShouldHandleGracefully()
    {
        // Test with null in JSON
        var jsonContent = "{\"Key\":null,\"Value\":\"test value\"}";
        var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

        var response = await Client.PostAsync("/api/v1/Settings", content);

        // Should return bad request due to model binding failure or validation
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateSetting_WithEmptyValue_ShouldSucceed()
    {
        var settingDto = new SettingInputDto
        {
            Key = "test.empty.value",
            Value = "" // Empty value should be allowed
        };

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result!.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result!.Data);
    }

    [Fact]
    public async Task CreateSetting_WithDuplicateKey_ShouldReturnBadRequest()
    {
        // Create first setting
        var firstSetting = CreateValidSettingDto("test.duplicate.key", "value1");
        await PostAsJsonAsync("/api/v1/Settings", firstSetting);

        // Try to create second setting with same key
        var duplicateSetting = CreateValidSettingDto("test.duplicate.key", "value2");

        var response = await PostAsJsonAsync("/api/v1/Settings", duplicateSetting);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result!.Succeeded);
        TestAssertions.AssertNotEmpty(result!.Messages);
    }

    [Fact]
    public async Task CreateSetting_ShouldCreateGlobalSetting()
    {
        var settingDto = CreateValidSettingDto("test.global.key", "global_value");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result!.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result!.Data);
    }


    [Fact]
    public async Task CreateSetting_WithSpecialCharacters_ShouldAcceptValidCharacters()
    {
        var settingDto = CreateValidSettingDto("test.special-chars_key.123", "value with spaces and symbols: @#$%");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result!.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result!.Data);
    }

    [Fact]
    public async Task CreateSetting_WithLongValues_ShouldHandleLargeData()
    {
        var longValue = new string('x', 1000); // 1000 character string
        var settingDto = CreateValidSettingDto("test.long.value", longValue);

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result!.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result!.Data);

        // Verify the long value is preserved
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual(longValue, settingDetail!.Data!.Value);
    }

    [Fact]
    public async Task CreateSetting_WithNumericValues_ShouldStoreAsString()
    {

        var settingDto = CreateValidSettingDto("test.numeric.value", "12345.67");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result!.Succeeded);

        // Verify it's stored as string
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{result.Data}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual("12345.67", settingDetail!.Data!.Value);
    }

    [Fact]
    public async Task CreateSetting_WithBooleanValues_ShouldStoreAsString()
    {

        var settingDto = CreateValidSettingDto("test.boolean.value", "true");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result!.Succeeded);

        // Verify it's stored as string
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{result.Data}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual("true", settingDetail!.Data!.Value);
    }

    [Fact]
    public async Task CreateSetting_WithJsonValue_ShouldStoreJsonAsString()
    {

        var jsonValue = "{\"name\":\"test\",\"enabled\":true,\"count\":42}";
        var settingDto = CreateValidSettingDto("test.json.value", jsonValue);

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result!.Succeeded);

        // Verify the JSON is preserved as string
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{result.Data}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual(jsonValue, settingDetail!.Data!.Value);
    }

    [Fact]
    public async Task CreateSetting_WithNullValues_ShouldReturnBadRequest()
    {


        // Test with null in JSON - this should be handled by model validation
        var jsonContent = "{\"Key\":\"test.null.key\",\"Value\":null}";
        var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

        var response = await Client.PostAsync("/api/v1/Settings", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await ReadResponseStringAsync(response);
        var result = JsonSerializer.Deserialize<Result<Guid?>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result!.Succeeded);
    }

    [Fact]
    public async Task CreateSetting_WithSameKey_ShouldReturnBadRequest()
    {
        // Create first setting
        var setting1 = CreateValidSettingDto("shared.key.name", "value_1");
        var response1 = await PostAsJsonAsync("/api/v1/Settings", setting1);
        TestAssertions.AssertEqual(HttpStatusCode.Created, response1.StatusCode);

        // Try to create setting with same key (should fail since settings are global)
        var setting2 = CreateValidSettingDto("shared.key.name", "value_2");
        var response2 = await PostAsJsonAsync("/api/v1/Settings", setting2);
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response2.StatusCode);

        var result2 = await ReadResponseAsync<Result<Guid>>(response2);
        TestAssertions.AssertFalse(result2!.Succeeded);
    }

    [Fact]
    public async Task CreateSetting_WithLongKey_ShouldReturnBadRequest()
    {

        var longKey = new string('x', 100); // Exceeds 50 character limit
        var settingDto = new SettingInputDto
        {
            Key = longKey,
            Value = "test value"
        };

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertFalse(result!.Succeeded);
        TestAssertions.AssertNotEmpty(result!.Messages);
    }

    [Fact]
    public async Task CreateSetting_ResponseStructure_ShouldHaveCorrectFormat()
    {

        var settingDto = CreateValidSettingDto("test.response.structure", "response_value");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result!.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEqual(Guid.Empty, result!.Data);
        TestAssertions.AssertEqual(ResultStatusCode.Created, result!.StatusCode);
    }

    [Fact(Skip = "Settings are now global entities, no tenant header validation")]
    public Task CreateSetting_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }

    [Fact(Skip = "Settings are now global entities, no tenant header validation")]
    public Task CreateSetting_WithNonExistentValidTenant_ShouldCreateSuccessfully()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }

    [Fact]
    public async Task CreateSetting_ConcurrentCreation_ShouldHandleRaceConditions()
    {


        // Create multiple tasks that try to create settings simultaneously
        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 0; i < 5; i++)
        {
            var settingDto = CreateValidSettingDto($"concurrent.test.key.{i}", $"value_{i}");
            tasks.Add(PostAsJsonAsync("/api/v1/Settings", settingDto));
        }

        var responses = await Task.WhenAll(tasks);

        // All should succeed since they have unique keys
        foreach (var response in responses)
        {
            TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}