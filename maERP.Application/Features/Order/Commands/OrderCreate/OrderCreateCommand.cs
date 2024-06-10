using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderCreate;

public class OrderCreateCommand : IRequest<int>
{
    public int Id { get; set; }  
}