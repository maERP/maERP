#nullable disable

using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Contracts;
using maERP.Data.Models;
using maERP.Server.Models;
using maERP.Server.Exceptions;
using maERP.Data.Dtos.Product;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Repository
{
	public class ProductsSalesChannelsRepository : GenericRepository<ProductSalesChannel>, IProductsSalesChannelsRepository
	{
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsSalesChannelsRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ProductSalesChannel> GetDetails(int id)
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

        public async Task<ProductSalesChannel> getByRemoteProductIdAsync(int productId, int salesChannelId = 0)
        {
            if (salesChannelId > 0)
            {
                return await _context.ProductSalesChannel
                    .Where(p => p.RemoteProductId == productId)
                    .Where(p => p.SalesChannelId == salesChannelId)
                    .FirstOrDefaultAsync();
            }

            return await _context.ProductSalesChannel
                    .Where(p => p.RemoteProductId == productId)
                    .FirstOrDefaultAsync();
        }
    }
}