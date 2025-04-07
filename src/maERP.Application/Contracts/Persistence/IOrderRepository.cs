using maERP.Domain.Entities;
using maERP.Domain.Enums;

namespace maERP.Application.Contracts.Persistence;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetWithDetailsAsync(int id);
    Task<Order?> GetByRemoteOrderIdAsync(int salesChannelId, string remoteOrderId);
    Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus, string username, string comment = "");
    Task<List<OrderHistory>> GetOrderHistoryAsync(int orderId);
}