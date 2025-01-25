using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Warehouse;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class WarehousesEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IWarehouseService WarehouseService { get; set; }

    [Parameter]
    public int warehouseId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    protected WarehouseVm Warehouse = new();

    protected override async Task OnParametersSetAsync()
    {
        if (warehouseId != 0)
        {
            Title = "Bearbeiten";
            Warehouse = await WarehouseService.GetWarehouseDetails(warehouseId);
        }
    }

    protected async Task Save()
    {
        if (warehouseId != 0)
        {
            await WarehouseService.UpdateWarehouse(warehouseId, Warehouse);
        }
        else
        {
            await WarehouseService.CreateWarehouse(Warehouse);
        }

        NavigateToList();
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/Warehouses");
    }
}