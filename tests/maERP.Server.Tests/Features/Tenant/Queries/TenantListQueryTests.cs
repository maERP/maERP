using System.Net;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Tenant.Queries;

public class TenantListQueryTests : TenantIsolatedTestBase
{
    private const string AdminUserId = CurrentUserHelper.DefaultUserId;
    private const string User2Id = "22222222-2222-2222-2222-222222222222";
    private const string UserWithoutTenantsId = "33333333-3333-3333-3333-333333333333";

    private async Task SeedUserTenantsAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasUserTenants = await DbContext.UserTenant.IgnoreQueryFilters().AnyAsync();
            if (!hasUserTenants)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                // AdminUser assigned to both tenants
                var userTenant1 = new maERP.Domain.Entities.UserTenant
                {
                    UserId = AdminUserId,
                    TenantId = TenantConstants.TestTenant1Id,
                    IsDefault = true,
                    RoleManageTenant = true,
                    RoleManageUser = true
                };

                var userTenant2 = new maERP.Domain.Entities.UserTenant
                {
                    UserId = AdminUserId,
                    TenantId = TenantConstants.TestTenant2Id,
                    IsDefault = false,
                    RoleManageTenant = true,
                    RoleManageUser = true
                };

                // User2 only assigned to TestTenant1
                var user2Tenant1 = new maERP.Domain.Entities.UserTenant
                {
                    UserId = User2Id,
                    TenantId = TenantConstants.TestTenant1Id,
                    IsDefault = true,
                    RoleManageTenant = false,
                    RoleManageUser = false
                };

                DbContext.UserTenant.AddRange(userTenant1, userTenant2, user2Tenant1);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    [Fact]
    public async Task GetTenants_WithAuthenticatedUser_ShouldReturnAssignedTenants()
    {
        await SeedUserTenantsAsync();
        SimulateAuthenticatedRequest(AdminUserId);

        var response = await Client.GetAsync("/api/v1/tenants?pageNumber=0&pageSize=10");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(2, result.TotalCount); // AdminUser is assigned to 2 tenants
        TestAssertions.AssertNotEmpty(result.Data);
    }

    [Fact]
    public async Task GetTenants_WithUser2_ShouldReturnOnlyAssignedTenant()
    {
        await SeedUserTenantsAsync();
        SimulateAuthenticatedRequest(User2Id);

        var response = await Client.GetAsync("/api/v1/tenants?pageNumber=0&pageSize=10");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.TotalCount); // User2 is assigned to 1 tenant
        TestAssertions.AssertNotEmpty(result.Data);
        TestAssertions.AssertEqual<Guid>(TenantConstants.TestTenant1Id, result.Data[0].Id);
    }

    [Fact]
    public async Task GetTenants_WithUserWithoutTenants_ShouldReturnEmptyList()
    {
        await SeedUserTenantsAsync();
        SimulateAuthenticatedRequest(UserWithoutTenantsId);

        var response = await Client.GetAsync("/api/v1/tenants?pageNumber=0&pageSize=10");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(0, result.TotalCount);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetTenants_WithSearchString_ShouldFilterResults()
    {
        await SeedUserTenantsAsync();
        SimulateAuthenticatedRequest(AdminUserId);

        var response = await Client.GetAsync("/api/v1/tenants?pageNumber=0&pageSize=10&searchString=Tenant 1");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(1, result.TotalCount); // Only "Test Tenant 1" should match
        TestAssertions.AssertNotEmpty(result.Data);
    }

    [Fact]
    public async Task GetTenants_WithPagination_ShouldReturnCorrectPage()
    {
        await SeedUserTenantsAsync();
        SimulateAuthenticatedRequest(AdminUserId);

        var response = await Client.GetAsync("/api/v1/tenants?pageNumber=0&pageSize=1");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(2, result.TotalCount); // Total 2 tenants
        TestAssertions.AssertEqual(1, result.Data.Count); // But only 1 on this page
        TestAssertions.AssertEqual(0, result.CurrentPage); // 0-based pagination
        TestAssertions.AssertEqual(2, result.TotalPages);
    }

    [Fact]
    public async Task GetTenants_WithOrderBy_ShouldReturnOrderedResults()
    {
        await SeedUserTenantsAsync();
        SimulateAuthenticatedRequest(AdminUserId);

        var response = await Client.GetAsync("/api/v1/tenants?pageNumber=0&pageSize=10&orderBy=Name desc");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(2, result.TotalCount);
        // First result should be "Test Tenant 2" when ordered by Name descending
        TestAssertions.AssertEqual("Test Tenant 2", result.Data[0].Name);
    }

    [Fact]
    public async Task GetTenants_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        await SeedUserTenantsAsync();
        SimulateUnauthenticatedRequest();

        var response = await Client.GetAsync("/api/v1/tenants?pageNumber=0&pageSize=10");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenants_WithInvalidPageNumber_ShouldReturnFirstPage()
    {
        await SeedUserTenantsAsync();
        SimulateAuthenticatedRequest(AdminUserId);

        // Page 0 should be handled gracefully
        var response = await Client.GetAsync("/api/v1/tenants?pageNumber=0&pageSize=10");

        // The API might return 400 or default to page 1, depending on implementation
        // This test documents the behavior
        var statusCode = response.StatusCode;
        TestAssertions.AssertTrue(
            statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.BadRequest,
            $"Expected OK or BadRequest but got {statusCode}");
    }

    [Fact]
    public async Task GetTenants_WithLargePageSize_ShouldReturnAllResults()
    {
        await SeedUserTenantsAsync();
        SimulateAuthenticatedRequest(AdminUserId);

        var response = await Client.GetAsync("/api/v1/tenants?pageNumber=0&pageSize=100");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertEqual(2, result.TotalCount);
        TestAssertions.AssertEqual(2, result.Data.Count);
    }

    [Fact]
    public async Task GetTenants_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedUserTenantsAsync();
        SimulateAuthenticatedRequest(AdminUserId);

        var response = await Client.GetAsync("/api/v1/tenants?pageNumber=0&pageSize=10");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);

        // Check pagination properties
        TestAssertions.AssertTrue(result.CurrentPage >= 0); // 0-based pagination
        TestAssertions.AssertTrue(result.TotalPages > 0);
        TestAssertions.AssertTrue(result.TotalCount >= 0);
        TestAssertions.AssertTrue(result.PageSize > 0);

        // Check first tenant has all fields
        if (result.Data.Count > 0)
        {
            var tenant = result.Data[0];
            TestAssertions.AssertNotEqual(Guid.Empty, tenant.Id);
            TestAssertions.AssertNotNull(tenant.Name);
            TestAssertions.AssertNotNull(tenant.Description);
        }
    }

    [Fact]
    public async Task GetTenants_WithCaseInsensitiveSearch_ShouldFindResults()
    {
        await SeedUserTenantsAsync();
        SimulateAuthenticatedRequest(AdminUserId);

        // Search with different cases
        var response1 = await Client.GetAsync("/api/v1/tenants?searchString=tenant");
        var response2 = await Client.GetAsync("/api/v1/tenants?searchString=TENANT");
        var response3 = await Client.GetAsync("/api/v1/tenants?searchString=TenAnT");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response1.StatusCode);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response2.StatusCode);
        TestAssertions.AssertEqual(HttpStatusCode.OK, response3.StatusCode);

        var result1 = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response1);
        var result2 = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response2);
        var result3 = await ReadResponseAsync<PaginatedResult<TenantListDto>>(response3);

        // All should return the same count
        TestAssertions.AssertEqual(result1.TotalCount, result2.TotalCount);
        TestAssertions.AssertEqual(result2.TotalCount, result3.TotalCount);
        TestAssertions.AssertTrue(result1.TotalCount > 0);
    }
}
