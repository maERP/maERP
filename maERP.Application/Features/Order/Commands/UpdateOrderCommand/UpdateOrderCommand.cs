using MediatR;

namespace maERP.Application.Features.Order.Commands.UpdateOrderCommand;

public class UpdateOrderCommand : IRequest<int>
{
    public int Id { get; set; }     
    public double TaxRate { get; set; }
}