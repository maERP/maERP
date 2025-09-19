using System.Net;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.TaxClass.Commands;

public class TaxClassCreateCommandTests : TenantIsolatedTestBase
{
    private async Task SeedTestDataAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
    }

    private TaxClassInputDto CreateValidTaxClassDto()
    {
        return new TaxClassInputDto
        {
            TaxRate = 19.0
        };
    }

    [Fact]
    public async Task CreateTaxClass_WithValidData_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = 97.77 // Use unique tax rate that won't conflict with seed data
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);
    }

    [Fact]
    public async Task CreateTaxClass_WithoutTenantHeader_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        RemoveTenantHeader();
        var taxClassDto = CreateValidTaxClassDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateTaxClass_WithInvalidTenantHeader_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetInvalidTenantHeader();
        var taxClassDto = CreateValidTaxClassDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateTaxClass_WithNegativeTaxRate_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = -5.0
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Tax Rate"));
    }

    [Fact]
    public async Task CreateTaxClass_WithTaxRateOver100_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = 150.0
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("Tax Rate"));
    }

    [Fact]
    public async Task CreateTaxClass_WithZeroTaxRate_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = 0.0
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateTaxClass_TenantIsolation_ShouldCreateSeparatelyForEachTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassDto1 = new TaxClassInputDto { TaxRate = 15.5 };
        var taxClassDto2 = new TaxClassInputDto { TaxRate = 25.5 };

        // Act - Create for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto1);
        var result1 = await ReadResponseAsync<Result<Guid>>(response1);

        // Act - Create for tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto2);
        var result2 = await ReadResponseAsync<Result<Guid>>(response2);

        // Assert
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);
        TestAssertions.AssertNotEqual(result1.Data, result2.Data);

        // Verify tenant isolation - tenant 1 can't access tenant 2's tax class
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{result2.Data}");
        TestAssertions.AssertHttpStatusCode(getResponse, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateTaxClass_WithDecimalPrecision_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = 19.75 // Test decimal precision
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);

        // Verify the created tax class has correct tax rate
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{result.Data}");
        var getResult = await ReadResponseAsync<Result<TaxClassDetailDto>>(getResponse);
        TestAssertions.AssertEqual(19.75, getResult.Data!.TaxRate);
    }

    [Fact]
    public async Task CreateTaxClass_MultipleConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var tasks = new List<Task<HttpResponseMessage>>();

        // Act - Create multiple tax classes concurrently
        for (int i = 0; i < 5; i++)
        {
            var taxClassDto = new TaxClassInputDto
            {
                TaxRate = 10.0 + i // Different tax rates
            };
            tasks.Add(PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto));
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
            TestAssertions.AssertTrue(ids.Add(result.Data)); // Should return true if unique
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    public async Task CreateTaxClass_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        SetInvalidTenantHeaderValue(invalidTenantId);
        var taxClassDto = CreateValidTaxClassDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateTaxClass_WithEmptyTenantHeaderValue_ShouldReturnCreated()
    {
        // Arrange
        await SeedTestDataAsync();
        SetInvalidTenantHeaderValue("");
        var taxClassDto = CreateValidTaxClassDto();

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateTaxClass_WithNullDto_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await PostAsJsonAsync<TaxClassInputDto?>("/api/v1/TaxClasses", null);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateTaxClass_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = 99.99 // Use unique tax rate that won't conflict with seed data
        };
        var startTime = DateTime.UtcNow;

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5); // Should respond within 5 seconds
    }

    [Fact]
    public async Task CreateTaxClass_WithMaxTaxRate_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = 100.0 // Maximum valid tax rate
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task CreateTaxClass_AfterTenantSwitch_ShouldCreateInCorrectTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassDto1 = new TaxClassInputDto { TaxRate = 15.0 };
        var taxClassDto2 = new TaxClassInputDto { TaxRate = 25.0 };

        // Act - Create in tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto1);
        var result1 = await ReadResponseAsync<Result<Guid>>(response1);

        // Switch to tenant 2 and create
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto2);
        var result2 = await ReadResponseAsync<Result<Guid>>(response2);

        // Assert
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);

        // Verify each tenant can only access their own tax class
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var get1 = await Client.GetAsync($"/api/v1/TaxClasses/{result1.Data}");
        TestAssertions.AssertHttpSuccess(get1);
        var get2 = await Client.GetAsync($"/api/v1/TaxClasses/{result2.Data}");
        TestAssertions.AssertHttpStatusCode(get2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateTaxClass_VerifyResultStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var taxClassDto = new TaxClassInputDto
        {
            TaxRate = 98.88 // Use unique tax rate that won't conflict with seed data
        };

        // Act
        var response = await PostAsJsonAsync("/api/v1/TaxClasses", taxClassDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<Guid>>(response);

        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Created, result.StatusCode);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }
}