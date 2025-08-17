using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Tenant.Commands.TenantCreate;

public class TenantCreateCommand : TenantInputDto, IRequest<Result<int>>
{
}