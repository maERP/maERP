#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using maERP.Shared.Models;
using maERP.Shared.Dtos.TaxClass;

namespace maERP.Server.Repository;

public interface ITaxClassRepository : IGenericRepository<TaxClass>
{
    
}

public class TaxClassRepository : GenericRepository<TaxClass>, ITaxClassRepository
{
    private readonly ApplicationDbContext _context;

    public TaxClassRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
    }
}