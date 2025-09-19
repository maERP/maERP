using maERP.Domain.Entities;
using maERP.Domain.Enums;

namespace maERP.Application.Contracts.Persistence;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetWithDetailsAsync(Guid id);
    Task<Order?> GetByRemoteOrderIdAsync(Guid salesChannelId, string remoteOrderId);
    Task<List<OrderHistory>> GetOrderHistoryAsync(Guid orderId);
    Task<bool> CanCreateInvoice(Guid orderId);
}