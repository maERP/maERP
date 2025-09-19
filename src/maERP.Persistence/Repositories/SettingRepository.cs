using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;

namespace maERP.Persistence.Repositories;

public class SettingRepository : GenericRepository<Setting>, ISettingRepository
{
    public SettingRepository(ApplicationDbContext context, ITenantContext tenantContext) : base(context, tenantContext)
    {
    }
}