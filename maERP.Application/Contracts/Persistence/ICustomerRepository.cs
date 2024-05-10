using maERP.Domain.Models;

namespace maERP.Application.Contracts.Persistence;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<Customer> GetCustomerWithDetails(int id);
}