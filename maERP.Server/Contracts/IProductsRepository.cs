using maERP.Server.Data;
using maERP.Server.Models;

namespace maERP.Server.Contracts
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        Task<BaseProductDto> GetDetails(int id);
    }
}