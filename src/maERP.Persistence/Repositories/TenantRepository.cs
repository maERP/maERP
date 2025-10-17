using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

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
}