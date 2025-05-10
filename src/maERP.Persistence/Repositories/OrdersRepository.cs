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
    
    public async Task<List<OrderHistory>> GetOrderHistoryAsync(int orderId)
    {
        return await Context.OrderHistory
            .Where(oh => oh.OrderId == orderId)
            .OrderByDescending(oh => oh.DateCreated)
            .ToListAsync();
    }

    public async Task<bool> CanCreateInvoice(int orderId)
    {
        var order = await Context.Order
            .Where(o => o.Id == orderId)
            .FirstOrDefaultAsync();
            
        if (order == null)
        {
            return false;
        }
        
        // Check if the payment status is completely paid
        if (order.PaymentStatus != PaymentStatus.CompletelyPaid)
        {
            return false;
        }
        
        // Check if an invoice already exists for this order
        var invoiceExists = await Context.Invoice
            .AnyAsync(i => i.OrderId == orderId);
            
        // Return false if invoice already exists
        return !invoiceExists;
    }
}