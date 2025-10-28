using maERP.Application.Features.Manufacturer.Commands.ManufacturerCreate;
using maERP.Application.Features.Manufacturer.Commands.ManufacturerUpdate;
using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of manufacturers API client
/// </summary>
public class ManufacturersApiClient : ApiClientBase, IManufacturersApiClient
{
    private const string BaseEndpoint = "api/v1/Manufacturers";

    public ManufacturersApiClient(HttpClient httpClient, ILogger<ManufacturersApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<PaginatedResult<ManufacturerListDto>?> GetAllAsync(
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
        return await GetAsync<PaginatedResult<ManufacturerListDto>>(url, cancellationToken);
    }

    public async Task<ManufacturerDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<ManufacturerDetailDto>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateAsync(
        ManufacturerCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync(BaseEndpoint, command, cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateAsync(
        Guid id,
        ManufacturerUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/{id}", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"{BaseEndpoint}/{id}", cancellationToken);
    }
}
