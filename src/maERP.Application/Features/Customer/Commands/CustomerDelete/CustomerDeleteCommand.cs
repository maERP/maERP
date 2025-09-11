using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Customer.Commands.CustomerDelete;

public class CustomerDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}