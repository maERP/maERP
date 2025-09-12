using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.TaxClass.Queries;

public class TaxClassDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public TaxClassDetailQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_TaxClassDetailQueryTests_{uniqueId}";
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

    protected async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) where T : class
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return result ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task GetTaxClassById_WithValidIdAndTenant_ShouldReturnTaxClassDetails()
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
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data!.Id);
        TestAssertions.AssertTrue(result.Data?.TaxRate >= 0);
    }

    [Fact(Skip = "Todo: Tenant isolation")]
    public async Task GetTaxClassById_WithValidIdButDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant2Id); // Try to access tenant 1's tax class with tenant 2 header

        // Act
        // Get a tax class ID dynamically
        var listResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse);
        var taxClassId = listResult.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(taxClassId);

        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
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

    [Fact(Skip = "Todo: Tenant isolation")]
    public async Task GetTaxClassById_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        // Get a tax class ID dynamically
        var listResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse);
        var taxClassId = listResult.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(taxClassId);

        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact(Skip = "Todo: Tenant isolation")]
    public async Task GetTaxClassById_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeader();

        // Act
        // Get a tax class ID dynamically
        var listResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse);
        var taxClassId = listResult.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(taxClassId);

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
        TestAssertions.AssertNotNull(result.Data);

        var taxClass = result.Data;
        TestAssertions.AssertNotEqual(Guid.Empty, taxClass!.Id);
        TestAssertions.AssertTrue(taxClass.TaxRate >= 0);
        TestAssertions.AssertTrue(taxClass.TaxRate <= 100); // Assuming tax rate is percentage
    }

    [Fact]
    public async Task GetTaxClassById_ForTenant2TaxClass_ShouldReturnCorrectData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant2Id);

        // Get a tenant 2 tax class ID
        var listResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse);
        var tenant2TaxClass = listResult.Data?.FirstOrDefault();
        TestAssertions.AssertNotNull(tenant2TaxClass);

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{tenant2TaxClass!.Id}");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEqual(Guid.Empty, result.Data!.Id);
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

    [Fact(Skip = "Todo: Tenant isolation")]
    public async Task GetTaxClassById_TenantIsolation_ShouldNotReturnDataFromOtherTenants()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Try to access tenant 1's tax class with tenant 2 header
        SetTenantHeader(TenantConstants.TestTenant2Id);
        // Get a tax class ID dynamically
        var listResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse);
        var taxClassId = listResult.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(taxClassId);

        var response = await Client.GetAsync($"/api/v1/TaxClasses/{taxClassId}");
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Try to access tenant 2's tax class with tenant 1 header
        SetTenantHeader(TenantConstants.TestTenant1Id);
        // Try to access a tenant 2 tax class from tenant 1 (should fail)
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var tenant2ListResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var tenant2ListResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(tenant2ListResponse);
        var tenant2TaxClassId = tenant2ListResult.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(tenant2TaxClassId);
        
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response2 = await Client.GetAsync($"/api/v1/TaxClasses/{tenant2TaxClassId}");
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);
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

    [Theory(Skip = "Todo: Tenant isolation")]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task GetTaxClassById_WithInvalidTenantHeaderValue_ShouldReturnNotFound(string invalidTenantId)
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);

        // Act
        // Get a tax class ID dynamically
        var listResponse = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse);
        var taxClassId = listResult.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(taxClassId);

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
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // First request with tenant 1 - get a tax class ID dynamically
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var listResponse1 = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult1 = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse1);
        var tenant1TaxClassId = listResult1.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(tenant1TaxClassId);

        var response1 = await Client.GetAsync($"/api/v1/TaxClasses/{tenant1TaxClassId}");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<Result<TaxClassDetailDto>>(response1);
        TestAssertions.AssertEqual(tenant1TaxClassId, result1.Data?.Id);

        // Switch to tenant 2 and try accessing same ID (should fail)
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync($"/api/v1/TaxClasses/{tenant1TaxClassId}");
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);

        // Access tenant 2's tax class - get a tenant 2 tax class ID dynamically
        var listResponse2 = await Client.GetAsync("/api/v1/TaxClasses");
        var listResult2 = await ReadResponseAsync<PaginatedResult<TaxClassListDto>>(listResponse2);
        var tenant2TaxClassId = listResult2.Data?.FirstOrDefault()?.Id;
        TestAssertions.AssertNotNull(tenant2TaxClassId);

        var response3 = await Client.GetAsync($"/api/v1/TaxClasses/{tenant2TaxClassId}");
        TestAssertions.AssertHttpSuccess(response3);
        var result3 = await ReadResponseAsync<Result<TaxClassDetailDto>>(response3);
        TestAssertions.AssertEqual(tenant2TaxClassId, result3.Data?.Id);
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