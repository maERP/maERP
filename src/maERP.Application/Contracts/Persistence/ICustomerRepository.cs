using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<Customer?> GetCustomerWithDetails(Guid id);
    Task<Customer?> GetByCustomerIdAsync(int customerId);
    Task<Customer?> GetCustomerByEmailAsync(string email);
    Task<Customer?> GetCustomerByRemoteCustomerIdAsync(Guid salesChannelId, string remoteCustomerId);
    Task AddCustomerToSalesChannelAsync(Guid customerId, Guid salesChannelId, string remoteCustomerId);
    Task<ICollection<CustomerAddress>> GetCustomerAddressByCustomerIdAsync(Guid customerId);
    Task<CustomerAddress> AddCustomerAddressAsync(CustomerAddress customerAddress);
}