using maERP.Data.Models;
using maERP.Data.Dtos.Product;

namespace maERP.Server.Contracts
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        Task<ProductDto> GetDetails(int id);
    }
}