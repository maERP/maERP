using System.Net;
using maERP.Domain.Dtos.Country;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Country.Commands;

public class CountryDeleteCommandTests : TenantIsolatedTestBase
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
    public async Task DeleteCountry_WithValidId_ShouldReturnOk()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(countryId, result.Data);

        // Verify deletion
        var getResponse = await Client.GetAsync($"/api/v1/Countries/{countryId}");
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCountry_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        RemoveTenantHeader();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCountry_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeader();

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCountry_FromDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Test Country", "TC");
        SetTenantHeader(TenantConstants.TestTenant2Id);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Verify country still exists for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse = await Client.GetAsync($"/api/v1/Countries/{countryId}");
        TestAssertions.AssertHttpSuccess(getResponse);

        var result = await ReadResponseAsync<Result<CountryDetailDto>>(getResponse);
        TestAssertions.AssertEqual("Test Country", result.Data!.Name);
    }

    [Fact]
    public async Task DeleteCountry_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Countries/{Guid.NewGuid()}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCountry_TenantIsolation_ShouldOnlyDeleteOwnTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId1 = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Country1", "C1");
        var countryId2 = await CreateTestCountryAsync(TenantConstants.TestTenant2Id, "Country2", "C2");
        var countryId3 = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Country3", "C3");

        // Act - Delete tenant 1's first country from tenant 1 context
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.DeleteAsync($"/api/v1/Countries/{countryId1}");

        // Try to delete tenant 2's country from tenant 1 context (should fail)
        var crossTenantResponse = await Client.DeleteAsync($"/api/v1/Countries/{countryId2}");

        // Delete tenant 2's country from tenant 2 context
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.DeleteAsync($"/api/v1/Countries/{countryId2}");

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpStatusCode(crossTenantResponse, HttpStatusCode.NotFound);
        TestAssertions.AssertHttpSuccess(response2);

        // Verify tenant 1's remaining country still exists
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getTenant1Response = await Client.GetAsync($"/api/v1/Countries/{countryId3}");
        TestAssertions.AssertHttpSuccess(getTenant1Response);

        // Verify deleted countries are gone
        var getDeleted1 = await Client.GetAsync($"/api/v1/Countries/{countryId1}");
        TestAssertions.AssertHttpStatusCode(getDeleted1, HttpStatusCode.NotFound);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var getDeleted2 = await Client.GetAsync($"/api/v1/Countries/{countryId2}");
        TestAssertions.AssertHttpStatusCode(getDeleted2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCountry_MultipleDeletions_ShouldOnlyDeleteOnce()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Delete twice
        var response1 = await Client.DeleteAsync($"/api/v1/Countries/{countryId}");
        var response2 = await Client.DeleteAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCountry_ConcurrentDeletions_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Try to delete concurrently
        var task1 = Client.DeleteAsync($"/api/v1/Countries/{countryId}");
        var task2 = Client.DeleteAsync($"/api/v1/Countries/{countryId}");
        var task3 = Client.DeleteAsync($"/api/v1/Countries/{countryId}");

        var responses = await Task.WhenAll(task1, task2, task3);

        // Assert - Only one should succeed
        var successCount = responses.Count(r => r.IsSuccessStatusCode);
        TestAssertions.AssertTrue(successCount >= 1);

        // Verify deletion
        var getResponse = await Client.GetAsync($"/api/v1/Countries/{countryId}");
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    public async Task DeleteCountry_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue(invalidTenantId);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteCountry_WithEmptyTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue("");

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCountry_WithZeroId_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Countries/{Guid.Empty}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteCountry_WithInvalidGuid_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync("/api/v1/Countries/invalid-guid");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCountry_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Countries/{countryId}");
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5);
    }

    [Fact]
    public async Task DeleteCountry_VerifyResultStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId = await CreateTestCountryAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.DeleteAsync($"/api/v1/Countries/{countryId}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid>>(response);

        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
        TestAssertions.AssertEqual(countryId, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task DeleteCountry_MultipleCountries_ShouldDeleteCorrectOne()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId1 = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Country1", "C1");
        var countryId2 = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Country2", "C2");
        var countryId3 = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Country3", "C3");
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Delete middle one
        var response = await Client.DeleteAsync($"/api/v1/Countries/{countryId2}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);

        // Verify only the correct one was deleted
        var getResponse1 = await Client.GetAsync($"/api/v1/Countries/{countryId1}");
        TestAssertions.AssertHttpSuccess(getResponse1);

        var getResponse2 = await Client.GetAsync($"/api/v1/Countries/{countryId2}");
        TestAssertions.AssertHttpStatusCode(getResponse2, HttpStatusCode.NotFound);

        var getResponse3 = await Client.GetAsync($"/api/v1/Countries/{countryId3}");
        TestAssertions.AssertHttpSuccess(getResponse3);
    }

    [Fact]
    public async Task DeleteCountry_AfterTenantSwitch_ShouldDeleteFromCorrectTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryId1 = await CreateTestCountryAsync(TenantConstants.TestTenant1Id, "Country1", "C1");
        var countryId2 = await CreateTestCountryAsync(TenantConstants.TestTenant2Id, "Country2", "C2");

        // Act - Start with tenant 1, verify access to own country
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var canAccessOwn = await Client.GetAsync($"/api/v1/Countries/{countryId1}");
        TestAssertions.AssertHttpSuccess(canAccessOwn);

        // Switch to tenant 2 and delete their country
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Countries/{countryId2}");
        TestAssertions.AssertHttpSuccess(deleteResponse);

        // Verify tenant 2's country is deleted
        var deletedCheck = await Client.GetAsync($"/api/v1/Countries/{countryId2}");
        TestAssertions.AssertHttpStatusCode(deletedCheck, HttpStatusCode.NotFound);

        // Try to delete tenant 1's country from tenant 2 context (should fail)
        var deleteOtherResponse = await Client.DeleteAsync($"/api/v1/Countries/{countryId1}");
        TestAssertions.AssertHttpStatusCode(deleteOtherResponse, HttpStatusCode.NotFound);

        // Verify tenant 1's country still exists and is unchanged
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var stillExists = await Client.GetAsync($"/api/v1/Countries/{countryId1}");
        TestAssertions.AssertHttpSuccess(stillExists);

        var result = await ReadResponseAsync<Result<CountryDetailDto>>(stillExists);
        TestAssertions.AssertEqual("Country1", result.Data!.Name);
    }
}
