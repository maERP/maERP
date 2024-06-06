using MediatR;

namespace maERP.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<int>
{
    public int Id { get; set; }  
}