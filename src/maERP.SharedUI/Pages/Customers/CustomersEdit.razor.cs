using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Customer;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ICustomerService CustomerService { get; set; }

    [Parameter]
    public int customerId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    private MudForm? _form;

    protected CustomerVm Customer = new();

    protected override async Task OnParametersSetAsync()
    {
        if (customerId != 0)
        {
            Customer = await CustomerService.GetCustomerDetails(customerId);
        }
    }

    protected async Task Save()
    {
        await CustomerService.UpdateCustomer(customerId, Customer);

        ReturnToList();
    }

    public void ReturnToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/Customers");
    }
}