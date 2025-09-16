using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
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

public class SettingUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public SettingUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SettingUpdateCommandTests_{uniqueId}";
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

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PutAsync(requestUri, content);
    }

    protected async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) where T : class
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

    private async Task<Guid> CreateTestSettingAsync(Guid tenantId, string key = "test.update.key", string value = "original_value")
    {
        SetTenantHeader(tenantId);
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

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateSetting_WithValidData_ShouldReturnOk()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "test.persist.update", "original_persist_value");
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

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

    [Fact]
    public async Task UpdateSetting_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);

        // Try to update from tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var updateDto = CreateUpdateDto(settingId, "test.wrong.tenant", "value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateSetting_TenantIsolation_ShouldMaintainSeparation()
    {
        await SeedTestDataAsync();
        var tenant1SettingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "isolation.test", "tenant1_value");
        var tenant2SettingId = await CreateTestSettingAsync(TenantConstants.TestTenant2Id, "isolation.test", "tenant2_value");

        // Update tenant 1 setting
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto1 = CreateUpdateDto(tenant1SettingId, "isolation.test", "updated_tenant1_value");
        var response1 = await PutAsJsonAsync($"/api/v1/Settings/{tenant1SettingId}", updateDto1);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);

        // Verify tenant 2 setting is unchanged
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var getResponse2 = await Client.GetAsync($"/api/v1/Settings/{tenant2SettingId}");
        TestAssertions.AssertHttpSuccess(getResponse2);
        var setting2Detail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse2);
        TestAssertions.AssertEqual("tenant2_value", setting2Detail.Data!.Value);
    }

    [Fact]
    public async Task UpdateSetting_WithSpecialCharacters_ShouldAcceptValidCharacters()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "original.key", "original.value");
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();

        // Create two settings
        var settingId1 = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "first.key", "first.value");
        var settingId2 = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "second.key", "second.value");

        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var jsonValue = "{\"updated\":true,\"version\":2,\"items\":[\"a\",\"b\",\"c\"]}";
        var updateDto = CreateUpdateDto(settingId, "test.json.update", jsonValue);

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify the JSON is preserved as string
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual(jsonValue, settingDetail.Data!.Value);
    }

    [Fact]
    public async Task UpdateSetting_WithoutTenantHeader_ShouldReturnNotFoundForTenantSpecificSetting()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);

        // Remove tenant header
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");

        var updateDto = CreateUpdateDto(settingId, "test.no.tenant", "value");

        var response = await PutAsJsonAsync($"/api/v1/Settings/{settingId}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateSetting_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var updateDto = CreateUpdateDto(Guid.Empty, "invalid.id.test", "value"); // Invalid ID

        var response = await PutAsJsonAsync("/api/v1/Settings/invalid", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateSetting_CrossTenantKeyConflict_ShouldAllowSameKeysAcrossTenants()
    {
        await SeedTestDataAsync();

        // Create setting for tenant 1
        var setting1Id = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "cross.tenant.key", "tenant1_value");

        // Create setting for tenant 2 with same key
        var setting2Id = await CreateTestSettingAsync(TenantConstants.TestTenant2Id, "cross.tenant.key", "tenant2_value");

        // Update tenant 1 setting - should succeed even though tenant 2 has same key
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateUpdateDto(setting1Id, "cross.tenant.key", "updated_tenant1_value");
        var response = await PutAsJsonAsync($"/api/v1/Settings/{setting1Id}", updateDto);

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify tenant 2 setting is unaffected
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{setting2Id}");
        var settingDetail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse);
        TestAssertions.AssertEqual("tenant2_value", settingDetail.Data!.Value);
    }
}