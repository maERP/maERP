using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Customer;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ICustomerService CustomerService { get; set; }

    [Parameter]
    public int customerId { get; set; }

    protected CustomerVm Customer = new();

    protected override async Task OnParametersSetAsync()
    {
        if (customerId != 0)
        {
            Customer = await CustomerService.GetCustomerDetails(customerId);
        }
    }

    protected void NavEditCustomer()
    {
        NavigationManager.NavigateTo($"/Customers/{customerId}/edit");
    }
}