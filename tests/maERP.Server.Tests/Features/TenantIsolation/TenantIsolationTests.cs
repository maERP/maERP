using System.Net;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.TenantIsolation;

public class TenantIsolationTests : TenantIsolatedTestBase
{
    [Fact]
    public async Task ApiCall_WithInvalidTenantId_ShouldNotReturnOtherTenantData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(Guid.NewGuid()); // Non-existent tenant

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels");

        // Assert - In test environment with invalid tenant ID, should return empty data
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);

        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task ApiCall_WithTenant1_ShouldReturnTenant1DataOnly()
    {
        // Arrange - Seed test data
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act - Call with tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response = await Client.GetAsync("/api/v1/AiModels");
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Verify tenant 1 gets exactly its data (2 models)
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);

        // Verify the actual data belongs to tenant 1
        var modelNames = result.Data?.Select(x => x.Name).ToList() ?? new List<string>();
        TestAssertions.AssertTrue(modelNames.Contains("ChatGPT-4O Tenant 1"));
        TestAssertions.AssertTrue(modelNames.Contains("Claude 3.5 Tenant 1"));
        TestAssertions.AssertFalse(modelNames.Contains("ChatGPT-4O Tenant 2"));
    }

    [Fact]
    public async Task ApiCall_WithTenant2_ShouldReturnTenant2DataOnly()
    {
        // Arrange - Seed test data
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act - Call with tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response = await Client.GetAsync("/api/v1/AiModels");
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Verify tenant 2 gets exactly its data (1 model)
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);

        // Verify the actual data belongs to tenant 2
        var modelNames = result.Data?.Select(x => x.Name).ToList() ?? new List<string>();
        TestAssertions.AssertTrue(modelNames.Contains("ChatGPT-4O Tenant 2"));
        TestAssertions.AssertFalse(modelNames.Contains("ChatGPT-4O Tenant 1"));
        TestAssertions.AssertFalse(modelNames.Contains("Claude 3.5 Tenant 1"));
    }

    [Fact]
    public async Task ApiCall_WithoutTenantHeader_ShouldHaveEmptyTenantContext()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        RemoveTenantHeader(); // Ensure no tenant header

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels");

        // Assert - In testing environment we stay authenticated but tenant context is empty, so no tenant data should be returned
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);

        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task ApiCall_WithInvalidTenantHeaderValue_ShouldReturnEmptyData(string invalidTenantId)
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeaderValue(invalidTenantId);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels");

        // Assert - Invalid header format should return Unauthorized, except empty value behaves like missing header in test environment
        if (string.IsNullOrWhiteSpace(invalidTenantId))
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);

            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertEmpty(result.Data);
        }
        else
        {
            TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            var responseContent = await ReadResponseStringAsync(response);
            TestAssertions.AssertTrue(responseContent.Contains("Invalid X-Tenant-Id header format"));
        }
    }
}
