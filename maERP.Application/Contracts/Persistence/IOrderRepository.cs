using maERP.Domain.Models;

namespace maERP.Application.Contracts.Persistence;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetByRemoteOrderIdAsync(int salesChannelId, string remoteOrderId);
}