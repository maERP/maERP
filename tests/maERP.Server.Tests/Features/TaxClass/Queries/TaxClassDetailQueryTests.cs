using System.Net;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.TaxClass.Queries;

public class TaxClassDetailQueryTests : TenantIsolatedTestBase
{
    private async Task<Guid> CreateTestTaxClassAsync(Guid tenantId, double taxRate = 19.0)
    {
        TenantContext.SetCurrentTenantId(tenantId);

        var taxClass = new Domain.Entities.TaxClass
        {
            TaxRate = taxRate,
            TenantId = tenantId,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        DbContext.TaxClass.Add(taxClass);
        await DbContext.SaveChangesAsync();

        TenantContext.SetCurrentTenantId(null);
        return taxClass.Id;
    }

    private async Task SeedTestDataAsync()
    {
        var hasData = await DbContext.Tenant.IgnoreQueryFilters().AnyAsync();
        if (!hasData)
        {
            await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        }
    }

    [Fact]
    public async Task GetTaxClassById_WithValidIdAndTenant_ShouldReturnTaxClassDetails()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 15.5);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(taxClassId, result.Data!.Id);
        TestAssertions.AssertEqual(15.5, result.Data.TaxRate);
    }

    [Fact]
    public async Task GetTaxClassById_WithValidIdButDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 14.0);
        SetTenantHeader(TenantConstants.TestTenant2Id); // Try to access tenant 1's tax class with tenant 2 header

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Verify the tax class still exists for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var validResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        TestAssertions.AssertHttpSuccess(validResponse);
        var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(validResponse);
        TestAssertions.AssertEqual(14.0, result.Data!.TaxRate);
    }

    [Fact]
    public async Task GetTaxClassById_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{Guid.NewGuid()}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        RemoveTenantHeader();

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeader();

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_WithZeroId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{Guid.Empty}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_WithNegativeId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/invalid-guid");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_ResponseStructure_ShouldContainAllRequiredFields()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 23.5);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        var taxClass = result.Data;
        TestAssertions.AssertEqual(taxClassId, taxClass!.Id);
        TestAssertions.AssertEqual(23.5, taxClass.TaxRate);
        TestAssertions.AssertTrue(taxClass.TaxRate >= 0);
        TestAssertions.AssertTrue(taxClass.TaxRate <= 100); // Assuming tax rate is percentage
    }

    [Fact]
    public async Task GetTaxClassById_ForTenant2TaxClass_ShouldReturnCorrectData()
    {
        // Arrange
        await SeedTestDataAsync();
        var tenant2TaxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant2Id, 8.25);
        SetTenantHeader(TenantConstants.TestTenant2Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{tenant2TaxClassId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(tenant2TaxClassId, result.Data!.Id);
        TestAssertions.AssertEqual(8.25, result.Data.TaxRate);
    }

    [Fact]
    public async Task GetTaxClassById_WithStringId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/invalid-guid");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_TenantIsolation_ShouldNotReturnDataFromOtherTenants()
    {
        // Arrange
        await SeedTestDataAsync();
        var tenant1TaxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 12.5);
        var tenant2TaxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant2Id, 18.5);

        // Act & Assert - Try to access tenant 1's tax class with tenant 2 header
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var crossTenantResponse1 = await Client.GetAsync($"/api/v1/TaxClasses/{tenant1TaxClassId}");
        TestAssertions.AssertHttpStatusCode(crossTenantResponse1, HttpStatusCode.NotFound);

        // Verify tenant 2 can access their own tax class
        var tenant2Response = await Client.GetAsync($"/api/v1/TaxClasses/{tenant2TaxClassId}");
        TestAssertions.AssertHttpSuccess(tenant2Response);
        var tenant2Result = await ReadResponseAsync<Result<TaxClassDetailDto>>(tenant2Response);
        TestAssertions.AssertEqual(tenant2TaxClassId, tenant2Result.Data!.Id);
        TestAssertions.AssertEqual(18.5, tenant2Result.Data.TaxRate);

        // Act & Assert - Try to access tenant 2's tax class with tenant 1 header
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var crossTenantResponse2 = await Client.GetAsync($"/api/v1/TaxClasses/{tenant2TaxClassId}");
        TestAssertions.AssertHttpStatusCode(crossTenantResponse2, HttpStatusCode.NotFound);

        // Verify tenant 1 can access their own tax class
        var tenant1Response = await Client.GetAsync($"/api/v1/TaxClasses/{tenant1TaxClassId}");
        TestAssertions.AssertHttpSuccess(tenant1Response);
        var tenant1Result = await ReadResponseAsync<Result<TaxClassDetailDto>>(tenant1Response);
        TestAssertions.AssertEqual(tenant1TaxClassId, tenant1Result.Data!.Id);
        TestAssertions.AssertEqual(12.5, tenant1Result.Data.TaxRate);
    }

    [Fact]
    public async Task GetTaxClassById_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        // Get a tax class ID to test with
        var listResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse);
        var taxClassId = listResult.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(taxClassId);

        var tasks = Enumerable.Range(1, 5).Select(_ => Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}")).ToArray();
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertNotEqual(Guid.Empty, result.Data!.Id);
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    public async Task GetTaxClassById_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue(invalidTenantId);

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTaxClassById_WithEmptyTenantHeaderValue_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue("");

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_ResultStructure_ShouldHaveCorrectStatusCode()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        // Get a tax class ID dynamically
        var listResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse);
        var taxClassId = listResult.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(taxClassId);

        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task GetTaxClassById_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var startTime = DateTime.UtcNow;

        // Act
        // Get a tax class ID dynamically
        var listResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse);
        var taxClassId = listResult.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(taxClassId);

        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5); // Should respond within 5 seconds
    }

    [Fact]
    public async Task GetTaxClassById_MultipleValidIds_ShouldReturnCorrectTaxClasses()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act & Assert for multiple valid IDs
        // Get available tax class IDs for tenant 1
        var listResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse);
        var taxClassIds = listResult.Data?.Take(2).Select(tc => tc.Id).ToArray() ?? new Guid[0];
        
        foreach (var id in taxClassIds)
        {
            var response = await Client.GetAsync($"/api/v1/TaxClasses/{id}");
            TestAssertions.AssertHttpSuccess(response);

            var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertEqual(id, result.Data!.Id);
        }
    }

    [Fact]
    public async Task GetTaxClassById_AfterTenantSwitch_ShouldReturnCorrectData()
    {
        // Arrange
        await SeedTestDataAsync();
        var tenant1TaxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 21.0);
        var tenant2TaxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant2Id, 16.0);

        // Act & Assert - First request with tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync($"/api/v1/TaxClasses/{tenant1TaxClassId}");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<Result<TaxClassDetailDto>>(response1);
        TestAssertions.AssertEqual(tenant1TaxClassId, result1.Data!.Id);
        TestAssertions.AssertEqual(21.0, result1.Data.TaxRate);

        // Switch to tenant 2 and try accessing tenant 1's ID (should fail)
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var crossTenantResponse = await Client.GetAsync($"/api/v1/TaxClasses/{tenant1TaxClassId}");
        TestAssertions.AssertHttpStatusCode(crossTenantResponse, HttpStatusCode.NotFound);

        // Access tenant 2's own tax class (should succeed)
        var response2 = await Client.GetAsync($"/api/v1/TaxClasses/{tenant2TaxClassId}");
        TestAssertions.AssertHttpSuccess(response2);
        var result2 = await ReadResponseAsync<Result<TaxClassDetailDto>>(response2);
        TestAssertions.AssertEqual(tenant2TaxClassId, result2.Data!.Id);
        TestAssertions.AssertEqual(16.0, result2.Data.TaxRate);

        // Switch back to tenant 1 and try accessing tenant 2's ID (should fail)
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var crossTenantResponse2 = await Client.GetAsync($"/api/v1/TaxClasses/{tenant2TaxClassId}");
        TestAssertions.AssertHttpStatusCode(crossTenantResponse2, HttpStatusCode.NotFound);

        // Verify tenant 1 can still access their own tax class
        var response3 = await Client.GetAsync($"/api/v1/TaxClasses/{tenant1TaxClassId}");
        TestAssertions.AssertHttpSuccess(response3);
        var result3 = await ReadResponseAsync<Result<TaxClassDetailDto>>(response3);
        TestAssertions.AssertEqual(tenant1TaxClassId, result3.Data!.Id);
        TestAssertions.AssertEqual(21.0, result3.Data.TaxRate);
    }

    [Fact]
    public async Task GetTaxClassById_WithMaxIntId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{Guid.NewGuid()}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_WithSpecialCharactersInUrl_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/invalid%20OR%201=1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_VerifyTaxRateValidation_ShouldReturnValidTaxRate()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        // Get a tax class ID dynamically
        var listResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse);
        var taxClassId = listResult.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(taxClassId);

        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Tax rate should be within valid range (0-100 for percentage)
        TestAssertions.AssertTrue(result.Data!.TaxRate >= 0);
        TestAssertions.AssertTrue(result.Data.TaxRate <= 100);
    }
}