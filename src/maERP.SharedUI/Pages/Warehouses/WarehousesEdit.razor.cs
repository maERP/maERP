using maERP.Domain.Dtos.Warehouse;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class WarehousesEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int warehouseId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    protected WarehouseDetailDto Warehouse = new();

    protected override async Task OnParametersSetAsync()
    {
        if (warehouseId != 0)
        {
            Title = "Bearbeiten";
            Warehouse = await HttpService.GetAsync<WarehouseDetailDto>($"/api/v1/Warehouses/{warehouseId}") ?? new WarehouseDetailDto();
        }
    }

    protected async Task Save()
    {
        if (warehouseId != 0)
        {
            await HttpService.PutAsJsonAsync<WarehouseDetailDto>($"/api/v1/Warehouses/{warehouseId}", Warehouse);
        }
        else
        {
            await HttpService.PostAsJsonAsync<WarehouseDetailDto>("/api/v1/Warehouses/", Warehouse);
        }

        NavigateToList();
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/Warehouses");
    }
}