using maERP.Data.Models;

namespace maERP.Server.Contracts
{
	public interface ICustomerRepository : IGenericRepository<Customer>
	{
		Task<Customer> getDetails(int id);
	}
}