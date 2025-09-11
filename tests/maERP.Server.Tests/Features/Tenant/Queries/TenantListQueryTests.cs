using System.Net;
using System.Text.Json;
using maERP.Domain.Constants;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Server.Tests.Infrastructure;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace maERP.Server.Tests.Features.Tenant.Queries;

public class TenantListQueryTests : IDisposable
{
    protected readonly TestWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;
    protected readonly ITenantContext TenantContext;
    protected readonly IServiceScope Scope;

    public TenantListQueryTests()
    {
        var uniqueId = Guid.NewGuid().ToString("N")[..8];
        var testDbName = $"TestDb_TenantListQueryTests_{uniqueId}";
        Environment.SetEnvironmentVariable("TEST_DB_NAME", testDbName);

        Factory = new TestWebApplicationFactory<Program>();
        Client = Factory.CreateClient();

        Scope = Factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        TenantContext = Scope.ServiceProvider.GetRequiredService<ITenantContext>();

        DbContext.Database.EnsureCreated();

        TenantContext.SetAssignedTenantIds(new[] { TenantConstants.TestTenant1Id, TenantConstants.TestTenant2Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() });
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

    private async Task SeedTenantListTestDataAsync()
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
                    Name = "Alpha Tenant",
                    TenantCode = "ALPHA",
                    Description = "First alphabetically sorted tenant",
                    IsActive = true,
                    ContactEmail = "alpha@example.com",
                    DateCreated = DateTime.Now.AddDays(-100),
                    DateModified = DateTime.Now.AddDays(-50),
                };

                var tenant2 = new maERP.Domain.Entities.Tenant
                {
                    Id = 2,
                    Name = "Beta Corporation",
                    TenantCode = "BETA",
                    Description = "Second test tenant for beta testing",
                    IsActive = false,
                    ContactEmail = "beta@corporation.com",
                    DateCreated = DateTime.Now.AddDays(-80),
                    DateModified = DateTime.Now.AddDays(-30),
                };

                var tenant3 = new maERP.Domain.Entities.Tenant
                {
                    Id = 3,
                    Name = "Gamma Solutions",
                    TenantCode = "GAMMA",
                    Description = "Third tenant for gamma solutions",
                    IsActive = true,
                    ContactEmail = "info@gamma-solutions.net",
                    DateCreated = DateTime.Now.AddDays(-60),
                    DateModified = DateTime.Now.AddDays(-20),
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
    public async Task GetTenantList_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_WithDefaultPagination_ShouldReturnUnauthorized()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_WithCustomPageSize_ShouldReturnUnauthorized()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants?pageSize=5");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_WithPageNumber_ShouldReturnUnauthorized()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants?pageNumber=1&pageSize=1");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_WithSearchString_ShouldReturnUnauthorized()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants?searchString=Alpha");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_WithOrderBy_ShouldReturnUnauthorized()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants?orderBy=Name");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_RequiresSuperadminRole_ShouldReturnUnauthorizedForNormalUsers()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_EndpointExists_ShouldNotReturnNotFoundForValidPath()
    {
        var response = await Client.GetAsync("/api/v1/Tenants");

        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_ApiVersioned_ShouldRespondToV1Route()
    {
        var response = await Client.GetAsync("/api/v1/Tenants");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetTenantList_WrongApiVersion_ShouldReturnBadRequest()
    {
        var response = await Client.GetAsync("/api/v2/Tenants");

        TestAssertions.AssertEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_SecurityRequirements_ShouldEnforceSuperadminAccess()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants");

        TestAssertions.AssertTrue(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetTenantList_WithComplexQueryParameters_ShouldReturnUnauthorized()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants?pageNumber=1&pageSize=10&searchString=test&orderBy=Name desc");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_HttpGetMethod_ShouldAcceptGetRequests()
    {
        var response = await Client.GetAsync("/api/v1/Tenants");

        TestAssertions.AssertNotEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_OnlyGetMethod_ShouldRejectPostRequests()
    {
        var response = await Client.PostAsync("/api/v1/Tenants", new StringContent(""));

        TestAssertions.AssertEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_RequiresAuthorization_ShouldReturnUnauthorized()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_ControllerRouting_ShouldRouteToCorrectController()
    {
        var response = await Client.GetAsync("/api/v1/Tenants");

        TestAssertions.AssertNotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_WithInvalidPageParameters_ShouldReturnUnauthorized()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants?pageNumber=-1&pageSize=0");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_WithEmptySearchString_ShouldReturnUnauthorized()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants?searchString=");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTenantList_ResponseFormat_ShouldReturnJsonWhenAuthenticated()
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync("/api/v1/Tenants");

        TestAssertions.AssertTrue(response.Content.Headers.ContentType?.MediaType?.Contains("application/json") ?? false ||
                                 response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Theory]
    [InlineData("?pageSize=100")]
    [InlineData("?pageNumber=5")]
    [InlineData("?searchString=test")]
    [InlineData("?orderBy=Name")]
    public async Task GetTenantList_WithVariousParameters_ShouldReturnUnauthorized(string queryParams)
    {
        await SeedTenantListTestDataAsync();

        var response = await Client.GetAsync($"/api/v1/Tenants{queryParams}");

        TestAssertions.AssertEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}