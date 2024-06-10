using MediatR;

namespace maERP.Application.Features.Order.Queries.OrderDetail;

public class OrderDetailQuery : IRequest<OrderDetailResponse>
{
    public int Id { get; set; }
}