using maERP.Server.Data.SalesChannelData.Shopware5;

namespace maERP.Server.Contracts.SalesChannelContracts
{
	public interface IShopware5Repository
	{
		Task<Shopware5Product> getProductList();
	}
}