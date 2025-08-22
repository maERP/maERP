using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.Tenant.Commands.TenantCreate;
using maERP.Application.Features.Tenant.Commands.TenantUpdate;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class TenantCrudTest : IClassFixture<MaErpWebApplicationFactory<Program>>
{
    private readonly MaErpWebApplicationFactory<Program> _webApplicationFactory;

    public TenantCrudTest(MaErpWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    private string GetSuperadminToken()
    {
        // Generate JWT token directly without relying on Identity services
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "superadmin-test-id"),
            new Claim(ClaimTypes.Name, "superadmin@test.com"),
            new Claim(ClaimTypes.Email, "superadmin@test.com"),
            new Claim(ClaimTypes.Role, "Superadmin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKeyForJWTTokenGeneration123456789"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "maERP",
            audience: "maERP",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [Theory]
    [InlineData("/api/v1/Tenants")]
    public async Task SuperadminCanCreateTenant(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        
        var token = GetSuperadminToken();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        await _webApplicationFactory.InitializeDbForTests();
        
        var tenant = new TenantCreateCommand()
        {
            Name = "Test Tenant",
            Description = "Test tenant description"
        };

        HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, tenant);
        var result = await response.Content.ReadFromJsonAsync<Result<int>>();

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Created, result.StatusCode);
        Assert.IsType<int>(result.Data);
    }

    [Theory]
    [InlineData("/api/v1/Tenants/")]
    public async Task SuperadminCanGetAllTenants(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        
        var token = GetSuperadminToken();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        await _webApplicationFactory.InitializeDbForTests(
            new List<Tenant> {
                new() {
                    Id = 1,
                    Name = "Test Tenant 1",
                    Description = "First test tenant",
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                },
                new() {
                    Id = 2,
                    Name = "Test Tenant 2", 
                    Description = "Second test tenant",
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                }
            }
        );

        HttpResponseMessage response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<PaginatedResult<TenantListDto>>();

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.NotEmpty(result.Data);
        Assert.Equal(2, result.Data.Count);
    }

    [Theory]
    [InlineData("/api/v1/Tenants")]
    public async Task NonSuperadminCannotAccessTenants(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();

        var tenant = new TenantCreateCommand()
        {
            Name = "Test Tenant",
            Description = "Test tenant description"
        };

        // Test without authentication
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, tenant);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

        // Test with regular user token (if we had one - for now just test unauthorized)
        response = await httpClient.GetAsync(url);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/Tenants/1")]
    public async Task SuperadminCanUpdateTenant(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        
        var token = GetSuperadminToken();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        await _webApplicationFactory.InitializeDbForTests(
            new List<Tenant> {
                new() {
                    Id = 1,
                    Name = "Original Tenant",
                    Description = "Original description",
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                }
            }
        );

        var updateCommand = new TenantUpdateCommand()
        {
            Id = 1,
            Name = "Updated Tenant",
            Description = "Updated description"
        };

        HttpResponseMessage response = await httpClient.PutAsJsonAsync(url, updateCommand);
        var result = await response.Content.ReadFromJsonAsync<Result<int>>();

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/Tenants/1")]
    public async Task SuperadminCanDeleteTenant(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        
        var token = GetSuperadminToken();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        await _webApplicationFactory.InitializeDbForTests(
            new List<Tenant> {
                new() {
                    Id = 1,
                    Name = "Tenant to Delete",
                    Description = "This tenant will be deleted",
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                }
            }
        );

        HttpResponseMessage response = await httpClient.DeleteAsync(url);
        
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}