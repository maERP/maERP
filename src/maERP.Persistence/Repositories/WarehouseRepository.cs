using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;

namespace maERP.Persistence.Repositories;

public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(ApplicationDbContext context, ITenantContext tenantContext) : base(context, tenantContext)
    {
    }

    /*
    public bool IsUnique(string name)
    {
        return !_context.Warehouse.Any(c => c.Name == name);
    }
    */
}