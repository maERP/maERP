using maERP.Application.Dtos.Order;
using MediatR;

namespace maERP.Application.Features.Order.Queries.GetOrderDetails;

public class GetOrderDetailsQuery : IRequest<OrderDetailDto>
{
    public int Id { get; set; }
}