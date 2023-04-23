#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Contracts;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using maERP.Shared.Dtos.Product;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<ProductDetailDto> GetDetails(int id)
    {
        var product = await _context.Product.Include(q => q.TaxClass)
            .ProjectTo<ProductDetailDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.Id == id);

        return product;
    }
}