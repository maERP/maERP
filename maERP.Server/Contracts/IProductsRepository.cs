using maERP.Shared.Models;
using maERP.Shared.Dtos.Product;

namespace maERP.Server.Contracts
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        Task<ProductDto> GetDetails(int id);
    }
}