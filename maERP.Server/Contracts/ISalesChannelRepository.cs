using maERP.Data.Models;

namespace maERP.Server.Contracts
{
	public interface ISalesChannelRepository : IGenericRepository<SalesChannel>
	{
		Task<SalesChannel> getDetails(int id);
	}
}