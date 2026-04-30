using System.Net;
using maERP.Application.Features.Account.Commands.ChangePassword;
using maERP.Application.Features.Account.Commands.UpdateCurrentUser;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.Account;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Features.Account;

public class AccountControllerTests : TenantIsolatedTestBase
{
    private const string OriginalPassword = "InitialP@ssword1";

    private async Task<ApplicationUser> SeedCurrentUserAsync(string? userId = null, string? email = null, string? password = null)
    {
        await TestDataSeeder.SeedTestDataAsync(DbContext, TenantContext);

        userId ??= CurrentUserHelper.DefaultUserId;
        email ??= $"account-{userId}@test.com";
        password ??= OriginalPassword;

        var existing = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (existing != null)
        {
            return existing;
        }

        using var scope = Factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = new ApplicationUser
        {
            Id = userId,
            Email = email,
            NormalizedEmail = email.ToUpperInvariant(),
            UserName = email,
            NormalizedUserName = email.ToUpperInvariant(),
            EmailConfirmed = true,
            Firstname = "Initial",
            Lastname = "User",
            PhoneNumber = "+49 123 4567890",
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        var created = await userManager.CreateAsync(user, password);
        TestAssertions.AssertTrue(created.Succeeded);

        await DbContext.UserTenant.AddAsync(new UserTenant
        {
            UserId = userId,
            TenantId = TenantConstants.TestTenant1Id,
            IsDefault = true,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        });
        await DbContext.SaveChangesAsync();

        return user;
    }

    private void AuthenticateAs(string userId)
    {
        SimulateAuthenticatedRequest(userId);
    }

    [Fact]
    public async Task GetMe_WithoutAuthentication_ReturnsUnauthorized()
    {
        await SeedCurrentUserAsync();
        SimulateUnauthenticatedRequest();

        var response = await Client.GetAsync("/api/v1/Account/me");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetMe_WithAuthentication_ReturnsOwnProfile()
    {
        var user = await SeedCurrentUserAsync();
        AuthenticateAs(user.Id);

        var response = await Client.GetAsync("/api/v1/Account/me");

        TestAssertions.AssertEqual(HttpStatusCode.OK, response.StatusCode);
        var result = await ReadResponseAsync<Result<CurrentUserProfileDto>>(response);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertEqual(user.Id, result.Data!.Id);
        TestAssertions.AssertEqual(user.Email!, result.Data.Email);
        TestAssertions.AssertEqual("Initial", result.Data.Firstname);
        TestAssertions.AssertEqual("User", result.Data.Lastname);
    }

    [Fact]
    public async Task UpdateMe_WithValidData_PersistsChangesForOwnUser()
    {
        var user = await SeedCurrentUserAsync();
        AuthenticateAs(user.Id);

        var command = new UpdateCurrentUserCommand
        {
            Email = "renamed@test.com",
            Firstname = "Renamed",
            Lastname = "Person",
            PhoneNumber = "+49 555 0000"
        };

        var response = await PutAsJsonAsync("/api/v1/Account/me", command);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var reloaded = await DbContext.Users.AsNoTracking().FirstAsync(u => u.Id == user.Id);
        TestAssertions.AssertEqual("renamed@test.com", reloaded.Email!);
        TestAssertions.AssertEqual("Renamed", reloaded.Firstname);
        TestAssertions.AssertEqual("Person", reloaded.Lastname);
        TestAssertions.AssertEqual("+49 555 0000", reloaded.PhoneNumber!);
    }

    [Fact]
    public async Task UpdateMe_DoesNotAffectOtherUsers()
    {
        var me = await SeedCurrentUserAsync();
        var otherId = "22222222-2222-2222-2222-222222222222";
        var other = await SeedCurrentUserAsync(otherId, "other@test.com");

        AuthenticateAs(me.Id);

        var command = new UpdateCurrentUserCommand
        {
            Email = "renamed@test.com",
            Firstname = "Renamed",
            Lastname = "Person",
            PhoneNumber = string.Empty
        };

        var response = await PutAsJsonAsync("/api/v1/Account/me", command);
        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        var otherReloaded = await DbContext.Users.AsNoTracking().FirstAsync(u => u.Id == other.Id);
        TestAssertions.AssertEqual("other@test.com", otherReloaded.Email!);
        TestAssertions.AssertEqual("Initial", otherReloaded.Firstname);
    }

    [Fact]
    public async Task UpdateMe_WithEmailAlreadyInUse_ReturnsBadRequest()
    {
        var me = await SeedCurrentUserAsync();
        await SeedCurrentUserAsync("33333333-3333-3333-3333-333333333333", "occupied@test.com");

        AuthenticateAs(me.Id);

        var command = new UpdateCurrentUserCommand
        {
            Email = "occupied@test.com",
            Firstname = "Renamed",
            Lastname = "Person"
        };

        var response = await PutAsJsonAsync("/api/v1/Account/me", command);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateMe_WithMissingFirstname_ReturnsBadRequest()
    {
        var user = await SeedCurrentUserAsync();
        AuthenticateAs(user.Id);

        var command = new UpdateCurrentUserCommand
        {
            Email = user.Email!,
            Firstname = string.Empty,
            Lastname = "Person"
        };

        var response = await PutAsJsonAsync("/api/v1/Account/me", command);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ChangePassword_WithCorrectCurrentPassword_Succeeds()
    {
        var user = await SeedCurrentUserAsync();
        AuthenticateAs(user.Id);

        var command = new ChangePasswordCommand
        {
            CurrentPassword = OriginalPassword,
            NewPassword = "NewP@ssword2",
            NewPasswordConfirm = "NewP@ssword2"
        };

        var response = await PostAsJsonAsync("/api/v1/Account/change-password", command);

        TestAssertions.AssertEqual(HttpStatusCode.NoContent, response.StatusCode);

        using var scope = Factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var reloaded = await userManager.FindByIdAsync(user.Id);
        TestAssertions.AssertNotNull(reloaded);
        TestAssertions.AssertTrue(await userManager.CheckPasswordAsync(reloaded!, "NewP@ssword2"));
        TestAssertions.AssertFalse(await userManager.CheckPasswordAsync(reloaded!, OriginalPassword));
    }

    [Fact]
    public async Task ChangePassword_WithWrongCurrentPassword_ReturnsBadRequest()
    {
        var user = await SeedCurrentUserAsync();
        AuthenticateAs(user.Id);

        var command = new ChangePasswordCommand
        {
            CurrentPassword = "WrongOldP@ss1",
            NewPassword = "NewP@ssword2",
            NewPasswordConfirm = "NewP@ssword2"
        };

        var response = await PostAsJsonAsync("/api/v1/Account/change-password", command);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);

        using var scope = Factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var reloaded = await userManager.FindByIdAsync(user.Id);
        TestAssertions.AssertTrue(await userManager.CheckPasswordAsync(reloaded!, OriginalPassword));
    }

    [Fact]
    public async Task ChangePassword_WhenConfirmDoesNotMatch_ReturnsBadRequest()
    {
        var user = await SeedCurrentUserAsync();
        AuthenticateAs(user.Id);

        var command = new ChangePasswordCommand
        {
            CurrentPassword = OriginalPassword,
            NewPassword = "NewP@ssword2",
            NewPasswordConfirm = "DifferentP@ss3"
        };

        var response = await PostAsJsonAsync("/api/v1/Account/change-password", command);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ChangePassword_WithoutAuthentication_ReturnsUnauthorized()
    {
        await SeedCurrentUserAsync();
        SimulateUnauthenticatedRequest();

        var command = new ChangePasswordCommand
        {
            CurrentPassword = OriginalPassword,
            NewPassword = "NewP@ssword2",
            NewPasswordConfirm = "NewP@ssword2"
        };

        var response = await PostAsJsonAsync("/api/v1/Account/change-password", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
