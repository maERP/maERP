using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersDelete
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required ICustomerService _customerService { get; set; }


    [Parameter]
    public int customerId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (customerId > 0)
        {
            await _customerService.DeleteCustomer(customerId);
            _navigationManager.NavigateTo("/Customers");
        }
    }
}