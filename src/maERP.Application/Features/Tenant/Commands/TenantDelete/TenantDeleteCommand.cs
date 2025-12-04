using maERP.Application.Mediator;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.Tenant.Commands.TenantDelete;

public class TenantDeleteCommand : IRequest<Result<Guid>>
{
    public Guid TenantId { get; set; }
    public string UserId { get; set; } = string.Empty;
}
