using AutoMapper;
using Blazored.LocalStorage;
using maERP.Shared.Wrapper;
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

    public async Task<PaginatedResult<CustomerVM>> GetCustomers(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var customers = await _client.CustomersGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<CustomerVM>>(customers);
    }

    public async Task<CustomerVM> GetCustomerDetails(int id)
    {
        await AddBearerToken();
        var customer = await _client.CustomersGET2Async(id);
        return _mapper.Map<CustomerVM>(customer);
    }

    public async Task<Response<Guid>> CreateCustomer(CustomerVM customer)
    {
        try
        {
            await AddBearerToken();
            var customerCreateCommand = _mapper.Map<CustomerCreateCommand>(customer);
            await _client.CustomersPOSTAsync(customerCreateCommand);
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

    public async Task<Response<Guid>> UpdateCustomer(int id, CustomerVM customer)
    {
        try
        {
            await AddBearerToken();
            var customerUpdateCommand = _mapper.Map<CustomerUpdateCommand>(customer);
            await _client.CustomersPUTAsync(id, customerUpdateCommand);
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
            await _client.CustomersDELETEAsync(id);
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