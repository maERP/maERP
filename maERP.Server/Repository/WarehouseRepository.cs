using AutoMapper;
using maERP.Server.Contracts;
using maERP.Server.Services;
using maERP.Shared.Models.Database;

namespace maERP.Server.Repository;

public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}