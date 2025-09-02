using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Features.Warehouse.Queries;

public class WarehouseDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public WarehouseDetailQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_WarehouseDetailQueryTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        DbContext.Database.EnsureCreated();

        TenantContext.SetAssignedTenantIds(new[] { 1, 2 });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(int tenantId)
    {
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantId.ToString());
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
    public async Task GetWarehouseById_WithValidIdAndTenant_ShouldReturnWarehouseDetails()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<WarehouseDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Id ?? 0);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(result.Data?.Name));
    }

    [Fact]
    public async Task GetWarehouseById_WithValidIdButDifferentTenant_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(2); // Try to access tenant 1's warehouse with tenant 2 header

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseById_WithNonExistentId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/999");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseById_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseById_WithInvalidTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(999); // Invalid tenant

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseById_WithZeroId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/0");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseById_WithNegativeId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/-1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseById_ResponseStructure_ShouldContainAllRequiredFields()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<WarehouseDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        var warehouse = result.Data;
        TestAssertions.AssertTrue(warehouse!.Id > 0);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(warehouse.Name));
        TestAssertions.AssertTrue(warehouse.ProductCount >= 0);
    }

    [Fact]
    public async Task GetWarehouseById_ForTenant2Warehouse_ShouldReturnCorrectData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(2);

        // Act - Assuming test data contains a warehouse with ID 3 for tenant 2
        var response = await Client.GetAsync("/api/v1/Warehouses/3");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<WarehouseDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data?.Id ?? 0);
        TestAssertions.AssertTrue(result.Data?.Name.Contains("Tenant 2") ?? false);
    }

    [Fact]
    public async Task GetWarehouseById_WithStringId_ShouldReturnBadRequest()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/abc");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetWarehouseById_TenantIsolation_ShouldNotReturnDataFromOtherTenants()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Try to access tenant 1's warehouse with tenant 2 header
        SetTenantHeader(2);
        var response = await Client.GetAsync("/api/v1/Warehouses/1");
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);

        // Try to access tenant 2's warehouse with tenant 1 header
        SetTenantHeader(1);
        var response2 = await Client.GetAsync("/api/v1/Warehouses/3");
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseById_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var tasks = Enumerable.Range(1, 5).Select(_ => Client.GetAsync("/api/v1/Warehouses/1")).ToArray();
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<Result<WarehouseDetailDto>>(response);
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
    public async Task GetWarehouseById_WithInvalidTenantHeaderValue_ShouldReturnNotFound(string invalidTenantId)
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseById_ResultStructure_ShouldHaveCorrectStatusCode()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<WarehouseDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(ResultStatusCode.Ok, result.StatusCode);
    }

    [Fact]
    public async Task GetWarehouseById_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1");
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5); // Should respond within 5 seconds
    }

    [Fact]
    public async Task GetWarehouseById_MultipleValidIds_ShouldReturnCorrectWarehouses()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act & Assert for multiple valid IDs
        foreach (var id in new[] { 1, 2 })
        {
            var response = await Client.GetAsync($"/api/v1/Warehouses/{id}");
            TestAssertions.AssertHttpSuccess(response);
            
            var result = await ReadResponseAsync<Result<WarehouseDetailDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertEqual(id, result.Data?.Id ?? 0);
        }
    }

    [Fact]
    public async Task GetWarehouseById_AfterTenantSwitch_ShouldReturnCorrectData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // First request with tenant 1
        SetTenantHeader(1);
        var response1 = await Client.GetAsync("/api/v1/Warehouses/1");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<Result<WarehouseDetailDto>>(response1);
        TestAssertions.AssertEqual(1, result1.Data?.Id ?? 0);

        // Switch to tenant 2 and try accessing same ID (should fail)
        SetTenantHeader(2);
        var response2 = await Client.GetAsync("/api/v1/Warehouses/1");
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.NotFound);

        // Access tenant 2's warehouse
        var response3 = await Client.GetAsync("/api/v1/Warehouses/3");
        TestAssertions.AssertHttpSuccess(response3);
        var result3 = await ReadResponseAsync<Result<WarehouseDetailDto>>(response3);
        TestAssertions.AssertEqual(3, result3.Data?.Id ?? 0);
    }

    [Fact]
    public async Task GetWarehouseById_WithMaxIntId_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync($"/api/v1/Warehouses/{int.MaxValue}");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseById_WithSpecialCharactersInUrl_ShouldReturnBadRequest()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1%20OR%201=1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetWarehouseById_VerifyProductCountField_ShouldReturnValidCount()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<WarehouseDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        
        // ProductCount should be non-negative
        TestAssertions.AssertTrue(result.Data!.ProductCount >= 0);
    }

    [Fact]
    public async Task GetWarehouseById_WithDifferentTenants_ShouldReturnWarehouseData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Test tenant 1 can access warehouse 1
        SetTenantHeader(1);
        var response1 = await Client.GetAsync("/api/v1/Warehouses/1");
        TestAssertions.AssertHttpSuccess(response1);
        var result1 = await ReadResponseAsync<Result<WarehouseDetailDto>>(response1);
        TestAssertions.AssertTrue(result1.Data?.Name.Contains("Tenant 1") ?? false);

        // Test tenant 2 can access warehouse 3
        SetTenantHeader(2);
        var response3 = await Client.GetAsync("/api/v1/Warehouses/3");
        TestAssertions.AssertHttpSuccess(response3);
        var result3 = await ReadResponseAsync<Result<WarehouseDetailDto>>(response3);
        TestAssertions.AssertTrue(result3.Data?.Name.Contains("Tenant 2") ?? false);
    }

    [Fact]
    public async Task GetWarehouseById_WithNonExistentTenant_ShouldReturnNotFound()
    {
        // Arrange - Use a tenant that doesn't exist in seeded data
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(999); // Non-existent tenant

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseById_ValidateNameField_ShouldNotBeEmpty()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses/1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<WarehouseDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertFalse(string.IsNullOrWhiteSpace(result.Data!.Name));
        TestAssertions.AssertTrue(result.Data.Name.Length > 0);
    }
}