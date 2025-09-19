using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context, ITenantContext tenantContext) : base(context, tenantContext)
    {
    }

    public async Task<Order?> GetWithDetailsAsync(Guid id)
    {
        return await Entities
            .Where(o => o.Id == id)
            .Include(o => o.OrderItems)
            .Include(o => o.OrderHistories)
            .AsSplitQuery()
            .FirstOrDefaultAsync() ?? null;
    }

    public async Task<Order?> GetByRemoteOrderIdAsync(Guid salesChannelId, string remoteOrderId)
    {
        return await Entities
            .Where(o => o.RemoteOrderId == remoteOrderId)
            .Where(o => o.SalesChannelId == salesChannelId)
            .FirstOrDefaultAsync() ?? null;
    }

    public async Task<List<OrderHistory>> GetOrderHistoryAsync(Guid orderId)
    {
        var currentTenantId = TenantContext.GetCurrentTenantId();
        var query = Context.OrderHistory.AsQueryable();
        
        if (currentTenantId.HasValue)
        {
            query = query.Where(oh => oh.TenantId == null || oh.TenantId == currentTenantId.Value);
        }
        else
        {
            query = query.Where(oh => oh.TenantId == null);
        }
        
        return await query
            .Where(oh => oh.OrderId == orderId)
            .OrderByDescending(oh => oh.DateCreated)
            .ToListAsync();
    }

    public async Task<bool> CanCreateInvoice(Guid orderId)
    {
        var order = await Entities
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
        var currentTenantId = TenantContext.GetCurrentTenantId();
        var invoiceQuery = Context.Invoice.AsQueryable();
        
        if (currentTenantId.HasValue)
        {
            invoiceQuery = invoiceQuery.Where(i => i.TenantId == null || i.TenantId == currentTenantId.Value);
        }
        else
        {
            invoiceQuery = invoiceQuery.Where(i => i.TenantId == null);
        }
        
        var invoiceExists = await invoiceQuery.AnyAsync(i => i.OrderId == orderId);

        // Return false if invoice already exists
        return !invoiceExists;
    }
}