using maERP.Domain.Dtos.Order;
using MediatR;

namespace maERP.Application.Features.Order.Queries.OrderDetail;

public class OrderDetailQuery : IRequest<OrderDetailDto>
{
    public int Id { get; set; }
}