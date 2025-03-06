using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderCreate;

public class OrderCreateCommand : OrderInputDto, IRequest<Result<int>>
{
}