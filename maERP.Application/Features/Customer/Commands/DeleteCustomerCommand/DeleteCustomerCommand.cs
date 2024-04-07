using MediatR;

namespace maERP.Application.Features.Customer.Commands.DeleteCustomerCommand;

public class DeleteCustomerCommand : IRequest<int>
{
    public int Id { get; set; }     
}