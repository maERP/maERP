using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Setting.Commands;

public class SettingDeleteCommandTests : GlobalTestBase
{
    private async Task<Guid> CreateTestSettingAsync(string key = "test.delete.key", string value = "delete_value")
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
    public async Task DeleteSetting_WithValidId_ShouldReturnNoContent()
    {
        var settingId = await CreateTestSettingAsync();

        var response = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_WithValidId_ShouldRemoveFromDatabase()
    {
        var settingId = await CreateTestSettingAsync("test.delete.persist", "persistent_value");

        var response = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify the setting is deleted
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{settingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_WithNonExistentId_ShouldReturnNotFound()
    {

        var response = await Client.DeleteAsync($"/api/v1/Settings/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_WithInvalidId_ShouldReturnBadRequest()
    {

        var response = await Client.DeleteAsync("/api/v1/Settings/invalid");

        // Invalid GUID format should return BadRequest (400), not NotFound (404)
        // This is consistent with HTTP standards and other controllers in the system
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact(Skip = "Settings are now global entities, no tenant restrictions")]
    public Task DeleteSetting_WithWrongTenant_ShouldReturnNotFound()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }

    [Fact(Skip = "Settings are now global entities without tenant isolation")]
    public Task DeleteSetting_TenantIsolation_ShouldOnlyDeleteFromCorrectTenant()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }

    [Fact(Skip = "Settings are now global entities, no tenant header required")]
    public Task DeleteSetting_WithoutTenantHeader_ShouldReturnNotFoundForTenantSpecificSetting()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }

    [Fact]
    public async Task DeleteSetting_MultipleSettings_ShouldDeleteOnlySpecifiedSetting()
    {
        var setting1Id = await CreateTestSettingAsync("multi.delete.test1", "value1");
        var setting2Id = await CreateTestSettingAsync("multi.delete.test2", "value2");
        var setting3Id = await CreateTestSettingAsync("multi.delete.test3", "value3");

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

        var response = await Client.DeleteAsync($"/api/v1/Settings/{Guid.Empty}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_WithNegativeId_ShouldReturnBadRequest()
    {

        var response = await Client.DeleteAsync("/api/v1/Settings/invalid-guid");

        // Invalid GUID format should return BadRequest (400), not NotFound (404)
        // This is consistent with HTTP standards and other controllers in the system
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_IdempotentDelete_ShouldReturnNotFoundOnSecondDelete()
    {
        var settingId = await CreateTestSettingAsync();

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
        var settingId = await CreateTestSettingAsync();

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

        var response = await Client.DeleteAsync($"/api/v1/Settings/{Guid.NewGuid()}"); // Non-existent Guid

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_ResponseHeaders_ShouldNotContainContent()
    {
        var settingId = await CreateTestSettingAsync();

        var response = await Client.DeleteAsync($"/api/v1/Settings/{settingId}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertEqual("", content);
    }

    [Fact]
    public async Task DeleteSetting_VerifyDatabaseConsistency_ShouldMaintainIntegrity()
    {
        var settingId = await CreateTestSettingAsync("database.consistency.test", "consistency_value");

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
        // Create a system setting
        var settingDto = new SettingInputDto
        {
            Key = "system.delete.test",
            Value = "system_value"
        };
        var createResponse = await PostAsJsonAsync("/api/v1/Settings", settingDto);
        var createResult = await ReadResponseAsync<Result<Guid>>(createResponse);
        var systemSettingId = createResult!.Data;

        // Delete system setting
        var response = await Client.DeleteAsync($"/api/v1/Settings/{systemSettingId}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify setting is deleted
        var getResponse = await Client.GetAsync($"/api/v1/Settings/{systemSettingId}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteSetting_AfterMultipleOperations_ShouldMaintainConsistency()
    {
        // Create a setting
        var settingId = await CreateTestSettingAsync("operations.test", "original_value");

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

    [Fact(Skip = "Settings are now global entities, no tenant header validation")]
    public Task DeleteSetting_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }

    [Fact(Skip = "Settings are now global entities, no tenant header validation")]
    public Task DeleteSetting_WithNonExistentValidTenant_ShouldReturnNotFound()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }

    [Fact(Skip = "Settings are now global entities without tenant isolation")]
    public Task DeleteSetting_CrossTenantAttempt_ShouldPreventUnauthorizedDeletion()
    {
        // This test is no longer relevant since Settings are global
        return Task.CompletedTask;
    }

    [Fact]
    public async Task DeleteSetting_BulkOperation_ShouldHandleMultipleIndependentDeletes()
    {
        // Create multiple settings
        var settingIds = new List<Guid>();
        for (int i = 0; i < 5; i++)
        {
            var settingId = await CreateTestSettingAsync($"bulk.delete.test{i}", $"value{i}");
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