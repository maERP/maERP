using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of tenants API client
/// </summary>
public class TenantsApiClient : ApiClientBase, ITenantsApiClient
{
    private const string BaseEndpoint = "api/v1/Tenants";

    public TenantsApiClient(HttpClient httpClient, ILogger<TenantsApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<PaginatedResult<TenantListDto>?> GetAllAsync(
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
        return await GetAsync<PaginatedResult<TenantListDto>>(url, cancellationToken);
    }

    public async Task<TenantDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<TenantDetailDto>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    public async Task<TenantDetailDto?> CreateAsync(
        TenantInputDto input,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync<TenantInputDto, TenantDetailDto>(
            BaseEndpoint,
            input,
            cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateAsync(
        Guid id,
        TenantInputDto input,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/{id}", input, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"{BaseEndpoint}/{id}", cancellationToken);
    }
}
