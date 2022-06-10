using maERP.Data.Models;
using maERP.Data.Dtos.Product;

namespace maERP.Server.Contracts
{
    public interface IProductsSalesChannelsRepository : IGenericRepository<ProductSalesChannel>
    {
        Task<ProductSalesChannel> GetDetails(int id);
    }
}