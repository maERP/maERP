using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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

    public async Task<bool> ExistsGloballyAsync(Guid id)
    {
        return await _context.UserTenant.IgnoreQueryFilters().AsNoTracking().AnyAsync(ut => ut.Id == id);
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
            .Where(ut => ut.UserId == userId)
            .Select(ut => new TenantListDto
            {
                Id = ut.Tenant!.Id,
                Name = ut.Tenant.Name,
                Description = ut.Tenant.Description,
                CompanyName = ut.Tenant.CompanyName,
                ContactEmail = ut.Tenant.ContactEmail,
                Phone = ut.Tenant.Phone,
                Website = ut.Tenant.Website,
                Street = ut.Tenant.Street,
                Street2 = ut.Tenant.Street2,
                PostalCode = ut.Tenant.PostalCode,
                City = ut.Tenant.City,
                State = ut.Tenant.State,
                Country = ut.Tenant.Country,
                Iban = ut.Tenant.Iban,
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

    // Transaction support methods
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Add(UserTenant entity)
    {
        _context.Set<UserTenant>().Add(entity);
    }
}