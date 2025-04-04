using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Queries.OrderDetail;

/// <summary>
/// Query for retrieving detailed information about a specific order.
/// Implements IRequest to work with MediatR, returning order details wrapped in a Result.
/// </summary>
public class OrderDetailQuery : IRequest<Result<OrderDetailDto>>
{
    /// <summary>
    /// The unique identifier of the order to retrieve
    /// </summary>
    public int Id { get; set; }
}