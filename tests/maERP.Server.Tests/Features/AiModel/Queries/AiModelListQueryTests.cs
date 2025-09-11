using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Features.AiModel.Queries;

public class AiModelListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public AiModelListQueryTests()
    {
        // Create a unique factory per test class to ensure complete isolation
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_AiModelListQueryTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        // Ensure database is created for this test
        DbContext.Database.EnsureCreated();

        // Initialize tenant context with default tenants and reset current tenant
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
    public async Task GetAiModels_WithValidTenant_ShouldReturnTenantData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetAiModels_WithDifferentTenant_ShouldReturnOnlyThatTenantData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(2);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual("ChatGPT-4O Tenant 2", result.Data?.First().Name);
    }

    [Fact]
    public async Task GetAiModels_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetAiModels_WithPagination_ShouldRespectPageSize()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels?pageNumber=0&pageSize=1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetAiModels_WithPaginationSecondPage_ShouldReturnSecondPageData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels?pageNumber=1&pageSize=1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetAiModels_WithSearchString_ShouldFilterResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels?searchString=ChatGPT");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.First().Name.Contains("ChatGPT") ?? false);
    }

    [Fact]
    public async Task GetAiModels_WithSearchStringNoMatch_ShouldReturnEmpty()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels?searchString=NonexistentModel");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetAiModels_WithOrderByName_ShouldReturnOrderedResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels?orderBy=Name");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        
        var names = result.Data?.Select(x => x.Name).ToList();
        TestAssertions.AssertEqual("ChatGPT-4O Tenant 1", names?[0]);
        TestAssertions.AssertEqual("Claude 3.5 Tenant 1", names?[1]);
    }

    [Fact]
    public async Task GetAiModels_WithOrderByNameDescending_ShouldReturnDescOrderedResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels?orderBy=Name desc");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        
        var names = result.Data?.Select(x => x.Name).ToList();
        TestAssertions.AssertEqual("Claude 3.5 Tenant 1", names?[0]);
        TestAssertions.AssertEqual("ChatGPT-4O Tenant 1", names?[1]);
    }

    [Fact]
    public async Task GetAiModels_WithMultipleOrderBy_ShouldRespectMultipleSorting()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels?orderBy=AiModelType,Name");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetAiModels_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels?pageNumber=10&pageSize=10");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(2, result.TotalCount);
    }

    [Fact]
    public async Task GetAiModels_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels?pageSize=0");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetAiModels_WithNegativePageNumber_ShouldHandleGracefully()
    {
        // Arrange
        SetTenantHeader(999); // Non-existent tenant to avoid conflicts

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels?pageNumber=-1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        // Should return empty results for non-existent tenant
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetAiModels_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        // Arrange - Use a tenant that doesn't exist in seeded data
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(999); // Non-existent tenant

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
        TestAssertions.AssertEqual(0, result.TotalPages);
    }

    [Fact]
    public async Task GetAiModels_ResponseStructure_ShouldContainRequiredFields()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstModel = result.Data?.First();
        TestAssertions.AssertNotNull(firstModel);
        TestAssertions.AssertTrue(firstModel!.Id > 0);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstModel.Name));
        TestAssertions.AssertTrue(firstModel.NCtx > 0);
        TestAssertions.AssertTrue(firstModel.AiModelType >= 0);
    }
}