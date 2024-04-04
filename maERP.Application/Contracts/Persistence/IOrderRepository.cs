using maERP.Domain;

namespace maERP.Application.Contracts.Persistence;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order> GetByRemoteOrderIdAsync(string remoteOrderId, int salesChannelId);
}
