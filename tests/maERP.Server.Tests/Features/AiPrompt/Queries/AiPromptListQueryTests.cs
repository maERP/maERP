using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Features.AiPrompt.Queries;

public class AiPromptListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public AiPromptListQueryTests()
    {
        // Create a unique factory per test class to ensure complete isolation
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_AiPromptListQueryTests_{uniqueId}";
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
    public async Task GetAiPrompts_WithValidTenant_ShouldReturnTenantData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetAiPrompts_WithDifferentTenant_ShouldReturnOnlyThatTenantData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(2);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertTrue(result.Data?.First().Identifier.Contains("Tenant 2") ?? false);
    }

    [Fact]
    public async Task GetAiPrompts_WithoutTenantHeader_ShouldReturnEmptyResult()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetAiPrompts_WithPagination_ShouldRespectPageSize()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?pageNumber=0&pageSize=1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetAiPrompts_WithPaginationSecondPage_ShouldReturnSecondPageData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?pageNumber=1&pageSize=1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        TestAssertions.AssertEqual(2, result.TotalCount);
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetAiPrompts_WithSearchString_ShouldFilterResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?searchString=Prompt");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);
        TestAssertions.AssertTrue(result.Data?.All(p => p.Identifier.Contains("Prompt") || p.PromptText.Contains("Prompt")) ?? false);
    }

    [Fact]
    public async Task GetAiPrompts_WithSearchStringNoMatch_ShouldReturnEmpty()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?searchString=NonexistentPrompt");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetAiPrompts_WithOrderByIdentifier_ShouldReturnOrderedResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?orderBy=Identifier");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        
        var identifiers = result.Data?.Select(x => x.Identifier).ToList();
        TestAssertions.AssertTrue(string.Compare(identifiers?[0], identifiers?[1], StringComparison.Ordinal) <= 0);
    }

    [Fact]
    public async Task GetAiPrompts_WithOrderByIdentifierDescending_ShouldReturnDescOrderedResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?orderBy=Identifier desc");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        
        var identifiers = result.Data?.Select(x => x.Identifier).ToList();
        TestAssertions.AssertTrue(string.Compare(identifiers?[0], identifiers?[1], StringComparison.Ordinal) >= 0);
    }

    [Fact]
    public async Task GetAiPrompts_WithMultipleOrderBy_ShouldRespectMultipleSorting()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?orderBy=DateCreated,Identifier");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetAiPrompts_WithInvalidPageNumber_ShouldReturnEmptyResults()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?pageNumber=10&pageSize=10");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(2, result.TotalCount);
    }

    [Fact]
    public async Task GetAiPrompts_WithZeroPageSize_ShouldUseDefaultPageSize()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?pageSize=0");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
    }

    [Fact]
    public async Task GetAiPrompts_WithNegativePageNumber_ShouldHandleGracefully()
    {
        // Arrange
        SetTenantHeader(999); // Non-existent tenant to avoid conflicts

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?pageNumber=-1");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetAiPrompts_WithNonExistentTenant_ShouldReturnEmptyPaginatedResult()
    {
        // Arrange - Use a tenant that doesn't exist in seeded data
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(999); // Non-existent tenant

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
        TestAssertions.AssertEqual(0, result.TotalPages);
    }

    [Fact]
    public async Task GetAiPrompts_ResponseStructure_ShouldContainRequiredFields()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var firstPrompt = result.Data?.First();
        TestAssertions.AssertNotNull(firstPrompt);
        TestAssertions.AssertTrue(firstPrompt!.Id > 0);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstPrompt.Identifier));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(firstPrompt.PromptText));
        TestAssertions.AssertTrue(firstPrompt.DateCreated != DateTime.MinValue);
        TestAssertions.AssertTrue(firstPrompt.DateModified != DateTime.MinValue);
    }

    [Fact]
    public async Task GetAiPrompts_TenantIsolation_ShouldNotReturnDataFromOtherTenants()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Test Tenant 1
        SetTenantHeader(1);
        var responseTenant1 = await Client.GetAsync("/api/v1/AiPrompts");
        var resultTenant1 = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(responseTenant1);

        // Test Tenant 2
        SetTenantHeader(2);
        var responseTenant2 = await Client.GetAsync("/api/v1/AiPrompts");
        var resultTenant2 = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(responseTenant2);

        // Assert
        TestAssertions.AssertNotNull(resultTenant1.Data);
        TestAssertions.AssertNotNull(resultTenant2.Data);
        
        // Verify that tenant data don't overlap
        var tenant1Identifiers = resultTenant1.Data.Select(p => p.Identifier).ToList();
        var tenant2Identifiers = resultTenant2.Data.Select(p => p.Identifier).ToList();
        
        TestAssertions.AssertFalse(tenant1Identifiers.Any(id => tenant2Identifiers.Contains(id)));
    }

    [Fact]
    public async Task GetAiPrompts_WithOrderByDateCreated_ShouldOrderByCreationDate()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?orderBy=DateCreated");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);

        var dates = result.Data?.Select(x => x.DateCreated).ToList();
        for (int i = 1; i < dates?.Count; i++)
        {
            TestAssertions.AssertTrue(dates[i - 1] <= dates[i]);
        }
    }

    [Fact]
    public async Task GetAiPrompts_WithOrderByDateModified_ShouldOrderByModificationDate()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts?orderBy=DateModified");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);

        var dates = result.Data?.Select(x => x.DateModified).ToList();
        for (int i = 1; i < dates?.Count; i++)
        {
            TestAssertions.AssertTrue(dates[i - 1] <= dates[i]);
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task GetAiPrompts_WithInvalidTenantHeaderValue_ShouldReturnEmptyResult(string invalidTenantId)
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);

        // Act
        var response = await Client.GetAsync("/api/v1/AiPrompts");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetAiPrompts_ConcurrentRequests_ShouldHandleCorrectly()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        SetTenantHeader(1);

        // Act
        var tasks = Enumerable.Range(1, 5).Select(_ => Client.GetAsync("/api/v1/AiPrompts")).ToArray();
        var responses = await Task.WhenAll(tasks);

        // Assert
        foreach (var response in responses)
        {
            TestAssertions.AssertHttpSuccess(response);
            var result = await ReadResponseAsync<PaginatedResult<AiPromptListDto>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertNotNull(result.Data);
            TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        }
    }
}