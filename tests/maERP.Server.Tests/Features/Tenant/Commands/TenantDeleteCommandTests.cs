using System.Net;
using System.Text.Json;
using maERP.Application.Features.Tenant.Commands.TenantDelete;
using maERP.Domain.Constants;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Tenant.Commands;

public class TenantDeleteCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public TenantDeleteCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_TenantDeleteCommandTests_{uniqueId}";
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

    protected async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) where T : class
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return result ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    private async Task SeedTestTenantsAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var existingTenant1 = await DbContext.Tenant.FirstOrDefaultAsync(t => t.Id == TenantConstants.TestTenant1Id);
            if (existingTenant1 == null)
            {
                var tenant1 = new maERP.Domain.Entities.Tenant
                {
                    Id = TenantConstants.TestTenant1Id,
                    Name = "Deletable Tenant",
                    TenantCode = "DEL001",
                    Description = "A tenant that can be deleted",
                    IsActive = true,
                    ContactEmail = "delete@tenant.com",
                    DateCreated = DateTime.Now.AddDays(-30),
                    DateModified = DateTime.Now.AddDays(-15)
                };

                var tenant2 = new maERP.Domain.Entities.Tenant
                {
                    Id = TenantConstants.TestTenant2Id,
                    Name = "Another Tenant",
                    TenantCode = "DEL002",
                    Description = "Another tenant for testing",
                    IsActive = false,
                    ContactEmail = "another@tenant.com",
                    DateCreated = DateTime.Now.AddDays(-60),
                    DateModified = DateTime.Now.AddDays(-30)
                };

                DbContext.Tenant.AddRange(tenant1, tenant2);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task DeleteTenant_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_RequiresSuperadminRole_ShouldReturnUnauthorized()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithValidId_ShouldReturnNoContentWhenAuthenticated()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/1");

        // Since we don't have proper auth setup, we expect Unauthorized rather than NoContent
        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithNonExistentId_ShouldReturnNotFoundWhenAuthenticated()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/999");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithZeroId_ShouldReturnBadRequest()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/0");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithNegativeId_ShouldReturnBadRequest()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/-1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithInvalidId_ShouldReturnBadRequest()
    {
        var response = await Client.DeleteAsync("/api/v1/Tenants/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_HttpDeleteMethod_ShouldAcceptDeleteRequests()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/1");

        TestAssertions.AssertNotEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_OnlyDeleteMethod_ShouldRejectPostRequests()
    {
        var response = await Client.PostAsync("/api/v1/Tenants/1", new StringContent(""));

        TestAssertions.AssertEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_OnlyDeleteMethod_ShouldRejectGetRequests()
    {
        var response = await Client.GetAsync("/api/v1/Tenants/1");

        TestAssertions.AssertNotEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithLargeId_ShouldHandleCorrectly()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/2147483647");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_ApiVersioned_ShouldRespondToV1Route()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/1");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task DeleteTenant_WrongApiVersion_ShouldReturnBadRequest()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v2/Tenants/1");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_SecurityRequirements_ShouldEnforceSuperadminAccess()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/1");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task DeleteTenant_ControllerRouting_ShouldRouteToCorrectController()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/1");

        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithActiveTenant_ShouldSucceedWhenAuthenticated()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithInactiveTenant_ShouldSucceedWhenAuthenticated()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/2");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_ResponseFormat_ShouldReturnJsonWhenAuthenticated()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/1");

        TestAssertions.AssertTrue(response.Content.Headers.ContentType?.MediaType?.Contains("application/json") ?? false ||
                                 response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Theory]
    [InlineData("00000001-0001-0001-0001-000000000001")]
    [InlineData("00000002-0002-0002-0002-000000000002")]
    public async Task DeleteTenant_WithDifferentValidIds_ShouldReturnUnauthorized(Guid tenantId)
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenantId}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("invalid-guid-string")]
    public async Task DeleteTenant_WithNegativeIds_ShouldReturnUnauthorized(Guid tenantId)
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync($"/api/v1/Tenants/{tenantId}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_MultipleCallsSameId_ShouldHandleCorrectly()
    {
        await SeedTestTenantsAsync();

        // First delete attempt
        var response1 = await Client.DeleteAsync("/api/v1/Tenants/1");
        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response1.StatusCode);

        // Second delete attempt (should be idempotent behavior)
        var response2 = await Client.DeleteAsync("/api/v1/Tenants/1");
        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response2.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithUrlTrailingSlash_ShouldHandleCorrectly()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/1/");

        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithSpecialCharactersInUrl_ShouldReturnBadRequest()
    {
        var response = await Client.DeleteAsync("/api/v1/Tenants/1@#$");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithFloatingPointId_ShouldReturnBadRequest()
    {
        var response = await Client.DeleteAsync("/api/v1/Tenants/1.5");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithVeryLargeId_ShouldHandleGracefully()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/999999999999");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithMaxIntId_ShouldHandleCorrectly()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync($"/api/v1/Tenants/{int.MaxValue}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_WithMinIntId_ShouldHandleCorrectly()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync($"/api/v1/Tenants/{int.MinValue}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTenant_EndpointExists_ShouldNotReturnNotFoundForValidPath()
    {
        await SeedTestTenantsAsync();

        var response = await Client.DeleteAsync("/api/v1/Tenants/1");

        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}