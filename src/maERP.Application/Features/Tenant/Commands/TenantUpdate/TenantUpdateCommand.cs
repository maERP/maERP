using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Tenant.Commands.TenantUpdate;

public class TenantUpdateCommand : TenantInputDto, IRequest<Result<Guid>>
{
    public Guid TenantId { get; set; }
    public string UserId { get; set; } = string.Empty;
}
