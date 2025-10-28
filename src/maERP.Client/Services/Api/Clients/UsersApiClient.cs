using maERP.Application.Features.Superadmin.Users.Commands.UserCreate;
using maERP.Application.Features.Superadmin.Users.Commands.UserUpdate;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of users API client
/// </summary>
public class UsersApiClient : ApiClientBase, IUsersApiClient
{
    private const string BaseEndpoint = "api/v1/Users";

    public UsersApiClient(HttpClient httpClient, ILogger<UsersApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<PaginatedResult<UserListDto>?> GetAllAsync(
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

        var url = BuildUrl(BaseEndpoint, queryParams);
        return await GetAsync<PaginatedResult<UserListDto>>(url, cancellationToken);
    }

    public async Task<UserDetailDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<UserDetailDto>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateAsync(
        UserCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync(BaseEndpoint, command, cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateAsync(
        string id,
        UserUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/{id}", command, cancellationToken);
    }

    public new async Task<HttpResponseMessage> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        return await base.DeleteAsync($"{BaseEndpoint}/{id}", cancellationToken);
    }
}
