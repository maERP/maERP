namespace maERP.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<int> CreateAsync(T entity);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(int id);
    Task<bool> IsUniqueAsync(T entity, int? id = null);
}