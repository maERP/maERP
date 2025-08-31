using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;

namespace maERP.Persistence.Repositories;

public class ManufacturerRepository : GenericRepository<Manufacturer>, IManufacturerRepository
{
    public ManufacturerRepository(ApplicationDbContext context, ITenantContext tenantContext) : base(context, tenantContext)
    {
    }
}