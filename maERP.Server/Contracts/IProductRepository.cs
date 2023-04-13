using maERP.Shared.Dtos.Product;
using maERP.Shared.Models;

namespace maERP.Server.Contracts;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<ProductDetailDto> GetDetails(int id);
}