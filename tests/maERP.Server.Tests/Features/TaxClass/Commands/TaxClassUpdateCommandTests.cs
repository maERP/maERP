using System.Net;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.TaxClass.Commands;

public class TaxClassUpdateCommandTests : TenantIsolatedTestBase
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

    private TaxClassInputDto CreateValidUpdateDto(double taxRate = 25.0)
    {
        return new TaxClassInputDto
        {
            TaxRate = taxRate
        };
    }

    [Fact]
    public async Task UpdateTaxClass_WithValidData_ShouldReturnOk()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 19.0);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto(25.0);

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(taxClassId, result.Data);

        // Verify the update
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        var getResult = await ReadResponseAsync<Result<TaxClassDetailDto>>(getResponse);
        TestAssertions.AssertEqual(25.0, getResult.Data!.TaxRate);
    }

    [Fact]
    public async Task UpdateTaxClass_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        RemoveTenantHeader();
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateTaxClass_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeader();
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateTaxClass_FromDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 15.0);
        SetTenantHeader(TenantConstants.TestTenant2Id); // Attempt to access from different tenant
        var updateDto = CreateValidUpdateDto(25.0);

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Verify the original tax class is unchanged
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        var getResult = await ReadResponseAsync<Result<TaxClassDetailDto>>(getResponse);
        TestAssertions.AssertEqual(15.0, getResult.Data!.TaxRate); // Original value preserved
    }

    [Fact]
    public async Task UpdateTaxClass_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{Guid.NewGuid()}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateTaxClass_WithNegativeTaxRate_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new TaxClassInputDto { TaxRate = -5.0 };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("TaxRate") || content.Contains("must be greater"));
    }

    [Fact]
    public async Task UpdateTaxClass_WithTaxRateOver100_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new TaxClassInputDto { TaxRate = 150.0 };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(content.Contains("TaxRate") || content.Contains("must be less"));
    }

    [Fact]
    public async Task UpdateTaxClass_WithZeroTaxRate_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 19.0);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new TaxClassInputDto { TaxRate = 0.0 };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify the update
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        var getResult = await ReadResponseAsync<Result<TaxClassDetailDto>>(getResponse);
        TestAssertions.AssertEqual(0.0, getResult.Data!.TaxRate);
    }

    [Fact]
    public async Task UpdateTaxClass_TenantIsolation_ShouldOnlyUpdateOwnTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId1 = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 19.0);
        var taxClassId2 = await CreateTestTaxClassAsync(TenantConstants.TestTenant2Id, 20.0);
        var updateDto1 = CreateValidUpdateDto(30.0);
        var updateDto2 = CreateValidUpdateDto(35.0);

        // Act - Update tenant 1's tax class from tenant 1 context
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId1}", updateDto1);

        // Try to update tenant 2's tax class from tenant 1 context (should fail)
        var crossTenantUpdateDto = CreateValidUpdateDto(40.0); // Use different rate to avoid uniqueness conflicts
        var crossTenantResponse = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId2}", crossTenantUpdateDto);

        // Act - Update tenant 2's tax class from tenant 2 context
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId2}", updateDto2);

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpStatusCode(crossTenantResponse, HttpStatusCode.NotFound);
        TestAssertions.AssertHttpSuccess(response2);

        // Verify each tenant sees only their updated data
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var getTenant1Response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId1}");
        var getTenant1Result = await ReadResponseAsync<Result<TaxClassDetailDto>>(getTenant1Response);
        TestAssertions.AssertEqual(30.0, getTenant1Result.Data!.TaxRate);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var getTenant2Response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId2}");
        var getTenant2Result = await ReadResponseAsync<Result<TaxClassDetailDto>>(getTenant2Response);
        TestAssertions.AssertEqual(35.0, getTenant2Result.Data!.TaxRate);

        // Verify cross-tenant access is still blocked
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var crossAccessResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId2}");
        TestAssertions.AssertHttpStatusCode(crossAccessResponse, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateTaxClass_WithDecimalPrecision_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new TaxClassInputDto { TaxRate = 19.75 };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);

        // Verify precision is maintained
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        var getResult = await ReadResponseAsync<Result<TaxClassDetailDto>>(getResponse);
        TestAssertions.AssertEqual(19.75, getResult.Data!.TaxRate);
    }

    [Fact]
    public async Task UpdateTaxClass_ConcurrentUpdates_ShouldHandleCorrectly()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 10.0);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Perform concurrent updates
        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 0; i < 5; i++)
        {
            var updateDto = new TaxClassInputDto { TaxRate = 20.0 + i };
            tasks.Add(PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto));
        }

        var responses = await Task.WhenAll(tasks);

        // Assert - All should succeed
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
        }

        // Verify final state (should be one of the values)
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        var getResult = await ReadResponseAsync<Result<TaxClassDetailDto>>(getResponse);
        TestAssertions.AssertTrue(getResult.Data!.TaxRate >= 20.0 && getResult.Data.TaxRate <= 24.0);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    public async Task UpdateTaxClass_WithInvalidTenantHeaderValue_ShouldReturnUnauthorized(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue(invalidTenantId);
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateTaxClass_WithEmptyTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetInvalidTenantHeaderValue("");
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateTaxClass_WithNullDto_ShouldReturnBadRequest()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await PutAsJsonAsync<TaxClassInputDto?>($"/api/v1/TaxClasses/{taxClassId}", null);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateTaxClass_WithMaxTaxRate_ShouldSucceed()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = new TaxClassInputDto { TaxRate = 100.0 };

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task UpdateTaxClass_MultipleUpdatesSequentially_ShouldApplyAllChanges()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 10.0);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Update multiple times
        var response1 = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", new TaxClassInputDto { TaxRate = 15.0 });
        TestAssertions.AssertHttpSuccess(response1);

        var response2 = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", new TaxClassInputDto { TaxRate = 20.0 });
        TestAssertions.AssertHttpSuccess(response2);

        var response3 = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", new TaxClassInputDto { TaxRate = 25.0 });
        TestAssertions.AssertHttpSuccess(response3);

        // Assert - Final value should be the last update
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        var getResult = await ReadResponseAsync<Result<TaxClassDetailDto>>(getResponse);
        TestAssertions.AssertEqual(25.0, getResult.Data!.TaxRate);
    }

    [Fact]
    public async Task UpdateTaxClass_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto();
        var startTime = DateTime.UtcNow;

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5);
    }

    [Fact]
    public async Task UpdateTaxClass_VerifyResultStructure_ShouldHaveCorrectFormat()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<Guid>>(response);

        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
        TestAssertions.AssertEqual(taxClassId, result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }
}