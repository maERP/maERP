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
    Task<ProductSalesChannel> GetDetails(uint id);
    Task<ProductSalesChannel> getByRemoteProductIdAsync(uint productId, uint salesChannelId = 0);
}

public class ProductSalesChannelRepository : GenericRepository<ProductSalesChannel>, IProductSalesChannelRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductSalesChannelRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<ProductSalesChannel> GetDetails(uint id)
    {
        var productSalesChannel = await _context.Product.Include(q => q.TaxClass)
            .ProjectTo<ProductSalesChannel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.Id == id);

        if(productSalesChannel == null)
        {
            throw new NotFoundException(nameof(GetDetails), id);
        }

        return productSalesChannel;
    }

    public async Task<ProductSalesChannel> getByRemoteProductIdAsync(uint productId, uint salesChannelId = 0)
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