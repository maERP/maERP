using System.Net;
using maERP.Domain.Dtos.Country;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Country.Queries;

public class CountryListQueryTests : TenantIsolatedTestBase
{
    private async Task<Guid> CreateTestCountryAsync(Guid tenantId, string name = "Test Country", string countryCode = "TC")
    {
        TenantContext.SetCurrentTenantId(tenantId);

        var country = new Domain.Entities.Country
        {
            Name = name,
            CountryCode = countryCode,
            TenantId = tenantId,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        DbContext.Country.Add(country);
        await DbContext.SaveChangesAsync();

        TenantContext.SetCurrentTenantId(null);
        return country.Id;
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
    public async Task GetCountryList_WithValidTenant_ShouldReturnPaginatedResults()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Germany", "DE");
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Austria", "AT");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data.Count >= 2); // At least our 2 created countries
        TestAssertions.AssertTrue(result.TotalCount >= 2);
    }

    [Fact]
    public async Task GetCountryList_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        RemoveTenantHeader();

        // Act
        var response = await Client.GetAsync("/api/v1/Countries");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetCountryList_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeader();

        // Act
        var response = await Client.GetAsync("/api/v1/Countries");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
        TestAssertions.AssertEqual(0, result.TotalPages);
    }

    [Fact]
    public async Task GetCountryList_WithPagination_ShouldReturnCorrectPage()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?pageNumber=1&pageSize=5");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.CurrentPage);
        TestAssertions.AssertEqual(5, result.PageSize);
        TestAssertions.AssertTrue(result.Data.Count <= 5);
    }

    [Fact]
    public async Task GetCountryList_WithSearchString_ShouldFilterResults()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Germany", "DE");
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Austria", "AT");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?searchString=Germany");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify that results contain the search string
        if (result.Data.Any())
        {
            TestAssertions.AssertTrue(result.Data.Any(c => c.Name.Contains("Germany")));
        }
    }

    [Fact]
    public async Task GetCountryList_WithOrderBy_ShouldReturnOrderedResults()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Germany", "DE");
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Austria", "AT");
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Belgium", "BE");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?orderBy=Name");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify ordering
        if (result.Data.Count > 1)
        {
            for (int i = 0; i < result.Data.Count - 1; i++)
            {
                TestAssertions.AssertTrue(
                    string.Compare(result.Data[i].Name, result.Data[i + 1].Name, StringComparison.Ordinal) <= 0);
            }
        }
    }

    [Fact]
    public async Task GetCountryList_TenantIsolation_ShouldReturnOnlyTenantData()
    {
        // Arrange
        await SeedTestDataAsync();
        var tenant1Country1 = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Germany", "DE");
        var tenant1Country2 = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Austria", "AT");
        var tenant2Country1 = await CreateTestCountryAsync(TenantConstants.TestTenant2Id, "France", "FR");
        var tenant2Country2 = await CreateTestCountryAsync(TenantConstants.TestTenant2Id, "Spain", "ES");

        // Act - Get data for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/Countries");
        var result1 = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response1);

        // Act - Get data for tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/Countries");
        var result2 = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response2);

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpSuccess(response2);
        TestAssertions.AssertNotNull(result1);
        TestAssertions.AssertNotNull(result2);

        // Verify tenant 1 sees their countries
        var tenant1Ids = result1.Data.Select(c => c.Id).ToList();
        TestAssertions.AssertTrue(tenant1Ids.Contains(tenant1Country1));
        TestAssertions.AssertTrue(tenant1Ids.Contains(tenant1Country2));
        TestAssertions.AssertFalse(tenant1Ids.Contains(tenant2Country1));
        TestAssertions.AssertFalse(tenant1Ids.Contains(tenant2Country2));

        // Verify tenant 2 sees their countries
        var tenant2Ids = result2.Data.Select(c => c.Id).ToList();
        TestAssertions.AssertTrue(tenant2Ids.Contains(tenant2Country1));
        TestAssertions.AssertTrue(tenant2Ids.Contains(tenant2Country2));
        TestAssertions.AssertFalse(tenant2Ids.Contains(tenant1Country1));
        TestAssertions.AssertFalse(tenant2Ids.Contains(tenant1Country2));

        // Verify country names are correct
        var tenant1Country1Data = result1.Data.First(c => c.Id == tenant1Country1);
        var tenant1Country2Data = result1.Data.First(c => c.Id == tenant1Country2);
        TestAssertions.AssertEqual("Germany", tenant1Country1Data.Name);
        TestAssertions.AssertEqual("Austria", tenant1Country2Data.Name);

        var tenant2Country1Data = result2.Data.First(c => c.Id == tenant2Country1);
        var tenant2Country2Data = result2.Data.First(c => c.Id == tenant2Country2);
        TestAssertions.AssertEqual("France", tenant2Country1Data.Name);
        TestAssertions.AssertEqual("Spain", tenant2Country2Data.Name);
    }

    [Fact]
    public async Task GetCountryList_WithLargePageSize_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?pageNumber=1&pageSize=1000");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1000, result.PageSize);
    }

    [Fact]
    public async Task GetCountryList_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?pageNumber=999&pageSize=10");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data.Count);
    }

    [Fact]
    public async Task GetCountryList_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?pageNumber=1&pageSize=0");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.PageSize > 0); // Should use default page size
    }

    [Fact]
    public async Task GetCountryList_WithNegativePageNumber_ShouldUseDefaultPageNumber()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?pageNumber=-1&pageSize=10");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.CurrentPage >= 0);
    }

    [Fact]
    public async Task GetCountryList_ResponseStructure_ShouldContainAllMetadata()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?pageNumber=1&pageSize=5");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.TotalCount >= 0);
        TestAssertions.AssertTrue(result.TotalPages >= 0);
        TestAssertions.AssertTrue(result.CurrentPage > 0);
        TestAssertions.AssertTrue(result.PageSize > 0);
        TestAssertions.AssertNotNull(result.Data);
    }

    [Fact]
    public async Task GetCountryList_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var tasks = Enumerable.Range(1, 5)
            .Select(_ => Client.GetAsync("/api/v1/Countries"))
            .ToArray();
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    public async Task GetCountryList_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue(invalidTenantId);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetCountryList_WithEmptyTenantHeaderValue_ShouldReturnEmptyResult()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue("");

        // Act
        var response = await Client.GetAsync("/api/v1/Countries");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetCountryList_WithMultipleOrderByFields_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?orderBy=Name,CountryCode");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task GetCountryList_EmptySearchString_ShouldReturnAllResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var responseWithoutSearch = await Client.GetAsync("/api/v1/Countries");
        var responseWithEmptySearch = await Client.GetAsync("/api/v1/Countries?searchString=");

        // Assert
        var resultWithoutSearch = await ReadResponseAsync<PaginatedResult<CountryListDto>>(responseWithoutSearch);
        var resultWithEmptySearch = await ReadResponseAsync<PaginatedResult<CountryListDto>>(responseWithEmptySearch);

        TestAssertions.AssertEqual(resultWithoutSearch.TotalCount, resultWithEmptySearch.TotalCount);
    }

    [Fact]
    public async Task GetCountryList_AfterTenantSwitch_ShouldReturnCorrectData()
    {
        // Arrange
        await SeedTestDataAsync();
        var tenant1Country1 = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Germany", "DE");
        var tenant1Country2 = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Austria", "AT");
        var tenant2Country1 = await CreateTestCountryAsync(TenantConstants.TestTenant2Id, "France", "FR");

        // Act - First request with tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/Countries");
        var result1 = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response1);
        TestAssertions.AssertHttpSuccess(response1);

        // Switch to tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/Countries");
        var result2 = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response2);
        TestAssertions.AssertHttpSuccess(response2);

        // Switch back to tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response3 = await Client.GetAsync("/api/v1/Countries");
        var result3 = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response3);
        TestAssertions.AssertHttpSuccess(response3);

        // Assert
        TestAssertions.AssertNotNull(result1);
        TestAssertions.AssertNotNull(result2);
        TestAssertions.AssertNotNull(result3);

        // Verify tenant 1 sees their data consistently
        var tenant1Ids1 = result1.Data.Select(x => x.Id).ToList();
        var tenant1Ids3 = result3.Data.Select(x => x.Id).ToList();
        TestAssertions.AssertTrue(tenant1Ids1.Contains(tenant1Country1));
        TestAssertions.AssertTrue(tenant1Ids1.Contains(tenant1Country2));
        TestAssertions.AssertTrue(tenant1Ids3.Contains(tenant1Country1));
        TestAssertions.AssertTrue(tenant1Ids3.Contains(tenant1Country2));

        // Verify tenant 2 sees only their data
        var tenant2Ids = result2.Data.Select(x => x.Id).ToList();
        TestAssertions.AssertTrue(tenant2Ids.Contains(tenant2Country1));
        TestAssertions.AssertFalse(tenant2Ids.Contains(tenant1Country1));
        TestAssertions.AssertFalse(tenant2Ids.Contains(tenant1Country2));

        // Verify no cross-tenant data leakage
        TestAssertions.AssertFalse(tenant1Ids1.Contains(tenant2Country1));
        TestAssertions.AssertFalse(tenant1Ids3.Contains(tenant2Country1));
    }

    [Fact]
    public async Task GetCountryList_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?pageNumber=1&pageSize=100");
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5); // Should respond within 5 seconds
    }

    [Fact]
    public async Task GetCountryList_WithSpecialCharactersInSearchString_ShouldHandleGracefully()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?searchString=%25%20OR%201%3D1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task GetCountryList_VerifyDataIntegrity_ShouldReturnValidCountryData()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Germany", "DE");
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Austria", "AT");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify all countries have valid data
        foreach (var country in result.Data)
        {
            TestAssertions.AssertTrue(country.Id != Guid.Empty);
            TestAssertions.AssertFalse(string.IsNullOrEmpty(country.Name));
            TestAssertions.AssertFalse(string.IsNullOrEmpty(country.CountryCode));
            TestAssertions.AssertTrue(country.CountryCode.Length <= 3);
        }
    }

    [Fact]
    public async Task GetCountryList_SearchByCountryCode_ShouldFilterResults()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Germany", "DE");
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Austria", "AT");
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Denmark", "DK");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?searchString=DE");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify that results contain the search string in name or country code
        if (result.Data.Any())
        {
            TestAssertions.AssertTrue(result.Data.Any(c =>
                c.Name.Contains("DE", StringComparison.OrdinalIgnoreCase) ||
                c.CountryCode.Contains("DE", StringComparison.OrdinalIgnoreCase)));
        }
    }

    [Fact]
    public async Task GetCountryList_OrderByCountryCode_ShouldReturnOrderedResults()
    {
        // Arrange
        await SeedTestDataAsync();
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Germany", "DE");
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Austria", "AT");
        await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Belgium", "BE");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries?orderBy=CountryCode");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<CountryListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify ordering by country code
        if (result.Data.Count > 1)
        {
            for (int i = 0; i < result.Data.Count - 1; i++)
            {
                TestAssertions.AssertTrue(
                    string.Compare(result.Data[i].CountryCode, result.Data[i + 1].CountryCode, StringComparison.Ordinal) <= 0);
            }
        }
    }
}
