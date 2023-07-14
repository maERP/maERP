#nullable disable

using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using Microsoft.EntityFrameworkCore;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

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

public class GenericRepository<T> : IGenericRepository<T> where T : ABaseModel
{
    protected readonly ApplicationDbContext _context;
    protected readonly IMapper _mapper;

    public GenericRepository(ApplicationDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<List<TResult>> GetAllAsync<TResult>()
    {
        return await _context.Set<T>()
            .ProjectTo<TResult>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(int? id)
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

    public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
    {
        var entity = _mapper.Map<T>(source);

        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<TResult>(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync<TSource>(int id, TSource source)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
        {
            throw new NotFoundException(typeof(T).Name, id);
        }

        _mapper.Map(source, entity);
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if(entity is null)
        {
            throw new NotFoundException(typeof(T).Name, id);
        }

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }
}