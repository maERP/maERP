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

public class SettingDeleteCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public SettingDeleteCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_SettingDeleteCommandTests_{uniqueId}";
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

    private async Task<Guid> CreateTestSettingAsync(Guid tenantId, string key = "test.delete.key", string value = "delete_value")
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

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task DeleteSetting_WithValidId_ShouldReturnNoContent()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_WithValidId_ShouldRemoveFromDatabase()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "test.delete.persist", "persistent_value");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify the setting is deleted
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Settings/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_WithInvalidId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/Settings/invalid");

        // Invalid GUID format should return BadRequest (400), not NotFound (404)
        // This is consistent with HTTP standards and other controllers in the system
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_WithWrongTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);

        // Try to delete from tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);

        // Verify setting still exists for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertHttpSuccess(getResponse);
    }

    [Fact]
    public async Task DeleteSetting_TenantIsolation_ShouldOnlyDeleteFromCorrectTenant()
    {
        await SeedTestDataAsync();
        var tenant1SettingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "isolation.delete.test", "tenant1_value");
        var tenant2SettingId = await CreateTestSettingAsync(TenantConstants.TestTenant2Id, "isolation.delete.test", "tenant2_value");

        // Delete tenant 1 setting
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Settings/{tenant1SettingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Verify tenant 1 setting is deleted
        var getResponse1 = await Client.GetAsync($"/api/v1/Settings/{tenant1SettingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponse1.StatusCode);

        // Verify tenant 2 setting still exists
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var getResponse2 = await Client.GetAsync($"/api/v1/Settings/{tenant2SettingId}");
        TestAssertions.AssertHttpSuccess(getResponse2);
        var setting2Detail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse2);
        TestAssertions.AssertEqual("tenant2_value", setting2Detail.Data!.Value);
    }

    [Fact]
    public async Task DeleteSetting_WithoutTenantHeader_ShouldReturnNotFoundForTenantSpecificSetting()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);

        // Remove tenant header
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");

        var response = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);

        // Verify setting still exists for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertHttpSuccess(getResponse);
    }

    [Fact]
    public async Task DeleteSetting_MultipleSettings_ShouldDeleteOnlySpecifiedSetting()
    {
        await SeedTestDataAsync();
        var setting1Id = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "multi.delete.test1", "value1");
        var setting2Id = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "multi.delete.test2", "value2");
        var setting3Id = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "multi.delete.test3", "value3");

        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Delete only the second setting
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Settings/{setting2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Verify only the second setting is deleted
        var getResponse1 = await Client.GetAsync($"/api/v1/Settings/{setting1Id}");
        TestAssertions.AssertHttpSuccess(getResponse1);

        var getResponse2 = await Client.GetAsync($"/api/v1/Settings/{setting2Id}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponse2.StatusCode);

        var getResponse3 = await Client.GetAsync($"/api/v1/Settings/{setting3Id}");
        TestAssertions.AssertHttpSuccess(getResponse3);
    }

    [Fact]
    public async Task DeleteSetting_WithZeroId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Settings/{Guid.Empty}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_WithNegativeId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/Settings/invalid-guid");

        // Invalid GUID format should return BadRequest (400), not NotFound (404)
        // This is consistent with HTTP standards and other controllers in the system
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_IdempotentDelete_ShouldReturnNotFoundOnSecondDelete()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // First delete should succeed
        var firstResponse = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, firstResponse.StatusCode);

        // Second delete should return NotFound
        var secondResponse = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, secondResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_ConcurrentDeletes_ShouldHandleRaceConditions()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Create multiple tasks that try to delete the same setting simultaneously
        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 0; i < 5; i++)
        {
            tasks.Add(Client.DeleteAsync($"/api/v1/Settings/{settingId}"));
        }

        var responses = await Task.WhenAll(tasks);

        // One should succeed (NoContent), others should return NotFound
        var successfulDeletes = responses.Where(r => r.StatusCode == HttpStatusCode.NoContent).ToList();
        var notFoundResponses = responses.Where(r => r.StatusCode == HttpStatusCode.NotFound).ToList();

        TestAssertions.AssertEqual(1, successfulDeletes.Count);
        TestAssertions.AssertEqual(4, notFoundResponses.Count);
    }

    [Fact]
    public async Task DeleteSetting_WithLargeId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Settings/{Guid.NewGuid()}"); // Non-existent Guid

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_ResponseHeaders_ShouldNotContainContent()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertEqual("", content);
    }

    [Fact]
    public async Task DeleteSetting_VerifyDatabaseConsistency_ShouldMaintainIntegrity()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "database.consistency.test", "consistency_value");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Verify setting exists in database before deletion
        var settingBefore = await DbContext.Setting.FindAsync(settingId);
        TestAssertions.AssertNotNull(settingBefore);
        TestAssertions.AssertEqual("database.consistency.test", settingBefore!.Key);

        var response = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify setting is removed from database
        DbContext.ChangeTracker.Clear(); // Clear EF tracking cache
        var settingAfter = await DbContext.Setting.FindAsync(settingId);
        TestAssertions.AssertEqual(null, settingAfter);
    }

    [Fact]
    public async Task DeleteSetting_SystemSettings_ShouldDeleteWithoutTenantRestriction()
    {
        await SeedTestDataAsync();

        // Create a system setting (no tenant header)
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        var settingDto = new SettingInputDto
        {
            Key = "system.delete.test",
            Value = "system_value"
        };
        var createResponse = await PostAsJsonAsync("/api/v1/Settings", settingDto);
        var createResult = await ReadResponseAsync<Result<int>>(createResponse);
        var systemSettingId = createResult.Data;

        // Delete system setting (still no tenant header)
        var response = await Client.DeleteAsync($"/api/v1/Settings/{systemSettingId}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify setting is deleted
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{systemSettingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_AfterMultipleOperations_ShouldMaintainConsistency()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Create a setting
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "operations.test", "original_value");

        // Update it
        var updateDto = new SettingInputDto
        {
            Id = settingId,
            Key = "operations.test",
            Value = "updated_value"
        };
        var json = JsonSerializer.Serialize(updateDto);
        var updateContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var updateResponse = await Client.PutAsync($"/api/v1/Settings/{settingId}", updateContent);
        TestAssertions.AssertEqual(HttpStatusCode.OK, updateResponse.StatusCode);

        // Then delete it
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Verify it's gone
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_WithInvalidTenantHeader_ShouldReturnNotFoundForTenantSpecificSetting()
    {
        await SeedTestDataAsync();
        var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id);

        // Set invalid tenant header
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", "invalid_tenant");

        var response = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);

        // Verify setting still exists for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertHttpSuccess(getResponse);
    }

    [Fact]
    public async Task DeleteSetting_CrossTenantAttempt_ShouldPreventUnauthorizedDeletion()
    {
        await SeedTestDataAsync();
        var tenant1SettingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, "cross.tenant.delete", "tenant1_value");
        var tenant2SettingId = await CreateTestSettingAsync(TenantConstants.TestTenant2Id, "cross.tenant.delete", "tenant2_value");

        // Tenant 2 tries to delete tenant 1's setting
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Settings/{tenant1SettingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, deleteResponse.StatusCode);

        // Verify tenant 1's setting is still intact
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse1 = await Client.GetAsync($"/api/v1/Settings/{tenant1SettingId}");
        TestAssertions.AssertHttpSuccess(getResponse1);
        var setting1Detail = await ReadResponseAsync<Result<SettingDetailDto>>(getResponse1);
        TestAssertions.AssertEqual("tenant1_value", setting1Detail.Data!.Value);

        // Verify tenant 2 can still delete their own setting
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var deleteResponse2 = await Client.DeleteAsync($"/api/v1/Settings/{tenant2SettingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, deleteResponse2.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_BulkOperation_ShouldHandleMultipleIndependentDeletes()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Create multiple settings
        var settingIds = new List<Guid>();
        for (int i = 0; i < 5; i++)
        {
            var settingId = await CreateTestSettingAsync(TenantConstants.TestTenant1Id, $"bulk.delete.test{i}", $"value{i}");
            settingIds.Add(settingId);
        }

        // Delete all settings
        var deleteTasks = settingIds.Select(id => Client.DeleteAsync($"/api/v1/Settings/{id}"));
        var deleteResponses = await Task.WhenAll(deleteTasks);

        // All deletions should succeed
        foreach (var response in deleteResponses)
        {
            TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        // Verify all settings are deleted
        foreach (var settingId in settingIds)
        {
            var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
            TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}