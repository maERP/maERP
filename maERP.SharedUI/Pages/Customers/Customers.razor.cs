using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Customer;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Customers;

public partial class Customers
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required ICustomerService _customerService { get; set; }


    private ICollection<CustomerVM>? customers;

    protected override async Task OnInitializedAsync()
    {
        customers = await _customerService.GetCustomers();
    }
}