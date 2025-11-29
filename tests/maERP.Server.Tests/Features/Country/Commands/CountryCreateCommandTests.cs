using System.Net;
using maERP.Domain.Dtos.Country;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Country.Commands;

public class CountryCreateCommandTests : TenantIsolatedTestBase
{
    private async Task SeedTestDataAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
    }

    private CountryInputDto CreateValidCountryDto()
    {
        return new CountryInputDto
        {
            Name = "Test Country",
            CountryCode = "TC"
        };
    }

    [Fact]
    public async Task CreateCountry_WithValidData_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var countryDto = new CountryInputDto
        {
            Name = "United Kingdom",
            CountryCode = "GB"
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Countries", countryDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);
    }

    [Fact]
    public async Task CreateCountry_WithoutTenantHeader_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        RemoveTenantHeader();
        var countryDto = new CountryInputDto
        {
            Name = "France",
            CountryCode = "FR"
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Countries", countryDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateCountry_WithInvalidTenantHeader_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetInvalidTenantHeader();
        var countryDto = new CountryInputDto
        {
            Name = "Spain",
            CountryCode = "ES"
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Countries", countryDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateCountry_WithEmptyName_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var countryDto = new CountryInputDto
        {
            Name = "",
            CountryCode = "XX"
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Countries", countryDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Name"));
    }

    [Fact]
    public async Task CreateCountry_WithEmptyCountryCode_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var countryDto = new CountryInputDto
        {
            Name = "Test Country",
            CountryCode = ""
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Countries", countryDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Country Code"));
    }

    [Fact]
    public async Task CreateCountry_WithTooLongCountryCode_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var countryDto = new CountryInputDto
        {
            Name = "Test Country",
            CountryCode = "TOOLONG"
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Countries", countryDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Country Code"));
    }

    [Fact]
    public async Task CreateCountry_TenantIsolation_ShouldCreateSeparatelyForEachTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var countryDto1 = new CountryInputDto { Name = "Italy", CountryCode = "IT" };
        var countryDto2 = new CountryInputDto { Name = "Portugal", CountryCode = "PT" };

        // Act - Create for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await PostAsJsonAsync("/api/v1/Countries", countryDto1);
        var result1 = await ReadResponseAsync<Result<Guid>>(response1);

        // Act - Create for tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await PostAsJsonAsync("/api/v1/Countries", countryDto2);
        var result2 = await ReadResponseAsync<Result<Guid>>(response2);

        // Assert
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);
        TestAssertions.AssertNotEqual(result1.Data, result2.Data);

        // Verify tenant isolation - tenant 1 can't access tenant 2's country
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse = await Client.GetAsync($"/api/v1/Countries/{result2.Data}");
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateCountry_MultipleConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var tasks = new List<Task<HttpResponseMessage>>();

        // Act - Create multiple countries concurrently
        for (int i = 0; i < 5; i++)
        {
            var countryDto = new CountryInputDto
            {
                Name = $"Country {i}",
                CountryCode = $"C{i}"
            };
            tasks.Add(PostAsJsonAsync("/api/v1/Countries", countryDto));
        }

        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
            var result = await ReadResponseAsync<Result<Guid>>(response);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertNotEqual(Guid.Empty, result.Data);
        }

        // Verify all were created with unique IDs
        var ids = new HashSet<Guid>();
        foreach (var response in responses)
        {
            var result = await ReadResponseAsync<Result<Guid>>(response);
            TestAssertions.AssertTrue(ids.Add(result.Data));
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    public async Task CreateCountry_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        SetInvalidTenantHeaderValue(invalidTenantId);
        var countryDto = CreateValidCountryDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/Countries", countryDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateCountry_WithEmptyTenantHeaderValue_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetInvalidTenantHeaderValue("");
        var countryDto = new CountryInputDto
        {
            Name = "Belgium",
            CountryCode = "BE"
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Countries", countryDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateCountry_WithNullDto_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await PostAsJsonAsync<CountryInputDto?>("/api/v1/Countries", null);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateCountry_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var countryDto = new CountryInputDto
        {
            Name = "Netherlands",
            CountryCode = "NL"
        };
        var startTime = DateTime.UtcNow;

        // Act
        var response = await PostAsJsonAsync("/api/v1/Countries", countryDto);
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5);
    }

    [Fact]
    public async Task CreateCountry_VerifyResultStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var countryDto = new CountryInputDto
        {
            Name = "Sweden",
            CountryCode = "SE"
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/Countries", countryDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);

        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Created, result.StatusCode);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task CreateCountry_VerifyDataPersisted_ShouldReturnCorrectDetails()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var countryDto = new CountryInputDto
        {
            Name = "Norway",
            CountryCode = "NO"
        };

        // Act
        var createResponse = await PostAsJsonAsync("/api/v1/Countries", countryDto);
        var result = await ReadResponseAsync<Result<Guid>>(createResponse);

        // Verify the created country
        var getResponse = await Client.GetAsync($"/api/v1/Countries/{result.Data}");
        var getResult = await ReadResponseAsync<Result<CountryDetailDto>>(getResponse);

        // Assert
        TestAssertions.AssertHttpSuccess(getResponse);
        TestAssertions.AssertEqual("Norway", getResult.Data!.Name);
        TestAssertions.AssertEqual("NO", getResult.Data.CountryCode);
    }
}
