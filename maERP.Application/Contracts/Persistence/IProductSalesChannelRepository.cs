using maERP.Domain.Models;

namespace maERP.Application.Contracts.Persistence;

public interface IProductSalesChannelRepository : IGenericRepository<ProductSalesChannel>
{
    Task<ProductSalesChannel> getByRemoteProductIdAsync(int productId, int salesChannelId = 0);
}
