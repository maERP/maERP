using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Tenant.Commands.TenantUpdate;

public class TenantUpdateCommand : TenantInputDto, IRequest<Result<int>>
{
    public int Id { get; set; }
}