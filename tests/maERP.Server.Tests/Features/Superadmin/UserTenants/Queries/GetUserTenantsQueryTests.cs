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

namespace maERP.Server.Tests.Features.Superadmin.UserTenants.Queries;

public class GetUserTenantsQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public GetUserTenantsQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_GetUserTenantsQueryTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        DbContext.Database.EnsureCreated();

        TenantContext.SetAssignedTenantIds(new[] { TenantConstants.TestTenant1Id, TenantConstants.TestTenant2Id, Guid.NewGuid() });
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

    private async Task<(string UserId1, string UserId2, string UserId3)> SeedUserTenantTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.UserTenant.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

                // Create test users
                var user1 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user1@test.com",
                    UserName = "user1@test.com",
                    NormalizedUserName = "USER1@TEST.COM",
                    NormalizedEmail = "USER1@TEST.COM",
                    Firstname = "John",
                    Lastname = "Doe",
                    DateCreated = DateTime.UtcNow.AddDays(-10),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                };

                var user2 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user2@test.com",
                    UserName = "user2@test.com",
                    NormalizedUserName = "USER2@TEST.COM",
                    NormalizedEmail = "USER2@TEST.COM",
                    Firstname = "Jane",
                    Lastname = "Smith",
                    DateCreated = DateTime.UtcNow.AddDays(-8),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                };

                var user3 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user3@test.com",
                    UserName = "user3@test.com",
                    NormalizedUserName = "USER3@TEST.COM",
                    NormalizedEmail = "USER3@TEST.COM",
                    Firstname = "Bob",
                    Lastname = "Wilson",
                    DateCreated = DateTime.UtcNow.AddDays(-5),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                };

                DbContext.Users.AddRange(user1, user2, user3);

                // Create tenant assignments
                var userTenantAssignments = new List<Domain.Entities.UserTenant>
                {
                    // User 1: Assigned to tenants 1 and 2, default is tenant 1
                    new() { UserId = user1.Id, TenantId = TenantConstants.TestTenant1Id, IsDefault = true, RoleManageUser = false },
                    new() { UserId = user1.Id, TenantId = TenantConstants.TestTenant2Id, IsDefault = false, RoleManageUser = false },

                    // User 2: Assigned to tenant 2 only, default is tenant 2
                    new() { UserId = user2.Id, TenantId = TenantConstants.TestTenant2Id, IsDefault = true, RoleManageUser = false },

                    // User 3: Assigned to tenant 3 only, default is tenant 3
                    new() { UserId = user3.Id, TenantId = TenantConstants.TestTenant3Id, IsDefault = true, RoleManageUser = false }
                };

                DbContext.UserTenant.AddRange(userTenantAssignments);
                await DbContext.SaveChangesAsync();

                return (user1.Id, user2.Id, user3.Id);
            }

            // Return existing user IDs
            var existingUsers = await DbContext.Users
                .Where(u => u.Email!.Contains("test.com"))
                .OrderBy(u => u.Email)
                .Select(u => u.Id)
                .ToListAsync();

            return (existingUsers[0], existingUsers[1], existingUsers[2]);
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
    public async Task GetUserTenants_WithValidUserId_ShouldReturnUserTenants()
    {
        var (userId1, _, _) = await SeedUserTenantTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data.Count); // User 1 is assigned to 2 tenants
    }

    [Fact]
    public async Task GetUserTenants_WithUserAssignedToMultipleTenants_ShouldReturnAllTenants()
    {
        var (userId1, _, _) = await SeedUserTenantTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(2, result.Data.Count);

        var tenantIds = result.Data.Select(t => t.TenantId).OrderBy(id => id).ToList();
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, tenantIds[0]);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, tenantIds[1]);
    }

    [Fact]
    public async Task GetUserTenants_WithUserAssignedToSingleTenant_ShouldReturnSingleTenant()
    {
        var (_, userId2, _) = await SeedUserTenantTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId2}/tenants");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(1, result.Data.Count);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, result.Data[0].TenantId);
        TestAssertions.AssertTrue(result.Data[0].IsDefault);
    }

    [Fact]
    public async Task GetUserTenants_WithNonExistentUserId_ShouldReturnEmptyList()
    {
        await SeedUserTenantTestDataAsync();
        var nonExistentUserId = Guid.NewGuid().ToString();

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{nonExistentUserId}/tenants");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetUserTenants_WithEmptyUserId_ShouldReturnBadRequest()
    {
        await SeedUserTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/superadmin/users/ /tenants");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetUserTenants_ResponseStructure_ShouldHaveCorrectFormat()
    {
        var (userId1, _, _) = await SeedUserTenantTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotEmpty(result.Data);
    }

    [Fact]
    public async Task GetUserTenants_TenantFields_ShouldIncludeAllRequiredFields()
    {
        var (userId1, _, _) = await SeedUserTenantTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotEmpty(result.Data);

        foreach (var tenant in result.Data)
        {
            TestAssertions.AssertTrue(tenant.TenantId != Guid.Empty);
            TestAssertions.AssertFalse(string.IsNullOrEmpty(tenant.TenantName));
            TestAssertions.AssertFalse(string.IsNullOrEmpty(tenant.TenantCode));
            // IsDefault can be true or false, both are valid
        }
    }

    [Fact]
    public async Task GetUserTenants_DefaultTenantFlag_ShouldBeCorrectlySet()
    {
        var (userId1, _, _) = await SeedUserTenantTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        var defaultTenants = result.Data.Where(t => t.IsDefault).ToList();
        TestAssertions.AssertEqual(1, defaultTenants.Count); // Only one default tenant
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, defaultTenants[0].TenantId); // Tenant 1 is default for User 1
    }

    [Fact]
    public async Task GetUserTenants_TenantNames_ShouldBeIncluded()
    {
        var (userId1, _, _) = await SeedUserTenantTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        foreach (var tenant in result.Data)
        {
            TestAssertions.AssertFalse(string.IsNullOrWhiteSpace(tenant.TenantName));
            TestAssertions.AssertFalse(string.IsNullOrWhiteSpace(tenant.TenantCode));
        }
    }

    [Fact]
    public async Task GetUserTenants_WithMultipleUsersInDifferentTenants_ShouldReturnCorrectData()
    {
        var (userId1, userId2, userId3) = await SeedUserTenantTestDataAsync();

        // Test User 1 (tenants 1, 2)
        var response1 = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");
        var result1 = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response1);
        TestAssertions.AssertEqual(2, result1.Data!.Count);

        // Test User 2 (tenant 2)
        var response2 = await Client.GetAsync($"/api/v1/superadmin/users/{userId2}/tenants");
        var result2 = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response2);
        TestAssertions.AssertEqual(1, result2.Data!.Count);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, result2.Data[0].TenantId);

        // Test User 3 (tenant 3)
        var response3 = await Client.GetAsync($"/api/v1/superadmin/users/{userId3}/tenants");
        var result3 = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response3);
        TestAssertions.AssertEqual(1, result3.Data!.Count);
        TestAssertions.AssertEqual(TenantConstants.TestTenant3Id, result3.Data[0].TenantId);
    }

    [Fact]
    public async Task GetUserTenants_DataIsolation_ShouldNotReturnOtherUsersData()
    {
        var (userId1, userId2, userId3) = await SeedUserTenantTestDataAsync();

        // Get tenants for user 1
        var response1 = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");
        var result1 = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response1);
        var user1TenantIds = result1.Data!.Select(t => t.TenantId).ToList();

        // Get tenants for user 2
        var response2 = await Client.GetAsync($"/api/v1/superadmin/users/{userId2}/tenants");
        var result2 = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response2);
        var user2TenantIds = result2.Data!.Select(t => t.TenantId).ToList();

        // Get tenants for user 3
        var response3 = await Client.GetAsync($"/api/v1/superadmin/users/{userId3}/tenants");
        var result3 = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response3);
        var user3TenantIds = result3.Data!.Select(t => t.TenantId).ToList();

        // Verify user 1 and user 2 have overlapping tenants (tenant 2)
        TestAssertions.AssertContains(TenantConstants.TestTenant2Id, user1TenantIds);
        TestAssertions.AssertContains(TenantConstants.TestTenant2Id, user2TenantIds);

        // Verify user 3 has unique tenant
        TestAssertions.AssertContains(TenantConstants.TestTenant3Id, user3TenantIds);
        TestAssertions.AssertDoesNotContain(TenantConstants.TestTenant3Id, user1TenantIds);
        TestAssertions.AssertDoesNotContain(TenantConstants.TestTenant3Id, user2TenantIds);
    }

    [Fact]
    public async Task GetUserTenants_WithInvalidGuidFormat_ShouldReturnBadRequest()
    {
        await SeedUserTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/superadmin/users/invalid-guid/tenants");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetUserTenants_OrderingConsistency_ShouldReturnSameOrderEachTime()
    {
        var (userId1, _, _) = await SeedUserTenantTestDataAsync();

        // Make multiple requests
        var response1 = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");
        var result1 = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response1);

        var response2 = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");
        var result2 = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response2);

        TestAssertions.AssertEqual(result1.Data!.Count, result2.Data!.Count);

        // Verify order is consistent
        for (int i = 0; i < result1.Data.Count; i++)
        {
            TestAssertions.AssertEqual(result1.Data[i].TenantId, result2.Data[i].TenantId);
            TestAssertions.AssertEqual(result1.Data[i].IsDefault, result2.Data[i].IsDefault);
        }
    }

    [Fact]
    public async Task GetUserTenants_WithTenantHeader_ShouldStillReturnAllUserTenants()
    {
        var (userId1, _, _) = await SeedUserTenantTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);

        // Should return all tenants for the user, not just the one in the header
        TestAssertions.AssertEqual(2, result.Data.Count);
        var tenantIds = result.Data.Select(t => t.TenantId).OrderBy(id => id).ToList();
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, tenantIds[0]);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, tenantIds[1]);
    }

    [Fact]
    public async Task GetUserTenants_WithEmptyDatabase_ShouldReturnEmptyList()
    {
        var nonExistentUserId = Guid.NewGuid().ToString();

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{nonExistentUserId}/tenants");

        TestAssertions.AssertHttpSuccess(response);
        var result = await ReadResponseAsync<Result<List<UserTenantAssignmentDto>>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEmpty(result.Data);
    }

    [Fact]
    public async Task GetUserTenants_JsonResponseFormat_ShouldBeWellFormed()
    {
        var (userId1, _, _) = await SeedUserTenantTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/superadmin/users/{userId1}/tenants");

        TestAssertions.AssertHttpSuccess(response);
        var content = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertFalse(string.IsNullOrEmpty(content));

        // Verify it's valid JSON by deserializing
        var result = JsonSerializer.Deserialize<Result<List<UserTenantAssignmentDto>>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        TestAssertions.AssertNotNull(result);
        if (result != null)
        {
            TestAssertions.AssertNotNull(result.Data);
        }
    }
}
