using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Tenant.Queries.TenantDetail;

public class TenantDetailQuery : IRequest<Result<TenantDetailDto>>
{
    public int Id { get; set; }

    public TenantDetailQuery(int id)
    {
        Id = id;
    }
}