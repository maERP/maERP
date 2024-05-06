using maERP.SharedUI.Models.Customer;

namespace maERP.SharedUI.Pages.Customers;

public partial class Customers
{

    private ICollection<CustomerVM>? customers;

    protected override async Task OnInitializedAsync()
    {
        customers = await _customerService.GetCustomers();
    }
}