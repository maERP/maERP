using AutoMapper;
using Blazored.LocalStorage;
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

    public async Task<List<CustomerVM>> GetCustomers()
    {
        await AddBearerToken();
        var customers = await _client.CustomersAllAsync();
        return _mapper.Map<List<CustomerVM>>(customers);
    }

    public async Task<CustomerVM> GetCustomerDetails(int id)
    {
        await AddBearerToken();
        var customer = await _client.CustomersGETAsync(id);
        return _mapper.Map<CustomerVM>(customer);
    }

    public async Task<Response<Guid>> CreateCustomer(CustomerVM customer)
    {
        try
        {
            await AddBearerToken();
            var createCustomerCommand = _mapper.Map<CreateCustomerCommand>(customer);
            await _client.CustomersPOSTAsync(createCustomerCommand);
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
            var updateCustomerCommand = _mapper.Map<UpdateCustomerCommand>(customer);
            await _client.CustomersPUTAsync(id.ToString(), updateCustomerCommand);
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