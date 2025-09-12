using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.TaxClass.Commands;

public class TaxClassUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public TaxClassUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_TaxClassUpdateCommandTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        DbContext.Database.EnsureCreated();

        TenantContext.SetAssignedTenantIds(new[] { TenantConstants.TestTenant1Id, TenantConstants.TestTenant2Id });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(Guid tenantId)
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantId.ToString());
    }

    protected void SetInvalidTenantHeader()
    {
        SetTenantHeader(Guid.NewGuid()); // Non-existent tenant ID for testing tenant isolation
    }

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PutAsync(requestUri, content);
    }

    protected async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) where T : class
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return result ?? throw new InvalidOperationException("Failed to deserialize response");
    }

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

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
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

    [Fact(Skip = "Todo: Tenant isolation")]
    public async Task UpdateTaxClass_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact(Skip = "Todo: Tenant isolation")]
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

    [Fact(Skip = "Todo: Tenant isolation")]
    public async Task UpdateTaxClass_FromDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant2Id); // Different tenant
        var updateDto = CreateValidUpdateDto();

        // Act
        var response = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", updateDto);

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
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

    [Fact(Skip = "Todo: Tenant isolation")]
    public async Task UpdateTaxClass_TenantIsolation_ShouldOnlyUpdateOwnTenant()
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId1 = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id, 19.0);
        var taxClassId2 = await CreateTestTaxClassAsync(TenantConstants.TestTenant2Id, 20.0);
        var updateDto = CreateValidUpdateDto(30.0);

        // Act - Update tenant 1's tax class
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId1}", updateDto);

        // Try to update tenant 2's tax class from tenant 1
        var response2 = await PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId2}", updateDto);

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);

        // Verify tenant 2's tax class was not modified
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var getResponse = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId2}");
        var getResult = await ReadResponseAsync<Result<TaxClassDetailDto>>(getResponse);
        TestAssertions.AssertEqual(20.0, getResult.Data!.TaxRate); // Original value
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

    [Theory(Skip = "Todo: Tenant isolation")]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task UpdateTaxClass_WithInvalidTenantHeaderValue_ShouldReturnNotFound(string invalidTenantId)
    {
        // Arrange
        await SeedTestDataAsync();
        var taxClassId = await CreateTestTaxClassAsync(TenantConstants.TestTenant1Id);
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);
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