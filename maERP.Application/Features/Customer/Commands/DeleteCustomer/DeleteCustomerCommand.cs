using MediatR;

namespace maERP.Application.Features.Customer.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<int>
{
    public int Id { get; set; }     
}