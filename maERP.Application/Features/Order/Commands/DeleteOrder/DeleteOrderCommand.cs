using MediatR;

namespace maERP.Application.Features.Order.Commands.DeleteOrder;

public class DeleteOrderCommand : IRequest<int>
{
    public int Id { get; set; }     
}