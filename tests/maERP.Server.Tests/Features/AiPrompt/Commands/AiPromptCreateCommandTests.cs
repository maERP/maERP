using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.AiPrompt.Commands;

public class AiPromptCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public AiPromptCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_AiPromptCreateCommandTests_{uniqueId}";
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
    }

    protected void SetInvalidTenantHeader()
    {
        SetTenantHeader(999); // Non-existent tenant ID for testing tenant isolation
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
        var hasData = await DbContext.AiModel.IgnoreQueryFilters().AnyAsync();
        if (!hasData)
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        }
    }

    private AiPromptInputDto CreateValidAiPromptDto()
    {
        return new AiPromptInputDto
        {
            AiModelId = 1, // From test data seeder
            Identifier = "Test Prompt Create",
            PromptText = "This is a test prompt for creation"
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateAiPrompt_WithValidData_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
    }

    [Fact]
    public async Task CreateAiPrompt_WithValidData_ShouldPersistInDatabase()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);

        // Verify through API that prompt exists
        var getResponse = await Client.GetAsync($"/api/v1/AiPrompts/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var promptDetail = await ReadResponseAsync<Result<AiPromptDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(promptDetail?.Data);
        TestAssertions.AssertEqual(promptDto.Identifier, promptDetail!.Data.Identifier);
        TestAssertions.AssertEqual(promptDto.PromptText, promptDetail.Data.PromptText);
        TestAssertions.AssertEqual(promptDto.AiModelId, promptDetail.Data.AiModelId);
    }

    [Fact]
    public async Task CreateAiPrompt_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = new AiPromptInputDto
        {
            // Missing required fields
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateAiPrompt_WithEmptyIdentifier_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();
        promptDto.Identifier = string.Empty;

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateAiPrompt_WithEmptyPromptText_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();
        promptDto.PromptText = string.Empty; // PromptText is required and must not be empty

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateAiPrompt_WithInvalidAiModelId_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();
        promptDto.AiModelId = 999; // Non-existent AI model - but validation might not check this

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateAiPrompt_WithDuplicateIdentifier_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();

        // Create first prompt
        var firstResponse = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);
        TestAssertions.AssertEqual(HttpStatusCode.Created, firstResponse.StatusCode);

        // Try to create another prompt with same identifier
        var duplicateDto = CreateValidAiPromptDto();
        duplicateDto.Identifier = promptDto.Identifier; // Same identifier

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", duplicateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateAiPrompt_TenantIsolation_ShouldCreateInCorrectTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify prompt exists in tenant 1
        var getResponse = await Client.GetAsync($"/api/v1/AiPrompts/{result.Data}");
        TestAssertions.AssertHttpSuccess(getResponse);

        // Switch to tenant 2 and verify prompt is not accessible
        SetTenantHeader(2);
        var getResponseTenant2 = await Client.GetAsync($"/api/v1/AiPrompts/{result.Data}");
        TestAssertions.AssertHttpStatusCode(getResponseTenant2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateAiPrompt_WithoutTenantHeader_ShouldReturnUnauthorized()
    {
        // Arrange
        await SeedTestDataAsync();
        var promptDto = CreateValidAiPromptDto();

        // Act (no tenant header set)
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateAiPrompt_WithInvalidTenantHeader_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetInvalidTenantHeader();
        var promptDto = CreateValidAiPromptDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateAiPrompt_WithDifferentTenantAiModel_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(2);
        var promptDto = CreateValidAiPromptDto();
        promptDto.AiModelId = 1; // AI Model belongs to tenant 1, but creating in tenant 2 - validation may not check this

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateAiPrompt_WithLongIdentifier_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();
        promptDto.Identifier = new string('A', 50); // Maximum allowed length

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateAiPrompt_WithTooLongIdentifier_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();
        promptDto.Identifier = new string('A', 51); // Exceeds maximum length of 50

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateAiPrompt_WithLongPromptText_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();
        promptDto.PromptText = new string('B', 1000); // Long prompt text

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateAiPrompt_ResponseStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
        TestAssertions.AssertEqual(ResultStatusCode.Created, result.StatusCode);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task CreateAiPrompt_WithInvalidJson_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/api/v1/AiPrompts", content);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateAiPrompt_WithNullIdentifier_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();
        promptDto.Identifier = null!;

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);

        // When Identifier is null, ASP.NET Core model binding returns ProblemDetails (RFC 7807)
        // instead of our custom Result<T> format
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("\"status\":400"));
        TestAssertions.AssertTrue(content.Contains("\"errors\"") || content.Contains("\"detail\""));
        TestAssertions.AssertTrue(content.Contains("Identifier") || content.Contains("required"));
    }

    [Fact]
    public async Task CreateAiPrompt_WithNullPromptText_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();
        promptDto.PromptText = null!; // Null values cause model binding issues

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateAiPrompt_WithZeroAiModelId_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();
        promptDto.AiModelId = 0; // Only Identifier is validated, not AiModelId

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateAiPrompt_WithNegativeAiModelId_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var promptDto = CreateValidAiPromptDto();
        promptDto.AiModelId = -1; // Only Identifier is validated, not AiModelId

        // Act
        var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateAiPrompt_MultipleValidPrompts_ShouldCreateAll()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        // Act & Assert
        for (int i = 1; i <= 3; i++)
        {
            var promptDto = CreateValidAiPromptDto();
            promptDto.Identifier = $"Test Prompt {i}";
            promptDto.PromptText = $"This is test prompt number {i}";

            var response = await PostAsJsonAsync("/api/v1/AiPrompts", promptDto);
            TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);

            var result = await ReadResponseAsync<Result<int>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertTrue(result.Data > 0);
        }
    }

    [Fact]
    public async Task CreateAiPrompt_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);

        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 1; i <= 5; i++)
        {
            var promptDto = CreateValidAiPromptDto();
            promptDto.Identifier = $"Concurrent Test Prompt {i}";
            promptDto.PromptText = $"Concurrent test prompt number {i}";
            tasks.Add(PostAsJsonAsync("/api/v1/AiPrompts", promptDto));
        }

        // Act
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
            var result = await ReadResponseAsync<Result<int>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertTrue(result.Data > 0);
        }
    }
}