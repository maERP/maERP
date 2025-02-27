using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;

public class TaxClassUpdateCommand : TaxClassUpdateDto, IRequest<Result<int>>
{
}