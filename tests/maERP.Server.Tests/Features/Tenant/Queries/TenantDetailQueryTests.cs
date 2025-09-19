using System;
using System.Net;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;

namespace maERP.Server.Tests.Features.Tenant.Queries;

public class TenantDetailQueryTests : TenantIsolatedTestBase
{
    private async Task SeedTenantsAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
    }

    private void SetSuperadminAuthentication()
    {
        SimulateAuthenticatedRequest();
        SetTestUserRoles("Superadmin");
    }

    [Fact]
    public async Task GetTenantDetail_WithSuperadmin_ShouldReturnTenant()
    {
        await SeedTenantsAsync();
        SetSuperadminAuthentication();

        var response = await Client.GetAsync($"/api/v1/Tenants/{TenantConstants.TestTenant1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<TenantDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, result.Data.Id);
    }

    [Fact]
    public async Task GetTenantDetail_WithUnknownId_ShouldReturnNotFound()
    {
        await SeedTenantsAsync();
        SetSuperadminAuthentication();

        var response = await Client.GetAsync($"/api/v1/Tenants/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithoutSuperadminRole_ShouldReturnForbidden()
    {
        await SeedTenantsAsync();
        SimulateAuthenticatedRequest();
        SetTestUserRoles("User");

        var response = await Client.GetAsync($"/api/v1/Tenants/{TenantConstants.TestTenant1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        await SeedTenantsAsync();
        SimulateUnauthenticatedRequest();

        var response = await Client.GetAsync($"/api/v1/Tenants/{TenantConstants.TestTenant1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithInvalidGuid_ShouldReturnNotFound()
    {
        await SeedTenantsAsync();
        SetSuperadminAuthentication();

        var response = await Client.GetAsync("/api/v1/Tenants/invalid-guid");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
