using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
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
        return await _context.Order
            .Where(o => o.Id == id)
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync() ?? null;
    }

    public async Task<Order?> GetByRemoteOrderIdAsync(int salesChannelId, string remoteOrderId)
    {
        return await _context.Order
            .Where(o => o.RemoteOrderId == remoteOrderId)
            .Where(o => o.SalesChannelId == salesChannelId)
            .FirstOrDefaultAsync() ?? null;
    }
}