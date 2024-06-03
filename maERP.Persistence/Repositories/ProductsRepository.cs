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
        return await _context.Product.Include(ps => ps.ProductSalesChannels).FirstOrDefaultAsync(p => p.Sku == sku);
    }

    public async Task<Product?> GetWithDetailsAsync(int id)
    {
        return await _context.Product.Include(ps => ps.ProductSalesChannels).Include(ps => ps.ProductStocks).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> UpdateStockAsync(int productId, int warehouseId, int newStock)
    {
        var productStock = await _context.ProductStock.FirstOrDefaultAsync(ps => ps.ProductId == productId && ps.WarehouseId == warehouseId);
        
        if (productStock == null)
        {
            return false;
        }

        productStock.Stock = newStock;
        await _context.SaveChangesAsync();

        return true;
    }
}