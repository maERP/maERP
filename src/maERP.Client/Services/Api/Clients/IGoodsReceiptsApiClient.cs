using maERP.Application.Features.GoodsReceipt.Commands.GoodsReceiptCreate;
using maERP.Domain.Dtos.GoodsReceipt;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for goods receipt operations
/// </summary>
public interface IGoodsReceiptsApiClient
{
    /// <summary>
    /// Get paginated list of goods receipts
    /// </summary>
    Task<PaginatedResult<GoodsReceiptListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 50,
        string searchTerm = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get goods receipt details by ID
    /// </summary>
    Task<GoodsReceiptDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new goods receipt
    /// </summary>
    Task<HttpResponseMessage> CreateAsync(GoodsReceiptCreateCommand command, CancellationToken cancellationToken = default);
}
