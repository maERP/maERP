#nullable disable

using maERP.Server.Contracts.SalesChannelContracts;
using maERP.Server.Models;
using maERP.Shared.Models.SalesChannels.Shopware5;
using maERP.Shared.Models.SalesChannels.Shopware5.ProductResponse;

namespace maERP.Server.Repository.SalesChannelRepositories
{
	public class Shopware5Repository : IShopware5Repository
    {
        private readonly ApplicationDbContext _context;

        public Shopware5Repository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Shopware5Response<Shopware5ProductResponse>> getProductList()
        {
            throw new NotImplementedException();
        }
    }
}