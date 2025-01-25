using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.Customer;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface ICustomerService
{
    Task<PaginatedResult<CustomerVm>> GetCustomers(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<CustomerVm> GetCustomerDetails(int id);
    Task<Response<Guid>> CreateCustomer(CustomerVm customer);
    Task<Response<Guid>> UpdateCustomer(int id, CustomerVm customer);
    Task<Response<Guid>> DeleteCustomer(int id);
}
