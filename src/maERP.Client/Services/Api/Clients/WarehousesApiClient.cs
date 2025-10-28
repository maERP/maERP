using maERP.Application.Features.Warehouse.Commands.WarehouseCreate;
using maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of warehouses API client
/// </summary>
public class WarehousesApiClient : ApiClientBase, IWarehousesApiClient
{
    private const string BaseEndpoint = "api/v1/Warehouses";

    public WarehousesApiClient(HttpClient httpClient, ILogger<WarehousesApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<PaginatedResult<WarehouseListDto>?> GetAllAsync(
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
        return await GetAsync<PaginatedResult<WarehouseListDto>>(url, cancellationToken);
    }

    public async Task<WarehouseDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<WarehouseDetailDto>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateAsync(
        WarehouseCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync(BaseEndpoint, command, cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateAsync(
        Guid id,
        WarehouseUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/{id}", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id, Guid? newWarehouseId = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>();
        if (newWarehouseId.HasValue)
        {
            queryParams.Add("newWarehouseId", newWarehouseId.Value.ToString());
        }

        var url = BuildUrl($"{BaseEndpoint}/{id}", queryParams);
        return await DeleteAsync(url, cancellationToken);
    }
}
