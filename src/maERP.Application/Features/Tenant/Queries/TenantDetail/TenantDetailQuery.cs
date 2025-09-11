using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Tenant.Queries.TenantDetail;

public class TenantDetailQuery : IRequest<Result<TenantDetailDto>>
{
    public Guid Id { get; set; }

    public TenantDetailQuery(Guid id)
    {
        Id = id;
    }
}