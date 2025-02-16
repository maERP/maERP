using maERP.Domain.Dtos.Customer;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Customers;

public partial class CustomersEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int customerId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    private MudForm? _form;

    protected CustomerDetailDto CustomerDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        if (customerId != 0)
        {
            CustomerDetail = await HttpService.GetAsync<CustomerDetailDto>("/api/v1/Customers/" + customerId) ?? new CustomerDetailDto();
        }
    }

    protected async Task Save()
    {
        await HttpService.PutAsJsonAsync<CustomerDetailDto>("/api/v1/Customers/" + customerId, CustomerDetail);

        ReturnToList();
    }

    public void ReturnToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/Customers");
    }
}