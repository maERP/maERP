#nullable disable

using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Contracts;
using maERP.Shared.Models;
using maERP.Server.Models;
using maERP.Server.Exceptions;
using maERP.Shared.Dtos.TaxClass;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Repository
{
	public class TaxClassesRepository : GenericRepository<TaxClass>, ITaxClassesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TaxClassesRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<TaxClassDto> GetDetails(int id)
        {
            var taxClass = await _context.TaxClass
                .ProjectTo<TaxClassDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if(taxClass == null)
            {
                throw new NotFoundException(nameof(GetDetails), id);
            }

            return taxClass;
        }
    }
}