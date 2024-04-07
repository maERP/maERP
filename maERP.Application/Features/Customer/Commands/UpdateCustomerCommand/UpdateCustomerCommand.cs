using MediatR;

namespace maERP.Application.Features.Customer.Commands.UpdateCustomerCommand;

public class UpdateCustomerCommand : IRequest<int>
{
    public int Id { get; set; }     
    public double TaxRate { get; set; }
}