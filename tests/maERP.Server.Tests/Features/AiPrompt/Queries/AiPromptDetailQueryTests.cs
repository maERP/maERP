using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Features.AiPrompt.Queries;

public class AiPromptDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public AiPromptDetailQueryTests()
    {
        // Create a unique factory per test class to ensure complete isolation
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_AiPromptDetailQueryTests_{uniqueId}";
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
    public async Task GetAiPromptById_WithValidIdAndTenant_ShouldReturnPromptDetails()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Id ?? 0);
        TestAssertions.AssertTrue(!string.IsNullOrEmpty(result.Data?.Identifier));
        TestAssertions.AssertTrue(!string.IsNullOrEmpty(result.Data?.PromptText));
    }

    [Fact]
    public async Task GetAiPromptById_WithValidIdButDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(2); // Try to access tenant 1's prompt with tenant 2 header

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/999");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(999); // Invalid tenant

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithZeroId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/0");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithNegativeId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/-1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_ResponseStructure_ShouldContainAllRequiredFields()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        var prompt = result.Data;
        TestAssertions.AssertTrue(prompt!.Id > 0);
        TestAssertions.AssertTrue(prompt.AiModelId > 0);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(prompt.Identifier));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(prompt.PromptText));
    }

    [Fact]
    public async Task GetAiPromptById_ForTenant2Prompt_ShouldReturnCorrectData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(2);

        // Act - Assuming test data contains a prompt with ID 3 for tenant 2
        var response = await Client.GetAsync("/api/v1/AiPrompts/3");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Id ?? 0);
        TestAssertions.AssertTrue(result.Data?.Identifier.Contains("Tenant 2") ?? false);
    }

    [Fact]
    public async Task GetAiPromptById_WithStringId_ShouldReturnBadRequest()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/abc");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetAiPromptById_WithNonExistentTenant_ShouldReturnNotFound()
    {
        // Arrange - Use a tenant that doesn't exist in seeded data
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(999); // Non-existent tenant

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var tasks = Enumerable.Range(1, 5).Select(_ => Client.GetAsync("/api/v1/AiPrompts/1")).ToArray();
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertEqual(1, result.Data?.Id ?? 0);
        }
    }

    [Fact]
    public async Task GetAiPromptById_WithDifferentTenants_ShouldReturnPromptData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Test tenant 1 can access prompt 1
        SetTenantHeader(1);
        var response1 = await Client.GetAsync("/api/v1/AiPrompts/1");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<Result<AiPromptDetailDto>>(response1);
        TestAssertions.AssertTrue(result1.Data?.Identifier.Contains("Tenant 1") ?? false);

        // Test tenant 2 can access prompt 3
        SetTenantHeader(2);
        var response3 = await Client.GetAsync("/api/v1/AiPrompts/3");
        TestAssertions.AssertHttpSuccess(response3);
        var result3 = await ReadResponseAsync<Result<AiPromptDetailDto>>(response3);
        TestAssertions.AssertTrue(result3.Data?.Identifier.Contains("Tenant 2") ?? false);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task GetAiPromptById_WithInvalidTenantHeaderValue_ShouldReturnNotFound(string invalidTenantId)
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_TenantIsolation_ShouldNotReturnDataFromOtherTenants()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Try to access tenant 1's prompt with tenant 2 header
        SetTenantHeader(2);
        var response = await Client.GetAsync("/api/v1/AiPrompts/1");
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Try to access tenant 2's prompt with tenant 1 header
        SetTenantHeader(1);
        var response2 = await Client.GetAsync("/api/v1/AiPrompts/3");
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithValidTenant1Prompt_ShouldReturnSuccessfulResult()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/2"); // Second prompt for tenant 1

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Id ?? 0);
        TestAssertions.AssertTrue(result.Data?.Identifier.Contains("Tenant 1") ?? false);
    }

    [Fact]
    public async Task GetAiPromptById_ResultStructure_ShouldHaveCorrectStatusCode()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task GetAiPromptById_WithNonExistentPrompt_ShouldReturnNotFoundResult()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/999");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/1");
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5); // Should respond within 5 seconds
    }

    [Fact]
    public async Task GetAiPromptById_MultipleValidIds_ShouldReturnCorrectPrompts()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act & Assert for multiple valid IDs
        foreach (var id in new[] { 1, 2 })
        {
            var response = await Client.GetAsync($"/api/v1/AiPrompts/{id}");
            TestAssertions.AssertHttpSuccess(response);
            
            var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertEqual(id, result.Data?.Id ?? 0);
        }
    }
}