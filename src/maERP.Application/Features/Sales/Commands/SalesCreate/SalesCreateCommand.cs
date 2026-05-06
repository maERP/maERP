using maERP.Domain.Dtos.Sales;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Sales.Commands.SalesCreate;

/// <summary>
/// Command for creating a new sales in the system.
/// Inherits from SalesInputDto to get all sales properties and implements IRequest
/// to work with MediatR, returning the ID of the newly created sales wrapped in a Result.
/// </summary>
public class SalesCreateCommand : SalesInputDto, IRequest<Result<Guid>>
{
}