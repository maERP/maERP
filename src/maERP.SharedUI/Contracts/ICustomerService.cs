using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.Customer;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface ICustomerService
{
    Task<PaginatedResult<CustomerVM>> GetCustomers(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<CustomerVM> GetCustomerDetails(int id);
    Task<Response<Guid>> CreateCustomer(CustomerVM customer);
    Task<Response<Guid>> UpdateCustomer(int id, CustomerVM customer);
    Task<Response<Guid>> DeleteCustomer(int id);
}
