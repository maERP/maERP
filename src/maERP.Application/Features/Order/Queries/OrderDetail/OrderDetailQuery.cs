using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Queries.OrderDetail;

public class OrderDetailQuery : IRequest<Result<OrderDetailDto>>
{
    public int Id { get; set; }
}