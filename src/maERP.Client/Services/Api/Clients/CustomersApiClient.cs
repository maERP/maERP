using maERP.Application.Features.Customer.Commands.CustomerCreate;
using maERP.Application.Features.Customer.Commands.CustomerUpdate;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of customers API client
/// </summary>
public class CustomersApiClient : ApiClientBase, ICustomersApiClient
{
    private const string BaseEndpoint = "api/v1/Customers";

    public CustomersApiClient(HttpClient httpClient, ILogger<CustomersApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<PaginatedResult<CustomerListDto>?> GetAllAsync(
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
        return await GetAsync<PaginatedResult<CustomerListDto>>(url, cancellationToken);
    }

    public async Task<CustomerDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<CustomerDetailDto>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    public async Task<CustomerDetailDto?> CreateAsync(
        CustomerCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync<CustomerCreateCommand, CustomerDetailDto>(
            BaseEndpoint,
            command,
            cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateAsync(
        Guid id,
        CustomerUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/{id}", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"{BaseEndpoint}/{id}", cancellationToken);
    }
}
