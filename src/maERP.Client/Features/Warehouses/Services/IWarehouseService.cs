using maERP.Client.Core.Models;
using maERP.Domain.Dtos.Warehouse;

namespace maERP.Client.Features.Warehouses.Services;

/// <summary>
/// Service interface for warehouse-related API operations.
/// </summary>
public interface IWarehouseService
{
    /// <summary>
    /// Gets a paginated list of warehouses with full pagination metadata.
    /// </summary>
    Task<PaginatedResponse<WarehouseListDto>> GetWarehousesAsync(
        QueryParameters parameters,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a single warehouse by ID.
    /// </summary>
    Task<WarehouseDetailDto?> GetWarehouseAsync(Guid id, CancellationToken ct = default);
}
