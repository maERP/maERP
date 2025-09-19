using System.Net;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;

namespace maERP.Server.Tests.Features.AiModel.Queries;

public class AiModelDetailQueryTests : TenantIsolatedTestBase
{
    [Fact]
    public async Task GetAiModelById_WithValidIdAndTenant_ShouldReturnModelDetails()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/20000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiModelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(Guid.Parse("20000001-0001-0001-0001-000000000001"), result.Data?.Id ?? Guid.Empty);
        TestAssertions.AssertEqual("ChatGPT-4O Tenant 1", result.Data?.Name);
        TestAssertions.AssertEqual((uint)4096, result.Data?.NCtx ?? 0);
    }

    [Fact]
    public async Task GetAiModelById_WithValidIdButDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant2Id); // Try to access tenant 1's model with tenant 2 header

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/20000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiModelById_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/99999999-9999-9999-9999-999999999999");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiModelById_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/20000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiModelById_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeader();

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/20000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiModelById_WithZeroId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/00000000-0000-0000-0000-000000000000");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiModelById_WithNegativeId_ShouldReturnBadRequest()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/-1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetAiModelById_ResponseStructure_ShouldContainAllRequiredFields()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/20000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiModelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        var model = result.Data;
        TestAssertions.AssertTrue(model.Id != Guid.Empty);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(model.Name));
        TestAssertions.AssertTrue(model.NCtx > 0);
        TestAssertions.AssertNotNull(model.ApiKey);
        TestAssertions.AssertNotNull(model.ApiUsername);
        TestAssertions.AssertNotNull(model.ApiPassword);
    }

    [Fact]
    public async Task GetAiModelById_ForTenant2Model_ShouldReturnCorrectData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant2Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/20000003-0003-0003-0003-000000000003");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<AiModelDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(Guid.Parse("20000003-0003-0003-0003-000000000003"), result.Data?.Id ?? Guid.Empty);
        TestAssertions.AssertEqual("ChatGPT-4O Tenant 2", result.Data?.Name);
    }

    [Fact]
    public async Task GetAiModelById_WithStringId_ShouldReturnBadRequest()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/abc");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetAiModelById_WithNonExistentTenant_ShouldReturnNotFound()
    {
        // Arrange - Use a tenant that doesn't exist in seeded data
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(Guid.Parse("99999999-9999-9999-9999-999999999999")); // Non-existent tenant

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/20000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAiModelById_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var tasks = Enumerable.Range(1, 5).Select(_ => Client.GetAsync("/api/v1/AiModels/20000001-0001-0001-0001-000000000001")).ToArray();
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<Result<AiModelDetailDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertEqual(Guid.Parse("20000001-0001-0001-0001-000000000001"), result.Data?.Id ?? Guid.Empty);
        }
    }

    [Fact]
    public async Task GetAiModelById_WithDifferentTenants_ShouldReturnModelData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Test tenant 1 can access model 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/AiModels/20000001-0001-0001-0001-000000000001");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<Result<AiModelDetailDto>>(response1);
        TestAssertions.AssertEqual("ChatGPT-4O Tenant 1", result1.Data?.Name);

        // Test tenant 2 can access model 3
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response3 = await Client.GetAsync("/api/v1/AiModels/20000003-0003-0003-0003-000000000003");
        TestAssertions.AssertHttpSuccess(response3);
        var result3 = await ReadResponseAsync<Result<AiModelDetailDto>>(response3);
        TestAssertions.AssertEqual("ChatGPT-4O Tenant 2", result3.Data?.Name);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    public async Task GetAiModelById_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeaderValue(invalidTenantId);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/20000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetAiModelById_WithEmptyTenantHeaderValue_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeaderValue("");

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels/20000001-0001-0001-0001-000000000001");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }
}