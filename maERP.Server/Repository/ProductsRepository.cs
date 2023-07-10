#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using maERP.Shared.Dtos.Product;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public interface IProductRepository : IGenericRepository<Product>
{

}

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }
}