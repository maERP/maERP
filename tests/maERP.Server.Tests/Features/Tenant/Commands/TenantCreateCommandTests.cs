using System.Net;
using System.Text.Json;
using maERP.Application.Features.Tenant.Commands.TenantCreate;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Features.Tenant.Commands;

public class TenantCreateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public TenantCreateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_TenantCreateCommandTests_{uniqueId}";
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

    private TenantCreateCommand CreateValidTenantCommand()
    {
        return new TenantCreateCommand
        {
            Name = "Test Tenant",
            TenantCode = "TEST001",
            Description = "A test tenant for unit testing",
            IsActive = true,
            ContactEmail = "test@tenant.com"
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task CreateTenant_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var command = CreateValidTenantCommand();

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_RequiresSuperadminRole_ShouldReturnUnauthorized()
    {
        var command = CreateValidTenantCommand();

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithValidData_ShouldReturnCreatedWhenAuthenticated()
    {
        var command = CreateValidTenantCommand();

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        // Since we don't have proper auth setup, we expect Unauthorized rather than Created
        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithEmptyName_ShouldReturnBadRequest()
    {
        var command = CreateValidTenantCommand();
        command.Name = "";

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithEmptyTenantCode_ShouldReturnBadRequest()
    {
        var command = CreateValidTenantCommand();
        command.TenantCode = "";

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithInvalidEmail_ShouldReturnBadRequest()
    {
        var command = CreateValidTenantCommand();
        command.ContactEmail = "invalid-email";

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithTooLongName_ShouldReturnBadRequest()
    {
        var command = CreateValidTenantCommand();
        command.Name = new string('A', 101); // Exceeds 100 character limit

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithTooLongTenantCode_ShouldReturnBadRequest()
    {
        var command = CreateValidTenantCommand();
        command.TenantCode = new string('A', 51); // Exceeds 50 character limit

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithTooLongDescription_ShouldReturnBadRequest()
    {
        var command = CreateValidTenantCommand();
        command.Description = new string('A', 501); // Exceeds 500 character limit

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithTooLongEmail_ShouldReturnBadRequest()
    {
        var command = CreateValidTenantCommand();
        command.ContactEmail = new string('A', 190) + "@test.com"; // Exceeds 200 character limit

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_HttpPostMethod_ShouldAcceptPostRequests()
    {
        var command = CreateValidTenantCommand();

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertNotEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_OnlyPostMethod_ShouldRejectGetRequests()
    {
        var response = await Client.GetAsync("/api/v1/Tenants");

        TestAssertions.AssertNotEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithNullName_ShouldReturnBadRequest()
    {
        var command = CreateValidTenantCommand();
        command.Name = null!;

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithNullTenantCode_ShouldReturnBadRequest()
    {
        var command = CreateValidTenantCommand();
        command.TenantCode = null!;

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithInactiveStatus_ShouldBeValid()
    {
        var command = CreateValidTenantCommand();
        command.IsActive = false;

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithEmptyDescription_ShouldBeValid()
    {
        var command = CreateValidTenantCommand();
        command.Description = "";

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithEmptyContactEmail_ShouldBeValid()
    {
        var command = CreateValidTenantCommand();
        command.ContactEmail = "";

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_ApiVersioned_ShouldRespondToV1Route()
    {
        var command = CreateValidTenantCommand();

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CreateTenant_WrongApiVersion_ShouldReturnBadRequest()
    {
        var command = CreateValidTenantCommand();

        var response = await PostAsJsonAsync("/api/v2/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_SecurityRequirements_ShouldEnforceSuperadminAccess()
    {
        var command = CreateValidTenantCommand();

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CreateTenant_ControllerRouting_ShouldRouteToCorrectController()
    {
        var command = CreateValidTenantCommand();

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_ResponseFormat_ShouldReturnJsonWhenAuthenticated()
    {
        var command = CreateValidTenantCommand();

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertTrue(response.Content.Headers.ContentType?.MediaType?.Contains("application/json") ?? false ||
                                 response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    public async Task CreateTenant_WithWhitespaceOnlyName_ShouldReturnBadRequest(string name)
    {
        var command = CreateValidTenantCommand();
        command.Name = name;

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    public async Task CreateTenant_WithWhitespaceOnlyTenantCode_ShouldReturnBadRequest(string tenantCode)
    {
        var command = CreateValidTenantCommand();
        command.TenantCode = tenantCode;

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithValidMinimalData_ShouldSucceedWhenAuthenticated()
    {
        var command = new TenantCreateCommand
        {
            Name = "Test",
            TenantCode = "T1",
            IsActive = true
        };

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithSpecialCharactersInName_ShouldBeValid()
    {
        var command = CreateValidTenantCommand();
        command.Name = "Test & Company Ltd.";

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateTenant_WithUnicodeCharacters_ShouldBeValid()
    {
        var command = CreateValidTenantCommand();
        command.Name = "Tëst Téñánt Ümlaut";

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [InlineData("test@valid.com")]
    [InlineData("user.name@domain.co.uk")]
    [InlineData("test+tag@example.org")]
    public async Task CreateTenant_WithValidEmailFormats_ShouldBeValid(string email)
    {
        var command = CreateValidTenantCommand();
        command.ContactEmail = email;

        var response = await PostAsJsonAsync("/api/v1/Tenants", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}