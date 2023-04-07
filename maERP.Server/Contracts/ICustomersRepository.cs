using maERP.Shared.Dtos.Customer;
using maERP.Shared.Models;

namespace maERP.Server.Contracts;

public interface ICustomersRepository : IGenericRepository<Customer>
{
    Task<CustomerDetailDto> GetDetails(int id);
}