using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class UserTenantRepository : IUserTenantRepository
{
    private readonly ApplicationDbContext _context;

    public UserTenantRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<UserTenant> Entities => _context.UserTenant;

    public async Task<Guid> CreateAsync(UserTenant entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(UserTenant entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserTenant entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<UserTenant>> GetAllAsync()
    {
        return await _context.UserTenant
            .Include(ut => ut.Tenant)
            .Include(ut => ut.User)
            .OrderBy(ut => ut.UserId)
            .ToListAsync();
    }

    public async Task<UserTenant?> GetByIdAsync(Guid id, bool asNoTracking = false)
    {
        if (asNoTracking)
        {
            return await _context.UserTenant
                .AsNoTracking()
                .Include(ut => ut.Tenant)
                .Include(ut => ut.User)
                .FirstOrDefaultAsync(ut => ut.Id == id);
        }

        return await _context.UserTenant
            .Include(ut => ut.Tenant)
            .Include(ut => ut.User)
            .FirstOrDefaultAsync(ut => ut.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.UserTenant.AsNoTracking().AnyAsync(ut => ut.Id == id);
    }

    public async Task<bool> IsUniqueAsync(UserTenant entity, Guid? id = null)
    {
        var query = _context.UserTenant.AsQueryable();

        if (id.HasValue)
        {
            query = query.Where(ut => ut.Id != id.Value);
        }

        return !await query.AnyAsync(ut => ut.UserId == entity.UserId && ut.TenantId == entity.TenantId);
    }

    public async Task<List<TenantListDto>> GetUserTenantsAsync(string userId)
    {
        var userTenants = await _context.UserTenant
            .IgnoreQueryFilters()
            .Include(ut => ut.Tenant)
            .Where(ut => ut.UserId == userId && ut.Tenant!.IsActive)
            .Select(ut => new TenantListDto
            {
                Id = ut.Tenant!.Id,
                Name = ut.Tenant.Name,
                TenantCode = ut.Tenant.TenantCode,
                Description = ut.Tenant.Description,
                IsActive = ut.Tenant.IsActive,
                ContactEmail = ut.Tenant.ContactEmail,
                DateCreated = ut.Tenant.DateCreated,
                DateModified = ut.Tenant.DateModified
            })
            .ToListAsync();

        return userTenants;
    }

    public void Attach(UserTenant entity)
    {
        _context.Set<UserTenant>().Attach(entity);
    }

    public void AttachRange(IEnumerable<UserTenant> entities)
    {
        _context.Set<UserTenant>().AttachRange(entities);
    }

    public IQueryable<TCt> GetContext<TCt>() where TCt : class => _context.Set<TCt>();
}