using maERP.Domain.Dtos.Country;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Country.Commands.CountryUpdate;

public class CountryUpdateCommand : CountryInputDto, IRequest<Result<Guid>>
{
}
