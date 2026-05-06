using maERP.Client.Core.Models;
using maERP.Domain.Dtos.Sales;

namespace maERP.Client.Features.Saless.Services;

/// <summary>
/// Service interface for sales-related API operations.
/// </summary>
public interface ISalesService
{
    /// <summary>
    /// Gets a paginated list of saless with full pagination metadata.
    /// </summary>
    Task<PaginatedResponse<SalesListDto>> GetSalessAsync(
        QueryParameters parameters,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a single sales by ID.
    /// </summary>
    Task<SalesDetailDto?> GetSalesAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Creates a new sales.
    /// </summary>
    Task CreateSalesAsync(SalesInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing sales.
    /// </summary>
    Task UpdateSalesAsync(Guid id, SalesInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Deletes an sales.
    /// </summary>
    Task DeleteSalesAsync(Guid id, CancellationToken ct = default);
}
