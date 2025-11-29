using System.Net;
using maERP.Domain.Dtos.Country;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Country.Commands;

public class CountryUpdateCommandTests : TenantIsolatedTestBase
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

    private CountryInputDto CreateValidUpdateDto(string name = "Updated Country", string countryCode = "UC")
    {
        return new CountryInputDto
        {
            Name = name,
            CountryCode = countryCode
        };
    }

    [Fact]
    public async Task UpdateCountry_WithValidData_ShouldReturnOk()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Germany", "DE");
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto("Deutschland", "DE");

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(countryId, result.Data);

        // Verify the update
        var getResponse = await Client.GetAsync($"/api/v1/Countries/{countryId}");
        var getResult = await ReadResponseAsync<Result<CountryDetailDto>>(getResponse);
        TestAssertions.AssertEqual("Deutschland", getResult.Data!.Name);
        TestAssertions.AssertEqual("DE", getResult.Data.CountryCode);
    }

    [Fact]
    public async Task UpdateCountry_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        RemoveTenantHeader();
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateCountry_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeader();
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateCountry_FromDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Austria", "AT");
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var updateDto = CreateValidUpdateDto("Updated Austria", "AT");

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Verify the original country is unchanged
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse = await Client.GetAsync($"/api/v1/Countries/{countryId}");
        var getResult = await ReadResponseAsync<Result<CountryDetailDto>>(getResponse);
        TestAssertions.AssertEqual("Austria", getResult.Data!.Name);
    }

    [Fact]
    public async Task UpdateCountry_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{Guid.NewGuid()}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateCountry_WithEmptyName_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new CountryInputDto { Name = "", CountryCode = "TC" };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Name"));
    }

    [Fact]
    public async Task UpdateCountry_WithEmptyCountryCode_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new CountryInputDto { Name = "Test Country", CountryCode = "" };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Country Code"));
    }

    [Fact]
    public async Task UpdateCountry_WithTooLongCountryCode_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new CountryInputDto { Name = "Test Country", CountryCode = "TOOLONG" };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Country Code"));
    }

    [Fact]
    public async Task UpdateCountry_TenantIsolation_ShouldOnlyUpdateOwnTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId1 = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Country1", "C1");
        var countryId2 = await CreateTestCountryAsync(TenantConstants.TestTenant2Id, "Country2", "C2");
        var updateDto1 = CreateValidUpdateDto("Updated Country1", "U1");
        var updateDto2 = CreateValidUpdateDto("Updated Country2", "U2");

        // Act - Update tenant 1's country from tenant 1 context
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await PutAsJsonAsync($"/api/v1/Countries/{countryId1}", updateDto1);

        // Try to update tenant 2's country from tenant 1 context (should fail)
        var crossTenantUpdateDto = CreateValidUpdateDto("Cross Update", "CU");
        var crossTenantResponse = await PutAsJsonAsync($"/api/v1/Countries/{countryId2}", crossTenantUpdateDto);

        // Act - Update tenant 2's country from tenant 2 context
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await PutAsJsonAsync($"/api/v1/Countries/{countryId2}", updateDto2);

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpStatusCode(crossTenantResponse, HttpStatusCode.NotFound);
        TestAssertions.AssertHttpSuccess(response2);

        // Verify each tenant sees only their updated data
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getTenant1Response = await Client.GetAsync($"/api/v1/Countries/{countryId1}");
        var getTenant1Result = await ReadResponseAsync<Result<CountryDetailDto>>(getTenant1Response);
        TestAssertions.AssertEqual("Updated Country1", getTenant1Result.Data!.Name);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var getTenant2Response = await Client.GetAsync($"/api/v1/Countries/{countryId2}");
        var getTenant2Result = await ReadResponseAsync<Result<CountryDetailDto>>(getTenant2Response);
        TestAssertions.AssertEqual("Updated Country2", getTenant2Result.Data!.Name);
    }

    [Fact]
    public async Task UpdateCountry_ConcurrentUpdates_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Original", "OR");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Perform concurrent updates
        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 0; i < 5; i++)
        {
            var updateDto = new CountryInputDto { Name = $"Update{i}", CountryCode = $"U{i}" };
            tasks.Add(PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto));
        }

        var responses = await Task.WhenAll(tasks);

        // Assert - All should succeed
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    public async Task UpdateCountry_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue(invalidTenantId);
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateCountry_WithEmptyTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue("");
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateCountry_WithNullDto_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await PutAsJsonAsync<CountryInputDto?>($"/api/v1/Countries/{countryId}", null);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateCountry_MultipleUpdatesSequentially_ShouldApplyAllChanges()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Initial", "IN");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Update multiple times
        var response1 = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", new CountryInputDto { Name = "First", CountryCode = "F1" });
        TestAssertions.AssertHttpSuccess(response1);

        var response2 = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", new CountryInputDto { Name = "Second", CountryCode = "S2" });
        TestAssertions.AssertHttpSuccess(response2);

        var response3 = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", new CountryInputDto { Name = "Final", CountryCode = "FN" });
        TestAssertions.AssertHttpSuccess(response3);

        // Assert - Final value should be the last update
        var getResponse = await Client.GetAsync($"/api/v1/Countries/{countryId}");
        var getResult = await ReadResponseAsync<Result<CountryDetailDto>>(getResponse);
        TestAssertions.AssertEqual("Final", getResult.Data!.Name);
        TestAssertions.AssertEqual("FN", getResult.Data.CountryCode);
    }

    [Fact]
    public async Task UpdateCountry_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto();
        var startTime = DateTime.UtcNow;

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto);
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5);
    }

    [Fact]
    public async Task UpdateCountry_VerifyResultStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/Countries/{countryId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid>>(response);

        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
        TestAssertions.AssertEqual(countryId, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }
}
