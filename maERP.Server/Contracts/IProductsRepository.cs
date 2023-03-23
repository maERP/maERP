using maERP.Shared.Dtos.Product;
using maERP.Shared.Models;

namespace maERP.Server.Contracts;

public interface IProductsRepository : IGenericRepository<Product>
{
    Task<ProductDto> GetDetails(int id);
}