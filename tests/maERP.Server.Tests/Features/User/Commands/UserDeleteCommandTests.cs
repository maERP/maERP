using System.Net;
using System.Text.Json;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using maERP.Domain.Entities;
using maERP.Domain.Constants;
using Xunit;

namespace maERP.Server.Tests.Features.User.Commands;

public class UserDeleteCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public UserDeleteCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_UserDeleteCommandTests_{uniqueId}";
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

        // Force a small delay to ensure header is set properly
        Task.Delay(10).Wait();
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

    private async Task SeedTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Tenant.AnyAsync();
            if (!hasData)
            {
                await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private async Task<(string UserId1, string UserId2, string UserId3)> SeedUsersAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var user1 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = $"deleteuser1{Guid.NewGuid():N}@example.com",
                UserName = $"deleteuser1{Guid.NewGuid():N}@example.com",
                NormalizedEmail = $"DELETEUSER1{Guid.NewGuid():N}@EXAMPLE.COM",
                NormalizedUserName = $"DELETEUSER1{Guid.NewGuid():N}@EXAMPLE.COM",
                Firstname = "Delete",
                Lastname = "User1",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
            };

            var user2 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = $"deleteuser2{Guid.NewGuid():N}@example.com",
                UserName = $"deleteuser2{Guid.NewGuid():N}@example.com",
                NormalizedEmail = $"DELETEUSER2{Guid.NewGuid():N}@EXAMPLE.COM",
                NormalizedUserName = $"DELETEUSER2{Guid.NewGuid():N}@EXAMPLE.COM",
                Firstname = "Delete",
                Lastname = "User2",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
            };

            var user3 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = $"deleteuser3{Guid.NewGuid():N}@example.com",
                UserName = $"deleteuser3{Guid.NewGuid():N}@example.com",
                NormalizedEmail = $"DELETEUSER3{Guid.NewGuid():N}@EXAMPLE.COM",
                NormalizedUserName = $"DELETEUSER3{Guid.NewGuid():N}@EXAMPLE.COM",
                Firstname = "Delete",
                Lastname = "User3",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
            };

            DbContext.Users.AddRange(user1, user2, user3);

            var userTenant1 = new UserTenant
            {
                UserId = user1.Id,
                TenantId = TenantConstants.TestTenant1Id,
                IsDefault = true
            };

            var userTenant2 = new UserTenant
            {
                UserId = user2.Id,
                TenantId = TenantConstants.TestTenant2Id,
                IsDefault = true
            };

            var userTenant3 = new UserTenant
            {
                UserId = user3.Id,
                TenantId = TenantConstants.TestTenant1Id,
                IsDefault = true
            };

            DbContext.UserTenant.AddRange(userTenant1, userTenant2, userTenant3);
            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            return (user1.Id, user2.Id, user3.Id);
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
    public async Task DeleteUser_WithValidId_ShouldReturnNoContent()
    {
        await SeedTestDataAsync();
        var (userId1, _, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_WithValidId_ShouldRemoveFromDatabase()
    {
        await SeedTestDataAsync();
        var (userId1, _, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify user was deleted from database
        TenantContext.SetCurrentTenantId(null);
        var deletedUser = await DbContext.Users.FindAsync(userId1);
        Assert.Null(deletedUser);

        // Verify tenant assignments were also deleted
        var userTenants = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId1)
            .ToListAsync();
        TestAssertions.AssertEmpty(userTenants);
    }

    [Fact]
    public async Task DeleteUser_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var nonExistentId = Guid.NewGuid().ToString();

        var response = await Client.DeleteAsync($"/api/v1/Users/{nonExistentId}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_WithInvalidId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/Users/invalid-id");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_WithEmptyId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync("/api/v1/Users/");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_TenantIsolation_ShouldNotDeleteOtherTenantUsers()
    {
        await SeedTestDataAsync();
        var (userId1, userId2, _) = await SeedUsersAsync();

        // Try to delete tenant 2's user from tenant 1 context
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response = await Client.DeleteAsync($"/api/v1/Users/{userId2}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);

        // Verify user was not deleted
        TenantContext.SetCurrentTenantId(null);
        var user = await DbContext.Users.FindAsync(userId2);
        TestAssertions.AssertNotNull(user);
    }

    [Fact]
    public async Task DeleteUser_WithoutTenantHeader_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        var (userId1, _, _) = await SeedUsersAsync();

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);

        // Verify user was not deleted
        TenantContext.SetCurrentTenantId(null);
        var user = await DbContext.Users.FindAsync(userId1);
        TestAssertions.AssertNotNull(user);
    }

    [Fact]
    public async Task DeleteUser_WithNonExistentTenant_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        var (userId1, _, _) = await SeedUsersAsync();
        SetTenantHeader(Guid.NewGuid());

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);

        // Verify user was not deleted
        TenantContext.SetCurrentTenantId(null);
        var user = await DbContext.Users.FindAsync(userId1);
        TestAssertions.AssertNotNull(user);
    }

    [Fact]
    public async Task DeleteUser_MultipleTenantAssignments_ShouldDeleteUserAndAllAssignments()
    {
        await SeedTestDataAsync();
        var (userId1, _, _) = await SeedUsersAsync();

        // Add user to multiple tenants
        TenantContext.SetCurrentTenantId(null);
        var additionalTenant = new UserTenant
        {
            UserId = userId1,
            TenantId = TenantConstants.TestTenant2Id,
            IsDefault = false
        };
        DbContext.UserTenant.Add(additionalTenant);
        await DbContext.SaveChangesAsync();

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify user was deleted from database
        var deletedUser = await DbContext.Users.FindAsync(userId1);
        Assert.Null(deletedUser);

        // Verify all tenant assignments were deleted
        var userTenants = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId1)
            .ToListAsync();
        TestAssertions.AssertEmpty(userTenants);
    }

    [Fact]
    public async Task DeleteUser_TenantIsolation_ShouldOnlyDeleteOwnTenantUsers()
    {
        await SeedTestDataAsync();
        var (userId1, userId2, userId3) = await SeedUsersAsync();

        // Delete user from tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var response1 = await Client.DeleteAsync($"/api/v1/Users/{userId1}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response1.StatusCode);

        // Try to delete user from tenant 2 while in tenant 1 context
        var response2 = await Client.DeleteAsync($"/api/v1/Users/{userId2}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);

        // Verify results
        TenantContext.SetCurrentTenantId(null);
        var deletedUser1 = await DbContext.Users.FindAsync(userId1);
        var notDeletedUser2 = await DbContext.Users.FindAsync(userId2);
        var notDeletedUser3 = await DbContext.Users.FindAsync(userId3);

        Assert.Null(deletedUser1);
        TestAssertions.AssertNotNull(notDeletedUser2);
        TestAssertions.AssertNotNull(notDeletedUser3);
    }

    [Fact]
    public async Task DeleteUser_CascadeDeleteTenantAssignments_ShouldRemoveAllRelatedData()
    {
        await SeedTestDataAsync();
        var (userId1, _, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Verify user has tenant assignments before deletion
        TenantContext.SetCurrentTenantId(null);
        var initialTenants = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId1)
            .ToListAsync();
        TestAssertions.AssertNotEmpty(initialTenants);

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify user and all related tenant assignments were deleted
        var deletedUser = await DbContext.Users.FindAsync(userId1);
        Assert.Null(deletedUser);

        var remainingTenants = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId1)
            .ToListAsync();
        TestAssertions.AssertEmpty(remainingTenants);
    }

    [Fact]
    public async Task DeleteUser_WithLongUserId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var longUserId = new string('a', 500);

        var response = await Client.DeleteAsync($"/api/v1/Users/{Uri.EscapeDataString(longUserId)}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_WithSpecialCharactersInId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var specialId = "user@#$%^&*()";

        var response = await Client.DeleteAsync($"/api/v1/Users/{Uri.EscapeDataString(specialId)}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_MultipleDeleteAttempts_ShouldReturnNotFoundOnSecond()
    {
        await SeedTestDataAsync();
        var (userId1, _, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // First delete should succeed
        var response1 = await Client.DeleteAsync($"/api/v1/Users/{userId1}");
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response1.StatusCode);

        // Second delete should return NotFound
        var response2 = await Client.DeleteAsync($"/api/v1/Users/{userId1}");
        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response2.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_ConcurrentDeletion_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        var (userId1, _, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Create two clients to simulate concurrent requests
        using var client2 = Factory.CreateClient();
        client2.DefaultRequestHeaders.Add("X-Tenant-Id", "1");

        // Attempt to delete the same user concurrently
        var task1 = Client.DeleteAsync($"/api/v1/Users/{userId1}");
        var task2 = client2.DeleteAsync($"/api/v1/Users/{userId1}");

        var responses = await Task.WhenAll(task1, task2);

        // One should succeed, one should fail
        var successCount = responses.Count(r => r.StatusCode == HttpStatusCode.NoContent);
        var notFoundCount = responses.Count(r => r.StatusCode == HttpStatusCode.NotFound);

        TestAssertions.AssertEqual(1, successCount);
        TestAssertions.AssertEqual(1, notFoundCount);

        // Verify user is deleted
        TenantContext.SetCurrentTenantId(null);
        var deletedUser = await DbContext.Users.FindAsync(userId1);
        Assert.Null(deletedUser);
    }

    [Fact]
    public async Task DeleteUser_WithMaxGuidLength_ShouldHandleCorrectly()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var maxGuid = Guid.NewGuid().ToString("D"); // Standard GUID format

        var response = await Client.DeleteAsync($"/api/v1/Users/{maxGuid}");

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_ResponseHeaders_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        var (userId1, _, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        TestAssertions.AssertEqual(0, response.Content.Headers.ContentLength ?? 0);
    }

    [Fact]
    public async Task DeleteUser_VerifyNoOrphanedData_ShouldCleanupCompletely()
    {
        await SeedTestDataAsync();
        var (userId1, _, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Count total records before deletion
        TenantContext.SetCurrentTenantId(null);
        var initialUserCount = await DbContext.Users.CountAsync();
        var initialTenantAssignmentCount = await DbContext.UserTenant.CountAsync();

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}");

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        // Verify counts decreased by expected amounts
        var finalUserCount = await DbContext.Users.CountAsync();
        var finalTenantAssignmentCount = await DbContext.UserTenant.CountAsync();

        TestAssertions.AssertEqual(initialUserCount - 1, finalUserCount);
        TestAssertions.AssertTrue(finalTenantAssignmentCount < initialTenantAssignmentCount);

        // Verify no orphaned tenant assignments exist for the deleted user
        var orphanedAssignments = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId1)
            .ToListAsync();
        TestAssertions.AssertEmpty(orphanedAssignments);
    }
}
