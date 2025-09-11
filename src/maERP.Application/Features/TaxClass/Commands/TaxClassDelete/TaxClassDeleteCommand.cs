using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassDelete;

public class TaxClassDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}