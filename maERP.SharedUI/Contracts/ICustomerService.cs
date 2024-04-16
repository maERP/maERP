using maERP.SharedUI.Models.Customer;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface ICustomerService
{
    Task<List<CustomerVM>> GetCustomers();
    Task<CustomerVM> GetCustomerDetails(int id);
    Task<Response<Guid>> CreateCustomer(CustomerVM customer);
    Task<Response<Guid>> UpdateCustomer(int id, CustomerVM customer);
    Task<Response<Guid>> DeleteCustomer(int id);
}
