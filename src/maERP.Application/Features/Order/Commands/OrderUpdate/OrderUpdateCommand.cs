using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Order.Commands.OrderUpdate;

public class OrderUpdateCommand : OrderInputDto, IRequest<Result<int>>
{
}