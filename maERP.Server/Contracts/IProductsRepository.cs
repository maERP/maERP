using maERP.Server.Data;
using maERP.Server.Models.Product;

namespace maERP.Server.Contracts
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        Task<ProductDto> GetDetails(int id);
    }
}