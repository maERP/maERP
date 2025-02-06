using maERP.Domain.Dtos.Customer;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int customerId { get; set; }

    protected CustomerDetailDto CustomerDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        if (customerId != 0)
        {
            CustomerDetail = await HttpService.GetAsync<CustomerDetailDto>("/api/v1/Customers/" + customerId) ?? new CustomerDetailDto();
        }
    }

    protected void NavEditCustomer()
    {
        NavigationManager.NavigateTo($"/Customers/{customerId}/edit");
    }
}