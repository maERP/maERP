using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;

namespace maERP.Persistence.Repositories;

public class AiModelRepository : GenericRepository<AiModel>, IAiModelRepository
{
    public AiModelRepository(ApplicationDbContext context, ITenantContext tenantContext) : base(context, tenantContext)
    {

    }
}