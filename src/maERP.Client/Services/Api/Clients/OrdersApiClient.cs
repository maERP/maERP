using maERP.Application.Features.Order.Commands.OrderCreate;
using maERP.Application.Features.Order.Commands.OrderUpdate;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of orders API client
/// </summary>
public class OrdersApiClient : ApiClientBase, IOrdersApiClient
{
    private const string BaseEndpoint = "api/v1/Orders";

    public OrdersApiClient(HttpClient httpClient, ILogger<OrdersApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<PaginatedResult<OrderListDto>?> GetAllAsync(
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
        return await GetAsync<PaginatedResult<OrderListDto>>(url, cancellationToken);
    }

    public async Task<PaginatedResult<OrderListDto>?> GetByCustomerAsync(
        int customerId,
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

        var url = BuildUrl($"{BaseEndpoint}/customer/{customerId}", queryParams);
        return await GetAsync<PaginatedResult<OrderListDto>>(url, cancellationToken);
    }

    public async Task<PaginatedResult<OrderListDto>?> GetReadyForDeliveryAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string orderBy = "",
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "pageNumber", pageNumber.ToString() },
            { "pageSize", pageSize.ToString() },
            { "orderBy", orderBy }
        };

        var url = BuildUrl($"{BaseEndpoint}/ready-for-delivery", queryParams);
        return await GetAsync<PaginatedResult<OrderListDto>>(url, cancellationToken);
    }

    public async Task<PaginatedResult<OrderListDto>?> GetNotPaidAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string orderBy = "",
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "pageNumber", pageNumber.ToString() },
            { "pageSize", pageSize.ToString() },
            { "orderBy", orderBy }
        };

        var url = BuildUrl($"{BaseEndpoint}/not-paid", queryParams);
        return await GetAsync<PaginatedResult<OrderListDto>>(url, cancellationToken);
    }

    public async Task<OrderDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<OrderDetailDto>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateAsync(
        OrderCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync(BaseEndpoint, command, cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateAsync(
        Guid id,
        OrderUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/{id}", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"{BaseEndpoint}/{id}", cancellationToken);
    }
}
