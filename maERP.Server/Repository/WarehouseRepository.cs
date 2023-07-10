using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using maERP.Shared.Dtos.Warehouse;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public interface IWarehouseRepository : IGenericRepository<Warehouse>
{
    
}

public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
    private readonly ApplicationDbContext _context;

    public WarehouseRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
    }
}