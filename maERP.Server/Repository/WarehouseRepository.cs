using AutoMapper;
using maERP.Server.Models;

namespace maERP.Server.Repository;

public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}