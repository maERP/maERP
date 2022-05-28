using maERP.Data.Models.SalesChannelData.Shopware5;

namespace maERP.Server.Contracts.SalesChannelContracts
{
	public interface IShopware5Repository
	{
		Task<Shopware5Product> getProductList();
	}
}