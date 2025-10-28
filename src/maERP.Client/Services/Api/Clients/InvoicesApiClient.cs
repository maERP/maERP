using maERP.Application.Features.Invoice.Commands.InvoiceCreate;
using maERP.Application.Features.Invoice.Commands.InvoiceUpdate;
using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of invoices API client
/// </summary>
public class InvoicesApiClient : ApiClientBase, IInvoicesApiClient
{
    private const string BaseEndpoint = "api/v1/Invoices";

    public InvoicesApiClient(HttpClient httpClient, ILogger<InvoicesApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<PaginatedResult<InvoiceListDto>?> GetAllAsync(
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
        return await GetAsync<PaginatedResult<InvoiceListDto>>(url, cancellationToken);
    }

    public async Task<InvoiceDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<InvoiceDetailDto>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    public async Task<HttpResponseMessage> GetPdfAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetResponseAsync($"{BaseEndpoint}/{id}/pdf", cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateAsync(
        InvoiceCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync(BaseEndpoint, command, cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateAsync(
        Guid id,
        InvoiceUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/{id}", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"{BaseEndpoint}/{id}", cancellationToken);
    }
}
