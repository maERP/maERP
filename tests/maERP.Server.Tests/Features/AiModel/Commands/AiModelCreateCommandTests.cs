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

public class AiModelCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public AiModelCreateCommandTests()
    {
        // Create a unique factory per test class to ensure complete isolation
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_AiModelCreateCommandTests_{uniqueId}";
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

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

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
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
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
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
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
            var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

            // Assert
            TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
            var result = await ReadResponseAsync<Result<int>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertTrue(result.Data > 0);
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
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
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
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
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
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
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
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
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
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
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
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
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
        var firstResponse = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);
        TestAssertions.AssertHttpStatusCode(firstResponse, HttpStatusCode.Created);

        // Act - Try to create second model with same name
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
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
        var response1 = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);

        // Act - Create model with same name in tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert - Should succeed because different tenants
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response2);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
    }

    [Fact]
    public async Task CreateAiModel_WithoutTenantHeader_ShouldHandleGracefully()
    {
        // Arrange
        var createCommand = new AiModelInputDto
        {
            Name = "No Tenant Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act - No tenant header set
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert - Should still process the request (tenant context may default to null)
        TestAssertions.AssertHttpSuccess(response);
    }

    [Fact]
    public async Task CreateAiModel_WithInvalidTenantHeader_ShouldHandleGracefully()
    {
        // Arrange
        SetTenantHeader(Guid.Parse("99999999-9999-9999-9999-999999999999")); // Invalid tenant
        var createCommand = new AiModelInputDto
        {
            Name = "Invalid Tenant Model",
            AiModelType = AiModelType.ChatGpt4O,
            ApiKey = "sk-test-key-123456789",
            NCtx = 4096
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<int>>(response);
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
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
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
        var response = await Client.PostAsJsonAsync("/api/v1/AiModels", createCommand);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }
}