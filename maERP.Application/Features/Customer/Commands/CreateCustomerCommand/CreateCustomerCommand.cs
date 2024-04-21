using MediatR;

namespace maERP.Application.Features.Customer.Commands.CreateCustomerCommand;

public class CreateCustomerCommand : IRequest<int>
{
    public int CustomerId { get; set; }
    public string Forename { get; set; } = string.Empty;  
    public string Lastname { get; set; } = string.Empty;
}