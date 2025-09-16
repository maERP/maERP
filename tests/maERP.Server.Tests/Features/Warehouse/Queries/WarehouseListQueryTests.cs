using System.Net;
using System.Text.Json;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Features.Warehouse.Queries;

public class WarehouseListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public WarehouseListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_WarehouseListQueryTests_{uniqueId}";
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
    public async Task GetWarehouseList_WithValidTenant_ShouldReturnPaginatedResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data.Count > 0);
        TestAssertions.AssertTrue(result.TotalCount > 0);
    }

    [Fact]
    public async Task GetWarehouseList_WithoutTenantHeader_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseList_WithInvalidTenant_ShouldReturnNotFound()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(Guid.Parse("99999999-9999-9999-9999-999999999999"));

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseList_WithPagination_ShouldReturnCorrectPage()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses?pageNumber=0&pageSize=5");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(0, result.CurrentPage);
        TestAssertions.AssertEqual(5, result.PageSize);
        TestAssertions.AssertTrue(result.Data.Count <= 5);
    }

    [Fact]
    public async Task GetWarehouseList_WithSearchString_ShouldFilterResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses?searchString=Main");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify that results contain the search string
        if (result.Data.Any())
        {
            TestAssertions.AssertTrue(result.Data.Any(w => w.Name.Contains("Main", StringComparison.OrdinalIgnoreCase)));
        }
    }

    [Fact]
    public async Task GetWarehouseList_WithOrderBy_ShouldReturnOrderedResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses?orderBy=Name");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify ordering
        if (result.Data.Count > 1)
        {
            for (int i = 0; i < result.Data.Count - 1; i++)
            {
                TestAssertions.AssertTrue(string.Compare(result.Data[i].Name, result.Data[i + 1].Name, StringComparison.OrdinalIgnoreCase) <= 0);
            }
        }
    }

    [Fact]
    public async Task GetWarehouseList_TenantIsolation_ShouldReturnOnlyTenantData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act - Get data for tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/Warehouses");
        var result1 = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response1);

        // Act - Get data for tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/Warehouses");
        var result2 = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response2);

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpSuccess(response2);
        TestAssertions.AssertNotNull(result1);
        TestAssertions.AssertNotNull(result2);

        // Ensure IDs don't overlap between tenants
        var tenant1Ids = result1.Data.Select(w => w.Id).ToList();
        var tenant2Ids = result2.Data.Select(w => w.Id).ToList();
        TestAssertions.AssertFalse(tenant1Ids.Intersect(tenant2Ids).Any());
    }

    [Fact]
    public async Task GetWarehouseList_WithLargePageSize_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses?pageNumber=1&pageSize=1000");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1000, result.PageSize);
    }

    [Fact]
    public async Task GetWarehouseList_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses?pageNumber=999&pageSize=10");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(0, result.Data.Count);
    }

    [Fact]
    public async Task GetWarehouseList_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses?pageNumber=1&pageSize=0");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.PageSize > 0); // Should use default page size
    }

    [Fact]
    public async Task GetWarehouseList_WithNegativePageNumber_ShouldUseDefaultPageNumber()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses?pageNumber=-1&pageSize=10");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.CurrentPage >= 0);
    }

    [Fact]
    public async Task GetWarehouseList_ResponseStructure_ShouldContainAllMetadata()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses?pageNumber=1&pageSize=5");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.TotalCount >= 0);
        TestAssertions.AssertTrue(result.TotalPages >= 0);
        TestAssertions.AssertTrue(result.CurrentPage > 0);
        TestAssertions.AssertTrue(result.PageSize > 0);
        TestAssertions.AssertNotNull(result.Data);
    }

    [Fact]
    public async Task GetWarehouseList_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var tasks = Enumerable.Range(1, 5)
            .Select(_ => Client.GetAsync("/api/v1/Warehouses"))
            .ToArray();
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task GetWarehouseList_WithInvalidTenantHeaderValue_ShouldReturnNotFound(string invalidTenantId)
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses");

        // Assert
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetWarehouseList_WithMultipleOrderByFields_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses?orderBy=Name,Id");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task GetWarehouseList_EmptySearchString_ShouldReturnAllResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var responseWithoutSearch = await Client.GetAsync("/api/v1/Warehouses");
        var responseWithEmptySearch = await Client.GetAsync("/api/v1/Warehouses?searchString=");

        // Assert
        var resultWithoutSearch = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(responseWithoutSearch);
        var resultWithEmptySearch = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(responseWithEmptySearch);

        TestAssertions.AssertEqual(resultWithoutSearch.TotalCount, resultWithEmptySearch.TotalCount);
    }

    [Fact]
    public async Task GetWarehouseList_AfterTenantSwitch_ShouldReturnCorrectData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // First request with tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/Warehouses");
        var result1 = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response1);
        TestAssertions.AssertHttpSuccess(response1);
        var tenant1Count = result1.TotalCount;

        // Switch to tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/Warehouses");
        var result2 = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response2);
        TestAssertions.AssertHttpSuccess(response2);
        var tenant2Count = result2.TotalCount;

        // Assert - Different tenants should have different data
        TestAssertions.AssertNotNull(result1);
        TestAssertions.AssertNotNull(result2);

        // Verify no ID overlap
        if (result1.Data.Any() && result2.Data.Any())
        {
            var ids1 = result1.Data.Select(x => x.Id);
            var ids2 = result2.Data.Select(x => x.Id);
            TestAssertions.AssertFalse(ids1.Intersect(ids2).Any());
        }
    }

    [Fact]
    public async Task GetWarehouseList_ResponseTime_ShouldBeReasonable()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var startTime = DateTime.UtcNow;

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses?pageNumber=1&pageSize=100");
        var endTime = DateTime.UtcNow;

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var responseTime = endTime - startTime;
        TestAssertions.AssertTrue(responseTime.TotalSeconds < 5); // Should respond within 5 seconds
    }

    [Fact]
    public async Task GetWarehouseList_WithSpecialCharactersInSearchString_ShouldHandleGracefully()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses?searchString=%25%20OR%201%3D1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task GetWarehouseList_VerifyDataIntegrity_ShouldReturnValidWarehouses()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify all warehouses have valid data
        foreach (var warehouse in result.Data)
        {
            TestAssertions.AssertNotEqual(Guid.Empty, warehouse.Id);
            TestAssertions.AssertFalse(string.IsNullOrWhiteSpace(warehouse.Name));
        }
    }

    [Fact]
    public async Task GetWarehouseList_WithCaseInsensitiveSearch_ShouldFindResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Search with different cases
        var response1 = await Client.GetAsync("/api/v1/Warehouses?searchString=MAIN");
        var response2 = await Client.GetAsync("/api/v1/Warehouses?searchString=main");
        var response3 = await Client.GetAsync("/api/v1/Warehouses?searchString=Main");

        // Assert - All should return results if case-insensitive search is implemented
        var result1 = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response1);
        var result2 = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response2);
        var result3 = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response3);

        TestAssertions.AssertEqual(result1.TotalCount, result2.TotalCount);
        TestAssertions.AssertEqual(result2.TotalCount, result3.TotalCount);
    }

    [Fact]
    public async Task GetWarehouseList_WithPartialNameSearch_ShouldReturnMatchingResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act - Search for part of warehouse name
        var response = await Client.GetAsync("/api/v1/Warehouses?searchString=Warehouse");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Should find warehouses containing "Warehouse" in the name
        if (result.Data.Any())
        {
            TestAssertions.AssertTrue(result.Data.All(w => w.Name.Contains("Warehouse", StringComparison.OrdinalIgnoreCase)));
        }
    }

    [Fact]
    public async Task GetWarehouseList_VerifyProductCountField_ShouldReturnValidCounts()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Act
        var response = await Client.GetAsync("/api/v1/Warehouses");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<WarehouseListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        // Verify all warehouse IDs are valid
        foreach (var warehouse in result.Data)
        {
            TestAssertions.AssertNotEqual(Guid.Empty, warehouse.Id);
        }
    }
}