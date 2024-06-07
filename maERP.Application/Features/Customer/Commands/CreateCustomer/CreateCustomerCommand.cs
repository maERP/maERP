using MediatR;

namespace maERP.Application.Features.Customer.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<int>
{
    public string Firstname { get; set; } = string.Empty;  
    public string Lastname { get; set; } = string.Empty;
}