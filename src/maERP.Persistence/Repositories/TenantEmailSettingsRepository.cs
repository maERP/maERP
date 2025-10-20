using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace maERP.Persistence.Repositories;

public class TenantEmailSettingsRepository : ITenantEmailSettingsRepository
{
    private readonly ApplicationDbContext _context;

    public TenantEmailSettingsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<TenantEmailSettings> Entities => _context.Set<TenantEmailSettings>().AsNoTracking();

    public async Task<TenantEmailSettings?> GetByTenantIdAsync(Guid tenantId)
    {
        return await _context.Set<TenantEmailSettings>()
            .Where(s => s.TenantId == tenantId)
            .FirstOrDefaultAsync();
    }

    public async Task<TenantEmailSettings?> GetActiveTenantSettingsAsync(Guid tenantId)
    {
        return await _context.Set<TenantEmailSettings>()
            .Where(s => s.TenantId == tenantId && s.IsActive)
            .FirstOrDefaultAsync();
    }

    public async Task<Guid> CreateAsync(TenantEmailSettings entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<ICollection<TenantEmailSettings>> GetAllAsync()
    {
        return await _context.Set<TenantEmailSettings>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TenantEmailSettings?> GetByIdAsync(Guid id, bool asNoTracking = false)
    {
        var query = _context.Set<TenantEmailSettings>()
            .Where(x => x.Id == id);

        return asNoTracking
            ? await query.AsNoTracking().FirstOrDefaultAsync()
            : await query.FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(TenantEmailSettings entity)
    {
        entity.DateModified = DateTime.UtcNow;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TenantEmailSettings entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Set<TenantEmailSettings>()
            .AsNoTracking()
            .AnyAsync(e => e.Id == id);
    }

    public async Task<bool> ExistsGloballyAsync(Guid id)
    {
        return await ExistsAsync(id);
    }

    public async Task<bool> IsUniqueAsync(TenantEmailSettings entity, Guid? id = null)
    {
        // Each tenant should only have one active email configuration
        var query = _context.Set<TenantEmailSettings>()
            .Where(s => s.TenantId == entity.TenantId && s.IsActive);

        if (id.HasValue)
        {
            query = query.Where(s => s.Id != id.Value);
        }

        return !await query.AnyAsync();
    }

    public void Attach(TenantEmailSettings entity)
    {
        _context.Set<TenantEmailSettings>().Attach(entity);
    }

    public void AttachRange(IEnumerable<TenantEmailSettings> entities)
    {
        _context.Set<TenantEmailSettings>().AttachRange(entities);
    }

    public IQueryable<TCt> GetContext<TCt>() where TCt : class
    {
        return _context.Set<TCt>();
    }

    // Transaction support methods
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Add(TenantEmailSettings entity)
    {
        _context.Set<TenantEmailSettings>().Add(entity);
    }
}
