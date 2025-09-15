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

namespace maERP.Server.Tests.Features.Setting.Commands;

public class SettingCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public SettingCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SettingCreateCommandTests_{uniqueId}";
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

        // Force a small delay to ensure header is set properly
        Task.Delay(10).Wait();
    }

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PostAsync(requestUri, content);
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

    private async Task SeedTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private static SettingInputDto CreateValidSettingDto(string key = "test.create.key", string value = "test_value")
    {
        return new SettingInputDto
        {
            Key = key,
            Value = value
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateSetting_WithValidData_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var settingDto = CreateValidSettingDto();

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);
    }

    [Fact]
    public async Task CreateSetting_WithValidData_ShouldPersistInDatabase()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var settingDto = CreateValidSettingDto("test.persist.key", "persist_value");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);

        // Verify through API that setting exists
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(settingDetail?.Data);
        TestAssertions.AssertEqual(settingDto.Key, settingDetail!.Data.Key);
        TestAssertions.AssertEqual(settingDto.Value, settingDetail.Data.Value);
    }

    [Fact]
    public async Task CreateSetting_WithNullKey_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var settingDto = new SettingInputDto
        {
            Key = "test.empty.value",
            Value = "" // Empty value should be allowed
        };

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);
    }

    [Fact]
    public async Task CreateSetting_WithDuplicateKey_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Create first setting
        var firstSetting = CreateValidSettingDto("test.duplicate.key", "value1");
        await PostAsJsonAsync("/api/v1/Settings", firstSetting);

        // Try to create second setting with same key
        var duplicateSetting = CreateValidSettingDto("test.duplicate.key", "value2");

        var response = await PostAsJsonAsync("/api/v1/Settings", duplicateSetting);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSetting_WithoutTenantHeader_ShouldCreateSystemSetting()
    {
        await SeedTestDataAsync();
        var settingDto = CreateValidSettingDto("test.system.key", "system_value");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);
    }

    [Fact(Skip = "Todo: implement feature")]
    public async Task CreateSetting_WithTenant1_ShouldIsolateFromTenant2()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var settingDto = CreateValidSettingDto("test.tenant1.isolation", "tenant1_value");

        var createResponse = await PostAsJsonAsync("/api/v1/Settings", settingDto);
        TestAssertions.AssertEqual(HttpStatusCode.Created, createResponse.StatusCode);
        var createResult = await ReadResponseAsync<Result<int>>(createResponse);
        var settingId = createResult.Data;

        // Verify tenant 1 can access it
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse1 = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertHttpSuccess(getResponse1);

        // Verify tenant 2 cannot access it
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var getResponse2 = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponse2.StatusCode);
    }

    [Fact]
    public async Task CreateSetting_WithSpecialCharacters_ShouldAcceptValidCharacters()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var settingDto = CreateValidSettingDto("test.special-chars_key.123", "value with spaces and symbols: @#$%");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);
    }

    [Fact]
    public async Task CreateSetting_WithLongValues_ShouldHandleLargeData()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var longValue = new string('x', 1000); // 1000 character string
        var settingDto = CreateValidSettingDto("test.long.value", longValue);

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);

        // Verify the long value is preserved
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual(longValue, settingDetail.Data!.Value);
    }

    [Fact]
    public async Task CreateSetting_WithNumericValues_ShouldStoreAsString()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var settingDto = CreateValidSettingDto("test.numeric.value", "12345.67");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify it's stored as string
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{result.Data}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual("12345.67", settingDetail.Data!.Value);
    }

    [Fact]
    public async Task CreateSetting_WithBooleanValues_ShouldStoreAsString()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var settingDto = CreateValidSettingDto("test.boolean.value", "true");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify it's stored as string
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{result.Data}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual("true", settingDetail.Data!.Value);
    }

    [Fact]
    public async Task CreateSetting_WithJsonValue_ShouldStoreJsonAsString()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var jsonValue = "{\"name\":\"test\",\"enabled\":true,\"count\":42}";
        var settingDto = CreateValidSettingDto("test.json.value", jsonValue);

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify the JSON is preserved as string
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{result.Data}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual(jsonValue, settingDetail.Data!.Value);
    }

    [Fact]
    public async Task CreateSetting_WithNullValues_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Test with null in JSON - this should be handled by model validation
        var jsonContent = "{\"Key\":\"test.null.key\",\"Value\":null}";
        var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

        var response = await Client.PostAsync("/api/v1/Settings", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Result<Guid?>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result!.Succeeded);
    }

    [Fact(Skip = "Todo: implement feature")]
    public async Task CreateSetting_MultipleTenants_ShouldMaintainSeparateNamespaces()
    {
        await SeedTestDataAsync();

        // Create setting for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var setting1 = CreateValidSettingDto("shared.key.name", "value_for_tenant_1");
        var response1 = await PostAsJsonAsync("/api/v1/Settings", setting1);
        TestAssertions.AssertEqual(HttpStatusCode.Created, response1.StatusCode);

        // Create setting for tenant 2 with same key
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var setting2 = CreateValidSettingDto("shared.key.name", "value_for_tenant_2");
        var response2 = await PostAsJsonAsync("/api/v1/Settings", setting2);
        TestAssertions.AssertEqual(HttpStatusCode.Created, response2.StatusCode);

        // Both should succeed because they are in different tenant contexts
        var result1 = await ReadResponseAsync<Result<int>>(response1);
        var result2 = await ReadResponseAsync<Result<int>>(response2);
        TestAssertions.AssertTrue(result1.Succeeded);
        TestAssertions.AssertTrue(result2.Succeeded);
        TestAssertions.AssertNotEqual(result1.Data, result2.Data);
    }

    [Fact]
    public async Task CreateSetting_WithLongKey_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var longKey = new string('x', 100); // Exceeds 50 character limit
        var settingDto = new SettingInputDto
        {
            Key = longKey,
            Value = "test value"
        };

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateSetting_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var settingDto = CreateValidSettingDto("test.response.structure", "response_value");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);
        TestAssertions.AssertEqual(ResultStatusCode.Created, result.StatusCode);
    }

    [Fact(Skip = "Todo: implement feature")]
    public async Task CreateSetting_WithInvalidTenantHeader_ShouldCreateWithNullTenant()
    {
        await SeedTestDataAsync();
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", "invalid_tenant_id");

        var settingDto = CreateValidSettingDto("test.invalid.tenant", "invalid_tenant_value");

        var response = await PostAsJsonAsync("/api/v1/Settings", settingDto);

        // Should still create successfully but as system setting
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateSetting_ConcurrentCreation_ShouldHandleRaceConditions()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

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