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

public class UserListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public UserListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_UserListQueryTests_{uniqueId}";
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

    private async Task<(List<string> Tenant1UserIds, List<string> Tenant2UserIds)> SeedUserListTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasUsers = await DbContext.Users.AnyAsync();
            if (!hasUsers)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                var tenant1UserIds = new List<string>();
                var tenant2UserIds = new List<string>();

                // Create test users for tenant 1
                var usersForTenant1 = new[]
                {
                    new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "alice@tenant1.com",
                        UserName = "alice@tenant1.com",
                        NormalizedUserName = "ALICE@TENANT1.COM",
                        NormalizedEmail = "ALICE@TENANT1.COM",
                        Firstname = "Alice",
                        Lastname = "Anderson",
                        DateCreated = DateTime.UtcNow.AddDays(-30),
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                    },
                    new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "bob@tenant1.com",
                        UserName = "bob@tenant1.com",
                        NormalizedUserName = "BOB@TENANT1.COM",
                        NormalizedEmail = "BOB@TENANT1.COM",
                        Firstname = "Bob",
                        Lastname = "Brown",
                        DateCreated = DateTime.UtcNow.AddDays(-25),
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                    },
                    new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "charlie@tenant1.com",
                        UserName = "charlie@tenant1.com",
                        NormalizedUserName = "CHARLIE@TENANT1.COM",
                        NormalizedEmail = "CHARLIE@TENANT1.COM",
                        Firstname = "Charlie",
                        Lastname = "Clark",
                        DateCreated = DateTime.UtcNow.AddDays(-20),
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                    },
                    new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "diana@tenant1.com",
                        UserName = "diana@tenant1.com",
                        NormalizedUserName = "DIANA@TENANT1.COM",
                        NormalizedEmail = "DIANA@TENANT1.COM",
                        Firstname = "Diana",
                        Lastname = "Davis",
                        DateCreated = DateTime.UtcNow.AddDays(-15),
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                    },
                    new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "eve@tenant1.com",
                        UserName = "eve@tenant1.com",
                        NormalizedUserName = "EVE@TENANT1.COM",
                        NormalizedEmail = "EVE@TENANT1.COM",
                        Firstname = "Eve",
                        Lastname = "Evans",
                        DateCreated = DateTime.UtcNow.AddDays(-10),
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                    }
                };

                // Create test users for tenant 2
                var usersForTenant2 = new[]
                {
                    new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "frank@tenant2.com",
                        UserName = "frank@tenant2.com",
                        NormalizedUserName = "FRANK@TENANT2.COM",
                        NormalizedEmail = "FRANK@TENANT2.COM",
                        Firstname = "Frank",
                        Lastname = "Fisher",
                        DateCreated = DateTime.UtcNow.AddDays(-12),
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                    },
                    new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "grace@tenant2.com",
                        UserName = "grace@tenant2.com",
                        NormalizedUserName = "GRACE@TENANT2.COM",
                        NormalizedEmail = "GRACE@TENANT2.COM",
                        Firstname = "Grace",
                        Lastname = "Green",
                        DateCreated = DateTime.UtcNow.AddDays(-8),
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                    },
                    new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "henry@tenant2.com",
                        UserName = "henry@tenant2.com",
                        NormalizedUserName = "HENRY@TENANT2.COM",
                        NormalizedEmail = "HENRY@TENANT2.COM",
                        Firstname = "Henry",
                        Lastname = "Hall",
                        DateCreated = DateTime.UtcNow.AddDays(-4),
                        EmailConfirmed = true,
                        PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                    }
                };

                // Add users to DbContext directly
                foreach (var user in usersForTenant1)
                {
                    DbContext.Users.Add(user);
                    tenant1UserIds.Add(user.Id);
                }

                foreach (var user in usersForTenant2)
                {
                    DbContext.Users.Add(user);
                    tenant2UserIds.Add(user.Id);
                }

                // Create tenant assignments
                var userTenantAssignments = new List<UserTenant>();

                foreach (var userId in tenant1UserIds)
                {
                    userTenantAssignments.Add(new UserTenant
                    {
                        UserId = userId,
                        TenantId = TenantConstants.TestTenant1Id,
                        IsDefault = true
                    });
                }

                foreach (var userId in tenant2UserIds)
                {
                    userTenantAssignments.Add(new UserTenant
                    {
                        UserId = userId,
                        TenantId = TenantConstants.TestTenant2Id,
                        IsDefault = true
                    });
                }

                DbContext.UserTenant.AddRange(userTenantAssignments);
                await DbContext.SaveChangesAsync();

                await EnsureCurrentUserCanManageAsync(
                    TenantConstants.TestTenant1Id,
                    TenantConstants.TestTenant2Id);

                return (tenant1UserIds, tenant2UserIds);
            }

            // If users already exist, return existing user IDs
            var existingTenant1Users = await DbContext.Users
                .Where(u => u.Email!.Contains("tenant1"))
                .Select(u => u.Id)
                .ToListAsync();

            var existingTenant2Users = await DbContext.Users
                .Where(u => u.Email!.Contains("tenant2"))
                .Select(u => u.Id)
                .ToListAsync();

            await EnsureCurrentUserCanManageAsync(
                TenantConstants.TestTenant1Id,
                TenantConstants.TestTenant2Id);

            return (existingTenant1Users, existingTenant2Users);
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
    public async Task GetUserList_WithValidTenant_ShouldReturnUserList()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);
    }

    [Fact]
    public async Task GetUserList_WithoutTenantHeader_ShouldUseDefaultTenant()
    {
        await SeedUserListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/superadmin/users");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);
    }

    [Fact]
    public async Task GetUserList_WithPagination_ShouldReturnCorrectPage()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users?pageNumber=1&pageSize=2");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data.Count);
        TestAssertions.AssertEqual(1, result.CurrentPage);
        TestAssertions.AssertEqual(2, result.PageSize);
        TestAssertions.AssertTrue(result.TotalPages > 1);
        TestAssertions.AssertTrue(result.TotalCount >= 5);
    }

    [Fact]
    public async Task GetUserList_WithLargePage_ShouldReturnAllUsers()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users?pageNumber=1&pageSize=100");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.Data.Count >= 5);
        TestAssertions.AssertEqual(1, result.TotalPages);
    }

    [Fact]
    public async Task GetUserList_WithSecondPage_ShouldReturnRemainingUsers()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users?pageNumber=2&pageSize=3");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.CurrentPage);
        TestAssertions.AssertEqual(3, result.PageSize);
    }

    [Fact]
    public async Task GetUserList_WithOrdering_ShouldReturnOrderedResults()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users?orderBy=firstname");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var users = result.Data.ToList();
        for (int i = 0; i < users.Count - 1; i++)
        {
            TestAssertions.AssertTrue(string.Compare(users[i].Firstname, users[i + 1].Firstname, StringComparison.Ordinal) <= 0);
        }
    }

    [Fact]
    public async Task GetUserList_WithMultipleOrderBy_ShouldApplyOrdering()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users?orderBy=lastname,firstname");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);
    }

    [Fact]
    public async Task GetUserList_WithTenant2_ShouldReturnOnlyTenant2Users()
    {
        var (tenant1UserIds, tenant2UserIds) = await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant2Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(tenant2UserIds.Count, result.Data.Count);

        foreach (var user in result.Data)
        {
            TestAssertions.AssertContains(user.Id, tenant2UserIds);
            TestAssertions.AssertDoesNotContain(user.Id, tenant1UserIds);
        }
    }

    [Fact]
    public async Task GetUserList_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.CurrentPage > 0);
        TestAssertions.AssertTrue(result.PageSize > 0);
        TestAssertions.AssertTrue(result.TotalPages > 0);
        TestAssertions.AssertTrue(result.TotalCount >= 0);
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task GetUserList_UserFields_ShouldIncludeAllRequiredFields()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var user = result.Data.First();
        TestAssertions.AssertFalse(string.IsNullOrEmpty(user.Id));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(user.Email));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(user.Firstname));
        TestAssertions.AssertFalse(string.IsNullOrEmpty(user.Lastname));
        TestAssertions.AssertTrue(user.DateCreated > DateTime.MinValue);
    }

    [Fact]
    public async Task GetUserList_TenantIsolation_ShouldNotReturnOtherTenantUsers()
    {
        var (tenant1UserIds, tenant2UserIds) = await SeedUserListTestDataAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.GetAsync("/api/v1/superadmin/users");
        var result1 = await ReadResponseAsync<PaginatedResult<UserListDto>>(response1);

        foreach (var user in result1.Data!)
        {
            TestAssertions.AssertContains(user.Id, tenant1UserIds);
            TestAssertions.AssertDoesNotContain(user.Id, tenant2UserIds);
        }

        SetTenantHeader(TenantConstants.TestTenant2Id);
        var response2 = await Client.GetAsync("/api/v1/superadmin/users");
        var result2 = await ReadResponseAsync<PaginatedResult<UserListDto>>(response2);

        foreach (var user in result2.Data!)
        {
            TestAssertions.AssertContains(user.Id, tenant2UserIds);
            TestAssertions.AssertDoesNotContain(user.Id, tenant1UserIds);
        }
    }

    [Fact]
    public async Task GetUserList_WithNonExistentTenant_ShouldReturnForbidden()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.GetAsync("/api/v1/superadmin/users");

        TestAssertions.AssertEqual(HttpStatusCode.Forbidden, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetUserList_WithoutManagePermission_ShouldReturnForbidden_old()
    {
        await SeedUserListTestDataAsync();
        await EnsureCurrentUserCannotManageAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users");

        TestAssertions.AssertEqual(HttpStatusCode.Forbidden, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task GetUserList_WithInvalidPageNumber_ShouldReturnFirstPage()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users?pageNumber=0&pageSize=5");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.CurrentPage);
    }

    [Fact]
    public async Task GetUserList_WithInvalidPageSize_ShouldUseDefaultPageSize()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users?pageNumber=1&pageSize=0");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertTrue(result.PageSize > 0);
    }

    [Fact]
    public async Task GetUserList_WithVeryLargePageSize_ShouldHandleGracefully()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users?pageNumber=1&pageSize=10000");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(10000, result.PageSize);
        TestAssertions.AssertEqual(1, result.TotalPages);
    }

    [Fact]
    public async Task GetUserList_WithInvalidOrderBy_ShouldReturnUnorderedResults()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users?orderBy=invalidfield");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);
    }

    [Fact]
    public async Task GetUserList_WithEmptyDatabase_ShouldReturnEmptyList()
    {
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
        TestAssertions.AssertEqual(0, result.TotalCount);
        TestAssertions.AssertEqual(0, result.TotalPages);
    }

    [Fact]
    public async Task GetUserList_WithDateOrdering_ShouldOrderByDateCreated()
    {
        await SeedUserListTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users?orderBy=dateCreated");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        var users = result.Data.ToList();
        for (int i = 0; i < users.Count - 1; i++)
        {
            TestAssertions.AssertTrue(users[i].DateCreated <= users[i + 1].DateCreated);
        }
    }

    [Fact]
    public async Task GetUserList_WithManagePermission_ShouldReturnData()
    {
        await SeedUserListTestDataAsync();
        Client.DefaultRequestHeaders.Remove("X-Test-Roles");
        Client.DefaultRequestHeaders.Add("X-Test-Roles", string.Empty);
        await CurrentUserHelper.EnsureUserAsync(Client, DbContext, TenantConstants.TestTenant1Id, true);

        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Data);

        Client.DefaultRequestHeaders.Remove("X-Test-Roles");
    }

    [Fact]
    public async Task GetUserList_WithoutManagePermission_ShouldReturnForbidden()
    {
        await SeedUserListTestDataAsync();
        Client.DefaultRequestHeaders.Remove("X-Test-Roles");
        Client.DefaultRequestHeaders.Add("X-Test-Roles", string.Empty);
        await CurrentUserHelper.EnsureUserAsync(Client, DbContext, TenantConstants.TestTenant1Id, false);

        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync("/api/v1/superadmin/users");

        TestAssertions.AssertEqual(HttpStatusCode.Forbidden, response.StatusCode);
        var result = await ReadResponseAsync<PaginatedResult<UserListDto>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);

        Client.DefaultRequestHeaders.Remove("X-Test-Roles");
    }
}
