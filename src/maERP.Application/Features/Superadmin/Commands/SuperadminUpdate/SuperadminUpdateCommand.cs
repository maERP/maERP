using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Commands.SuperadminUpdate;

public class SuperadminUpdateCommand : TenantInputDto, IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}