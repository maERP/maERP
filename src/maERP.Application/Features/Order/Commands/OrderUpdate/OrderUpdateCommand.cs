using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderUpdate;

public class OrderUpdateCommand : OrderUpdateDto, IRequest<Result<int>>
{
}