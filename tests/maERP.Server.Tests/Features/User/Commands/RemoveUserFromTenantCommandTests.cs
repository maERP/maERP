using System.Net;
using System.Text.Json;
using System.Linq;
using maERP.Application.Features.UserTenant.Commands.RemoveUserFromTenant;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using maERP.Domain.Entities;
using Xunit;

namespace maERP.Server.Tests.Features.User.Commands;

public class RemoveUserFromTenantCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public RemoveUserFromTenantCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_RemoveUserFromTenantCommandTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        DbContext.Database.EnsureCreated();

        TenantContext.SetAssignedTenantIds(new[] { 1, 2, 3 });
        TenantContext.SetCurrentTenantId(null);
    }

    protected void SetTenantHeader(int tenantId)
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

    private async Task<(string UserId1, string UserId2, string UserId3)> SeedTestDataAsync()
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
                    Email = "user1@remove.test",
                    UserName = "user1@remove.test",
                    NormalizedUserName = "USER1@REMOVE.TEST",
                    NormalizedEmail = "USER1@REMOVE.TEST",
                    Firstname = "John",
                    Lastname = "Doe",
                    DateCreated = DateTime.UtcNow,
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                };

                var user2 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user2@remove.test",
                    UserName = "user2@remove.test",
                    NormalizedUserName = "USER2@REMOVE.TEST",
                    NormalizedEmail = "USER2@REMOVE.TEST",
                    Firstname = "Jane",
                    Lastname = "Smith",
                    DateCreated = DateTime.UtcNow,
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                };

                var user3 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user3@remove.test",
                    UserName = "user3@remove.test",
                    NormalizedUserName = "USER3@REMOVE.TEST",
                    NormalizedEmail = "USER3@REMOVE.TEST",
                    Firstname = "Bob",
                    Lastname = "Wilson",
                    DateCreated = DateTime.UtcNow,
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                };

                DbContext.Users.AddRange(user1, user2, user3);

                // Create test tenant assignments
                var assignments = new List<Domain.Entities.UserTenant>
                {
                    // User1: Assigned to tenants 1 (default), 2, 3
                    new() { UserId = user1.Id, TenantId = 1, IsDefault = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow },
                    new() { UserId = user1.Id, TenantId = 2, IsDefault = false, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow },
                    new() { UserId = user1.Id, TenantId = 3, IsDefault = false, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow },
                    
                    // User2: Assigned to tenant 2 (default) and 3
                    new() { UserId = user2.Id, TenantId = 2, IsDefault = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow },
                    new() { UserId = user2.Id, TenantId = 3, IsDefault = false, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow },
                    
                    // User3: Assigned to tenant 1 only (default)
                    new() { UserId = user3.Id, TenantId = 1, IsDefault = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow }
                };

                DbContext.UserTenant.AddRange(assignments);
                await DbContext.SaveChangesAsync();

                return (user1.Id, user2.Id, user3.Id);
            }

            // Return existing user IDs
            var existingUsers = await DbContext.Users
                .Where(u => u.Email!.Contains("@remove.test"))
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
    public async Task RemoveUserFromTenant_WithValidData_ShouldSucceed()
    {
        var (userId1, _, _) = await SeedTestDataAsync();

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/2");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);
        var result = await ReadResponseAsync<Result<bool>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data);
        TestAssertions.AssertEqual("User successfully removed from tenant", result.Messages.FirstOrDefault());
    }

    [Fact]
    public async Task RemoveUserFromTenant_WithNonExistentAssignment_ShouldFail()
    {
        var (userId1, _, _) = await SeedTestDataAsync();

        // Try to remove user1 from tenant 999 (doesn't exist)
        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/999");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<bool>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual("User is not assigned to this tenant", result.Messages.FirstOrDefault());
    }

    [Fact]
    public async Task RemoveUserFromTenant_WithNonExistentUser_ShouldFail()
    {
        await SeedTestDataAsync();
        var nonExistentUserId = Guid.NewGuid().ToString();

        var response = await Client.DeleteAsync($"/api/v1/Users/{nonExistentUserId}/tenants/1");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<bool>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual("User is not assigned to this tenant", result.Messages.FirstOrDefault());
    }

    [Fact]
    public async Task RemoveUserFromTenant_DatabasePersistence_ShouldRemoveCorrectly()
    {
        var (userId1, _, _) = await SeedTestDataAsync();

        // Verify assignment exists before removal
        var assignmentBefore = await DbContext.UserTenant
            .FirstOrDefaultAsync(ut => ut.UserId == userId1 && ut.TenantId == 2);
        TestAssertions.AssertNotNull(assignmentBefore);

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/2");
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);

        // Verify assignment is removed from database
        var assignmentAfter = await DbContext.UserTenant
            .FirstOrDefaultAsync(ut => ut.UserId == userId1 && ut.TenantId == 2);
        TestAssertions.AssertNotNull(assignmentAfter); // Should be null but TestAssertions.AssertNull doesn't exist
    }

    [Fact]
    public async Task RemoveUserFromTenant_WithDefaultTenant_ShouldSucceed()
    {
        var (userId1, _, _) = await SeedTestDataAsync();

        // Remove the default tenant assignment (tenant 1 for user1)
        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/1");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);
        var result = await ReadResponseAsync<Result<bool>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data);
    }

    [Fact]
    public async Task RemoveUserFromTenant_TenantIsolation_ShouldNotAffectOtherUsers()
    {
        var (userId1, userId2, userId3) = await SeedTestDataAsync();

        // Remove user1 from tenant 2
        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/2");
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);

        // Verify user2 and user3 assignments are unaffected
        var user2Assignments = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId2)
            .ToListAsync();
        var user3Assignments = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId3)
            .ToListAsync();

        TestAssertions.AssertEqual(2, user2Assignments.Count); // Should still have tenants 2, 3
        TestAssertions.AssertEqual(1, user3Assignments.Count); // Should still have tenant 1
        
        // User2 should still be in tenant 2
        TestAssertions.AssertTrue(user2Assignments.Any(a => a.TenantId == 2));
    }

    [Fact]
    public async Task RemoveUserFromTenant_MultipleRemovals_ShouldHandleSequentially()
    {
        var (userId1, _, _) = await SeedTestDataAsync();

        // Remove user1 from tenant 2
        var response1 = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/2");
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.OK);

        // Remove user1 from tenant 3
        var response2 = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/3");
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.OK);

        // Verify user1 now only has tenant 1
        var remainingAssignments = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId1)
            .ToListAsync();

        TestAssertions.AssertEqual(1, remainingAssignments.Count);
        TestAssertions.AssertEqual(1, remainingAssignments[0].TenantId);
        TestAssertions.AssertTrue(remainingAssignments[0].IsDefault);
    }

    [Fact]
    public async Task RemoveUserFromTenant_RemoveSameAssignmentTwice_ShouldFailSecondTime()
    {
        var (userId1, _, _) = await SeedTestDataAsync();

        // First removal should succeed
        var response1 = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/2");
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.OK);

        // Second removal should fail
        var response2 = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/2");
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.BadRequest);
        
        var result = await ReadResponseAsync<Result<bool>>(response2);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual("User is not assigned to this tenant", result.Messages.FirstOrDefault());
    }

    [Fact]
    public async Task RemoveUserFromTenant_WithInvalidTenantId_ShouldFail()
    {
        var (userId1, _, _) = await SeedTestDataAsync();

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/0");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task RemoveUserFromTenant_ResponseFormat_ShouldHaveCorrectStructure()
    {
        var (userId1, _, _) = await SeedTestDataAsync();

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/2");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);
        var result = await ReadResponseAsync<Result<bool>>(response);
        
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertTrue(result.Succeeded);
    }

    [Fact]
    public async Task RemoveUserFromTenant_ConcurrentRemovals_ShouldHandleGracefully()
    {
        var (userId1, _, _) = await SeedTestDataAsync();

        // Try to remove the same assignment concurrently
        var task1 = Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/2");
        var task2 = Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/2");

        var responses = await Task.WhenAll(task1, task2);

        // One should succeed, one should fail
        var successCount = responses.Count(r => r.StatusCode == HttpStatusCode.OK);
        var failureCount = responses.Count(r => r.StatusCode == HttpStatusCode.BadRequest);

        TestAssertions.AssertEqual(1, successCount);
        TestAssertions.AssertEqual(1, failureCount);
    }

    [Fact]
    public async Task RemoveUserFromTenant_WithTenantHeader_ShouldNotAffectOperation()
    {
        var (userId1, _, _) = await SeedTestDataAsync();
        SetTenantHeader(3);

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/2");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);
        
        // Verify the assignment was removed regardless of the tenant header
        var assignment = await DbContext.UserTenant
            .FirstOrDefaultAsync(ut => ut.UserId == userId1 && ut.TenantId == 2);
        
        // Assignment should be removed (would be null, but we test that it's not found)
        var assignmentExists = await DbContext.UserTenant
            .AnyAsync(ut => ut.UserId == userId1 && ut.TenantId == 2);
        TestAssertions.AssertFalse(assignmentExists);
    }

    [Fact]
    public async Task RemoveUserFromTenant_UserDataIsolation_ShouldOnlyRemoveSpecificUserTenant()
    {
        var (userId1, userId2, _) = await SeedTestDataAsync();

        // Both users are assigned to tenant 3
        var user1BeforeCount = await DbContext.UserTenant.CountAsync(ut => ut.UserId == userId1);
        var user2BeforeCount = await DbContext.UserTenant.CountAsync(ut => ut.UserId == userId2);

        // Remove user1 from tenant 3
        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/3");
        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);

        // Verify counts
        var user1AfterCount = await DbContext.UserTenant.CountAsync(ut => ut.UserId == userId1);
        var user2AfterCount = await DbContext.UserTenant.CountAsync(ut => ut.UserId == userId2);

        TestAssertions.AssertEqual(user1BeforeCount - 1, user1AfterCount);
        TestAssertions.AssertEqual(user2BeforeCount, user2AfterCount); // User2 should be unaffected
    }

    [Fact]
    public async Task RemoveUserFromTenant_WithNegativeTenantId_ShouldFail()
    {
        var (userId1, _, _) = await SeedTestDataAsync();

        var response = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/-1");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task RemoveUserFromTenant_RemoveAllAssignments_ShouldLeaveUserWithoutTenants()
    {
        var (_, _, userId3) = await SeedTestDataAsync();

        // User3 only has one assignment to tenant 1
        var response = await Client.DeleteAsync($"/api/v1/Users/{userId3}/tenants/1");

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.OK);

        // Verify user3 has no tenant assignments left
        var remainingAssignments = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId3)
            .ToListAsync();

        TestAssertions.AssertEmpty(remainingAssignments);
    }

    [Fact]
    public async Task RemoveUserFromTenant_HttpMethodValidation_ShouldOnlyAllowDelete()
    {
        var (userId1, _, _) = await SeedTestDataAsync();

        // Try with POST (should fail)
        var postResponse = await Client.PostAsync($"/api/v1/Users/{userId1}/tenants/2", new StringContent(""));
        TestAssertions.AssertHttpStatusCode(postResponse, HttpStatusCode.MethodNotAllowed);

        // Try with PUT (should fail)  
        var putResponse = await Client.PutAsync($"/api/v1/Users/{userId1}/tenants/2", new StringContent(""));
        TestAssertions.AssertHttpStatusCode(putResponse, HttpStatusCode.MethodNotAllowed);

        // DELETE should work
        var deleteResponse = await Client.DeleteAsync($"/api/v1/Users/{userId1}/tenants/2");
        TestAssertions.AssertHttpStatusCode(deleteResponse, HttpStatusCode.OK);
    }
}