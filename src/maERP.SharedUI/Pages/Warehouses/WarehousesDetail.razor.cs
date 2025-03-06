using maERP.Domain.Dtos.Warehouse;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class WarehousesDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int warehouseId { get; set; }

    protected string Title = "Lager";

    protected WarehouseDetailDto Warehouse = new();

    protected override async Task OnParametersSetAsync()
    {
        if (warehouseId != 0)
        {
            Title = "Bearbeiten";
            Warehouse = await HttpService.GetAsync<WarehouseDetailDto>($"/api/v1/Warehouses/{warehouseId}")
                        ?? new WarehouseDetailDto();
        }
        else Title = "nicht gefunden";
    }
    
    protected void NavigateToList()
    {
        NavigationManager.NavigateTo("/Warehouses");
    }
    
    protected void NavigateToEdit()
    {
        NavigationManager.NavigateTo($"/Warehouses/Edit/{warehouseId}");
    }
}