using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerDelete;

public class CustomerDeleteCommand : IRequest<int>
{
    public int Id { get; set; }     
}