using maERP.Application.Dtos.Order;
using MediatR;

namespace maERP.Application.Features.Order.Queries.GetOrderDetailQuery;

public class GetOrderDetailQuery : IRequest<OrderDetailDto>
{
    public int Id { get; set; }
}