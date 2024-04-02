using AutoMapper;
using maERP.Server.Contracts;
using maERP.Server.Services;
using maERP.Shared.Models.Database;

namespace maERP.Server.Repository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}