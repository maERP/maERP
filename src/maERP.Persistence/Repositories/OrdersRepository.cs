using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Order?> GetWithDetailsAsync(int id)
    {
        return await Context.Order
            .Where(o => o.Id == id)
            .Include(o => o.OrderItems)
            .Include(o => o.OrderHistories)
            .FirstOrDefaultAsync() ?? null;
    }

    public async Task<Order?> GetByRemoteOrderIdAsync(int salesChannelId, string remoteOrderId)
    {
        return await Context.Order
            .Where(o => o.RemoteOrderId == remoteOrderId)
            .Where(o => o.SalesChannelId == salesChannelId)
            .FirstOrDefaultAsync() ?? null;
    }

    public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus, string username, string comment = "")
    {
        var order = await Context.Order.FirstOrDefaultAsync(o => o.Id == orderId);
        
        if (order == null)
        {
            return false;
        }

        // Speichern des alten Status, bevor wir ihn aktualisieren
        var oldStatus = order.Status;
        
        // Nur aktualisieren, wenn sich der Status tatsächlich ändert
        if (oldStatus != newStatus)
        {
            // Erstellen eines Eintrags in der OrderHistory
            var orderHistory = new OrderHistory
            {
                OrderId = orderId,
                Username = username,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                FieldName = "Status",
                OldValue = oldStatus.ToString(),
                NewValue = newStatus.ToString(),
                Comment = comment,
                Timestamp = DateTime.UtcNow
            };

            // Hinzufügen des OrderHistory-Eintrags zur Datenbank
            await Context.OrderHistory.AddAsync(orderHistory);
            
            // Aktualisieren des Status der Bestellung
            order.Status = newStatus;
            
            // Speichern aller Änderungen
            await Context.SaveChangesAsync();
            
            return true;
        }
        
        // Wenn der Status unverändert ist, geben wir trotzdem true zurück
        return true;
    }

    public async Task<List<OrderHistory>> GetOrderHistoryAsync(int orderId)
    {
        return await Context.OrderHistory
            .Where(oh => oh.OrderId == orderId)
            .OrderByDescending(oh => oh.Timestamp)
            .ToListAsync();
    }
}