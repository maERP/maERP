using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Commands.SuperadminCreate;

public class SuperadminCreateCommand : TenantInputDto, IRequest<Result<Guid>>
{
}