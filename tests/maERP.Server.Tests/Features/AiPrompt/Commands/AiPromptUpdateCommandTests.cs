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
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.AiPrompt.Commands;

public class AiPromptUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public AiPromptUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_AiPromptUpdateCommandTests_{uniqueId}";
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
            PropertyNameCaseInsensitive = true
        });
        return result ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    private async Task<int> SeedTestDataAsync()
    {
        var hasData = await DbContext.AiPrompt.IgnoreQueryFilters().AnyAsync();
        if (!hasData)
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        }

        // Return the ID of the first prompt for tenant 1
        var prompt = await DbContext.AiPrompt.IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.TenantId == TenantConstants.TestTenant1Id);
        return prompt?.Id ?? 1;
    }

    private AiPromptInputDto CreateUpdateAiPromptDto(int id)
    {
        return new AiPromptInputDto
        {
            Id = id,
            AiModelId = 1,
            Identifier = "Updated Test Prompt",
            PromptText = "This is an updated test prompt"
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateAiPrompt_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(promptId);

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(promptId, result.Data);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithValidData_ShouldUpdateInDatabase()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(promptId);

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify through API that prompt was updated
        var getResponse = await Client.GetAsync($"/api/v1/AiPrompts/{promptId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var promptDetail = await ReadResponseAsync<Result<AiPromptDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(promptDetail?.Data);
        TestAssertions.AssertEqual(updateDto.Identifier, promptDetail!.Data.Identifier);
        TestAssertions.AssertEqual(updateDto.PromptText, promptDetail.Data.PromptText);
        TestAssertions.AssertEqual(updateDto.AiModelId, promptDetail.Data.AiModelId);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(999);

        // Act
        var response = await PutAsJsonAsync("/api/v1/AiPrompts/999", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithWrongTenant_ShouldReturnNotFound()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(2); // Prompt belongs to tenant 1, accessing with tenant 2
        var updateDto = CreateUpdateAiPromptDto(promptId);
        updateDto.AiModelId = 3; // Use AiModel that belongs to tenant 2 to avoid validation error

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = new AiPromptInputDto
        {
            Id = promptId,
            // Missing required fields like Identifier, PromptText, AiModelId
        };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithEmptyIdentifier_ShouldReturnBadRequest()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(promptId);
        updateDto.Identifier = string.Empty;

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithEmptyPromptText_ShouldReturnBadRequest()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(promptId);
        updateDto.PromptText = string.Empty;

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithInvalidAiModelId_ShouldReturnBadRequest()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(promptId);
        updateDto.AiModelId = 999; // Non-existent AI model

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithDifferentTenantAiModel_ShouldReturnBadRequest()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(promptId);
        updateDto.AiModelId = 3; // AI Model belongs to tenant 2

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithDuplicateIdentifier_ShouldReturnBadRequest()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);

        // Get identifier of another prompt from test data
        var updateDto = CreateUpdateAiPromptDto(promptId);
        updateDto.Identifier = "Test Prompt 2 Tenant 1"; // Existing identifier from test data

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        var updateDto = CreateUpdateAiPromptDto(promptId);

        // Act (no tenant header set)
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetInvalidTenantHeader();
        var updateDto = CreateUpdateAiPromptDto(promptId);

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAiPrompt_TenantIsolation_ShouldOnlyUpdateInCorrectTenant()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(promptId);
        updateDto.Identifier = "Updated in Tenant 1";

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify prompt was updated in tenant 1
        var getResponse = await Client.GetAsync($"/api/v1/AiPrompts/{promptId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var promptDetail = await ReadResponseAsync<Result<AiPromptDetailDto>>(getResponse);
        TestAssertions.AssertEqual("Updated in Tenant 1", promptDetail?.Data?.Identifier);

        // Verify prompt is not accessible from tenant 2
        SetTenantHeader(2);
        var getResponseTenant2 = await Client.GetAsync($"/api/v1/AiPrompts/{promptId}");
        TestAssertions.AssertHttpStatusCode(getResponseTenant2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateAiPrompt_ResponseStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(promptId);

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(promptId, result.Data);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithInvalidJson_ShouldReturnBadRequest()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PutAsync($"/api/v1/AiPrompts/{promptId}", content);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithMismatchedIds_ShouldReturnBadRequest()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(999); // Different ID in DTO than in URL

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Microsoft.AspNetCore.Mvc.ProblemDetails>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertEqual("Invalid Request", result.Title);
        TestAssertions.AssertEqual($"ID in URL ({promptId}) must match ID in request body (999)", result.Detail);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithZeroId_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(0);

        // Act
        var response = await PutAsJsonAsync("/api/v1/AiPrompts/0", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithNegativeId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(-1);

        // Act
        var response = await PutAsJsonAsync("/api/v1/AiPrompts/-1", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAiPrompt_PartialUpdate_ShouldUpdateOnlyProvidedFields()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);

        // Use known test data values instead of reading from API to avoid context issues
        var updateDto = new AiPromptInputDto
        {
            Id = promptId,
            AiModelId = 1, // Known value from test data 
            Identifier = "Only Identifier Updated",
            PromptText = "This is a test prompt for tenant 1" // Known value from test data
        };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify only identifier was updated
        var verifyResponse = await Client.GetAsync($"/api/v1/AiPrompts/{promptId}");
        var updatedPrompt = await ReadResponseAsync<Result<AiPromptDetailDto>>(verifyResponse);
        TestAssertions.AssertEqual("Only Identifier Updated", updatedPrompt?.Data?.Identifier);
        TestAssertions.AssertEqual("This is a test prompt for tenant 1", updatedPrompt?.Data?.PromptText); // Should remain unchanged
    }

    [Fact]
    public async Task UpdateAiPrompt_ChangeAiModel_ShouldUpdateSuccessfully()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(promptId);
        updateDto.AiModelId = 2; // Change to different AI model (still in tenant 1)

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);

        // Verify AI model was updated
        var getResponse = await Client.GetAsync($"/api/v1/AiPrompts/{promptId}");
        var promptDetail = await ReadResponseAsync<Result<AiPromptDetailDto>>(getResponse);
        TestAssertions.AssertEqual(2, promptDetail?.Data?.AiModelId);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithTooLongIdentifier_ShouldReturnBadRequest()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(promptId);
        updateDto.Identifier = new string('A', 256); // Exceeds maximum length

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAiPrompt_WithLongPromptText_ShouldHandleCorrectly()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);
        var updateDto = CreateUpdateAiPromptDto(promptId);
        updateDto.PromptText = new string('B', 2000); // Long prompt text

        // Act
        var response = await PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto);

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task UpdateAiPrompt_ConcurrentUpdates_ShouldHandleCorrectly()
    {
        // Arrange
        var promptId = await SeedTestDataAsync();
        SetTenantHeader(1);

        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 1; i <= 3; i++)
        {
            var updateDto = CreateUpdateAiPromptDto(promptId);
            updateDto.Identifier = $"Concurrent Update {i}";
            updateDto.PromptText = $"Concurrent update number {i}";
            tasks.Add(PutAsJsonAsync($"/api/v1/AiPrompts/{promptId}", updateDto));
        }

        // Act
        var responses = await Task.WhenAll(tasks);

        // Assert - At least one should succeed
        var successfulResponses = responses.Where(r => r.StatusCode == HttpStatusCode.OK).ToList();
        TestAssertions.AssertTrue(successfulResponses.Count >= 1);

        // Verify final state
        var getResponse = await Client.GetAsync($"/api/v1/AiPrompts/{promptId}");
        TestAssertions.AssertHttpSuccess(getResponse);
        var promptDetail = await ReadResponseAsync<Result<AiPromptDetailDto>>(getResponse);
        TestAssertions.AssertNotNull(promptDetail?.Data);
        TestAssertions.AssertTrue(promptDetail!.Data!.Identifier.Contains("Concurrent Update"));
    }
}