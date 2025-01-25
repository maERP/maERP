using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersDelete
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ICustomerService CustomerService { get; set; }


    [Parameter]
    public int customerId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (customerId > 0)
        {
            await CustomerService.DeleteCustomer(customerId);
            NavigationManager.NavigateTo("/Customers");
        }
    }
}