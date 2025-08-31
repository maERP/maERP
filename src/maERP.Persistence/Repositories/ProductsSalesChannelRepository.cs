using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class ProductSalesChannelRepository : GenericRepository<ProductSalesChannel>, IProductSalesChannelRepository
{
    public ProductSalesChannelRepository(ApplicationDbContext context, ITenantContext tenantContext) : base(context, tenantContext)
    {
    }

    public async Task<ProductSalesChannel?> GetByRemoteProductIdAsync(int productId, int salesChannelId = 0)
    {
        if (salesChannelId > 0)
        {
            return await Context.ProductSalesChannel
                .Where(p => p.RemoteProductId == productId)
                // .Where(p => p.SalesChannelId == salesChannelId)
                .FirstOrDefaultAsync();
        }

        return await Context.ProductSalesChannel
                .Where(p => p.RemoteProductId == productId)
                .FirstOrDefaultAsync();
    }
}