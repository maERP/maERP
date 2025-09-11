using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Order.Commands.OrderDelete;

public class DeleteOrderCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}