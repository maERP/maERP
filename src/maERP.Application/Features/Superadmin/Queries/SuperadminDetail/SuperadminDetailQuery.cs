using maERP.Domain.Dtos.Superadmin;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Superadmin.Queries.SuperadminDetail;

public class SuperadminDetailQuery : IRequest<Result<SuperadminTenantDetailDto>>
{
    public Guid Id { get; set; }

    public SuperadminDetailQuery(Guid id)
    {
        Id = id;
    }
}