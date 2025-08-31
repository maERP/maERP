using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Features.TenantIsolation;

public class TenantIsolationTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public TenantIsolationTests()
    {
        // Create a unique factory per test class to ensure complete isolation
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_TenantIsolationTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        // Ensure database is created for this test
        DbContext.Database.EnsureCreated();

        // Initialize tenant context with default tenants and reset current tenant
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
    public async Task ApiCall_WithInvalidTenantId_ShouldNotReturnOtherTenantData()
    {
        // Arrange
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
    }

    [Fact]
    public async Task ApiCall_WithTenant1_ShouldReturnTenant1DataOnly()
    {
        // Arrange - Seed test data
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        
        // Act - Call with tenant 1
        SetTenantHeader(1);
        var response = await Client.GetAsync("/api/v1/AiModels");
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Verify tenant 1 gets exactly its data (2 models)
        TestAssertions.AssertEqual(2, result.Data?.Count ?? 0);
        
        // Verify the actual data belongs to tenant 1
        var modelNames = result.Data?.Select(x => x.Name).ToList() ?? new List<string>();
        TestAssertions.AssertTrue(modelNames.Contains("ChatGPT-4O Tenant 1"));
        TestAssertions.AssertTrue(modelNames.Contains("Claude 3.5 Tenant 1"));
        TestAssertions.AssertFalse(modelNames.Contains("ChatGPT-4O Tenant 2"));
    }

    [Fact]
    public async Task ApiCall_WithTenant2_ShouldReturnTenant2DataOnly()
    {
        // Arrange - Seed test data
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        
        // Act - Call with tenant 2
        SetTenantHeader(2);
        var response = await Client.GetAsync("/api/v1/AiModels");
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);

        // Verify tenant 2 gets exactly its data (1 model)
        TestAssertions.AssertEqual(1, result.Data?.Count ?? 0);
        
        // Verify the actual data belongs to tenant 2
        var modelNames = result.Data?.Select(x => x.Name).ToList() ?? new List<string>();
        TestAssertions.AssertTrue(modelNames.Contains("ChatGPT-4O Tenant 2"));
        TestAssertions.AssertFalse(modelNames.Contains("ChatGPT-4O Tenant 1"));
        TestAssertions.AssertFalse(modelNames.Contains("Claude 3.5 Tenant 1"));
    }

    [Fact]
    public async Task ApiCall_WithoutTenantHeader_ShouldHaveEmptyTenantContext()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        // No tenant header set intentionally

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);

        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("abc")]
    [InlineData("")]
    public async Task ApiCall_WithInvalidTenantHeaderValue_ShouldReturnEmptyData(string invalidTenantId)
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
        Client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        Client.DefaultRequestHeaders.Add("X-Tenant-Id", invalidTenantId);

        // Act
        var response = await Client.GetAsync("/api/v1/AiModels");

        // Assert
        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response);

        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }
}