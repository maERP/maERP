namespace maERP.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<List<TResult>> GetAllAsync<TResult>();
    Task<T> GetByIdAsync(int? id);
    Task<T> AddAsync(T entity);
    Task<TResult> AddAsync<TSource, TResult>(TSource source);
    Task UpdateAsync(T entity);
    Task UpdateAsync<TSource>(int id, TSource source);
    Task DeleteAsync(int id);
    Task<bool> Exists(int id);
}
