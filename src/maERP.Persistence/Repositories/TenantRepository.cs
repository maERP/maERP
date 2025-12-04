using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace maERP.Persistence.Repositories;

public class TenantRepository : ITenantRepository
{
    private readonly ApplicationDbContext _context;

    public TenantRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Tenant> Entities => _context.Tenant;

    public async Task<Guid> CreateAsync(Tenant entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(Tenant entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Tenant entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Tenant>> GetAllAsync()
    {
        return await _context.Tenant
            .Include(t => t.UserTenants)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<Tenant?> GetByIdAsync(Guid id, bool asNoTracking = false)
    {
        if (asNoTracking)
        {
            return await _context.Tenant
                .AsNoTracking()
                .Include(t => t.UserTenants)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        return await _context.Tenant
            .Include(t => t.UserTenants)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Tenant.AsNoTracking().AnyAsync(e => e.Id == id);
    }

    public async Task<bool> ExistsGloballyAsync(Guid id)
    {
        return await _context.Tenant.IgnoreQueryFilters().AsNoTracking().AnyAsync(e => e.Id == id);
    }

    public async Task<bool> IsUniqueAsync(Tenant entity, Guid? id = null)
    {
        // Tenants are unique by Name
        var query = _context.Tenant.AsQueryable();

        if (id.HasValue)
        {
            query = query.Where(t => t.Id != id.Value);
        }

        return !await query.AnyAsync(t => t.Name == entity.Name);
    }

    public async Task<IEnumerable<Tenant>> GetActivTenantsAsync()
    {
        return await _context.Tenant
            .Where(t => t.IsActive)
            .Include(t => t.UserTenants)
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public void Attach(Tenant entity)
    {
        _context.Set<Tenant>().Attach(entity);
    }

    public void AttachRange(IEnumerable<Tenant> entities)
    {
        _context.Set<Tenant>().AttachRange(entities);
    }

    public IQueryable<TCt> GetContext<TCt>() where TCt : class => _context.Set<TCt>();

    // Transaction support methods
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Add(Tenant entity)
    {
        _context.Set<Tenant>().Add(entity);
    }

    public async Task DeleteTenantWithCascadeAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            // Delete all tenant-related data in order (respecting foreign key constraints)
            // Using ExecuteDeleteAsync for efficient bulk deletion

            // 1. OrderItemSerialNumber
            await _context.OrderItemSerialNumber
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 2. OrderItem
            await _context.OrderItem
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 3. OrderHistory
            await _context.OrderHistory
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 4. Shipping
            await _context.Shipping
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 5. InvoiceItem
            await _context.InvoiceItem
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 6. Invoice
            await _context.Invoice
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 7. Order
            await _context.Order
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 8. CustomerSalesChannel
            await _context.CustomerSalesChannel
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 9. CustomerAddress
            await _context.CustomerAddress
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 10. Customer
            await _context.Customer
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 11. ProductStock
            await _context.ProductStock
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 12. ProductSalesChannel
            await _context.ProductSalesChannel
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 13. GoodsReceipt
            await _context.GoodsReceipt
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 14. Product
            await _context.Product
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 15. SalesChannel
            await _context.SalesChannel
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 16. Warehouse
            await _context.Warehouse
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 17. TaxClass
            await _context.TaxClass
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 18. ShippingProviderRate
            await _context.ShippingProviderRate
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 19. ShippingProvider
            await _context.ShippingProvider
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 20. AiPrompt
            await _context.AiPrompt
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 21. AiModel
            await _context.AiModel
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 22. Manufacturer
            await _context.Manufacturer
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 23. Country
            await _context.Country
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 24. UserTenant (cascade delete is configured, but delete explicitly for clarity)
            await _context.UserTenant
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            // 25. Tenant (final delete)
            await _context.Tenant
                .IgnoreQueryFilters()
                .Where(x => x.Id == tenantId)
                .ExecuteDeleteAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}