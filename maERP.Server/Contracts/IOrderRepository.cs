using maERP.Shared.Models.Database;

namespace maERP.Server.Contracts;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order> GetByRemoteOrderIdAsync(string remoteOrderId, int salesChannelId);
}
