using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IProductSalesChannelRepository : IGenericRepository<ProductSalesChannel>
{
    Task<ProductSalesChannel?> GetByRemoteProductIdAsync(string remoteProductId, Guid salesChannelId = default);
}
