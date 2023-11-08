using maERP.Server.Models;

namespace maERP.Server.Repository;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order> GetByRemoteOrderIdAsync(string remoteOrderId, int salesChannelId);
}
