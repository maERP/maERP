﻿using maERP.Application.Contracts.Persistence;
using maERP.Domain;
using maERP.Persistence.DatabaseContext;

namespace maERP.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}