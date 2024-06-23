using System.Linq.Expressions;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities.Common;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<CT> GetContext<CT>() where CT : class => _context.Set<CT>();

    public IQueryable<T> Entities => _context.Set<T>();

    public void Attach(T entity)
    {
        _context.Set<T>().Attach(entity);
    }

    public void AttachRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AttachRange(entities);
    }

    public async Task<int> CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id, bool asNoTracking = false)
    {
        if (asNoTracking)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Set<T>().AsNoTracking().AnyAsync(e => e.Id == id);
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

            if (await _context.Set<T>().AnyAsync(lambda))
            {
                return false;
            }
        }

        return true;
    }
}