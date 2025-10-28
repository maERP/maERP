using maERP.Application.Features.TaxClass.Commands.TaxClassCreate;
using maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of tax classes API client
/// </summary>
public class TaxClassesApiClient : ApiClientBase, ITaxClassesApiClient
{
    private const string BaseEndpoint = "api/v1/TaxClasses";

    public TaxClassesApiClient(HttpClient httpClient, ILogger<TaxClassesApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<PaginatedResult<TaxClassListDto>?> GetAllAsync(
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
        return await GetAsync<PaginatedResult<TaxClassListDto>>(url, cancellationToken);
    }

    public async Task<TaxClassDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<TaxClassDetailDto>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateAsync(
        TaxClassCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync(BaseEndpoint, command, cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateAsync(
        Guid id,
        TaxClassUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/{id}", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"{BaseEndpoint}/{id}", cancellationToken);
    }
}
