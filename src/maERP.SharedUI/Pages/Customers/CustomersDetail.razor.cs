using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Customer;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersDetail
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required ICustomerService _customerService { get; set; }

    [Parameter]
    public int customerId { get; set; }

    protected CustomerVM customer = new();

    protected override async Task OnParametersSetAsync()
    {
        if (customerId != 0)
        {
            customer = await _customerService.GetCustomerDetails(customerId);
        }
    }

    protected void NavEditCustomer()
    {
        _navigationManager.NavigateTo($"/Customers/{customerId}/edit");
    }
}