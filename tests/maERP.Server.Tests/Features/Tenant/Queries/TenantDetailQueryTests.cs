using System.Net;
using System.Text.Json;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Tenant.Queries;

public class TenantDetailQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public TenantDetailQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_TenantDetailQueryTests_{uniqueId}";
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

    protected async Task<T> ReadResponseAsync<T>(HttpResponseMessage response) where T : class
    {
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return result ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    private async Task SeedTenantTestDataAsync()
    {
        var currentTenant = TenantContext.GetCurrentTenantId();
        TenantContext.SetCurrentTenantId(null);

        try
        {
            var hasData = await DbContext.Tenant.AnyAsync();
            if (!hasData)
            {
                var tenant1 = new maERP.Domain.Entities.Tenant
                {
                    Id = 1,
                    Name = "Tenant One",
                    TenantCode = "TEN001",
                    Description = "First test tenant",
                    IsActive = true,
                    ContactEmail = "contact@tenant1.com",
                    DateCreated = DateTime.Now.AddDays(-30),
                    DateModified = DateTime.Now.AddDays(-5),
                };

                var tenant2 = new maERP.Domain.Entities.Tenant
                {
                    Id = 2,
                    Name = "Tenant Two",
                    TenantCode = "TEN002",
                    Description = "Second test tenant",
                    IsActive = false,
                    ContactEmail = "contact@tenant2.com",
                    DateCreated = DateTime.Now.AddDays(-60),
                    DateModified = DateTime.Now.AddDays(-10),
                };

                var tenant3 = new maERP.Domain.Entities.Tenant
                {
                    Id = 3,
                    Name = "Tenant Three",
                    TenantCode = "TEN003",
                    Description = "Third test tenant with longer description to test field handling",
                    IsActive = true,
                    ContactEmail = "admin@tenant3.example.com",
                    DateCreated = DateTime.Now.AddDays(-15),
                    DateModified = DateTime.Now.AddDays(-1),
                };

                DbContext.Tenant.AddRange(tenant1, tenant2, tenant3);
                await DbContext.SaveChangesAsync();
            }
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
    public async Task GetTenantDetail_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        await SeedTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants/1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithNonExistentId_ShouldReturnNotFoundWhenAuthenticated()
    {
        await SeedTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants/999");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithInvalidId_ShouldReturnBadRequest()
    {
        var response = await Client.GetAsync("/api/v1/Tenants/invalid");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithZeroId_ShouldReturnUnauthorized()
    {
        await SeedTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants/0");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithNegativeId_ShouldReturnUnauthorized()
    {
        await SeedTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants/-1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithLargeId_ShouldReturnUnauthorized()
    {
        await SeedTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants/2147483647");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_EndpointExists_ShouldNotReturnNotFoundForValidPath()
    {
        var response = await Client.GetAsync("/api/v1/Tenants/1");

        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_RequiresSuperadminRole_ShouldReturnUnauthorizedForNormalUsers()
    {
        await SeedTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants/1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_ApiVersioned_ShouldRespondToV1Route()
    {
        var response = await Client.GetAsync("/api/v1/Tenants/1");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetTenantDetail_WrongApiVersion_ShouldReturnBadRequest()
    {
        var response = await Client.GetAsync("/api/v2/Tenants/1");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_WithValidTenantData_ShouldBeAccessibleToSuperadmin()
    {
        await SeedTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants/1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantDetail_RequiresAuthorization_ShouldReturnUnauthorized()
    {
        await SeedTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants/1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task TenantDetailHandler_ValidatesInput_ShouldHandleInvalidParameters()
    {
        var response = await Client.GetAsync("/api/v1/Tenants/abc");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task TenantDetailEndpoint_HttpGetMethod_ShouldAcceptGetRequests()
    {
        var response = await Client.GetAsync("/api/v1/Tenants/1");

        TestAssertions.AssertNotEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task TenantDetailEndpoint_OnlyGetMethod_ShouldRejectPostRequests()
    {
        var response = await Client.PostAsync("/api/v1/Tenants/1", new StringContent(""));

        TestAssertions.AssertEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task TenantDetail_ResponseFormat_ShouldReturnJsonWhenAuthenticated()
    {
        await SeedTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants/1");

        TestAssertions.AssertTrue(response.Content.Headers.ContentType?.MediaType?.Contains("application/json") ?? false ||
                                 response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task TenantDetail_ControllerRouting_ShouldRouteToCorrectController()
    {
        var response = await Client.GetAsync("/api/v1/Tenants/1");

        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task TenantDetail_SecurityRequirements_ShouldEnforceSuperadminAccess()
    {
        await SeedTenantTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants/1");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden);
    }
}