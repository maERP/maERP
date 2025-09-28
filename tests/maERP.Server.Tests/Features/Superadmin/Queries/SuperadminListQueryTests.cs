using System.Net;
using System.Linq;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;

namespace maERP.Server.Tests.Features.Superadmin.Queries;

public class SuperadminListQueryTests : TenantIsolatedTestBase
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
    public async Task GetTenantList_WithSuperadmin_ShouldReturnAllTenants()
    {
        await SeedTenantsAsync();
        SetSuperadminAuthentication();

        var response = await Client.GetAsync("/api/v1/superadmin/tenants");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(3, result.Data.Count);
    }

    [Fact]
    public async Task GetTenantList_WithSearchString_ShouldFilterTenants()
    {
        await SeedTenantsAsync();
        SetSuperadminAuthentication();

        var response = await Client.GetAsync("/api/v1/superadmin/tenants?searchString=Test%20Tenant%202");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data.Count);
        var tenant = result.Data.Single();
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, tenant.Id);
        TestAssertions.AssertEqual("Test Tenant 2", tenant.Name);
    }

    [Fact]
    public async Task GetTenantList_WithPagination_ShouldRespectPageSize()
    {
        await SeedTenantsAsync();
        SetSuperadminAuthentication();

        var response = await Client.GetAsync("/api/v1/superadmin/tenants?pageNumber=0&pageSize=2");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data.Count);
        TestAssertions.AssertEqual(3, result.TotalCount);
        TestAssertions.AssertEqual(2, result.PageSize);
    }

    [Fact]
    public async Task GetTenantList_WithoutSuperadminRole_ShouldReturnForbidden()
    {
        await SeedTenantsAsync();
        SimulateAuthenticatedRequest();
        SetTestUserRoles("User");

        var response = await Client.GetAsync("/api/v1/superadmin/tenants");

        TestAssertions.AssertEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        await SeedTenantsAsync();
        SimulateUnauthenticatedRequest();

        var response = await Client.GetAsync("/api/v1/superadmin/tenants");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
