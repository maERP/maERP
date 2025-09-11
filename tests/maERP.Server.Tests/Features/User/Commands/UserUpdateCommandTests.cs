using System.Net;
using System.Text.Json;
using maERP.Application.Features.User.Commands.UserUpdate;
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

public class UserUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public UserUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_UserUpdateCommandTests_{uniqueId}";
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

    protected async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
    {
        var json = JsonSerializer.Serialize(value);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        return await Client.PutAsync(requestUri, content);
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

    private async Task<(string UserId1, string UserId2)> SeedUsersAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var user1 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = $"user1{Guid.NewGuid():N}@example.com",
                UserName = $"user1{Guid.NewGuid():N}@example.com",
                NormalizedEmail = $"USER1{Guid.NewGuid():N}@EXAMPLE.COM",
                NormalizedUserName = $"USER1{Guid.NewGuid():N}@EXAMPLE.COM",
                Firstname = "Test",
                Lastname = "User1",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
            };

            var user2 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = $"user2{Guid.NewGuid():N}@example.com",
                UserName = $"user2{Guid.NewGuid():N}@example.com",
                NormalizedEmail = $"USER2{Guid.NewGuid():N}@EXAMPLE.COM",
                NormalizedUserName = $"USER2{Guid.NewGuid():N}@EXAMPLE.COM",
                Firstname = "Test",
                Lastname = "User2",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEJYOKhHvgoNZq5tLEhlTWxgCRCg6FeIxRQdIgQJpG1rGJXfkNv9m+GZOhFVK4qWdWQ=="
            };

            DbContext.Users.AddRange(user1, user2);

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

            DbContext.UserTenant.AddRange(userTenant1, userTenant2);
            await DbContext.SaveChangesAsync();

            return (user1.Id, user2.Id);
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private UserUpdateCommand CreateValidUpdateCommand(string userId)
    {
        return new UserUpdateCommand
        {
            Id = userId,
            Email = $"updated{Guid.NewGuid():N}@example.com",
            Firstname = "Updated",
            Lastname = "User",
            Password = "", // Optional for update
            DefaultTenantId = TenantConstants.TestTenant1Id,
            TenantIds = new List<Guid> { TenantConstants.TestTenant1Id }
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateUser_WithValidData_ShouldReturnNoContent()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_WithValidData_ShouldUpdateInDatabase()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.Firstname = "UpdatedFirstname";
        updateCommand.Lastname = "UpdatedLastname";

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        
        // Verify through database that user was updated
        TenantContext.SetCurrentTenantId(null);
        DbContext.ChangeTracker.Clear();
        var updatedUser = await DbContext.Users.FindAsync(userId1);
        TestAssertions.AssertNotNull(updatedUser);
        TestAssertions.AssertEqual(updateCommand.Email, updatedUser!.Email);
        TestAssertions.AssertEqual("UpdatedFirstname", updatedUser.Firstname);
        TestAssertions.AssertEqual("UpdatedLastname", updatedUser.Lastname);
    }

    [Fact]
    public async Task UpdateUser_WithNonExistentId_ShouldReturnNotFound()
    {
        await SeedTestDataAsync();
        await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var nonExistentId = Guid.NewGuid().ToString();
        var updateCommand = CreateValidUpdateCommand(nonExistentId);

        var response = await PutAsJsonAsync($"/api/v1/Users/{nonExistentId}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = new UserUpdateCommand
        {
            Id = userId1,
            // Missing required fields
            Email = "",
            Firstname = "",
            Lastname = ""
        };

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateUser_WithDuplicateEmail_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var (userId1, userId2) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        
        // Get the email of the second user
        TenantContext.SetCurrentTenantId(null);
        var user2 = await DbContext.Users.FindAsync(userId2);
        TestAssertions.AssertNotNull(user2);
        
        // Try to update first user with second user's email
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.Email = user2!.Email!;

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateUser_WithInvalidEmail_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.Email = "invalid-email-format";

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateUser_WithNewPassword_ShouldUpdatePassword()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.Password = "NewP@ssw0rd123";

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        
        // Verify password hash was updated (we can't verify exact password due to hashing)
        TenantContext.SetCurrentTenantId(null);
        DbContext.ChangeTracker.Clear();
        var updatedUser = await DbContext.Users.FindAsync(userId1);
        TestAssertions.AssertNotNull(updatedUser);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(updatedUser!.PasswordHash));
    }

    [Fact]
    public async Task UpdateUser_WithWeakPassword_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.Password = "123"; // Too weak

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateUser_WithUpdatedTenantAssignments_ShouldUpdateTenants()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.DefaultTenantId = TenantConstants.TestTenant1Id;
        updateCommand.TenantIds = new List<Guid> { TenantConstants.TestTenant1Id, TenantConstants.TestTenant2Id };

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        
        // Verify tenant assignments were updated
        TenantContext.SetCurrentTenantId(null);
        var userTenants = await DbContext.UserTenant
            .Where(ut => ut.UserId == userId1)
            .ToListAsync();
        
        TestAssertions.AssertEqual(2, userTenants.Count);
        TestAssertions.AssertTrue(userTenants.Any(ut => ut.TenantId == TenantConstants.TestTenant1Id && ut.IsDefault));
        TestAssertions.AssertTrue(userTenants.Any(ut => ut.TenantId == TenantConstants.TestTenant2Id && !ut.IsDefault));
    }

    [Fact]
    public async Task UpdateUser_WithInvalidTenantIds_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.TenantIds = new List<Guid> { Guid.NewGuid() }; // Non-existent tenant

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateUser_WithLongName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.Firstname = new string('A', 256); // Exceeds typical limit

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        // May return ASP.NET model validation error or custom validation
        var responseContent = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(responseContent.Contains("validation") || responseContent.Contains("Firstname") || responseContent.Contains("length"));
    }

    [Fact]
    public async Task UpdateUser_TenantIsolation_ShouldNotUpdateOtherTenantUsers()
    {
        await SeedTestDataAsync();
        var (userId1, userId2) = await SeedUsersAsync();
        
        // Try to update tenant 2's user from tenant 1 context
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId2);
        updateCommand.Firstname = "ShouldNotUpdate";

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId2}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.NotFound, response.StatusCode);
        
        // Verify user was not updated
        TenantContext.SetCurrentTenantId(null);
        var user = await DbContext.Users.FindAsync(userId2);
        TestAssertions.AssertNotNull(user);
        TestAssertions.AssertNotEqual("ShouldNotUpdate", user!.Firstname);
    }

    [Fact]
    public async Task UpdateUser_WithoutTenantHeader_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        var updateCommand = CreateValidUpdateCommand(userId1);

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        // Should fail due to missing tenant context
        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_WithMismatchedIdInUrlAndBody_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);
        var differentId = Guid.NewGuid().ToString();
        updateCommand.Id = differentId;

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateUser_WithEmptyPassword_ShouldNotUpdatePassword()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        
        // Get original password hash
        TenantContext.SetCurrentTenantId(null);
        var originalUser = await DbContext.Users.FindAsync(userId1);
        var originalPasswordHash = originalUser!.PasswordHash;
        
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.Password = ""; // Empty password should not update

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        
        // Verify password hash was not changed
        DbContext.ChangeTracker.Clear();
        var updatedUser = await DbContext.Users.FindAsync(userId1);
        TestAssertions.AssertNotNull(updatedUser);
        TestAssertions.AssertEqual(originalPasswordHash, updatedUser!.PasswordHash);
    }

    [Fact]
    public async Task UpdateUser_WithSpecialCharactersInName_ShouldUpdateSuccessfully()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.Firstname = "José-María";
        updateCommand.Lastname = "García-Pérez";

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);
        
        // Verify special characters were preserved
        TenantContext.SetCurrentTenantId(null);
        DbContext.ChangeTracker.Clear();
        var updatedUser = await DbContext.Users.FindAsync(userId1);
        TestAssertions.AssertNotNull(updatedUser);
        TestAssertions.AssertEqual("José-María", updatedUser!.Firstname);
        TestAssertions.AssertEqual("García-Pérez", updatedUser.Lastname);
    }

    [Fact]
    public async Task UpdateUser_WithInvalidJson_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/Users/{userId1}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_WithEmptyBody_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PutAsync($"/api/v1/Users/{userId1}", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_RemovingFromAllTenants_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.TenantIds = new List<Guid>(); // Empty tenant list

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task UpdateUser_WithNullDefaultTenant_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        var (userId1, _) = await SeedUsersAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var updateCommand = CreateValidUpdateCommand(userId1);
        updateCommand.DefaultTenantId = null;
        updateCommand.TenantIds = new List<Guid> { TenantConstants.TestTenant1Id };

        var response = await PutAsJsonAsync($"/api/v1/Users/{userId1}", updateCommand);

        // Should either succeed or return validation error depending on business rules
        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.BadRequest);
    }
}