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
    Task<TaxClassDetailDto> GetDetails(uint id);
}

public class TaxClassRepository : GenericRepository<TaxClass>, ITaxClassRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TaxClassRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<TaxClassDetailDto> GetDetails(uint id)
    {
        var taxClass = await _context.TaxClass
            .ProjectTo<TaxClassDetailDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.Id == id);

        if(taxClass == null)
        {
            throw new NotFoundException(nameof(GetDetails), id);
        }

        return taxClass;
    }
}