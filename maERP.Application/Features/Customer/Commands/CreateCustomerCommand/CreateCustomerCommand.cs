using MediatR;

namespace maERP.Application.Features.Customer.Commands.CreateCustomerCommand;

public class CreateCustomerCommand : IRequest<int>
{
    public double TaxRate { get; set; }  
}