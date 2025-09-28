using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Commands.SuperadminDelete;

public class SuperadminDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public SuperadminDeleteCommand(Guid id)
    {
        Id = id;
    }
}