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
    public int WarehouseId { get; set; }

    private string _title = "Lager";
    private WarehouseDetailDto _warehouse = new();

    protected override async Task OnParametersSetAsync()
    {
        if (WarehouseId != 0)
        {
            _title = "Bearbeiten";
            _warehouse = await HttpService.GetAsync<WarehouseDetailDto>($"/api/v1/Warehouses/{WarehouseId}")
                        ?? new WarehouseDetailDto();
        }
        else _title = "nicht gefunden";
    }
}