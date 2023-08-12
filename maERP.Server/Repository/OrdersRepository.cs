#nullable enable

using AutoMapper;
using maERP.Server.Models;
using maERP.Shared.Pages.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.UriParser;

namespace maERP.Server.Repository;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order> GetByRemoteOrderIdAsync(string remoteOrderId, int salesChannelId);
}

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