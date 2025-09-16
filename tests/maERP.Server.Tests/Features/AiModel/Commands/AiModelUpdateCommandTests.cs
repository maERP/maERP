using System.Net;
using System.Net.Http.Json;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;

namespace maERP.Server.Tests.Features.AiModel.Commands;

public class AiModelUpdateCommandTests : TenantIsolatedTestBase
{
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

    [Fact]
    public async Task UpdateAiModel_WithValidData_ShouldReturnOk()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(createdId, result.Data);
    }

    [Fact]
    public async Task UpdateAiModel_WithUsernamePassword_ShouldReturnOk()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(createdId, result.Data);
    }

    [Fact]
    public async Task UpdateAiModel_WithNonExistentId_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = new AiModelInputDto
        {
            Name = "Non-existent Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync("/api/v1/AiModels/99999999-9999-9999-9999-999999999999", updateCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("not found") || m.Contains("AiModel")));
    }

    [Fact]
    public async Task UpdateAiModel_WithEmptyName_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("Name")));
    }

    [Fact]
    public async Task UpdateAiModel_WithTooLongName_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("50 characters")));
    }

    [Fact]
    public async Task UpdateAiModel_WithInvalidAiModelType_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("Ai Model Type")));
    }

    [Fact]
    public async Task UpdateAiModel_WithoutAuthentication_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("ApiKey") || m.Contains("ApiUsername")));
    }

    [Fact]
    public async Task UpdateAiModel_WithTooShortApiKey_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("Api Key") && m.Contains("10 characters")));
    }

    [Fact]
    public async Task UpdateAiModel_WithIncompleteUsernamePassword_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("ApiPassword")));
    }

    [Fact]
    public async Task UpdateAiModel_WithDuplicateName_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("already exists")));
    }

    [Fact]
    public async Task UpdateAiModel_WithSameName_ShouldReturnOk()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task UpdateAiModel_WithAllAiModelTypes_ShouldReturnOk()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
            var result = await ReadResponseAsync<Result<Guid>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertEqual(createdId, result.Data);
        }
    }

    [Fact]
    public async Task UpdateAiModel_WithZeroId_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateAiModel_WithNegativeId_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
    }

    [Fact]
    public async Task UpdateAiModel_WithStringId_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
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
    public async Task UpdateAiModel_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createdId = await CreateTestAiModel();

        // Remove tenant header
        RemoveTenantHeader();

        var updateCommand = new AiModelInputDto
        {
            Name = "Updated No Tenant Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert - Should return BadRequest when tenant header is missing
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateAiModel_WithInvalidTenantHeader_ShouldReturnBadRequest()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createdId = await CreateTestAiModel();

        // Set invalid tenant header (valid GUID but non-existent tenant)
        SetInvalidTenantHeader();

        var updateCommand = new AiModelInputDto
        {
            Name = "Updated Invalid Tenant Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert - Should return BadRequest for valid but non-existent tenant (resource not found)
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateAiModel_WithInvalidTenantHeaderFormat_ShouldReturnUnauthorized()
    {
        // Arrange
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createdId = await CreateTestAiModel();

        // Set invalid tenant header format
        SetInvalidTenantHeaderValue("invalid-tenant-id");

        var updateCommand = new AiModelInputDto
        {
            Name = "Updated Invalid Format Tenant Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert - Should return Unauthorized for invalid tenant header format
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateAiModel_CrossTenantAccess_ShouldReturnNotFound()
    {
        // Arrange - Create AI model in Tenant1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var createdId = await CreateTestAiModel("Tenant1 Model");

        // Try to update from Tenant2
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var updateCommand = new AiModelInputDto
        {
            Name = "Cross Tenant Updated Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{createdId}", updateCommand);

        // Assert - Should return NotFound when trying to access resource from different tenant
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertTrue(result.Messages.Any(m => m.Contains("not found") || m.Contains("AiModel")));
    }

    [Fact]
    public async Task UpdateAiModel_TenantIsolation_ShouldOnlyUpdateWithinSameTenant()
    {
        // Arrange - Create AI models in both tenants with same name
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var tenant1Id = await CreateTestAiModel("Shared Model Name");

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var tenant2Id = await CreateTestAiModel("Shared Model Name");

        // Update model in Tenant1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = new AiModelInputDto
        {
            Name = "Updated Tenant1 Model",
            AiModelType = AiModelType.Claude35,
            ApiKey = "sk-tenant1-key-123456789",
            NCtx = 8192
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/v1/AiModels/{tenant1Id}", updateCommand);

        // Assert - Update should succeed in Tenant1
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(tenant1Id, result.Data);

        // Verify model in Tenant2 remains unchanged by attempting to access it
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var tenant2Response = await Client.GetAsync($"/api/v1/AiModels/{tenant2Id}");
        TestAssertions.AssertHttpStatusCode(tenant2Response, HttpStatusCode.OK);
    }
}