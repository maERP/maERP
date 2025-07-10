using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Order.Commands.OrderCreate;

/// <summary>
/// Command for creating a new order in the system.
/// Inherits from OrderInputDto to get all order properties and implements IRequest
/// to work with MediatR, returning the ID of the newly created order wrapped in a Result.
/// </summary>
public class OrderCreateCommand : OrderInputDto, IRequest<Result<int>>
{
}