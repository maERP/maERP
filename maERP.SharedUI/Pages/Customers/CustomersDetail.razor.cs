using maERP.SharedUI.Models.Customer;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersDetail
{

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
}