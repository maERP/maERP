using maERP.Domain.Models;

namespace maERP.Application.Contracts.Persistence;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<Customer> GetCustomerWithDetails(int id);
    Task<Customer?> GetCustomerByEmailAsync(string email);
    Task<Customer?> GetCustomerByRemoteCustomerIdAsync(int salesChannelId, string remoteCustomerId);
    Task AddCustomerToSalesChannelAsync(int customerId, int salesChannelId, string remoteCustomerId);
    Task<ICollection<CustomerAddress>> GetCustomerAddressByCustomerIdAsync(int customerId);
    Task<CustomerAddress> AddCustomerAddressAsync(CustomerAddress customerAddress);
}