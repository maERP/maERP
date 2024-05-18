﻿using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
using maERP.Persistence.DatabaseContext;

namespace maERP.Persistence.Repositories;

public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    /*
    public bool IsUnique(string name)
    {
        return !_context.Warehouse.Any(c => c.Name == name);
    }
    */
}