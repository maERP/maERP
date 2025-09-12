using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Features.AiModel.Commands;

public class AiModelDeleteCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public AiModelDeleteCommandTests()
    {
        // Create a unique factory per test class to ensure complete isolation
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_AiModelDeleteCommandTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        // Ensure database is created for this test
        DbContext.Database.EnsureCreated();

        // Initialize tenant context with default tenants and reset current tenant
        TenantContext.SetAssignedTenantIds(new[] { TenantConstants.TestTenant1Id, TenantConstants.TestTenant2Id });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(Guid tenantId)
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantId.ToString());
    }

    protected void SetInvalidTenantHeader()
    {
        SetTenantHeader(Guid.Parse("99999999-9999-9999-9999-999999999999")); // Non-existent tenant ID for testing tenant isolation
    }

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
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

    protected async Task<Guid> CreateTestAiModel(string name = "Test Model", AiModelType aiModelType = AiModelType.ChatGpt4O)
    {
        var createCommand = new AiModelInputDto
        {
            Name = name,
            AiModelType = aiModelType,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
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
    public async Task DeleteAiModel_WithValidId_ShouldReturnNoContent()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createdId = await CreateTestAiModel();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiModels/{createdId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteAiModel_WithNonExistentId_ShouldReturnNoContent()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync("/api/v1/AiModels/99999999-9999-9999-9999-999999999999");

        // Assert - DELETE is idempotent, returns NoContent even if resource doesn't exist
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteAiModel_WithZeroId_ShouldReturnNoContent()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync("/api/v1/AiModels/00000000-0000-0000-0000-000000000000");

        // Assert - DELETE is idempotent, returns NoContent
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteAiModel_WithNegativeId_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync("/api/v1/AiModels/-1");

        // Assert - Invalid GUID format should return BadRequest
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteAiModel_WithStringId_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync("/api/v1/AiModels/abc");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteAiModel_MultipleDifferentModels_ShouldDeleteAll()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var modelIds = new List<Guid>();

        for (int i = 0; i < 5; i++)
        {
            var modelId = await CreateTestAiModel($"Model {i}");
            modelIds.Add(modelId);
        }

        // Act & Assert - Delete each model
        foreach (var modelId in modelIds)
        {
            var response = await Client.DeleteAsync($"/api/v1/AiModels/{modelId}");
            TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NoContent);
        }
    }

    [Fact]
    public async Task DeleteAiModel_SameIdTwice_ShouldSucceedBothTimes()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createdId = await CreateTestAiModel();

        // Act - First delete should succeed
        var firstResponse = await Client.DeleteAsync($"/api/v1/AiModels/{createdId}");
        TestAssertions.AssertHttpStatusCode(firstResponse, HttpStatusCode.NoContent);

        // Act - Second delete should also succeed (idempotent)
        var secondResponse = await Client.DeleteAsync($"/api/v1/AiModels/{createdId}");

        // Assert - DELETE is idempotent
        TestAssertions.AssertHttpStatusCode(secondResponse, HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteAiModel_AfterDeletion_ShouldNotBeAccessible()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createdId = await CreateTestAiModel();

        // Act - Delete the model
        var deleteResponse = await Client.DeleteAsync($"/api/v1/AiModels/{createdId}");
        TestAssertions.AssertHttpStatusCode(deleteResponse, HttpStatusCode.NoContent);

        // Act - Try to get the deleted model
        var getResponse = await Client.GetAsync($"/api/v1/AiModels/{createdId}");

        // Assert - Should return NotFound
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteAiModel_WithDifferentTenants_ShouldIsolateDeletion()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var tenant1ModelId = await CreateTestAiModel("Tenant 1 Model");

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var tenant2ModelId = await CreateTestAiModel("Tenant 2 Model");

        // Act - Try to delete tenant 1's model from tenant 2 context
        var response = await Client.DeleteAsync($"/api/v1/AiModels/{tenant1ModelId}");

        // Assert - Returns NoContent but doesn't actually delete due to tenant isolation
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NoContent);

        // Verify tenant 1 model still exists by switching back to tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse = await Client.GetAsync($"/api/v1/AiModels/{tenant1ModelId}");
        TestAssertions.AssertHttpSuccess(getResponse);
    }

    [Fact]
    public async Task DeleteAiModel_WithoutTenantHeader_ShouldHandleGracefully()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createdId = await CreateTestAiModel();

        // Remove tenant header
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiModels/{createdId}");

        // Assert - Should handle gracefully (could succeed or fail based on business logic)
        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.NoContent ||
                                 response.StatusCode == HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteAiModel_WithInvalidTenantHeader_ShouldReturnNoContent()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createdId = await CreateTestAiModel();

        SetInvalidTenantHeader();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/AiModels/{createdId}");

        // Assert - Returns NoContent due to idempotent behavior
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteAiModel_ConcurrentDeletions_ShouldHandleCorrectly()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var modelIds = new List<Guid>();
        for (int i = 0; i < 3; i++)
        {
            var modelId = await CreateTestAiModel($"Concurrent Model {i}");
            modelIds.Add(modelId);
        }

        // Act - Delete all models concurrently
        var deleteTasks = modelIds.Select(id => Client.DeleteAsync($"/api/v1/AiModels/{id}")).ToArray();
        var responses = await Task.WhenAll(deleteTasks);

        // Assert - All deletions should succeed
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NoContent);
        }
    }

    [Fact]
    public async Task DeleteAiModel_AllAiModelTypes_ShouldDeleteSuccessfully()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var aiModelTypes = new[] { AiModelType.Ollama, AiModelType.VLlm, AiModelType.LmStudio, AiModelType.ChatGpt4O, AiModelType.Claude35 };
        var modelIds = new List<Guid>();

        // Create models of different types
        for (int i = 0; i < aiModelTypes.Length; i++)
        {
            var createCommand = new AiModelInputDto
            {
                Name = $"Delete Test {aiModelTypes[i]} Model",
                AiModelType = aiModelTypes[i],
                ApiKey = $"sk-test-key-{i}-123456789",
                NCtx = 4096
            };

            var createResponse = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);
            TestAssertions.AssertHttpStatusCode(createResponse, HttpStatusCode.Created);
            var result = await ReadResponseAsync<Result<Guid>>(createResponse);
            modelIds.Add(result.Data);
        }

        // Act & Assert - Delete all models
        foreach (var modelId in modelIds)
        {
            var response = await Client.DeleteAsync($"/api/v1/AiModels/{modelId}");
            TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NoContent);
        }
    }

    [Fact]
    public async Task DeleteAiModel_CreatedWithDifferentAuth_ShouldDelete()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Create model with API key
        var apiKeyModel = new AiModelInputDto
        {
            Name = "API Key Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        var createResponse1 = await Client.PostAsJsonAsync("/api/v1/AiModels", apiKeyModel);
        var result1 = await ReadResponseAsync<Result<Guid>>(createResponse1);
        var apiKeyModelId = result1.Data;

        // Create model with username/password
        var userPassModel = new AiModelInputDto
        {
            Name = "Username Password Model",
            AiModelType = AiModelType.Claude35,
            ApiUsername = "testuser",
            ApiPassword = "testpass123",
            NCtx = 8192
        };

        var createResponse2 = await Client.PostAsJsonAsync("/api/v1/AiModels", userPassModel);
        var result2 = await ReadResponseAsync<Result<Guid>>(createResponse2);
        var userPassModelId = result2.Data;

        // Act - Delete both models
        var deleteResponse1 = await Client.DeleteAsync($"/api/v1/AiModels/{apiKeyModelId}");
        var deleteResponse2 = await Client.DeleteAsync($"/api/v1/AiModels/{userPassModelId}");

        // Assert - Both should succeed
        TestAssertions.AssertHttpStatusCode(deleteResponse1, HttpStatusCode.NoContent);
        TestAssertions.AssertHttpStatusCode(deleteResponse2, HttpStatusCode.NoContent);
    }
}