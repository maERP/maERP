using maERP.Application.Features.Warehouse.Commands.WarehouseCreate;
using maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for warehouse operations
/// </summary>
public interface IWarehousesApiClient
{
    /// <summary>
    /// Get paginated list of warehouses
    /// </summary>
    Task<PaginatedResult<WarehouseListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get warehouse details by ID
    /// </summary>
    Task<WarehouseDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new warehouse
    /// </summary>
    Task<HttpResponseMessage> CreateAsync(WarehouseCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing warehouse
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, WarehouseUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a warehouse
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, Guid? newWarehouseId = null, CancellationToken cancellationToken = default);
}
