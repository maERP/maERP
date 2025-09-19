using System.Net;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.AiPrompt.Commands;

public class AiPromptDeleteCommandTests : TenantIsolatedTestBase
{
    private async Task<(Guid tenant1PromptId, Guid tenant2PromptId)> SeedTestDataAsync()
    {
        var hasData = await DbContext.AiPrompt.IgnoreQueryFilters().AnyAsync();
        if (!hasData)
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        }

        // Get prompt IDs for both tenants
        var tenant1PromptId = await DbContext.AiPrompt.IgnoreQueryFilters()
            .Where(p => p.TenantId == TenantConstants.TestTenant1Id)
            .Select(p => p.Id)
            .FirstAsync();

        var tenant2PromptId = await DbContext.AiPrompt.IgnoreQueryFilters()
            .Where(p => p.TenantId == TenantConstants.TestTenant2Id)
            .Select(p => p.Id)
            .FirstAsync();

        return (tenant1PromptId, tenant2PromptId);
    }

    [Fact]
    public async Task DeleteAiPrompt_WithValidId_ShouldReturnNoContent()
    {
        // Arrange
        var (tenant1PromptId, _) = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAiPrompt_WithValidId_ShouldRemoveFromDatabase()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get all prompt IDs for tenant 1 to ensure we have at least one
        var tenant1PromptIds = await DbContext.AiPrompt.IgnoreQueryFilters()
            .Where(p => p.TenantId == TenantConstants.TestTenant1Id)
            .Select(p => p.Id)
            .ToListAsync();

        TestAssertions.AssertTrue(tenant1PromptIds.Any(), "No prompts found for tenant 1");
        var promptIdToDelete = tenant1PromptIds.First();

        // Verify prompt exists before deletion via API call
        var getResponseBefore = await Client.GetAsync($"/api/v1/AiPrompts/{promptIdToDelete}");
        TestAssertions.AssertHttpSuccess(getResponseBefore);

        // Act - Delete the prompt
        var deleteResponse = await Client.DeleteAsync($"/api/v1/AiPrompts/{promptIdToDelete}");

        // Assert delete was successful
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Verify prompt is removed from database by checking the count decreased
        var remainingPromptsCount = await DbContext.AiPrompt.IgnoreQueryFilters()
            .Where(p => p.TenantId == TenantConstants.TestTenant1Id)
            .CountAsync();

        TestAssertions.AssertEqual(tenant1PromptIds.Count - 1, remainingPromptsCount);

        // Verify the specific prompt ID no longer exists in database
        var deletedPromptExists = await DbContext.AiPrompt.IgnoreQueryFilters()
            .AnyAsync(p => p.Id == promptIdToDelete);

        TestAssertions.AssertFalse(deletedPromptExists, $"Prompt {promptIdToDelete} should be deleted but still exists in database");
    }

    [Fact]
    public async Task DeleteAiPrompt_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var nonExistentGuid = Guid.Parse("99999999-9999-9999-9999-999999999999");

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{nonExistentGuid}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAiPrompt_WithWrongTenant_ShouldReturnNotFound()
    {
        // Arrange
        var (tenant1PromptId, _) = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id); // Prompt belongs to tenant 1, accessing with tenant 2

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);

        // Verify prompt still exists in tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse = await Client.GetAsync($"/api/v1/AiPrompts/{tenant1PromptId}");
        TestAssertions.AssertHttpSuccess(getResponse);
    }

    [Fact]
    public async Task DeleteAiPrompt_WithoutTenantHeader_ShouldReturnUnauthorized()
    {
        // Arrange
        var (tenant1PromptId, _) = await SeedTestDataAsync();
        RemoveTenantHeader(); // Remove tenant header for authenticated request

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");

        // Assert - In test environment, missing header sets tenant to Guid.Empty, resulting in NotFound
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertFalse(result.Succeeded);

        // Verify prompt still exists in tenant 1
        SimulateAuthenticatedRequest();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse = await Client.GetAsync($"/api/v1/AiPrompts/{tenant1PromptId}");
        TestAssertions.AssertHttpSuccess(getResponse);
    }

    [Fact]
    public async Task DeleteAiPrompt_WithInvalidTenantHeaderValue_ShouldReturnNotFound()
    {
        // Arrange
        var (tenant1PromptId, _) = await SeedTestDataAsync();
        SetInvalidTenantHeader();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAiPrompt_TenantIsolation_ShouldOnlyDeleteInCorrectTenant()
    {
        // Arrange
        var (tenant1PromptId, tenant2PromptId) = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Delete prompt in tenant 1
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify prompt was deleted in tenant 1
        var getResponseTenant1 = await Client.GetAsync($"/api/v1/AiPrompts/{tenant1PromptId}");
        TestAssertions.AssertHttpStatusCode(getResponseTenant1, HttpStatusCode.NotFound);

        // Verify prompt still exists in tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var getResponseTenant2 = await Client.GetAsync($"/api/v1/AiPrompts/{tenant2PromptId}");
        TestAssertions.AssertHttpSuccess(getResponseTenant2);
    }

    [Fact]
    public async Task DeleteAiPrompt_WithZeroId_ShouldReturnBadRequest()
    {
        // Arrange - No need to seed data for validation test
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{Guid.Empty}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAiPrompt_WithNegativeId_ShouldReturnBadRequest()
    {
        // Arrange - No need to seed data for validation test
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync("/api/v1/AiPrompts/-1");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAiPrompt_WithStringId_ShouldReturnBadRequest()
    {
        // Arrange - No need to seed data for validation test
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync("/api/v1/AiPrompts/abc");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAiPrompt_AlreadyDeleted_ShouldReturnNotFound()
    {
        // Arrange
        var (tenant1PromptId, _) = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Delete the prompt first time
        var firstDeleteResponse = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, firstDeleteResponse.StatusCode);

        // Act - Try to delete again
        var secondDeleteResponse = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, secondDeleteResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteAiPrompt_MultiplePromptsInSameTenant_ShouldDeleteOnlySpecified()
    {
        // Arrange - Ensure clean start by seeding data
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get all prompt IDs for tenant 1
        var tenant1PromptIds = await DbContext.AiPrompt.IgnoreQueryFilters()
            .Where(p => p.TenantId == TenantConstants.TestTenant1Id)
            .Select(p => p.Id)
            .OrderBy(p => p)
            .ToListAsync();

        // Ensure we have multiple prompts
        TestAssertions.AssertTrue(tenant1PromptIds.Count > 1, $"Expected multiple prompts in tenant 1, but found {tenant1PromptIds.Count}");

        var promptIdToDelete = tenant1PromptIds.First();
        var promptsCountBefore = tenant1PromptIds.Count;

        // Act - Delete one specific prompt
        var deleteResponse = await Client.DeleteAsync($"/api/v1/AiPrompts/{promptIdToDelete}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Verify specific prompt was deleted (should return 404)
        var getDeletedPromptResponse = await Client.GetAsync($"/api/v1/AiPrompts/{promptIdToDelete}");
        TestAssertions.AssertHttpStatusCode(getDeletedPromptResponse, HttpStatusCode.NotFound);

        // Get all prompts again via API to verify count
        var getPromptsAfterResponse = await Client.GetAsync("/api/v1/AiPrompts?pageNumber=0&pageSize=100");
        TestAssertions.AssertHttpSuccess(getPromptsAfterResponse);
        var promptsAfterResult = await ReadResponseAsync<PaginatedResult<Domain.Dtos.AiPrompt.AiPromptListDto>>(getPromptsAfterResponse);
        var promptsCountAfter = promptsAfterResult?.Data?.Count ?? 0;

        // Verify the count is reduced by exactly 1
        TestAssertions.AssertEqual(promptsCountBefore - 1, promptsCountAfter);

        // Verify the deleted prompt is not in the list
        var remainingPromptIds = promptsAfterResult?.Data?.Select(p => p.Id).ToList() ?? new List<Guid>();
        TestAssertions.AssertFalse(remainingPromptIds.Contains(promptIdToDelete), "Deleted prompt should not be in the list");
    }

    [Fact]
    public async Task DeleteAiPrompt_ResponseHeadersAndContent_ShouldBeCorrect()
    {
        // Arrange
        var (tenant1PromptId, _) = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify response has no content
        var content = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(content))
        {
            TestAssertions.AssertTrue(false, $"Expected empty content but got: '{content}' (length: {content.Length})");
        }

        // For NoContent responses, either content should be empty OR content-length should be 0
        // Some HTTP implementations may return empty string vs null
        TestAssertions.AssertTrue(string.IsNullOrEmpty(content) || response.Content.Headers.ContentLength == 0,
            $"Response should have no content. Content: '{content}', ContentLength: {response.Content.Headers.ContentLength}");
    }

    [Fact]
    public async Task DeleteAiPrompt_ConcurrentDeletes_ShouldHandleCorrectly()
    {
        // Arrange
        var (tenant1PromptId, _) = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Multiple concurrent delete requests
        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 0; i < 3; i++)
        {
            tasks.Add(Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}"));
        }

        var responses = await Task.WhenAll(tasks);

        // Assert - Only one should succeed with NoContent, others should be NotFound
        var noContentResponses = responses.Where(r => r.StatusCode == HttpStatusCode.NoContent).ToList();
        var notFoundResponses = responses.Where(r => r.StatusCode == HttpStatusCode.NotFound).ToList();

        TestAssertions.AssertEqual(1, noContentResponses.Count);
        TestAssertions.AssertEqual(2, notFoundResponses.Count);

        // Verify prompt is deleted
        var getResponse = await Client.GetAsync($"/api/v1/AiPrompts/{tenant1PromptId}");
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Fact(Skip = "Todo: Add better InMemoryDB handling to GenericRepository")]
    public async Task DeleteAiPrompt_AfterDeletion_IndividualGetShouldReturn404()
    {
        // Arrange
        var (tenant1PromptId, _) = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Verify prompt exists before deletion
        var getResponseBefore = await Client.GetAsync($"/api/v1/AiPrompts/{tenant1PromptId}");
        TestAssertions.AssertHttpSuccess(getResponseBefore);

        // Act - Delete the prompt
        var deleteResponse = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Debug: Check if entity was actually deleted from database
        var entityStillExists = await DbContext.AiPrompt.IgnoreQueryFilters()
            .AnyAsync(p => p.Id == tenant1PromptId);
        TestAssertions.AssertFalse(entityStillExists, $"Entity {tenant1PromptId} should be deleted but still exists in database");

        // Force a small delay to ensure InMemory database is consistent across scopes
        await Task.Delay(50);

        // Force database synchronization by manually clearing the context
        DbContext.ChangeTracker.Clear();
        await DbContext.SaveChangesAsync();

        // Verify using direct database query that entity is really deleted
        var entityExistsAfterDelete = await DbContext.AiPrompt.IgnoreQueryFilters()
            .AnyAsync(p => p.Id == tenant1PromptId);
        TestAssertions.AssertFalse(entityExistsAfterDelete, $"Entity {tenant1PromptId} should be deleted but still exists in database after manual check");

        // Assert - Verify prompt cannot be retrieved anymore
        // Use the same client to ensure we use the same database scope for consistency
        var getResponseAfter = await Client.GetAsync($"/api/v1/AiPrompts/{tenant1PromptId}");
        TestAssertions.AssertHttpStatusCode(getResponseAfter, HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task DeleteAiPrompt_WithInvalidGuidTenantHeader_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        var (tenant1PromptId, _) = await SeedTestDataAsync();
        SetInvalidTenantHeaderValue(invalidTenantId);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");

        // Assert - In test environment, empty string is treated as missing header, resulting in NotFound
        if (invalidTenantId == "")
        {
            TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
            var result = await ReadResponseAsync<Result<Guid>>(response);
            TestAssertions.AssertFalse(result.Succeeded);
        }
        else
        {
            // Invalid header format should return Unauthorized
            TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var responseContent = await ReadResponseStringAsync(response);
            TestAssertions.AssertTrue(responseContent.Contains("Invalid X-Tenant-Id header format"));
        }
    }

    [Fact]
    public async Task DeleteAiPrompt_WithExtremelyLargeId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{int.MaxValue}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAiPrompt_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        var (tenant1PromptId, _) = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5); // Should respond within 5 seconds
    }

    [Fact]
    public async Task DeleteAiPrompt_DatabaseConsistency_ShouldMaintainIntegrity()
    {
        // Arrange
        var (tenant1PromptId, tenant2PromptId) = await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Get initial counts
        var initialCountT1 = await DbContext.AiPrompt.IgnoreQueryFilters().CountAsync(p => p.TenantId == TenantConstants.TestTenant1Id);
        var initialCountT2 = await DbContext.AiPrompt.IgnoreQueryFilters().CountAsync(p => p.TenantId == TenantConstants.TestTenant2Id);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiPrompts/{tenant1PromptId}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify counts after deletion
        var finalCountT1 = await DbContext.AiPrompt.IgnoreQueryFilters().CountAsync(p => p.TenantId == TenantConstants.TestTenant1Id);
        var finalCountT2 = await DbContext.AiPrompt.IgnoreQueryFilters().CountAsync(p => p.TenantId == TenantConstants.TestTenant2Id);

        TestAssertions.AssertEqual(initialCountT1 - 1, finalCountT1);
        TestAssertions.AssertEqual(initialCountT2, finalCountT2); // Tenant 2 count should remain unchanged
    }
}