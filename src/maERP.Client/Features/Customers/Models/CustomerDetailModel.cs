using maERP.Client.Core.Constants;
using maERP.Client.Features.Customers.Services;
using maERP.Domain.Dtos.Customer;

namespace maERP.Client.Features.Customers.Models;

/// <summary>
/// Model for customer detail page using MVUX pattern.
/// Receives customer ID from navigation data.
/// </summary>
public partial record CustomerDetailModel
{
    private readonly ICustomerService _customerService;
    private readonly INavigator _navigator;
    private readonly Guid _customerId;

    public CustomerDetailModel(
        ICustomerService customerService,
        INavigator navigator,
        Guid customerId)
    {
        _customerService = customerService;
        _navigator = navigator;
        _customerId = customerId;
    }

    /// <summary>
    /// Feed that loads the customer details.
    /// </summary>
    public IFeed<CustomerDetailDto> Customer => Feed.Async(async ct =>
    {
        var customer = await _customerService.GetCustomerAsync(_customerId, ct);
        return customer ?? throw new InvalidOperationException($"Customer {_customerId} not found");
    });

    /// <summary>
    /// Navigate to edit customer page.
    /// </summary>
    public async Task EditCustomer()
    {
        await _navigator.NavigateRouteAsync(
            this,
            Routes.CustomerEdit,
            data: new Dictionary<string, object> { ["customerId"] = _customerId });
    }

    /// <summary>
    /// Delete the customer and navigate back.
    /// </summary>
    public async Task DeleteCustomer(CancellationToken ct)
    {
        await _customerService.DeleteCustomerAsync(_customerId, ct);
        await _navigator.NavigateBackAsync(this);
    }

    /// <summary>
    /// Navigate back to customer list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
