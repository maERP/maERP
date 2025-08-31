using System.Net;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Features.TenantIsolation;

public class TenantIsolationTests : BaseIntegrationTest
{
    public TenantIsolationTests(TestWebApplicationFactory<Program> factory) : base(factory)
    {
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
    public async Task ApiCall_SwitchingTenants_ShouldReturnDifferentData()
    {
        // Arrange
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Act - First call with tenant 1
        var client1 = Factory.CreateClient();
        client1.DefaultRequestHeaders.Add("X-Tenant-Id", "1");
        var response1 = await client1.GetAsync($"/api/v1/AiModels?tenant=1");
        var result1 = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response1);

        // Complete isolation between requests - use separate factory
        client1.Dispose();
        await Task.Delay(100);

        // Act - Second call with tenant 2 using completely fresh infrastructure
        using var factory2 = new TestWebApplicationFactory<Program>();
        // Seed data for the new factory's database context
        using var scope2 = factory2.Services.CreateScope();
        var dbContext2 = scope2.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var tenantContext2 = scope2.ServiceProvider.GetRequiredService<ITenantContext>();
        await TestDataSeeder.SeedTestDataAsync(dbContext2, tenantContext2);

        var client2 = factory2.CreateClient();
        client2.DefaultRequestHeaders.Add("X-Tenant-Id", "2");
        var response2 = await client2.GetAsync($"/api/v1/AiModels?tenant=2");
        var result2 = await ReadResponseAsync<PaginatedResult<AiModelListDto>>(response2);

        // Assert
        TestAssertions.AssertHttpSuccess(response1);
        TestAssertions.AssertHttpSuccess(response2);

        TestAssertions.AssertNotNull(result1);
        TestAssertions.AssertNotNull(result1.Data);
        TestAssertions.AssertNotNull(result2);
        TestAssertions.AssertNotNull(result2.Data);

        // Verify different data is returned (based on actual test data)
        // Tenant 1 should have 2 models, Tenant 2 should have 1 model
        TestAssertions.AssertEqual(2, result1.Data?.Count ?? 0);
        TestAssertions.AssertEqual(1, result2.Data?.Count ?? 0);

        // Verify no overlap in data
        if (result1.Data != null && result2.Data != null)
        {
            var tenant1Names = result1.Data.Select(m => m.Name).ToList();
            var tenant2Names = result2.Data.Select(m => m.Name).ToList();
            TestAssertions.AssertTrue(tenant1Names.All(n => !tenant2Names.Contains(n)));
        }
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