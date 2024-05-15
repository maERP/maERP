using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {

    }
    
    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _context.Product.Include(ps => ps.ProductSalesChannel).FirstOrDefaultAsync(p => p.Sku == sku);
    }
}