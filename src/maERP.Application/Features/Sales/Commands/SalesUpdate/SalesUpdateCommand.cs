using maERP.Domain.Dtos.Sales;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Sales.Commands.SalesUpdate;

public class SalesUpdateCommand : SalesInputDto, IRequest<Result<Guid>>
{
}