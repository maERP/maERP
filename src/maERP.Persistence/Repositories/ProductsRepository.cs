using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context, ITenantContext tenantContext) : base(context, tenantContext)
    {

    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        var query = Context.Product.Where(p => p.Sku == sku);

        // Apply manual tenant filtering
        var currentTenantId = TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue)
        {
            query = query.Where(x => x.TenantId == null || x.TenantId == currentTenantId.Value);
        }

        return await query.Include(ps => ps.ProductSalesChannels).FirstOrDefaultAsync();
    }

    public async Task<Product?> GetWithDetailsAsync(Guid id)
    {
        var query = Context.Product.Where(p => p.Id == id);

        // Apply manual tenant filtering
        var currentTenantId = TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue)
        {
            query = query.Where(x => x.TenantId == null || x.TenantId == currentTenantId.Value);
        }

        return await query
            .Include(ps => ps.ProductSalesChannels)
            .Include(ps => ps.ProductStocks)
            .Include(ps => ps.Manufacturer)
            .AsSplitQuery()
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateStockAsync(Guid productId, Guid warehouseId, int newStock)
    {
        var productStock = await Context.ProductStock.FirstOrDefaultAsync(ps => ps.ProductId == productId && ps.WarehouseId == warehouseId);

        if (productStock == null)
        {
            return false;
        }

        productStock.Stock = newStock;
        await Context.SaveChangesAsync();

        return true;
    }

    public override async Task<bool> IsUniqueAsync(Product entity, Guid? id = null)
    {
        var currentTenantId = TenantContext.GetCurrentTenantId();

        var query = Context.Product.AsQueryable();

        // Add tenant isolation
        if (currentTenantId.HasValue)
        {
            query = query.Where(p => p.TenantId == currentTenantId.Value);
        }

        // Check for duplicate SKU
        query = query.Where(p => p.Sku == entity.Sku);

        // Exclude entity with provided id (for updates)
        if (id.HasValue)
        {
            query = query.Where(p => p.Id != id.Value);
        }

        var exists = await query.AnyAsync();
        return !exists;
    }
}