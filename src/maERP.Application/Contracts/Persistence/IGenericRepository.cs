namespace maERP.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> Entities { get; }
    IQueryable<TCt> GetContext<TCt>() where TCt : class;
    void Attach(T entity);
    void AttachRange(IEnumerable<T> entities);
    Task<Guid> CreateAsync(T entity);
    Task<ICollection<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id, bool asNoTracking = false);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> IsUniqueAsync(T entity, Guid? id = null);
}