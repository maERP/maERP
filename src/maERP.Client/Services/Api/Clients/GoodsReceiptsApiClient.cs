using maERP.Application.Features.GoodsReceipt.Commands.GoodsReceiptCreate;
using maERP.Domain.Dtos.GoodsReceipt;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of goods receipts API client
/// </summary>
public class GoodsReceiptsApiClient : ApiClientBase, IGoodsReceiptsApiClient
{
    private const string BaseEndpoint = "api/v1/GoodsReceipts";

    public GoodsReceiptsApiClient(HttpClient httpClient, ILogger<GoodsReceiptsApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<PaginatedResult<GoodsReceiptListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 50,
        string searchTerm = "",
        string orderBy = "",
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "pageNumber", pageNumber.ToString() },
            { "pageSize", pageSize.ToString() },
            { "searchTerm", searchTerm },
            { "orderBy", orderBy }
        };

        var url = BuildUrl(BaseEndpoint, queryParams);
        return await GetAsync<PaginatedResult<GoodsReceiptListDto>>(url, cancellationToken);
    }

    public async Task<GoodsReceiptDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<GoodsReceiptDetailDto>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateAsync(
        GoodsReceiptCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync(BaseEndpoint, command, cancellationToken);
    }
}
