#nullable disable

using AutoMapper;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Repository;

public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetByIdAsync(uint? id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(uint id);
    Task<bool> Exists(uint id);
}

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GenericRepository(ApplicationDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(uint? id)
    {
        if (id is null)
        {
            return null;
        };

        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(uint id)
    {
        var entity = await GetByIdAsync(id);

        if(entity is null)
        {
            throw new NotFoundException(typeof(T).Name, id);
        }

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(uint id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }

}