using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassCreate;

public class TaxClassCreateCommand : TaxClassCreateDto, IRequest<Result<int>>
{
}