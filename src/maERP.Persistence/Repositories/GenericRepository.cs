using System.Linq.Expressions;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities.Common;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext Context;

    public GenericRepository(ApplicationDbContext context)
    {
        Context = context;
    }

    public IQueryable<TCt> GetContext<TCt>() where TCt : class => Context.Set<TCt>();

    public IQueryable<T> Entities => Context.Set<T>();

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
        return await Context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id, bool asNoTracking = false)
    {
        if (asNoTracking)
        {
            return await Context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        return await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(T entity)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync();
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        return await Context.Set<T>().AsNoTracking().AnyAsync(e => e.Id == id);
    }
    
    public async Task<bool> IsUniqueAsync(T entity, int? id = null)
    {
        var type = typeof(T);
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var parameter = Expression.Parameter(type, "e");
            var propertyExpression = Expression.Property(parameter, property);
            var value = property.GetValue(entity);
            var constant = Expression.Constant(value);
            var equalityExpression = Expression.Equal(propertyExpression, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equalityExpression, parameter);
        
            // Exclude entity with provided id
            if (id.HasValue)
            {
                var idProperty = type.GetProperty("Id");
                var idPropertyExpression = Expression.Property(parameter, idProperty!);
                var idConstant = Expression.Constant(id.Value);
                var idEqualityExpression = Expression.NotEqual(idPropertyExpression, idConstant);
                var idLambda = Expression.Lambda<Func<T, bool>>(idEqualityExpression, parameter);
            
                var combinedExpression = Expression.AndAlso(lambda.Body, idLambda.Body);
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