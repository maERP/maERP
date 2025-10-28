using maERP.Application.Features.Order.Commands.OrderCreate;
using maERP.Application.Features.Order.Commands.OrderUpdate;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for order operations
/// </summary>
public interface IOrdersApiClient
{
    /// <summary>
    /// Get paginated list of orders
    /// </summary>
    Task<PaginatedResult<OrderListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get paginated list of orders by customer
    /// </summary>
    Task<PaginatedResult<OrderListDto>?> GetByCustomerAsync(
        int customerId,
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get paginated list of orders ready for delivery
    /// </summary>
    Task<PaginatedResult<OrderListDto>?> GetReadyForDeliveryAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get paginated list of orders not paid
    /// </summary>
    Task<PaginatedResult<OrderListDto>?> GetNotPaidAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get order details by ID
    /// </summary>
    Task<OrderDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new order
    /// </summary>
    Task<HttpResponseMessage> CreateAsync(OrderCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing order
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, OrderUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an order
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
