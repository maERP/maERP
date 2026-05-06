using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public sealed class TenantOAuthAppSettingsRepository : ITenantOAuthAppSettingsRepository
{
    private readonly ApplicationDbContext _context;

    public TenantOAuthAppSettingsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<TenantOAuthAppSettings>> GetByTenantIdAsync(Guid tenantId)
    {
        return _context.TenantOAuthAppSettings
            .IgnoreQueryFilters()
            .Where(s => s.TenantId == tenantId)
            .OrderBy(s => s.Provider)
            .ToListAsync();
    }

    public Task<TenantOAuthAppSettings?> GetByTenantAndProviderAsync(Guid tenantId, SalesChannelType provider)
    {
        return _context.TenantOAuthAppSettings
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(s => s.TenantId == tenantId && s.Provider == provider);
    }

    public async Task<Guid> CreateAsync(TenantOAuthAppSettings entity)
    {
        await _context.TenantOAuthAppSettings.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(TenantOAuthAppSettings entity)
    {
        entity.DateModified = DateTime.UtcNow;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TenantOAuthAppSettings entity)
    {
        _context.TenantOAuthAppSettings.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
