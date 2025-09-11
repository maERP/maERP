using System.Net;
using System.Text.Json;
using System.Linq;
using maERP.Application.Features.UserTenant.Commands.AssignUserToTenant;
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

public class AssignUserToTenantCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public AssignUserToTenantCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_AssignUserToTenantCommandTests_{uniqueId}";
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

    protected async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PostAsync(requestUri, content);
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
                    Email = "user1@assign.test",
                    UserName = "user1@assign.test",
                    NormalizedUserName = "USER1@ASSIGN.TEST",
                    NormalizedEmail = "USER1@ASSIGN.TEST",
                    Firstname = "John",
                    Lastname = "Doe",
                    DateCreated = DateTime.UtcNow,
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                };

                var user2 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user2@assign.test",
                    UserName = "user2@assign.test",
                    NormalizedUserName = "USER2@ASSIGN.TEST",
                    NormalizedEmail = "USER2@ASSIGN.TEST",
                    Firstname = "Jane",
                    Lastname = "Smith",
                    DateCreated = DateTime.UtcNow,
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                };

                var user3 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user3@assign.test",
                    UserName = "user3@assign.test",
                    NormalizedUserName = "USER3@ASSIGN.TEST",
                    NormalizedEmail = "USER3@ASSIGN.TEST",
                    Firstname = "Bob",
                    Lastname = "Wilson",
                    DateCreated = DateTime.UtcNow,
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
                };

                DbContext.Users.AddRange(user1, user2, user3);

                // User2 already has an assignment to tenant 1 (non-default)
                var existingAssignment = new Domain.Entities.UserTenant
                {
                    UserId = user2.Id,
                    TenantId = TenantConstants.TestTenant1Id,
                    IsDefault = false,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                DbContext.UserTenant.Add(existingAssignment);
                await DbContext.SaveChangesAsync();

                return (user1.Id, user2.Id, user3.Id);
            }

            // Return existing user IDs
            var existingUsers = await DbContext.Users
                .Where(u => u.Email!.Contains("@assign.test"))
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

    private AssignUserToTenantCommand CreateValidAssignCommand(string userId, int tenantId, bool isDefault = false)
    {
        return new AssignUserToTenantCommand
        {
            UserId = userId,
            TenantId = tenantId,
            IsDefault = isDefault
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task AssignUserToTenant_WithValidData_ShouldSucceed()
    {
        var (userId1, _, _) = await SeedTestDataAsync();
        var command = CreateValidAssignCommand(userId1, 1);

        var response = await PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
        TestAssertions.AssertEqual("User successfully assigned to tenant", result.Messages.FirstOrDefault());
    }

    [Fact]
    public async Task AssignUserToTenant_WithNonExistentUser_ShouldFail()
    {
        await SeedTestDataAsync();
        var nonExistentUserId = Guid.NewGuid().ToString();
        var command = CreateValidAssignCommand(nonExistentUserId, 1);

        var response = await PostAsJsonAsync($"/api/v1/Users/{nonExistentUserId}/tenants", command);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual("User not found", result.Messages.FirstOrDefault());
    }

    [Fact]
    public async Task AssignUserToTenant_WithNonExistentTenant_ShouldFail()
    {
        var (userId1, _, _) = await SeedTestDataAsync();
        var command = CreateValidAssignCommand(userId1, 999);

        var response = await PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual("Tenant not found", result.Messages.FirstOrDefault());
    }

    [Fact]
    public async Task AssignUserToTenant_WithExistingAssignment_ShouldFail()
    {
        var (_, userId2, _) = await SeedTestDataAsync();
        var command = CreateValidAssignCommand(userId2, 1);

        var response = await PostAsJsonAsync($"/api/v1/Users/{userId2}/tenants", command);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
        var result = await ReadResponseAsync<Result<int>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertEqual("User is already assigned to this tenant", result.Messages.FirstOrDefault());
    }

    [Fact]
    public async Task AssignUserToTenant_WithDefaultFlag_ShouldUpdateExistingDefaults()
    {
        var (_, userId2, _) = await SeedTestDataAsync();
        
        // First assign to tenant 2 as default
        var command1 = CreateValidAssignCommand(userId2, 2, true);
        var response1 = await PostAsJsonAsync($"/api/v1/Users/{userId2}/tenants", command1);
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);

        // Then assign to tenant 3 as default - should remove default from tenant 2
        var command2 = CreateValidAssignCommand(userId2, 3, true);
        var response2 = await PostAsJsonAsync($"/api/v1/Users/{userId2}/tenants", command2);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);

        // Verify tenant 3 is now the default
        var assignments = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId2)
            .ToListAsync();
        
        var defaultAssignments = assignments.Where(a => a.IsDefault).ToList();
        TestAssertions.AssertEqual(1, defaultAssignments.Count);
        TestAssertions.AssertEqual(3, defaultAssignments[0].TenantId);
    }

    [Fact]
    public async Task AssignUserToTenant_WithInvalidUserId_ShouldFail()
    {
        await SeedTestDataAsync();
        var command = CreateValidAssignCommand("", 1);

        var response = await PostAsJsonAsync("/api/v1/Users/invalid-user-id/tenants", command);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AssignUserToTenant_WithInvalidTenantId_ShouldFail()
    {
        var (userId1, _, _) = await SeedTestDataAsync();
        var command = CreateValidAssignCommand(userId1, 0);

        var response = await PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AssignUserToTenant_DatabasePersistence_ShouldSaveCorrectly()
    {
        var (userId1, _, _) = await SeedTestDataAsync();
        var command = CreateValidAssignCommand(userId1, 2, true);

        var response = await PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        
        var dbAssignment = await DbContext.UserTenant
            .FirstOrDefaultAsync(ut => ut.UserId == userId1 && ut.TenantId == TenantConstants.TestTenant2Id);
        
        TestAssertions.AssertNotNull(dbAssignment);
        TestAssertions.AssertEqual(userId1, dbAssignment!.UserId);
        TestAssertions.AssertEqual(2, dbAssignment.TenantId);
        TestAssertions.AssertTrue(dbAssignment.IsDefault);
        TestAssertions.AssertTrue(dbAssignment.DateCreated > DateTime.MinValue);
        TestAssertions.AssertTrue(dbAssignment.DateModified > DateTime.MinValue);
    }

    [Fact]
    public async Task AssignUserToTenant_MultipleAssignments_ShouldAllowMultipleNonDefaultTenants()
    {
        var (userId1, _, _) = await SeedTestDataAsync();
        
        // Assign to tenant 1 (non-default)
        var command1 = CreateValidAssignCommand(userId1, 1, false);
        var response1 = await PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command1);
        TestAssertions.AssertHttpStatusCode(response1, HttpStatusCode.Created);

        // Assign to tenant 2 (non-default)
        var command2 = CreateValidAssignCommand(userId1, 2, false);
        var response2 = await PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command2);
        TestAssertions.AssertHttpStatusCode(response2, HttpStatusCode.Created);

        // Assign to tenant 3 (default)
        var command3 = CreateValidAssignCommand(userId1, 3, true);
        var response3 = await PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command3);
        TestAssertions.AssertHttpStatusCode(response3, HttpStatusCode.Created);

        var assignments = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId1)
            .ToListAsync();

        TestAssertions.AssertEqual(3, assignments.Count);
        TestAssertions.AssertEqual(1, assignments.Count(a => a.IsDefault));
        TestAssertions.AssertEqual(3, assignments.First(a => a.IsDefault).TenantId);
    }

    [Fact]
    public async Task AssignUserToTenant_TenantIsolation_ShouldNotAffectOtherTenantData()
    {
        var (userId1, userId2, _) = await SeedTestDataAsync();
        
        // User1 assigned to tenant 1
        var command1 = CreateValidAssignCommand(userId1, 1, true);
        await PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command1);

        // User2 assigned to tenant 2  
        var command2 = CreateValidAssignCommand(userId2, 2, true);
        await PostAsJsonAsync($"/api/v1/Users/{userId2}/tenants", command2);

        // Verify isolation - each user should only see their own assignments
        var user1Assignments = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId1)
            .ToListAsync();

        var user2Assignments = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId2)
            .ToListAsync();

        // User1 should have assignment to tenant 1 only
        TestAssertions.AssertEqual(1, user1Assignments.Count);
        TestAssertions.AssertEqual(1, user1Assignments[0].TenantId);

        // User2 should have assignments to tenant 1 (existing) and tenant 2 (new)
        TestAssertions.AssertEqual(2, user2Assignments.Count);
        TestAssertions.AssertContains(1, user2Assignments.Select(a => a.TenantId));
        TestAssertions.AssertContains(2, user2Assignments.Select(a => a.TenantId));
    }

    [Fact]
    public async Task AssignUserToTenant_ResponseFormat_ShouldHaveCorrectStructure()
    {
        var (userId1, _, _) = await SeedTestDataAsync();
        var command = CreateValidAssignCommand(userId1, 1);

        var response = await PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        var result = await ReadResponseAsync<Result<int>>(response);
        
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertNotNull(result.Messages);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertTrue(result.Data > 0);
    }

    [Fact]
    public async Task AssignUserToTenant_WithMismatchedUserIdInUrlAndCommand_ShouldUseUrlId()
    {
        var (userId1, _, _) = await SeedTestDataAsync();
        var command = CreateValidAssignCommand("different-user-id", 1);

        var response = await PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        
        // Verify the assignment was created for the user ID from the URL, not the command
        var dbAssignment = await DbContext.UserTenant
            .FirstOrDefaultAsync(ut => ut.UserId == userId1 && ut.TenantId == TenantConstants.TestTenant1Id);
        
        TestAssertions.AssertNotNull(dbAssignment);
        TestAssertions.AssertEqual(userId1, dbAssignment!.UserId);
    }

    [Fact]
    public async Task AssignUserToTenant_JsonSerialization_ShouldHandleCorrectly()
    {
        var (userId1, _, _) = await SeedTestDataAsync();
        var command = CreateValidAssignCommand(userId1, 1, true);

        var json = JsonSerializer.Serialize(command);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(json));

        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync($"/api/v1/Users/{userId1}/tenants", content);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
    }

    [Fact]
    public async Task AssignUserToTenant_ConcurrentRequests_ShouldHandleGracefully()
    {
        var (userId1, _, _) = await SeedTestDataAsync();
        var command1 = CreateValidAssignCommand(userId1, 1);
        var command2 = CreateValidAssignCommand(userId1, 1); // Same assignment

        // Execute both requests concurrently
        var task1 = PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command1);
        var task2 = PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command2);

        var responses = await Task.WhenAll(task1, task2);

        // One should succeed, one should fail
        var successCount = responses.Count(r => r.StatusCode == HttpStatusCode.Created);
        var failureCount = responses.Count(r => r.StatusCode == HttpStatusCode.BadRequest);

        TestAssertions.AssertEqual(1, successCount);
        TestAssertions.AssertEqual(1, failureCount);
    }

    [Fact]
    public async Task AssignUserToTenant_WithTenantHeader_ShouldNotAffectAssignment()
    {
        var (userId1, _, _) = await SeedTestDataAsync();
        SetTenantHeader(2);
        
        var command = CreateValidAssignCommand(userId1, 1);
        var response = await PostAsJsonAsync($"/api/v1/Users/{userId1}/tenants", command);

        TestAssertions.AssertHttpStatusCode(response, HttpStatusCode.Created);
        
        // Verify assignment was created for the tenant in the command, not the header
        var dbAssignment = await DbContext.UserTenant
            .FirstOrDefaultAsync(ut => ut.UserId == userId1 && ut.TenantId == TenantConstants.TestTenant1Id);
        
        TestAssertions.AssertNotNull(dbAssignment);
        TestAssertions.AssertEqual(1, dbAssignment!.TenantId);
    }
}