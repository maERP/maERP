using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

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
        SetTenantHeader(999); // Non-existent tenant ID for testing tenant isolation
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
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Id ?? 0);
        TestAssertions.AssertTrue(result.Data?.TaxRate >= 0);
    }

    [Fact]
    public async Task GetTaxClassById_WithValidIdButDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(2); // Try to access tenant 1's tax class with tenant 2 header

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/999");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetInvalidTenantHeader();

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_WithZeroId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/0");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_WithNegativeId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/-1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_ResponseStructure_ShouldContainAllRequiredFields()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        var taxClass = result.Data;
        TestAssertions.AssertTrue(taxClass!.Id > 0);
        TestAssertions.AssertTrue(taxClass.TaxRate >= 0);
        TestAssertions.AssertTrue(taxClass.TaxRate <= 100); // Assuming tax rate is percentage
    }

    [Fact]
    public async Task GetTaxClassById_ForTenant2TaxClass_ShouldReturnCorrectData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(2);

        // Act - Assuming test data contains a tax class with ID 3 for tenant 2
        var response = await Client.GetAsync("/api/v1/TaxClasses/3");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Id ?? 0);
    }

    [Fact]
    public async Task GetTaxClassById_WithStringId_ShouldReturnBadRequest()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/abc");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetTaxClassById_TenantIsolation_ShouldNotReturnDataFromOtherTenants()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Try to access tenant 1's tax class with tenant 2 header
        SetTenantHeader(2);
        var response = await Client.GetAsync("/api/v1/TaxClasses/1");
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Try to access tenant 2's tax class with tenant 1 header
        SetTenantHeader(1);
        var response2 = await Client.GetAsync("/api/v1/TaxClasses/3");
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var tasks = Enumerable.Range(1, 5).Select(_ => Client.GetAsync("/api/v1/TaxClasses/1")).ToArray();
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<Result<TaxClassDetailDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertEqual(1, result.Data?.Id ?? 0);
        }
    }

    [Theory]
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
        var response = await Client.GetAsync("/api/v1/TaxClasses/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_ResultStructure_ShouldHaveCorrectStatusCode()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/1");

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
        SetTenantHeader(1);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/1");
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
        SetTenantHeader(1);

        // Act & Assert for multiple valid IDs
        foreach (var id in new[] { 1, 2 })
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

        // First request with tenant 1
        SetTenantHeader(1);
        var response1 = await Client.GetAsync("/api/v1/TaxClasses/1");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<Result<TaxClassDetailDto>>(response1);
        TestAssertions.AssertEqual(1, result1.Data?.Id ?? 0);

        // Switch to tenant 2 and try accessing same ID (should fail)
        SetTenantHeader(2);
        var response2 = await Client.GetAsync("/api/v1/TaxClasses/1");
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);

        // Access tenant 2's tax class
        var response3 = await Client.GetAsync("/api/v1/TaxClasses/3");
        TestAssertions.AssertHttpSuccess(response3);
        var result3 = await ReadResponseAsync<Result<TaxClassDetailDto>>(response3);
        TestAssertions.AssertEqual(3, result3.Data?.Id ?? 0);
    }

    [Fact]
    public async Task GetTaxClassById_WithMaxIntId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync($"/api/v1/TaxClasses/{int.MaxValue}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTaxClassById_WithSpecialCharactersInUrl_ShouldReturnBadRequest()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/1%20OR%201=1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetTaxClassById_VerifyTaxRateValidation_ShouldReturnValidTaxRate()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/TaxClasses/1");

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