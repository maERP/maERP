using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Order.Commands.OrderDelete;

public class DeleteOrderCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
}