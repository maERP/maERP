using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IProductSalesChannelRepository : IGenericRepository<ProductSalesChannel>
{
    Task<ProductSalesChannel?> GetByRemoteProductIdAsync(int productId, int salesChannelId = 0);
}
