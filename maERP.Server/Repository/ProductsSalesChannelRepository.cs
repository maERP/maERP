using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Contracts;
using maERP.Shared.Models.Database;
using maERP.Server.Services;

namespace maERP.Server.Repository;

public class ProductSalesChannelRepository : GenericRepository<ProductSalesChannel>, IProductSalesChannelRepository
{
    public ProductSalesChannelRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSalesChannel> getByRemoteProductIdAsync(int productId, int salesChannelId = 0)
    {
        if (salesChannelId > 0)
        {
            return await _context.ProductSalesChannel
                .Where(p => p.RemoteProductId == productId)
                // .Where(p => p.SalesChannelId == salesChannelId)
                .FirstOrDefaultAsync();
        }

        return await _context.ProductSalesChannel
                .Where(p => p.RemoteProductId == productId)
                .FirstOrDefaultAsync();
    }
}