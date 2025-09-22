using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class SettingRepository : ISettingRepository
{
    private readonly ApplicationDbContext _context;

    public SettingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Setting> Entities => _context.Setting;

    public async Task<Guid> CreateAsync(Setting entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(Setting entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Setting entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Setting>> GetAllAsync()
    {
        return await _context.Setting
            .OrderBy(s => s.Key)
            .ToListAsync();
    }

    public async Task<Setting?> GetByIdAsync(Guid id, bool asNoTracking = false)
    {
        if (asNoTracking)
        {
            return await _context.Setting
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        return await _context.Setting
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Setting.AsNoTracking().AnyAsync(e => e.Id == id);
    }

    public async Task<bool> ExistsGloballyAsync(Guid id)
    {
        return await _context.Setting.IgnoreQueryFilters().AsNoTracking().AnyAsync(e => e.Id == id);
    }

    public async Task<bool> IsUniqueAsync(Setting entity, Guid? id = null)
    {
        var query = _context.Setting.AsQueryable();

        if (id.HasValue)
        {
            query = query.Where(s => s.Id != id.Value);
        }

        return !await query.AnyAsync(s => s.Key == entity.Key);
    }

    public void Attach(Setting entity)
    {
        _context.Set<Setting>().Attach(entity);
    }

    public void AttachRange(IEnumerable<Setting> entities)
    {
        _context.Set<Setting>().AttachRange(entities);
    }

    public IQueryable<TCt> GetContext<TCt>() where TCt : class => _context.Set<TCt>();
}