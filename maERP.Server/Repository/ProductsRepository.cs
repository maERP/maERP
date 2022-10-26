#nullable disable

using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Contracts;
using maERP.Data.Models;
using maERP.Server.Models;
using maERP.Server.Exceptions;
using maERP.Data.Dtos.Product;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Repository
{
	public class ProductsRepository : GenericRepository<Product>, IProductsRepository
	{
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ProductDto> GetDetails(int id)
        {
            var product = await _context.Product.Include(q => q.TaxClass)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if(product == null)
            {
                throw new NotFoundException(nameof(GetDetails), id);
            }

            return product;
        }
    }
}