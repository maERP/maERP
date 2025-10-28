using maERP.Application.Features.Superadmin.Commands.SuperadminCreate;
using maERP.Application.Features.Superadmin.Commands.SuperadminUpdate;
using maERP.Application.Features.Superadmin.Users.Commands.UserCreate;
using maERP.Application.Features.Superadmin.Users.Commands.UserUpdate;
using maERP.Application.Features.Superadmin.UserTenants.Commands.AssignUserToTenant;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of superadmin API client
/// </summary>
public class SuperadminApiClient : ApiClientBase, ISuperadminApiClient
{
    private const string BaseEndpoint = "api/v1/superadmin";

    public SuperadminApiClient(HttpClient httpClient, ILogger<SuperadminApiClient> logger)
        : base(httpClient, logger)
    {
    }

    // Tenant operations

    public async Task<PaginatedResult<TenantListDto>?> GetTenantsAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "pageNumber", pageNumber.ToString() },
            { "pageSize", pageSize.ToString() },
            { "searchString", searchString },
            { "orderBy", orderBy }
        };

        var url = BuildUrl($"{BaseEndpoint}/tenants", queryParams);
        return await GetAsync<PaginatedResult<TenantListDto>>(url, cancellationToken);
    }

    public async Task<TenantDetailDto?> GetTenantByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<TenantDetailDto>($"{BaseEndpoint}/tenants/{id}", cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateTenantAsync(
        SuperadminCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync($"{BaseEndpoint}/tenants", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateTenantAsync(
        Guid id,
        SuperadminUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/tenants/{id}", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteTenantAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"{BaseEndpoint}/tenants/{id}", cancellationToken);
    }

    // User operations

    public async Task<PaginatedResult<UserListDto>?> GetUsersAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "pageNumber", pageNumber.ToString() },
            { "pageSize", pageSize.ToString() },
            { "searchString", searchString },
            { "orderBy", orderBy }
        };

        var url = BuildUrl($"{BaseEndpoint}/users", queryParams);
        return await GetAsync<PaginatedResult<UserListDto>>(url, cancellationToken);
    }

    public async Task<UserDetailDto?> GetUserByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<UserDetailDto>($"{BaseEndpoint}/users/{id}", cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateUserAsync(
        UserCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync($"{BaseEndpoint}/users", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateUserAsync(
        string id,
        UserUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/users/{id}", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteUserAsync(string id, CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"{BaseEndpoint}/users/{id}", cancellationToken);
    }

    // User-Tenant assignment operations

    public async Task<Result<List<UserTenantAssignmentDto>>?> GetUserTenantsAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        return await GetAsync<Result<List<UserTenantAssignmentDto>>>($"{BaseEndpoint}/users/{userId}/tenants", cancellationToken);
    }

    public async Task<HttpResponseMessage> AssignUserToTenantAsync(
        string userId,
        AssignUserToTenantCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync($"{BaseEndpoint}/users/{userId}/tenants", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> RemoveUserFromTenantAsync(
        string userId,
        Guid tenantId,
        CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"{BaseEndpoint}/users/{userId}/tenants/{tenantId}", cancellationToken);
    }
}
