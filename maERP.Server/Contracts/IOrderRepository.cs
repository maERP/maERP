using maERP.Server.Models;

namespace maERP.Server.Contracts;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order> GetByRemoteOrderIdAsync(string remoteOrderId, int salesChannelId);
}
