using maERP.Client.Core.Models;
using maERP.Domain.Dtos.Order;

namespace maERP.Client.Features.Orders.Services;

/// <summary>
/// Service interface for order-related API operations.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Gets a paginated list of orders with full pagination metadata.
    /// </summary>
    Task<PaginatedResponse<OrderListDto>> GetOrdersAsync(
        QueryParameters parameters,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a single order by ID.
    /// </summary>
    Task<OrderDetailDto?> GetOrderAsync(Guid id, CancellationToken ct = default);
}
