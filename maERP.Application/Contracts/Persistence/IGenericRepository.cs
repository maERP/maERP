﻿namespace maERP.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<int> CreateAsync(T entity);
    Task<ICollection<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id, bool asNoTracking = false);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(int id);
    Task<bool> IsUniqueAsync(T entity, int? id = null);
}