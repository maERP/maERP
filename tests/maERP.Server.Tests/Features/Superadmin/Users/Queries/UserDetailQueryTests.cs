using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using maERP.Domain.Entities;
using maERP.Domain.Constants;
using Xunit;

namespace maERP.Server.Tests.Features.Superadmin.Users.Queries;

public class UserDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public UserDetailQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_UserDetailQueryTests_{uniqueId}";
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

    private async Task<(string UserId1, string UserId2, string UserId3)> SeedUserTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasUsers = await DbContext.Users.AnyAsync();
            if (!hasUsers)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                // Create test users directly in DbContext (since UserManager is not available in tests)
                var user1Tenant1 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user1@tenant1.com",
                    UserName = "user1@tenant1.com",
                    NormalizedUserName = "USER1@TENANT1.COM",
                    NormalizedEmail = "USER1@TENANT1.COM",
                    Firstname = "Test",
                    Lastname = "User1",
                    DateCreated = DateTime.UtcNow.AddDays(-10),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ==" // Test@123
                };

                var user2Tenant1 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user2@tenant1.com",
                    UserName = "user2@tenant1.com",
                    NormalizedUserName = "USER2@TENANT1.COM",
                    NormalizedEmail = "USER2@TENANT1.COM",
                    Firstname = "Second",
                    Lastname = "User1",
                    DateCreated = DateTime.UtcNow.AddDays(-5),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                };

                var user3Tenant2 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user3@tenant2.com",
                    UserName = "user3@tenant2.com",
                    NormalizedUserName = "USER3@TENANT2.COM",
                    NormalizedEmail = "USER3@TENANT2.COM",
                    Firstname = "Third",
                    Lastname = "User2",
                    DateCreated = DateTime.UtcNow.AddDays(-2),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                };

                DbContext.Users.AddRange(user1Tenant1, user2Tenant1, user3Tenant2);

                // Create tenant assignments
                var userTenant1_1 = new UserTenant
                {
                    UserId = user1Tenant1.Id,
                    TenantId = TenantConstants.TestTenant1Id,
                    IsDefault = true,
                    RoleManageUser = false
                };

                var userTenant2_1 = new UserTenant
                {
                    UserId = user2Tenant1.Id,
                    TenantId = TenantConstants.TestTenant1Id,
                    IsDefault = true,
                    RoleManageUser = false
                };

                var userTenant3_2 = new UserTenant
                {
                    UserId = user3Tenant2.Id,
                    TenantId = TenantConstants.TestTenant2Id,
                    IsDefault = true,
                    RoleManageUser = false
                };

                DbContext.UserTenant.AddRange(userTenant1_1, userTenant2_1, userTenant3_2);
                await DbContext.SaveChangesAsync();

                await EnsureCurrentUserCanManageAsync(
                    TenantConstants.TestTenant1Id,
                    TenantConstants.TestTenant2Id);

                return (user1Tenant1.Id, user2Tenant1.Id, user3Tenant2.Id);
            }

            var existingUsers = await DbContext.Users.Take(3).ToListAsync();

            await EnsureCurrentUserCanManageAsync(
                TenantConstants.TestTenant1Id,
                TenantConstants.TestTenant2Id);

            return (existingUsers[0].Id, existingUsers[1].Id, existingUsers[2].Id);
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private async Task EnsureCurrentUserCanManageAsync(params Guid[] tenantIds)
    {
        Client.DefaultRequestHeaders.Remove("X-Test-Roles");
        Client.DefaultRequestHeaders.Add("X-Test-Roles", string.Empty);

        if (tenantIds == null || tenantIds.Length == 0)
        {
            tenantIds = new[] { TenantConstants.TestTenant1Id };
        }

        foreach (var tenantId in tenantIds)
        {
            await CurrentUserHelper.EnsureUserAsync(Client, DbContext, tenantId, true);
        }

        TenantContext.SetAssignedTenantIds(tenantIds);

        await CurrentUserHelper.SyncAssignmentsAsync(Client, DbContext, tenantIds, true, tenantIds[0]);
    }

    private async Task EnsureCurrentUserCannotManageAsync(Guid tenantId)
    {
        Client.DefaultRequestHeaders.Remove("X-Test-Roles");
        Client.DefaultRequestHeaders.Add("X-Test-Roles", string.Empty);
        await CurrentUserHelper.EnsureUserAsync(Client, DbContext, tenantId, false);
        TenantContext.SetAssignedTenantIds(new[] { tenantId });

        await CurrentUserHelper.SyncAssignmentsAsync(Client, DbContext, new[] { tenantId }, false, tenantId);
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task GetUserDetail_WithValidIdAndTenant_ShouldReturnUserDetail()
    {
        var (userId1, _, _) = await SeedUserTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(userId1, result.Data!.Id);
        TestAssertions.AssertEqual("user1@tenant1.com", result.Data.Email);
        TestAssertions.AssertEqual("Test", result.Data.Firstname);
        TestAssertions.AssertEqual("User1", result.Data.Lastname);
    }

    [Fact]
    public async Task GetUserDetail_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedUserTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var nonExistentId = Guid.NewGuid().ToString();
        var response = await Client.GetAsync($"/api/v1/superadmin/users/{nonExistentId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetUserDetail_WithWrongTenant_ShouldReturnNotFound()
    {
        var (_, _, userId3) = await SeedUserTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId3}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetUserDetail_WithoutTenantHeader_ShouldUseDefaultTenant()
    {
        var (userId1, _, _) = await SeedUserTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task GetUserDetail_WithValidId_ShouldIncludeAllUserFields()
    {
        var (userId1, _, _) = await SeedUserTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var user = result.Data!;
        TestAssertions.AssertEqual(userId1, user.Id);
        TestAssertions.AssertEqual("user1@tenant1.com", user.Email);
        TestAssertions.AssertEqual("Test", user.Firstname);
        TestAssertions.AssertEqual("User1", user.Lastname);
        TestAssertions.AssertNotNull(user.TenantAssignments);
    }

    [Fact]
    public async Task GetUserDetail_WithTenantAssignments_ShouldIncludeTenantDetails()
    {
        var (userId1, _, _) = await SeedUserTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var tenantAssignments = result.Data!.TenantAssignments;
        TestAssertions.AssertNotNull(tenantAssignments);
        TestAssertions.AssertNotEmpty(tenantAssignments);

        var assignment = tenantAssignments.First();
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, assignment.TenantId);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(assignment.TenantName));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(assignment.TenantCode));
        TestAssertions.AssertTrue(assignment.IsDefault);
    }

    [Fact]
    public async Task GetUserDetail_WithTenant2User_ShouldReturnCorrectUser()
    {
        var (_, _, userId3) = await SeedUserTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId3}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(userId3, result.Data!.Id);
        TestAssertions.AssertEqual("Third", result.Data.Firstname);
        TestAssertions.AssertEqual("User2", result.Data.Lastname);
    }

    [Fact]
    public async Task GetUserDetail_WithInvalidId_ShouldReturnBadRequest()
    {
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users/invalid-id");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetUserDetail_WithEmptyId_ShouldReturnBadRequest()
    {
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users/%20");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetUserDetail_WithNonExistentTenant_ShouldReturnNotFound()
    {
        var (userId1, _, _) = await SeedUserTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetUserDetail_ResponseStructure_ShouldHaveCorrectSuccessFormat()
    {
        var (userId1, _, _) = await SeedUserTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task GetUserDetail_ResponseStructure_ShouldHaveCorrectErrorFormat()
    {
        await SeedUserTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var nonExistentId = Guid.NewGuid().ToString();
        var response = await Client.GetAsync($"/api/v1/superadmin/users/{nonExistentId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetUserDetail_TenantIsolation_ShouldNotReturnOtherTenantUsers()
    {
        var (userId1, userId2, userId3) = await SeedUserTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync($"/api/v1/superadmin/users/{userId3}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response1.StatusCode);

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        var response3 = await Client.GetAsync($"/api/v1/superadmin/users/{userId2}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response3.StatusCode);
    }

    [Fact]
    public async Task GetUserDetail_WithUserWithoutTenantAssignments_ShouldReturnEmptyAssignmentsList()
    {
        var (userId1, _, _) = await SeedUserTestDataAsync();

        TenantContext.SetCurrentTenantId(null);
        var userTenantAssignment = await DbContext.UserTenant.FirstOrDefaultAsync(ut => ut.UserId == userId1);
        if (userTenantAssignment != null)
        {
            DbContext.UserTenant.Remove(userTenantAssignment);
            await DbContext.SaveChangesAsync();
        }

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetUserDetail_WithMultipleTenantAssignments_ShouldReturnAllAssignments()
    {
        var (userId1, _, _) = await SeedUserTestDataAsync();

        TenantContext.SetCurrentTenantId(null);
        var additionalAssignment = new UserTenant
        {
            UserId = userId1,
            TenantId = TenantConstants.TestTenant2Id,
            IsDefault = false,
            RoleManageUser = false
        };
        DbContext.UserTenant.Add(additionalAssignment);
        await DbContext.SaveChangesAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var tenantAssignments = result.Data!.TenantAssignments;
        TestAssertions.AssertNotNull(tenantAssignments);
        TestAssertions.AssertEqual(2, tenantAssignments.Count);

        var defaultAssignment = tenantAssignments.First(ta => ta.IsDefault);
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, defaultAssignment.TenantId);

        var nonDefaultAssignment = tenantAssignments.First(ta => !ta.IsDefault);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, nonDefaultAssignment.TenantId);
    }

    [Fact]
    public async Task GetUserDetail_ShouldNotIncludePasswordFields()
    {
        var (userId1, _, _) = await SeedUserTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        var user = result.Data!;
        TestAssertions.AssertEqual(string.Empty, user.Password);
        TestAssertions.AssertEqual(string.Empty, user.PasswordConfirm);
    }

    [Fact]
    public async Task GetUserDetail_WithLongUserId_ShouldHandleGracefully()
    {
        await SeedUserTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var longUserId = new string('a', 500);
        var response = await Client.GetAsync($"/api/v1/superadmin/users/{longUserId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetUserDetail_WithSpecialCharactersInId_ShouldHandleGracefully()
    {
        await SeedUserTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var specialId = "user@#$%^&*()";
        var response = await Client.GetAsync($"/api/v1/superadmin/users/{Uri.EscapeDataString(specialId)}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task GetUserDetail_WithManagePermission_ShouldReturnData()
    {
        var (userId1, _, _) = await SeedUserTestDataAsync();
        await EnsureCurrentUserCanManageAsync(TenantConstants.TestTenant1Id);

        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task GetUserDetail_WithoutManagePermission_ShouldReturnForbidden()
    {
        var (userId1, _, _) = await SeedUserTestDataAsync();
        await EnsureCurrentUserCannotManageAsync(TenantConstants.TestTenant1Id);

        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}");

        TestAssertions.AssertEqual(HttpStatusCode.Forbidden, response.StatusCode);
        var result = await ReadResponseAsync<Result<UserDetailDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        Assert.Null(result.Data);
    }
}
