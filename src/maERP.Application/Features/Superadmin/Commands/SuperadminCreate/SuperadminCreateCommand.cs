using maERP.Domain.Dtos.Superadmin;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Commands.SuperadminCreate;

public class SuperadminCreateCommand : SuperadminTenantInputDto, IRequest<Result<Guid>>
{
}
