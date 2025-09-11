using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IProductSalesChannelRepository : IGenericRepository<ProductSalesChannel>
{
    Task<ProductSalesChannel?> GetByRemoteProductIdAsync(Guid productId, Guid salesChannelId = default);
}
