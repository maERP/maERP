using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderDelete;

public class DeleteOrderCommand : IRequest<int>
{
    public int Id { get; set; }     
}