#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public interface IProductSalesChannelRepository : IGenericRepository<ProductSalesChannel>
{
    Task<ProductSalesChannel> getByRemoteProductIdAsync(int productId, int salesChannelId = 0);
}

public class ProductSalesChannelRepository : GenericRepository<ProductSalesChannel>, IProductSalesChannelRepository
{
    private readonly ApplicationDbContext _context;

    public ProductSalesChannelRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
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