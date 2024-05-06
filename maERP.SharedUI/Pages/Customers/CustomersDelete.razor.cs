using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersDelete
{

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