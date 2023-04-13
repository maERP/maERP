using maERP.Shared.Models;

namespace maERP.Server.Contracts;

public interface IProductSalesChannelsRepository : IGenericRepository<ProductSalesChannel>
{
    Task<ProductSalesChannel> GetDetails(int id);
    Task<ProductSalesChannel> getByRemoteProductIdAsync(int productId, int salesChannelId = 0);
}