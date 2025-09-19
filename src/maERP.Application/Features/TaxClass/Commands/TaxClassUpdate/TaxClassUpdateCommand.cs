using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;

public class TaxClassUpdateCommand : TaxClassInputDto, IRequest<Result<Guid>>
{
}