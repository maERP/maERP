#nullable disable

using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Contracts;
using maERP.Shared.Models;
using maERP.Server.Models;
using maERP.Server.Exceptions;
using maERP.Shared.Dtos.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Repository
{
	public class WarehouseRepository : GenericRepository<Warehouse>, IWarehousesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public WarehouseRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<WarehouseDto> GetDetails(int id)
        {
            var warehouse = await _context.Warehouse
                .ProjectTo<WarehouseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if(warehouse == null)
            {
                throw new NotFoundException(nameof(GetDetails), id);
            }

            return warehouse;
        }
    }
}