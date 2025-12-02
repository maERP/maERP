using System;
using System.Net;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.Superadmin;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Xunit;

namespace maERP.Server.Tests.Features.Superadmin.Queries;

public class SuperadminDetailQueryTests : TenantIsolatedTestBase
{
    private async Task SeedTenantsAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
    }

    private async Task SeedUsersForTenantAsync()
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        // Create test users
        var user1 = new ApplicationUser
        {
            Id = "user-1-id",
            UserName = "user1@test.com",
            Email = "user1@test.com",
            Firstname = "John",
            Lastname = "Doe",
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var user2 = new ApplicationUser
        {
            Id = "user-2-id",
            UserName = "user2@test.com",
            Email = "user2@test.com",
            Firstname = "Jane",
            Lastname = "Smith",
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        DbContext.Users.AddRange(user1, user2);

        // Create user-tenant associations
        var userTenant1 = new UserTenant
        {
            Id = Guid.NewGuid(),
            UserId = user1.Id,
            TenantId = TenantConstants.TestTenant1Id,
            IsDefault = true,
            RoleManageTenant = true,
            RoleManageUser = true,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var userTenant2 = new UserTenant
        {
            Id = Guid.NewGuid(),
            UserId = user2.Id,
            TenantId = TenantConstants.TestTenant1Id,
            IsDefault = false,
            RoleManageTenant = false,
            RoleManageUser = true,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        DbContext.UserTenant.AddRange(userTenant1, userTenant2);
        await DbContext.SaveChangesAsync();
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

        var response = await Client.GetAsync($"/api/v1/superadmin/tenants/{TenantConstants.TestTenant1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SuperadminTenantDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, result.Data.Id);
        TestAssertions.AssertNotNull(result.Data.Users);
    }

    [Fact]
    public async Task GetTenantDetail_WithUnknownId_ShouldReturnNotFound()
    {
        await SeedTenantsAsync();
        SetSuperadminAuthentication();

        var response = await Client.GetAsync($"/api/v1/superadmin/tenants/{Guid.NewGuid()}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithoutSuperadminRole_ShouldReturnForbidden()
    {
        await SeedTenantsAsync();
        SimulateAuthenticatedRequest();
        SetTestUserRoles("User");

        var response = await Client.GetAsync($"/api/v1/superadmin/tenants/{TenantConstants.TestTenant1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        await SeedTenantsAsync();
        SimulateUnauthenticatedRequest();

        var response = await Client.GetAsync($"/api/v1/superadmin/tenants/{TenantConstants.TestTenant1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithInvalidGuid_ShouldReturnNotFound()
    {
        await SeedTenantsAsync();
        SetSuperadminAuthentication();

        var response = await Client.GetAsync("/api/v1/superadmin/tenants/invalid-guid");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithSuperadmin_ShouldReturnUsersForTenant()
    {
        await SeedUsersForTenantAsync();
        SetSuperadminAuthentication();

        var response = await Client.GetAsync($"/api/v1/superadmin/tenants/{TenantConstants.TestTenant1Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SuperadminTenantDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data.UserCount);
        TestAssertions.AssertEqual(2, result.Data.Users.Count);

        // Users should be ordered by Lastname, then Firstname
        var firstUser = result.Data.Users[0];
        TestAssertions.AssertEqual("John", firstUser.Firstname);
        TestAssertions.AssertEqual("Doe", firstUser.Lastname);
        TestAssertions.AssertEqual("user1@test.com", firstUser.Email);
        TestAssertions.AssertTrue(firstUser.IsDefault);
        TestAssertions.AssertTrue(firstUser.RoleManageTenant);
        TestAssertions.AssertTrue(firstUser.RoleManageUser);

        var secondUser = result.Data.Users[1];
        TestAssertions.AssertEqual("Jane", secondUser.Firstname);
        TestAssertions.AssertEqual("Smith", secondUser.Lastname);
        TestAssertions.AssertFalse(secondUser.IsDefault);
        TestAssertions.AssertFalse(secondUser.RoleManageTenant);
        TestAssertions.AssertTrue(secondUser.RoleManageUser);
    }

    [Fact]
    public async Task GetTenantDetail_WithNoUsers_ShouldReturnEmptyUserList()
    {
        await SeedTenantsAsync();
        SetSuperadminAuthentication();

        var response = await Client.GetAsync($"/api/v1/superadmin/tenants/{TenantConstants.TestTenant3Id}");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<SuperadminTenantDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(0, result.Data.UserCount);
        TestAssertions.AssertEqual(0, result.Data.Users.Count);
    }
}
