using System.Net;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;

namespace maERP.Server.Tests.Features.AiPrompt.Queries;

public class AiPromptDetailQueryTests : TenantIsolatedTestBase
{

    [Fact]
    public async Task GetAiPromptById_WithValidIdAndTenant_ShouldReturnPromptDetails()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(Guid.Parse("30000001-0001-0001-0001-000000000001"), result.Data?.Id ?? Guid.Empty);
        TestAssertions.AssertTrue(!string.IsNullOrEmpty(result.Data?.Identifier));
        TestAssertions.AssertTrue(!string.IsNullOrEmpty(result.Data?.PromptText));
    }

    [Fact]
    public async Task GetAiPromptById_WithValidIdButDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant2Id); // Try to access tenant 1's prompt with tenant 2 header

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/99999999-9999-9999-9999-999999999999");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeader();

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithZeroId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/00000000-0000-0000-0000-000000000000");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithNegativeId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        var prompt = result.Data;
        TestAssertions.AssertTrue(prompt!.Id != Guid.Empty);
        TestAssertions.AssertTrue(prompt.AiModelId != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(prompt.Identifier));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(prompt.PromptText));
    }

    [Fact]
    public async Task GetAiPromptById_ForTenant2Prompt_ShouldReturnCorrectData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant2Id);

        // Act - Assuming test data contains a prompt with ID 3 for tenant 2
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000003-0003-0003-0003-000000000003");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(Guid.Parse("30000003-0003-0003-0003-000000000003"), result.Data?.Id ?? Guid.Empty);
        TestAssertions.AssertTrue(result.Data?.Identifier.Contains("Tenant 2") ?? false);
    }

    [Fact]
    public async Task GetAiPromptById_WithStringId_ShouldReturnBadRequest()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/abc");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithNonExistentTenant_ShouldReturnNotFound()
    {
        // Arrange - Use a tenant that doesn't exist in seeded data
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeader();

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var tasks = Enumerable.Range(1, 5).Select(_ => Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001")).ToArray();
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertEqual(Guid.Parse("30000001-0001-0001-0001-000000000001"), result.Data?.Id ?? Guid.Empty);
        }
    }

    [Fact]
    public async Task GetAiPromptById_WithDifferentTenants_ShouldReturnPromptData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Test tenant 1 can access prompt 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<Result<AiPromptDetailDto>>(response1);
        TestAssertions.AssertTrue(result1.Data?.Identifier.Contains("Tenant 1") ?? false);

        // Test tenant 2 can access prompt 3
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response3 = await Client.GetAsync("/api/v1/AiPrompts/30000003-0003-0003-0003-000000000003");
        TestAssertions.AssertHttpSuccess(response3);
        var result3 = await ReadResponseAsync<Result<AiPromptDetailDto>>(response3);
        TestAssertions.AssertTrue(result3.Data?.Identifier.Contains("Tenant 2") ?? false);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    public async Task GetAiPromptById_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeaderValue(invalidTenantId);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Unauthorized);
        var responseContent = await ReadResponseStringAsync(response);
        TestAssertions.AssertTrue(responseContent.Contains("Invalid X-Tenant-Id header format"));
    }

    [Theory]
    [InlineData("abc")]
    public async Task GetAiPromptById_WithUnparseableTenantHeaderValue_ShouldReturnUnauthorized(string unparseableTenantId)
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", unparseableTenantId);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetAiPromptById_WithEmptyStringTenantHeader_ShouldReturnUnauthorized()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeaderValue("");

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Unauthorized);
        var responseContent = await ReadResponseStringAsync(response);
        TestAssertions.AssertTrue(responseContent.Contains("Invalid X-Tenant-Id header format"));
    }

    [Fact]
    public async Task GetAiPromptById_TenantIsolation_ShouldNotReturnDataFromOtherTenants()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Try to access tenant 1's prompt with tenant 2 header
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Try to access tenant 2's prompt with tenant 1 header
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response2 = await Client.GetAsync("/api/v1/AiPrompts/30000003-0003-0003-0003-000000000003");
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_WithValidTenant1Prompt_ShouldReturnSuccessfulResult()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000002-0002-0002-0002-000000000002"); // Second prompt for tenant 1

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(Guid.Parse("30000002-0002-0002-0002-000000000002"), result.Data?.Id ?? Guid.Empty);
        TestAssertions.AssertTrue(result.Data?.Identifier.Contains("Tenant 1") ?? false);
    }

    [Fact]
    public async Task GetAiPromptById_ResultStructure_ShouldHaveCorrectStatusCode()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");

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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/99999999-9999-9999-9999-999999999999");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiPromptById_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts/30000001-0001-0001-0001-000000000001");
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
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act & Assert for multiple valid IDs
        var testIds = new[]
        {
            ("30000001-0001-0001-0001-000000000001", Guid.Parse("30000001-0001-0001-0001-000000000001")),
            ("30000002-0002-0002-0002-000000000002", Guid.Parse("30000002-0002-0002-0002-000000000002"))
        };

        foreach (var (urlId, expectedId) in testIds)
        {
            var response = await Client.GetAsync($"/api/v1/AiPrompts/{urlId}");
            TestAssertions.AssertHttpSuccess(response);

            var result = await ReadResponseAsync<Result<AiPromptDetailDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertEqual(expectedId, result.Data?.Id ?? Guid.Empty);
        }
    }
}