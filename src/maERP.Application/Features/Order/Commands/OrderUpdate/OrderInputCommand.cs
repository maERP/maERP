using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderUpdate;

public class OrderInputCommand : OrderInputDto, IRequest<Result<int>>
{
}