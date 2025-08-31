using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Features.AiModel.Commands;

public class AiModelUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public AiModelUpdateCommandTests()
    {
        // Create a unique factory per test class to ensure complete isolation
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_AiModelUpdateCommandTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        // Ensure database is created for this test
        DbContext.Database.EnsureCreated();

        // Initialize tenant context with default tenants and reset current tenant
        TenantContext.SetAssignedTenantIds(new[] { 1, 2 });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(int tenantId)
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantId.ToString());
    }

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await Client.PostAsync(requestUri, content);
    }

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await Client.PutAsync(requestUri, content);
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

    protected async Task<int> CreateTestAiModel(string name = "Test Model", AiModelType aiModelType = AiModelType.ChatGpt4O)
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
        var result = await ReadResponseAsync<Result<int>>(response);
        return result.Data;
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateAiModel_WithValidData_ShouldReturnOk()
    {
        // Arrange
        SetTenantHeader(1);
        var createdId = await CreateTestAiModel();

        var updateCommand = new AiModelInputDto
        {
            Name = "Updated AI Model",
            AiModelType = AiModelType.Claude35,
            ApiKey = "sk-updated-key-123456789",
            NCtx = 8192
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(createdId, result.Data);
    }

    [Fact]
    public async Task UpdateAiModel_WithUsernamePassword_ShouldReturnOk()
    {
        // Arrange
        SetTenantHeader(1);
        var createdId = await CreateTestAiModel();

        var updateCommand = new AiModelInputDto
        {
            Name = "Updated Model with Credentials",
            AiModelType = AiModelType.Ollama,
            ApiUsername = "updateduser",
            ApiPassword = "updatedpass123",
            NCtx = 2048
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(createdId, result.Data);
    }

    [Fact]
    public async Task UpdateAiModel_WithNonExistentId_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(1);
        var updateCommand = new AiModelInputDto
        {
            Name = "Non-existent Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync("/api/v1/AiModels/999", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("not found") || m.Contains("AiModel")));
    }

    [Fact]
    public async Task UpdateAiModel_WithEmptyName_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(1);
        var createdId = await CreateTestAiModel();

        var updateCommand = new AiModelInputDto
        {
            Name = "",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("Name")));
    }

    [Fact]
    public async Task UpdateAiModel_WithTooLongName_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(1);
        var createdId = await CreateTestAiModel();

        var updateCommand = new AiModelInputDto
        {
            Name = new string('B', 51), // 51 characters, exceeds 50 limit
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("50 characters")));
    }

    [Fact]
    public async Task UpdateAiModel_WithInvalidAiModelType_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(1);
        var createdId = await CreateTestAiModel();

        var updateCommand = new AiModelInputDto
        {
            Name = "Updated Model",
            AiModelType = (AiModelType)999, // Invalid enum value
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("Ai Model Type")));
    }

    [Fact]
    public async Task UpdateAiModel_WithoutAuthentication_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(1);
        var createdId = await CreateTestAiModel();

        var updateCommand = new AiModelInputDto
        {
            Name = "Updated Model",
            AiModelType = AiModelType.ChatGpt4O,
            // No ApiKey, ApiUsername, or ApiPassword provided
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("ApiKey") || m.Contains("ApiUsername")));
    }

    [Fact]
    public async Task UpdateAiModel_WithTooShortApiKey_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(1);
        var createdId = await CreateTestAiModel();

        var updateCommand = new AiModelInputDto
        {
            Name = "Updated Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "short", // Less than 10 characters
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("Api Key") && m.Contains("10 characters")));
    }

    [Fact]
    public async Task UpdateAiModel_WithIncompleteUsernamePassword_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(1);
        var createdId = await CreateTestAiModel();

        var updateCommand = new AiModelInputDto
        {
            Name = "Updated Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiUsername = "testuser", // Username without password
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("ApiPassword")));
    }

    [Fact]
    public async Task UpdateAiModel_WithDuplicateName_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(1);
        var firstId = await CreateTestAiModel("First Model");
        var secondId = await CreateTestAiModel("Second Model");

        var updateCommand = new AiModelInputDto
        {
            Name = "First Model", // Try to update second model to have same name as first
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{secondId}", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("already exists")));
    }

    [Fact]
    public async Task UpdateAiModel_WithSameName_ShouldReturnOk()
    {
        // Arrange
        SetTenantHeader(1);
        var createdId = await CreateTestAiModel("Same Name Model");

        var updateCommand = new AiModelInputDto
        {
            Name = "Same Name Model", // Keep the same name
            AiModelType = AiModelType.Claude35, // Change other properties
            ApiKey = "sk-different-key-123456789",
            NCtx = 8192
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert - Should succeed because it's updating the same model
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task UpdateAiModel_WithAllAiModelTypes_ShouldReturnOk()
    {
        // Arrange
        SetTenantHeader(1);
        var createdId = await CreateTestAiModel();
        var aiModelTypes = new[] { AiModelType.Ollama, AiModelType.VLlm, AiModelType.LmStudio, AiModelType.ChatGpt4O, AiModelType.Claude35 };

        foreach (var aiModelType in aiModelTypes)
        {
            var updateCommand = new AiModelInputDto
            {
                Name = $"Updated {aiModelType} Model",
                AiModelType = aiModelType,
                ApiKey = $"sk-test-key-{aiModelType}-123456789",
                NCtx = 4096
            };

            // Act
            var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

            // Assert
            TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);
            var result = await ReadResponseAsync<Result<int>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertEqual(createdId, result.Data);
        }
    }

    [Fact]
    public async Task UpdateAiModel_WithZeroId_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(1);
        var updateCommand = new AiModelInputDto
        {
            Name = "Updated Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync("/api/v1/AiModels/0", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateAiModel_WithNegativeId_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(1);
        var updateCommand = new AiModelInputDto
        {
            Name = "Updated Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync("/api/v1/AiModels/-1", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateAiModel_WithStringId_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(1);
        var updateCommand = new AiModelInputDto
        {
            Name = "Updated Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync("/api/v1/AiModels/abc", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateAiModel_WithoutTenantHeader_ShouldHandleGracefully()
    {
        // Arrange
        SetTenantHeader(1);
        var createdId = await CreateTestAiModel();

        // Remove tenant header
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");

        var updateCommand = new AiModelInputDto
        {
            Name = "Updated No Tenant Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert - Should either succeed or fail gracefully
        TestAssertions.AssertTrue(response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound);
    }
}