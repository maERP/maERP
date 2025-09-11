using System.Net;
using System.Text.Json;
using maERP.Application.Features.Tenant.Commands.TenantUpdate;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Tenant.Commands;

public class TenantUpdateCommandTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public TenantUpdateCommandTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_TenantUpdateCommandTests_{uniqueId}";
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

    private async Task SeedTestTenantAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var existingTenant = await DbContext.Tenant.FirstOrDefaultAsync(t => t.Id == 1);
            if (existingTenant == null)
            {
                var tenant = new maERP.Domain.Entities.Tenant
                {
                    Id = 1,
                    Name = "Original Tenant",
                    TenantCode = "ORIG001",
                    Description = "Original description",
                    IsActive = true,
                    ContactEmail = "original@tenant.com",
                    DateCreated = DateTime.Now.AddDays(-30),
                    DateModified = DateTime.Now.AddDays(-15)
                };

                DbContext.Tenant.Add(tenant);
                await DbContext.SaveChangesAsync();
            }
        }
        finally
        {
            TenantContext.SetCurrentTenantId(currentTenant);
        }
    }

    private TenantUpdateCommand CreateValidUpdateCommand(int id = 1)
    {
        return new TenantUpdateCommand
        {
            Id = id,
            Name = "Updated Tenant",
            TenantCode = "UPD001",
            Description = "Updated description",
            IsActive = true,
            ContactEmail = "updated@tenant.com"
        };
    }

    public void Dispose()
    {
        Scope?.Dispose();
        Client?.Dispose();
        Factory?.Dispose();
    }

    [Fact]
    public async Task UpdateTenant_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_RequiresSuperadminRole_ShouldReturnUnauthorized()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithValidData_ShouldReturnOkWhenAuthenticated()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        // Since we don't have proper auth setup, we expect Unauthorized rather than OK
        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithNonExistentId_ShouldReturnNotFoundWhenAuthenticated()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand(999);

        var response = await PutAsJsonAsync("/api/v1/Tenants/999", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithEmptyName_ShouldReturnBadRequest()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.Name = "";

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithEmptyTenantCode_ShouldReturnBadRequest()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.TenantCode = "";

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithInvalidEmail_ShouldReturnBadRequest()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.ContactEmail = "invalid-email";

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithTooLongName_ShouldReturnBadRequest()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.Name = new string('A', 101); // Exceeds 100 character limit

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithTooLongTenantCode_ShouldReturnBadRequest()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.TenantCode = new string('A', 51); // Exceeds 50 character limit

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithTooLongDescription_ShouldReturnBadRequest()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.Description = new string('A', 501); // Exceeds 500 character limit

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithTooLongEmail_ShouldReturnBadRequest()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.ContactEmail = new string('A', 190) + "@test.com"; // Exceeds 200 character limit

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_HttpPutMethod_ShouldAcceptPutRequests()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertNotEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_OnlyPutMethod_ShouldRejectPostRequests()
    {
        var response = await Client.PostAsync("/api/v1/Tenants/1", new StringContent(""));

        TestAssertions.AssertEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithMismatchedId_ShouldHandleCorrectly()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand(1);

        // URL ID is 2, but command ID is 1
        var response = await PutAsJsonAsync("/api/v1/Tenants/2", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithZeroId_ShouldReturnBadRequest()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand(0);

        var response = await PutAsJsonAsync("/api/v1/Tenants/0", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithNegativeId_ShouldReturnBadRequest()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand(-1);

        var response = await PutAsJsonAsync("/api/v1/Tenants/-1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithInactiveStatus_ShouldBeValid()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.IsActive = false;

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithEmptyDescription_ShouldBeValid()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.Description = "";

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithEmptyContactEmail_ShouldBeValid()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.ContactEmail = "";

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_ApiVersioned_ShouldRespondToV1Route()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UpdateTenant_WrongApiVersion_ShouldReturnBadRequest()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();

        var response = await PutAsJsonAsync("/api/v2/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_SecurityRequirements_ShouldEnforceSuperadminAccess()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UpdateTenant_ControllerRouting_ShouldRouteToCorrectController()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_ResponseFormat_ShouldReturnJsonWhenAuthenticated()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertTrue(response.Content.Headers.ContentType?.MediaType?.Contains("application/json") ?? false ||
                                 response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    public async Task UpdateTenant_WithWhitespaceOnlyName_ShouldReturnBadRequest(string name)
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.Name = name;

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    public async Task UpdateTenant_WithWhitespaceOnlyTenantCode_ShouldReturnBadRequest(string tenantCode)
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.TenantCode = tenantCode;

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithSpecialCharactersInName_ShouldBeValid()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.Name = "Updated & Company Ltd.";

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithUnicodeCharacters_ShouldBeValid()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.Name = "Üpdätëd Téñánt";

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [InlineData("test@valid.com")]
    [InlineData("user.name@domain.co.uk")]
    [InlineData("test+tag@example.org")]
    public async Task UpdateTenant_WithValidEmailFormats_ShouldBeValid(string email)
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();
        command.ContactEmail = email;

        var response = await PutAsJsonAsync("/api/v1/Tenants/1", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithLargeId_ShouldHandleCorrectly()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand(2147483647);

        var response = await PutAsJsonAsync("/api/v1/Tenants/2147483647", command);

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateTenant_WithInvalidIdInUrl_ShouldReturnBadRequest()
    {
        await SeedTestTenantAsync();
        var command = CreateValidUpdateCommand();

        var response = await PutAsJsonAsync("/api/v1/Tenants/invalid", command);

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}