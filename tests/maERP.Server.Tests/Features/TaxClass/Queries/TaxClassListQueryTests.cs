using System.Net;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.TaxClass.Queries;

public class TaxClassListQueryTests : TenantIsolatedTestBase
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
    public async Task GetTaxClassList_WithValidTenant_ShouldReturnPaginatedResults()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 15.5);
        await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 22.0);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data.Count >= 2); // At least our 2 created tax classes
        TestAssertions.AssertTrue(result.TotalCount >= 2);
    }

    [Fact]
    public async Task GetTaxClassList_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        RemoveTenantHeader();

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetTaxClassList_WithInvalidTenant_ShouldReturnEmptyResult()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeader();

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetTaxClassList_WithPagination_ShouldReturnCorrectPage()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses?pageNumber=1&pageSize=5");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.CurrentPage);
        TestAssertions.AssertEqual(5, result.PageSize);
        TestAssertions.AssertTrue(result.Data.Count <= 5);
    }

    [Fact]
    public async Task GetTaxClassList_WithSearchString_ShouldFilterResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses?searchString=19");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify that results contain the search string
        if (result.Data.Any())
        {
            TestAssertions.AssertTrue(result.Data.Any(tc => tc.TaxRate.ToString().Contains("19")));
        }
    }

    [Fact]
    public async Task GetTaxClassList_WithOrderBy_ShouldReturnOrderedResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses?orderBy=TaxRate");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify ordering
        if (result.Data.Count > 1)
        {
            for (int i = 0; i < result.Data.Count - 1; i++)
            {
                TestAssertions.AssertTrue(result.Data[i].TaxRate <= result.Data[i + 1].TaxRate);
            }
        }
    }

    [Fact]
    public async Task GetTaxClassList_TenantIsolation_ShouldReturnOnlyTenantData()
    {
        // Arrange
        await SeedTestDataAsync();
        var tenant1Tax1 = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 12.5);
        var tenant1Tax2 = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 18.0);
        var tenant2Tax1 = await CreateTestTaxClassAsync(TenantConstants.TestTenant2Id, 25.5);
        var tenant2Tax2 = await CreateTestTaxClassAsync(TenantConstants.TestTenant2Id, 8.25);

        // Act - Get data for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/TaxClasses");
        var result1 = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response1);

        // Act - Get data for tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/TaxClasses");
        var result2 = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response2);

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpSuccess(response2);
        TestAssertions.AssertNotNull(result1);
        TestAssertions.AssertNotNull(result2);

        // Verify tenant 1 sees their tax classes
        var tenant1Ids = result1.Data.Select(tc => tc.Id).ToList();
        TestAssertions.AssertTrue(tenant1Ids.Contains(tenant1Tax1));
        TestAssertions.AssertTrue(tenant1Ids.Contains(tenant1Tax2));
        TestAssertions.AssertFalse(tenant1Ids.Contains(tenant2Tax1));
        TestAssertions.AssertFalse(tenant1Ids.Contains(tenant2Tax2));

        // Verify tenant 2 sees their tax classes
        var tenant2Ids = result2.Data.Select(tc => tc.Id).ToList();
        TestAssertions.AssertTrue(tenant2Ids.Contains(tenant2Tax1));
        TestAssertions.AssertTrue(tenant2Ids.Contains(tenant2Tax2));
        TestAssertions.AssertFalse(tenant2Ids.Contains(tenant1Tax1));
        TestAssertions.AssertFalse(tenant2Ids.Contains(tenant1Tax2));

        // Verify tax rates are correct
        var tenant1Tax1Data = result1.Data.First(tc => tc.Id == tenant1Tax1);
        var tenant1Tax2Data = result1.Data.First(tc => tc.Id == tenant1Tax2);
        TestAssertions.AssertEqual(12.5, tenant1Tax1Data.TaxRate);
        TestAssertions.AssertEqual(18.0, tenant1Tax2Data.TaxRate);

        var tenant2Tax1Data = result2.Data.First(tc => tc.Id == tenant2Tax1);
        var tenant2Tax2Data = result2.Data.First(tc => tc.Id == tenant2Tax2);
        TestAssertions.AssertEqual(25.5, tenant2Tax1Data.TaxRate);
        TestAssertions.AssertEqual(8.25, tenant2Tax2Data.TaxRate);
    }

    [Fact]
    public async Task GetTaxClassList_WithLargePageSize_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses?pageNumber=1&pageSize=1000");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1000, result.PageSize);
    }

    [Fact]
    public async Task GetTaxClassList_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses?pageNumber=999&pageSize=10");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data.Count);
    }

    [Fact]
    public async Task GetTaxClassList_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses?pageNumber=1&pageSize=0");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.PageSize > 0); // Should use default page size
    }

    [Fact]
    public async Task GetTaxClassList_WithNegativePageNumber_ShouldUseDefaultPageNumber()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses?pageNumber=-1&pageSize=10");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.CurrentPage >= 0);
    }

    [Fact]
    public async Task GetTaxClassList_ResponseStructure_ShouldContainAllMetadata()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses?pageNumber=1&pageSize=5");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.TotalCount >= 0);
        TestAssertions.AssertTrue(result.TotalPages >= 0);
        TestAssertions.AssertTrue(result.CurrentPage > 0);
        TestAssertions.AssertTrue(result.PageSize > 0);
        TestAssertions.AssertNotNull(result.Data);
    }

    [Fact]
    public async Task GetTaxClassList_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var tasks = Enumerable.Range(1, 5)
            .Select(_ => Client.GetAsync("/api/v1/TaxClasses"))
            .ToArray();
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    public async Task GetTaxClassList_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue(invalidTenantId);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetTaxClassList_WithEmptyTenantHeaderValue_ShouldReturnEmptyResults()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue("");

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data.Count);
    }

    [Fact]
    public async Task GetTaxClassList_WithMultipleOrderByFields_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses?orderBy=TaxRate,Id");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task GetTaxClassList_EmptySearchString_ShouldReturnAllResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var responseWithoutSearch = await Client.GetAsync("/api/v1/TaxClasses");
        var responseWithEmptySearch = await Client.GetAsync("/api/v1/TaxClasses?searchString=");

        // Assert
        var resultWithoutSearch = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(responseWithoutSearch);
        var resultWithEmptySearch = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(responseWithEmptySearch);

        TestAssertions.AssertEqual(resultWithoutSearch.TotalCount, resultWithEmptySearch.TotalCount);
    }

    [Fact]
    public async Task GetTaxClassList_AfterTenantSwitch_ShouldReturnCorrectData()
    {
        // Arrange
        await SeedTestDataAsync();
        var tenant1Tax1 = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 16.5);
        var tenant1Tax2 = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 21.0);
        var tenant2Tax1 = await CreateTestTaxClassAsync(TenantConstants.TestTenant2Id, 9.75);

        // Act - First request with tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/TaxClasses");
        var result1 = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response1);
        TestAssertions.AssertHttpSuccess(response1);

        // Switch to tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/TaxClasses");
        var result2 = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response2);
        TestAssertions.AssertHttpSuccess(response2);

        // Switch back to tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response3 = await Client.GetAsync("/api/v1/TaxClasses");
        var result3 = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response3);
        TestAssertions.AssertHttpSuccess(response3);

        // Assert
        TestAssertions.AssertNotNull(result1);
        TestAssertions.AssertNotNull(result2);
        TestAssertions.AssertNotNull(result3);

        // Verify tenant 1 sees their data consistently
        var tenant1Ids1 = result1.Data.Select(x => x.Id).ToList();
        var tenant1Ids3 = result3.Data.Select(x => x.Id).ToList();
        TestAssertions.AssertTrue(tenant1Ids1.Contains(tenant1Tax1));
        TestAssertions.AssertTrue(tenant1Ids1.Contains(tenant1Tax2));
        TestAssertions.AssertTrue(tenant1Ids3.Contains(tenant1Tax1));
        TestAssertions.AssertTrue(tenant1Ids3.Contains(tenant1Tax2));

        // Verify tenant 2 sees only their data
        var tenant2Ids = result2.Data.Select(x => x.Id).ToList();
        TestAssertions.AssertTrue(tenant2Ids.Contains(tenant2Tax1));
        TestAssertions.AssertFalse(tenant2Ids.Contains(tenant1Tax1));
        TestAssertions.AssertFalse(tenant2Ids.Contains(tenant1Tax2));

        // Verify no cross-tenant data leakage
        TestAssertions.AssertFalse(tenant1Ids1.Contains(tenant2Tax1));
        TestAssertions.AssertFalse(tenant1Ids3.Contains(tenant2Tax1));
    }

    [Fact]
    public async Task GetTaxClassList_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses?pageNumber=1&pageSize=100");
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5); // Should respond within 5 seconds
    }

    [Fact]
    public async Task GetTaxClassList_WithSpecialCharactersInSearchString_ShouldHandleGracefully()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses?searchString=%25%20OR%201%3D1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task GetTaxClassList_VerifyDataIntegrity_ShouldReturnValidTaxRates()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify all tax rates are valid
        foreach (var taxClass in result.Data)
        {
            TestAssertions.AssertTrue(taxClass.TaxRate >= 0);
            TestAssertions.AssertTrue(taxClass.TaxRate <= 100); // Assuming percentage
            TestAssertions.AssertTrue(taxClass.Id != Guid.Empty);
        }
    }
}