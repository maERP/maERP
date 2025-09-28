using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Queries.SuperadminDetail;

public class SuperadminDetailQuery : IRequest<Result<TenantDetailDto>>
{
    public Guid Id { get; set; }

    public SuperadminDetailQuery(Guid id)
    {
        Id = id;
    }
}