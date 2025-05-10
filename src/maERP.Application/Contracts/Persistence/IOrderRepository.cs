using maERP.Domain.Entities;
using maERP.Domain.Enums;

namespace maERP.Application.Contracts.Persistence;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetWithDetailsAsync(int id);
    Task<Order?> GetByRemoteOrderIdAsync(int salesChannelId, string remoteOrderId);
    Task<List<OrderHistory>> GetOrderHistoryAsync(int orderId);
    Task<bool> CanCreateInvoice(int orderId);
}