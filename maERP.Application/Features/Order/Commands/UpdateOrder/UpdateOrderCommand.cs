using MediatR;

namespace maERP.Application.Features.Order.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest<int>
{
    public int Id { get; set; }
}