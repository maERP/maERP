using AutoMapper;
using maERP.Server.Models;

namespace maERP.Server.Repository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}