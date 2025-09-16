using System.Net;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;

namespace maERP.Server.Tests.Features.AiModel.Commands;

public class AiModelCreateCommandTests : TenantIsolatedTestBase
{

    [Fact]
    public async Task CreateAiModel_WithValidApiKey_ShouldReturnCreated()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createCommand = new AiModelInputDto
        {
            Name = "Test AI Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateAiModel_WithValidUsernamePassword_ShouldReturnCreated()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createCommand = new AiModelInputDto
        {
            Name = "Test AI Model Username",
            AiModelType = AiModelType.Claude35,
            ApiUsername = "testuser",
            ApiPassword = "testpass123",
            NCtx = 8192
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateAiModel_WithAllAiModelTypes_ShouldReturnCreated()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var aiModelTypes = new[] { AiModelType.Ollama, AiModelType.VLlm, AiModelType.LmStudio, AiModelType.ChatGpt4O, AiModelType.Claude35 };

        for (int i = 0; i < aiModelTypes.Length; i++)
        {
            var createCommand = new AiModelInputDto
            {
                Name = $"Test {aiModelTypes[i]} Model {i}",
                AiModelType = aiModelTypes[i],
                ApiKey = $"sk-test-key-{i}-123456789",
                NCtx = (uint)(1024 * (i + 1))
            };

            // Act
            var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

            // Assert
            TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
            var result = await ReadResponseAsync<Result<Guid>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertTrue(result.Data != Guid.Empty);
        }
    }

    [Fact]
    public async Task CreateAiModel_WithEmptyName_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createCommand = new AiModelInputDto
        {
            Name = "",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("Name")));
    }

    [Fact]
    public async Task CreateAiModel_WithTooLongName_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createCommand = new AiModelInputDto
        {
            Name = new string('A', 51), // 51 characters, exceeds 50 limit
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("50 characters")));
    }

    [Fact]
    public async Task CreateAiModel_WithInvalidAiModelType_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createCommand = new AiModelInputDto
        {
            Name = "Test AI Model",
            AiModelType = (AiModelType)999, // Invalid enum value
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("Ai Model Type")));
    }

    [Fact]
    public async Task CreateAiModel_WithoutAuthentication_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createCommand = new AiModelInputDto
        {
            Name = "Test AI Model",
            AiModelType = AiModelType.ChatGpt4O,
            // No ApiKey, ApiUsername, or ApiPassword provided
            NCtx = 4096
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("ApiKey") || m.Contains("ApiUsername")));
    }

    [Fact]
    public async Task CreateAiModel_WithTooShortApiKey_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createCommand = new AiModelInputDto
        {
            Name = "Test AI Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "short", // Less than 10 characters
            NCtx = 4096
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("Api Key") && m.Contains("10 characters")));
    }

    [Fact]
    public async Task CreateAiModel_WithIncompleteUsernamePassword_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createCommand = new AiModelInputDto
        {
            Name = "Test AI Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiUsername = "testuser", // Username without password
            NCtx = 4096
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("ApiPassword")));
    }

    [Fact]
    public async Task CreateAiModel_WithDuplicateName_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createCommand = new AiModelInputDto
        {
            Name = "Duplicate Name Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Create first model
        var firstResponse = await PostAsJsonAsync("/api/v1/AiModels", createCommand);
        TestAssertions.AssertHttpStatusCode(firstResponse, HttpStatusCode.Created);

        // Act - Try to create second model with same name
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("already exists")));
    }

    [Fact]
    public async Task CreateAiModel_WithDifferentTenants_ShouldAllowSameName()
    {
        // Arrange
        var createCommand = new AiModelInputDto
        {
            Name = "Cross-Tenant Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Create model in tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await PostAsJsonAsync("/api/v1/AiModels", createCommand);
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);

        // Act - Create model with same name in tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert - Should succeed because different tenants
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response2);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data != Guid.Empty);
    }

    [Fact]
    public async Task CreateAiModel_WithoutTenantHeader_ShouldHandleGracefully()
    {
        // Arrange
        RemoveTenantHeader();
        var createCommand = new AiModelInputDto
        {
            Name = "No Tenant Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act - No tenant header set
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert - Should still process the request (tenant context may default to null)
        TestAssertions.AssertHttpSuccess(response);
    }

    [Fact]
    public async Task CreateAiModel_WithInvalidTenantHeader_ShouldHandleGracefully()
    {
        // Arrange
        SetInvalidTenantHeader(); // Use helper method for non-existent tenant
        var createCommand = new AiModelInputDto
        {
            Name = "Invalid Tenant Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateAiModel_WithZeroNCtx_ShouldReturnCreated()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createCommand = new AiModelInputDto
        {
            Name = "Zero NCtx Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 0 // Zero context window
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateAiModel_WithLargeNCtx_ShouldReturnCreated()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createCommand = new AiModelInputDto
        {
            Name = "Large NCtx Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = uint.MaxValue // Maximum uint value
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }
}