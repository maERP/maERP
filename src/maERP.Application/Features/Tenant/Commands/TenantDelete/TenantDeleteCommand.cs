using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Tenant.Commands.TenantDelete;

public class TenantDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    public TenantDeleteCommand(int id)
    {
        Id = id;
    }
}