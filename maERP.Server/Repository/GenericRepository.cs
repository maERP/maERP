using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
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
        var entity = await GetByIdAsync(id) ?? throw new NotFoundException(typeof(T).Name, id);

        _mapper.Map(source, entity);
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id) ?? throw new NotFoundException(typeof(T).Name, id);
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }
}