using AutoMapper;
using Blazored.LocalStorage;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Customer;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class CustomerService : BaseHttpService, ICustomerService
{
    private readonly IMapper _mapper;

    public CustomerService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<PaginatedResult<CustomerVm>> GetCustomers(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var customers = await Client.CustomersGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<CustomerVm>>(customers);
    }

    public async Task<CustomerVm> GetCustomerDetails(int id)
    {
        await AddBearerToken();
        var customer = await Client.CustomersGET2Async(id);
        return _mapper.Map<CustomerVm>(customer);
    }

    public async Task<Response<Guid>> CreateCustomer(CustomerVm customer)
    {
        try
        {
            await AddBearerToken();
            var customerCreateCommand = _mapper.Map<CustomerCreateCommand>(customer);
            await Client.CustomersPOSTAsync(customerCreateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateCustomer(int id, CustomerVm customer)
    {
        try
        {
            await AddBearerToken();
            var customerUpdateCommand = _mapper.Map<CustomerUpdateCommand>(customer);
            await Client.CustomersPUTAsync(id, customerUpdateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
    public async Task<Response<Guid>> DeleteCustomer(int id)
    {
        try
        {
            await AddBearerToken();
            await Client.CustomersDELETEAsync(id);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}