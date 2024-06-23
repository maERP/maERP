using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetWithDetailsAsync(int id);
    Task<Order?> GetByRemoteOrderIdAsync(int salesChannelId, string remoteOrderId);
}