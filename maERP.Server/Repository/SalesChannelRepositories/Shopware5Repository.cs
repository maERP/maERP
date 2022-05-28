#nullable disable

using maERP.Server.Contracts.SalesChannelContracts;
using maERP.Server.Models;
using maERP.Data.Models.SalesChannelData.Shopware5;

namespace maERP.Server.Repository.SalesChannelRepositories
{
	public class Shopware5Repository : IShopware5Repository
    {
        private readonly ApplicationDbContext _context;

        public Shopware5Repository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Shopware5Product> getProductList()
        {
            return new Shopware5Product();
        }
    }
}