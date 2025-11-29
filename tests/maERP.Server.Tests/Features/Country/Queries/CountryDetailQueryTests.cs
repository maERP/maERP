using System.Net;
using maERP.Domain.Dtos.Country;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Country.Queries;

public class CountryDetailQueryTests : TenantIsolatedTestBase
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
    public async Task GetCountryById_WithValidIdAndTenant_ShouldReturnCountryDetails()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Germany", "DE");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CountryDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(countryId, result.Data!.Id);
        TestAssertions.AssertEqual("Germany", result.Data.Name);
        TestAssertions.AssertEqual("DE", result.Data.CountryCode);
    }

    [Fact]
    public async Task GetCountryById_WithValidIdButDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Austria", "AT");
        SetTenantHeader(TenantConstants.TestTenant2Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Verify the country still exists for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var validResponse = await Client.GetAsync($"/api/v1/Countries/{countryId}");
        TestAssertions.AssertHttpSuccess(validResponse);
        var result = await ReadResponseAsync<Result<CountryDetailDto>>(validResponse);
        TestAssertions.AssertEqual("Austria", result.Data!.Name);
    }

    [Fact]
    public async Task GetCountryById_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{Guid.NewGuid()}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetCountryById_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        RemoveTenantHeader();

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetCountryById_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeader();

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetCountryById_WithZeroId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{Guid.Empty}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetCountryById_WithInvalidGuid_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries/invalid-guid");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetCountryById_ResponseStructure_ShouldContainAllRequiredFields()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "France", "FR");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CountryDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        var country = result.Data;
        TestAssertions.AssertEqual(countryId, country!.Id);
        TestAssertions.AssertEqual("France", country.Name);
        TestAssertions.AssertEqual("FR", country.CountryCode);
    }

    [Fact]
    public async Task GetCountryById_ForTenant2Country_ShouldReturnCorrectData()
    {
        // Arrange
        await SeedTestDataAsync();
        var tenant2CountryId = await CreateTestCountryAsync(TenantConstants.TestTenant2Id, "Spain", "ES");
        SetTenantHeader(TenantConstants.TestTenant2Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{tenant2CountryId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CountryDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(tenant2CountryId, result.Data!.Id);
        TestAssertions.AssertEqual("Spain", result.Data.Name);
        TestAssertions.AssertEqual("ES", result.Data.CountryCode);
    }

    [Fact]
    public async Task GetCountryById_TenantIsolation_ShouldNotReturnDataFromOtherTenants()
    {
        // Arrange
        await SeedTestDataAsync();
        var tenant1CountryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Italy", "IT");
        var tenant2CountryId = await CreateTestCountryAsync(TenantConstants.TestTenant2Id, "Portugal", "PT");

        // Act & Assert - Try to access tenant 1's country with tenant 2 header
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var crossTenantResponse1 = await Client.GetAsync($"/api/v1/Countries/{tenant1CountryId}");
        TestAssertions.AssertHttpStatusCode(crossTenantResponse1, HttpStatusCode.NotFound);

        // Verify tenant 2 can access their own country
        var tenant2Response = await Client.GetAsync($"/api/v1/Countries/{tenant2CountryId}");
        TestAssertions.AssertHttpSuccess(tenant2Response);
        var tenant2Result = await ReadResponseAsync<Result<CountryDetailDto>>(tenant2Response);
        TestAssertions.AssertEqual(tenant2CountryId, tenant2Result.Data!.Id);
        TestAssertions.AssertEqual("Portugal", tenant2Result.Data.Name);

        // Act & Assert - Try to access tenant 2's country with tenant 1 header
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var crossTenantResponse2 = await Client.GetAsync($"/api/v1/Countries/{tenant2CountryId}");
        TestAssertions.AssertHttpStatusCode(crossTenantResponse2, HttpStatusCode.NotFound);

        // Verify tenant 1 can access their own country
        var tenant1Response = await Client.GetAsync($"/api/v1/Countries/{tenant1CountryId}");
        TestAssertions.AssertHttpSuccess(tenant1Response);
        var tenant1Result = await ReadResponseAsync<Result<CountryDetailDto>>(tenant1Response);
        TestAssertions.AssertEqual(tenant1CountryId, tenant1Result.Data!.Id);
        TestAssertions.AssertEqual("Italy", tenant1Result.Data.Name);
    }

    [Fact]
    public async Task GetCountryById_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Belgium", "BE");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var tasks = Enumerable.Range(1, 5).Select(_ => Client.GetAsync($"/api/v1/Countries/{countryId}")).ToArray();
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<Result<CountryDetailDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertEqual(countryId, result.Data!.Id);
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    public async Task GetCountryById_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue(invalidTenantId);

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetCountryById_WithEmptyTenantHeaderValue_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue("");

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetCountryById_ResultStructure_ShouldHaveCorrectStatusCode()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Netherlands", "NL");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<CountryDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task GetCountryById_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.GetAsync($"/api/v1/Countries/{countryId}");
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5);
    }

    [Fact]
    public async Task GetCountryById_AfterTenantSwitch_ShouldReturnCorrectData()
    {
        // Arrange
        await SeedTestDataAsync();
        var tenant1CountryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Sweden", "SE");
        var tenant2CountryId = await CreateTestCountryAsync(TenantConstants.TestTenant2Id, "Norway", "NO");

        // Act & Assert - First request with tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync($"/api/v1/Countries/{tenant1CountryId}");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<Result<CountryDetailDto>>(response1);
        TestAssertions.AssertEqual(tenant1CountryId, result1.Data!.Id);
        TestAssertions.AssertEqual("Sweden", result1.Data.Name);

        // Switch to tenant 2 and try accessing tenant 1's ID (should fail)
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var crossTenantResponse = await Client.GetAsync($"/api/v1/Countries/{tenant1CountryId}");
        TestAssertions.AssertHttpStatusCode(crossTenantResponse, HttpStatusCode.NotFound);

        // Access tenant 2's own country (should succeed)
        var response2 = await Client.GetAsync($"/api/v1/Countries/{tenant2CountryId}");
        TestAssertions.AssertHttpSuccess(response2);
        var result2 = await ReadResponseAsync<Result<CountryDetailDto>>(response2);
        TestAssertions.AssertEqual(tenant2CountryId, result2.Data!.Id);
        TestAssertions.AssertEqual("Norway", result2.Data.Name);

        // Switch back to tenant 1 and verify
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response3 = await Client.GetAsync($"/api/v1/Countries/{tenant1CountryId}");
        TestAssertions.AssertHttpSuccess(response3);
        var result3 = await ReadResponseAsync<Result<CountryDetailDto>>(response3);
        TestAssertions.AssertEqual(tenant1CountryId, result3.Data!.Id);
        TestAssertions.AssertEqual("Sweden", result3.Data.Name);
    }

    [Fact]
    public async Task GetCountryById_WithSpecialCharactersInUrl_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Countries/invalid%20OR%201=1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }
}
