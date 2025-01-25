using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Models.TaxClass;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClasses
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ITaxClassService TaxClassService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<OrderListVm> _dataGrid = new();

    private async Task<GridData<TaxClassVm>> LoadGridData(GridState<TaxClassVm> state)
    {
        var apiResponse = await TaxClassService.GetTaxClasses(state.Page, state.PageSize, _searchString);
        GridData<TaxClassVm> data = new()
        {
            Items = apiResponse.Data,
            TotalItems = apiResponse.TotalCount
        };

        return data;
    }

    private async Task Search()
    {
        await _dataGrid.ReloadServerData();
    }
}