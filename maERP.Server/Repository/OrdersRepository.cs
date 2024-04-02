using AutoMapper;
using maERP.Server.Contracts;
using maERP.Server.Services;
using maERP.Shared.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Repository;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<Order> GetByRemoteOrderIdAsync(string remoteOrderId, int salesChannelId)
    {
        return await _context.Order
            .Where(o => o.RemoteOrderId == remoteOrderId)
            .Where(o => o.SalesChannelId == salesChannelId)
            .FirstOrDefaultAsync() ?? throw new Exception($"Order with RemoteOrderId {remoteOrderId} not found");
    }
}