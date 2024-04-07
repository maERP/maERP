using MediatR;

namespace maERP.Application.Features.Order.Commands.DeleteOrderCommand;

public class DeleteOrderCommand : IRequest<int>
{
    public int Id { get; set; }     
}