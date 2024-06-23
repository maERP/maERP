using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class ProductSalesChannelRepository : GenericRepository<ProductSalesChannel>, IProductSalesChannelRepository
{
    public ProductSalesChannelRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ProductSalesChannel?> GetByRemoteProductIdAsync(int productId, int salesChannelId = 0)
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