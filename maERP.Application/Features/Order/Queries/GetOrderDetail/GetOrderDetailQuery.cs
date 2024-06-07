using MediatR;

namespace maERP.Application.Features.Order.Queries.GetOrderDetail;

public class GetOrderDetailQuery : IRequest<GetOrderDetailResponse>
{
    public int Id { get; set; }
}