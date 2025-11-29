using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Country.Commands.CountryDelete;

public class CountryDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}
