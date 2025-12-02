using maERP.Domain.Dtos.Superadmin;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Commands.SuperadminUpdate;

public class SuperadminUpdateCommand : SuperadminTenantInputDto, IRequest<Result<Guid>>
{
}
