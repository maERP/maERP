using System.Linq.Expressions;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities.Common;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext Context;
    protected readonly ITenantContext TenantContext;

    public GenericRepository(ApplicationDbContext context, ITenantContext tenantContext)
    {
        Context = context;
        TenantContext = tenantContext;
    }

    public IQueryable<TCt> GetContext<TCt>() where TCt : class => Context.Set<TCt>();

    public IQueryable<T> Entities
    {
        get
        {
            IQueryable<T> query = Context.Set<T>();
            var currentTenantId = TenantContext.GetCurrentTenantId();


            // Apply tenant filtering - only return entities for current tenant or tenant-agnostic entities
            if (currentTenantId.HasValue)
            {
                query = query.Where(x => x.TenantId == null || x.TenantId == currentTenantId.Value);
            }
            else
            {
                // If no tenant is set, only return tenant-agnostic entities
                query = query.Where(x => x.TenantId == null);
            }
            
            return query;
        }
    }

    public void Attach(T entity)
    {
        Context.Set<T>().Attach(entity);
    }

    public void AttachRange(IEnumerable<T> entities)
    {
        Context.Set<T>().AttachRange(entities);
    }

    public async Task<int> CreateAsync(T entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        var query = Context.Set<T>().AsQueryable();
        var currentTenantId = TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue)
        {
            // Apply manual tenant filtering for both production and test environments
            query = query.Where(x => x.TenantId == null || x.TenantId == currentTenantId.Value);
        }
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id, bool asNoTracking = false)
    {
        // Always start with ignoring query filters to ensure fresh database reads
        // especially important after DELETE operations in tests
        var query = Context.Set<T>().IgnoreQueryFilters().AsQueryable();

        // Apply manual tenant filtering - crucial for multi-tenant scenarios
        var currentTenantId = TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue)
        {
            // Manual tenant filtering for both production and test environments
            query = query.Where(x => x.TenantId == null || x.TenantId == currentTenantId.Value);
        }
        else
        {
            // If no tenant context, only return tenant-agnostic entities
            query = query.Where(x => x.TenantId == null);
        }

        // Apply id filter
        query = query.Where(x => x.Id == id);

        // Always use AsNoTracking to ensure fresh database reads, especially after deletions
        if (asNoTracking)
        {
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        else
        {
            // For tracking entities, still use AsNoTracking to get fresh data, then attach
            var result = await query.AsNoTracking().FirstOrDefaultAsync();
            if (result != null)
            {
                // Clear any potentially stale tracked entities with the same ID
                var trackedEntity = Context.ChangeTracker.Entries<T>()
                    .FirstOrDefault(e => e.Entity.Id == id);
                if (trackedEntity != null)
                {
                    Context.Entry(trackedEntity.Entity).State = EntityState.Detached;
                }
                
                Context.Attach(result);
            }
            return result;
        }
    }

    public async Task UpdateAsync(T entity)
    {
        // Force all properties to be updated by detaching any existing tracked entity
        // and then using Update which will mark all properties as modified
        var tracked = Context.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity.Id == entity.Id);
        if (tracked != null)
        {
            Context.Entry(tracked.Entity).State = EntityState.Detached;
        }
        
        Context.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        // First verify the entity exists and belongs to the current tenant
        var existingEntity = await Context.Set<T>().IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == entity.Id);
        
        if (existingEntity == null)
        {
            throw new InvalidOperationException($"Entity with ID {entity.Id} not found for deletion");
        }
        
        // Verify tenant isolation for security
        var currentTenantId = TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue && existingEntity.TenantId != null && existingEntity.TenantId != currentTenantId)
        {
            throw new UnauthorizedAccessException($"Cannot delete entity from different tenant");
        }
        
        // Remove the existing entity (not the passed-in entity which may be incomplete)
        Context.Remove(existingEntity);
        await Context.SaveChangesAsync();
        
        // Clear all tracked entities to ensure fresh reads from database
        Context.ChangeTracker.Clear();
        
        // Force garbage collection in InMemory database scenarios  
        if (Context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        var query = Context.Set<T>().AsQueryable();
        var currentTenantId = TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue)
        {
            // Apply manual tenant filtering for both production and test environments
            query = query.Where(x => x.TenantId == null || x.TenantId == currentTenantId.Value);
        }
        return await query.AsNoTracking().AnyAsync(e => e.Id == id);
    }

    public virtual async Task<bool> IsUniqueAsync(T entity, int? id = null)
    {
        var type = typeof(T);
        var properties = type.GetProperties();
        var currentTenantId = TenantContext.GetCurrentTenantId();

        foreach (var property in properties)
        {
            // Skip TenantId, DateCreated, DateModified and Id properties for uniqueness check
            if (property.Name == "TenantId" || property.Name == "DateCreated" ||
                property.Name == "DateModified" || property.Name == "Id")
            {
                continue;
            }

            var value = property.GetValue(entity);

            // Skip null values and complex types (except string)
            if (value == null || (value.GetType().IsClass && value.GetType() != typeof(string)))
            {
                continue;
            }

            // Skip empty strings
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                continue;
            }

            var parameter = Expression.Parameter(type, "e");
            var propertyExpression = Expression.Property(parameter, property);
            var constant = Expression.Constant(value, property.PropertyType);
            var equalityExpression = Expression.Equal(propertyExpression, constant);

            // Build the base lambda expression
            var lambda = Expression.Lambda<Func<T, bool>>(equalityExpression, parameter);

            // Add tenant isolation
            if (currentTenantId.HasValue)
            {
                var tenantProperty = type.GetProperty("TenantId");
                if (tenantProperty != null)
                {
                    var tenantPropertyExpression = Expression.Property(parameter, tenantProperty);
                    var tenantConstant = Expression.Constant(currentTenantId.Value, typeof(int?));
                    var tenantEqualityExpression = Expression.Equal(tenantPropertyExpression, tenantConstant);

                    var combinedExpression = Expression.AndAlso(equalityExpression, tenantEqualityExpression);
                    lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
                }
            }

            // Exclude entity with provided id
            if (id.HasValue)
            {
                var idProperty = type.GetProperty("Id");
                var idPropertyExpression = Expression.Property(parameter, idProperty!);
                var idConstant = Expression.Constant(id.Value);
                var idEqualityExpression = Expression.NotEqual(idPropertyExpression, idConstant);

                var combinedExpression = Expression.AndAlso(lambda.Body, idEqualityExpression);
                lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, lambda.Parameters);
            }

            if (await Context.Set<T>().AnyAsync(lambda))
            {
                return false;
            }
        }

        return true;
    }
}