using AutoMapper;
using maERP.Application.Contracts.Persistence;
using maERP.Domain;
using maERP.Persistence.DatabaseContext;

namespace maERP.Persistence.Repositories;

public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}