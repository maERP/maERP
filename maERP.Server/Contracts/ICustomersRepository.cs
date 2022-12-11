using maERP.Shared.Models;
using maERP.Shared.Dtos.Customer;

namespace maERP.Server.Contracts
{
    public interface ICustomersRepository : IGenericRepository<Customer>
    {
        Task<CustomerDto> GetDetails(int id);
    }
}