using maERP.Client.Core.Constants;
using maERP.Client.Features.Customers.Services;
using maERP.Domain.Dtos.Customer;

namespace maERP.Client.Features.Customers.Models;

/// <summary>
/// Model for customer list page using MVUX pattern.
/// </summary>
public partial record CustomerListModel
{
    private readonly ICustomerService _customerService;
    private readonly INavigator _navigator;

    public CustomerListModel(
        ICustomerService customerService,
        INavigator navigator)
    {
        _customerService = customerService;
        _navigator = navigator;
    }

    /// <summary>
    /// The search query entered by the user.
    /// </summary>
    public IState<string> SearchQuery => State<string>.Value(this, () => string.Empty);

    /// <summary>
    /// Feed of customers from the API.
    /// Automatically refreshes when SearchQuery changes.
    /// </summary>
    public IListFeed<CustomerListDto> Customers => ListFeed.Async(async ct =>
    {
        var query = await SearchQuery;
        var customers = await _customerService.GetCustomersAsync(
            page: 0,
            pageSize: 50,
            searchQuery: string.IsNullOrWhiteSpace(query) ? null : query,
            ct: ct);
        return customers;
    });

    /// <summary>
    /// Navigate to customer detail page.
    /// </summary>
    public async Task ViewCustomer(CustomerListDto customer)
    {
        await _navigator.NavigateRouteAsync(
            this,
            Routes.CustomerDetail,
            data: new Dictionary<string, object> { ["customerId"] = customer.Id });
    }

    /// <summary>
    /// Navigate to create new customer page.
    /// </summary>
    public async Task CreateCustomer()
    {
        await _navigator.NavigateRouteAsync(this, Routes.CustomerCreate);
    }
}
