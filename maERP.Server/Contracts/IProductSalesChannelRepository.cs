using maERP.Server.Models;

namespace maERP.Server.Contracts;

public interface IProductSalesChannelRepository : IGenericRepository<ProductSalesChannel>
{
    Task<ProductSalesChannel> getByRemoteProductIdAsync(int productId, int salesChannelId = 0);
}
