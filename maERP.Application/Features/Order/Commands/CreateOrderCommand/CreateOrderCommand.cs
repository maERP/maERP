using MediatR;

namespace maERP.Application.Features.Order.Commands.CreateOrderCommand;

public class CreateOrderCommand : IRequest<int>
{
    public double TaxRate { get; set; }  
}