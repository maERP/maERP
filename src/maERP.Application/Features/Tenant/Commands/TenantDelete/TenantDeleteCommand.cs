using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Tenant.Commands.TenantDelete;

public class TenantDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public TenantDeleteCommand(Guid id)
    {
        Id = id;
    }
}