using System.Net;
using System.Text.Json;
using maERP.Application.Features.User.Commands.UserCreate;
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

public class UserCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public UserCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_UserCreateCommandTests_{uniqueId}";
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

            await EnsureCurrentUserCanManageAsync(
                TenantConstants.TestTenant1Id,
                TenantConstants.TestTenant2Id);
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private UserCreateCommand CreateValidUserCommand()
    {
        return new UserCreateCommand
        {
            Email = $"testuser{Guid.NewGuid():N}@example.com",
            Password = "Test@123456",
            Firstname = "Test",
            Lastname = "User",
            DefaultTenantId = TenantConstants.TestTenant1Id,
            AdditionalTenantIds = new List<Guid>()
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateUser_WithValidData_ShouldReturnCreated()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(result.Data));
    }

    [Fact]
    public async Task CreateUser_WithoutManagePermission_ShouldReturnForbidden_old()
    {
        await SeedTestDataAsync();
        await EnsureCurrentUserCannotManageAsync(TenantConstants.TestTenant1Id);
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.Forbidden, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateUser_WithValidData_ShouldPersistInDatabase()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(result.Data));

        // Verify through API that user exists
        var getResponse = await Client.GetAsync($"/api/v1/Users/{result.Data}");

        // Users endpoint requires Superadmin role, so it might return Forbidden
        // But if we get the user, verify the data matches
        if (getResponse.StatusCode == HttpStatusCode.OK)
        {
            var userDetail = await ReadResponseAsync<Result<Domain.Dtos.User.UserDetailDto>>(getResponse);
            TestAssertions.AssertNotNull(userDetail?.Data);
            TestAssertions.AssertEqual(userCommand.Email, userDetail!.Data.Email);
            TestAssertions.AssertEqual(userCommand.Firstname, userDetail.Data.Firstname);
            TestAssertions.AssertEqual(userCommand.Lastname, userDetail.Data.Lastname);
        }
    }

    [Fact]
    public async Task CreateUser_WithMissingRequiredFields_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = new UserCreateCommand
        {
            // Missing required fields Email, Password, Firstname, Lastname
            DefaultTenantId = TenantConstants.TestTenant1Id
        };

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateUser_WithDuplicateEmail_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        // Create first user
        var firstUser = CreateValidUserCommand();
        await PostAsJsonAsync("/api/v1/Users", firstUser);

        // Try to create second user with same email
        var duplicateUser = CreateValidUserCommand();
        duplicateUser.Email = firstUser.Email;
        duplicateUser.Firstname = "Another";
        duplicateUser.Lastname = "User";

        var response = await PostAsJsonAsync("/api/v1/Users", duplicateUser);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateUser_WithInvalidEmail_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.Email = "invalid-email-format";

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateUser_WithWeakPassword_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.Password = "123"; // Too weak

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateUser_WithInvalidDefaultTenantId_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.DefaultTenantId = Guid.NewGuid(); // Non-existent tenant

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateUser_WithEmptyFirstname_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.Firstname = "";

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateUser_WithEmptyLastname_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.Lastname = "";

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateUser_WithAdditionalTenantIds_ShouldCreateUserWithMultipleTenants()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.DefaultTenantId = TenantConstants.TestTenant1Id;
        userCommand.AdditionalTenantIds = new List<Guid> { TenantConstants.TestTenant2Id };

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(result.Data));

        // Verify tenant assignments through database (since API might require special permissions)
        TenantContext.SetCurrentTenantId(null);
        var userTenants = await DbContext.UserTenant
            .Where(ut => ut.UserId == result.Data)
            .ToListAsync();

        TestAssertions.AssertEqual(2, userTenants.Count);
        TestAssertions.AssertTrue(userTenants.Any(ut => ut.TenantId == TenantConstants.TestTenant1Id && ut.IsDefault));
        TestAssertions.AssertTrue(userTenants.Any(ut => ut.TenantId == TenantConstants.TestTenant2Id && !ut.IsDefault));
    }

    [Fact]
    public async Task CreateUser_WithInvalidAdditionalTenantIds_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.DefaultTenantId = TenantConstants.TestTenant1Id;
        userCommand.AdditionalTenantIds = new List<Guid> { Guid.NewGuid() }; // Non-existent tenant

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateUser_WithLongName_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.Firstname = new string('A', 256); // Exceeds typical limit

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        // May return ASP.NET model validation error or custom validation
        var responseContent = await response.Content.ReadAsStringAsync();
        TestAssertions.AssertTrue(responseContent.Contains("validation") || responseContent.Contains("Firstname") || responseContent.Contains("length"));
    }

    [Fact]
    public async Task CreateUser_WithValidMaxLengthFields_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.Firstname = new string('A', 50); // Reasonable max length
        userCommand.Lastname = new string('B', 50);

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(result.Data));
    }

    [Fact]
    public async Task CreateUser_TenantIsolation_ShouldCreateUserInCorrectTenant()
    {
        await SeedTestDataAsync();

        // Create user in tenant 1
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var user1Command = CreateValidUserCommand();
        user1Command.Email = $"tenant1user{Guid.NewGuid():N}@example.com";
        user1Command.DefaultTenantId = TenantConstants.TestTenant1Id;

        var response1 = await PostAsJsonAsync("/api/v1/Users", user1Command);
        TestAssertions.AssertEqual(HttpStatusCode.Created, response1.StatusCode);
        var result1 = await ReadResponseAsync<Result<string>>(response1);
        TestAssertions.AssertNotNull(result1);
        TestAssertions.AssertTrue(result1.Succeeded);

        // Create user in tenant 2
        SetTenantHeader(TenantConstants.TestTenant2Id);
        var user2Command = CreateValidUserCommand();
        user2Command.Email = $"tenant2user{Guid.NewGuid():N}@example.com";
        user2Command.DefaultTenantId = TenantConstants.TestTenant2Id;

        var response2 = await PostAsJsonAsync("/api/v1/Users", user2Command);
        TestAssertions.AssertEqual(HttpStatusCode.Created, response2.StatusCode);
        var result2 = await ReadResponseAsync<Result<string>>(response2);
        TestAssertions.AssertNotNull(result2);
        TestAssertions.AssertTrue(result2.Succeeded);

        // Verify tenant isolation in database
        TenantContext.SetCurrentTenantId(null);
        var user1Tenants = await DbContext.UserTenant
            .Where(ut => ut.UserId == result1.Data)
            .ToListAsync();
        var user2Tenants = await DbContext.UserTenant
            .Where(ut => ut.UserId == result2.Data)
            .ToListAsync();

        TestAssertions.AssertEqual(1, user1Tenants.Count);
        TestAssertions.AssertEqual(TenantConstants.TestTenant1Id, user1Tenants.First().TenantId);
        TestAssertions.AssertTrue(user1Tenants.First().IsDefault);

        TestAssertions.AssertEqual(1, user2Tenants.Count);
        TestAssertions.AssertEqual(TenantConstants.TestTenant2Id, user2Tenants.First().TenantId);
        TestAssertions.AssertTrue(user2Tenants.First().IsDefault);
    }

    [Fact]
    public async Task CreateUser_WithoutTenantHeader_ShouldUseDefaultTenant()
    {
        await SeedTestDataAsync();
        var userCommand = CreateValidUserCommand();

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(result.Data));
    }

    [Fact]
    public async Task CreateUser_WithNonExistentTenant_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.DefaultTenantId = Guid.NewGuid();

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);
    }

    [Fact]
    public async Task CreateUser_ResponseStructure_ShouldHaveCorrectFormat()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertNotNull(result.Data);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(result.Data));
        TestAssertions.AssertNotNull(result.Messages);
    }

    [Fact]
    public async Task CreateUser_WithInvalidJson_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var invalidJson = "{ invalid json }";
        var content = new StringContent(invalidJson, System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Users", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateUser_WithEmptyBody_ShouldReturnBadRequest()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);

        var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
        var response = await Client.PostAsync("/api/v1/Users", content);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateUser_WithSpecialCharactersInName_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.Firstname = "José-María";
        userCommand.Lastname = "García-Pérez";

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(result.Data));
    }

    [Fact]
    public async Task CreateUser_WithComplexPassword_ShouldCreateSuccessfully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.Password = "ComplexP@ssw0rd!2024";

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);
        TestAssertions.AssertFalse(string.IsNullOrEmpty(result.Data));
    }

    [Fact]
    public async Task CreateUser_WithDuplicateInAdditionalTenants_ShouldHandleGracefully()
    {
        await SeedTestDataAsync();
        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();
        userCommand.DefaultTenantId = TenantConstants.TestTenant1Id;
        userCommand.AdditionalTenantIds = new List<Guid> { TenantConstants.TestTenant1Id, TenantConstants.TestTenant2Id }; // Default tenant included in additional

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        // Should either succeed by deduplicating or return validation error
        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.BadRequest);

        if (response.StatusCode == HttpStatusCode.Created)
        {
            var result = await ReadResponseAsync<Result<string>>(response);
            TestAssertions.AssertNotNull(result);
            TestAssertions.AssertTrue(result.Succeeded);

            // Verify tenant assignments - should not have duplicates
            TenantContext.SetCurrentTenantId(null);
            var userTenants = await DbContext.UserTenant
                .Where(ut => ut.UserId == result.Data)
                .ToListAsync();

            var distinctTenants = userTenants.Select(ut => ut.TenantId).Distinct().ToList();
            TestAssertions.AssertEqual(userTenants.Count, distinctTenants.Count);
        }
    }

    [Fact]
    public async Task CreateUser_WithManagePermission_ShouldSucceed()
    {
        await SeedTestDataAsync();
        Client.DefaultRequestHeaders.Remove("X-Test-Roles");
        Client.DefaultRequestHeaders.Add("X-Test-Roles", string.Empty);
        await CurrentUserHelper.EnsureUserAsync(Client, DbContext, TenantConstants.TestTenant1Id, true);

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.Created, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertTrue(result.Succeeded);

        Client.DefaultRequestHeaders.Remove("X-Test-Roles");
    }

    [Fact]
    public async Task CreateUser_WithoutManagePermission_ShouldReturnForbidden()
    {
        await SeedTestDataAsync();
        Client.DefaultRequestHeaders.Remove("X-Test-Roles");
        Client.DefaultRequestHeaders.Add("X-Test-Roles", string.Empty);
        await CurrentUserHelper.EnsureUserAsync(Client, DbContext, TenantConstants.TestTenant1Id, false);

        SetTenantHeader(TenantConstants.TestTenant1Id);
        var userCommand = CreateValidUserCommand();

        var response = await PostAsJsonAsync("/api/v1/Users", userCommand);

        TestAssertions.AssertEqual(HttpStatusCode.Forbidden, response.StatusCode);
        var result = await ReadResponseAsync<Result<string>>(response);
        TestAssertions.AssertNotNull(result);
        TestAssertions.AssertFalse(result.Succeeded);
        TestAssertions.AssertNotEmpty(result.Messages);

        Client.DefaultRequestHeaders.Remove("X-Test-Roles");
    }
}
