using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Models.TaxClass;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClasses
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required ITaxClassService _taxClassService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<OrderListVM> _dataGrid = new();

    private async Task<GridData<TaxClassVM>> LoadGridData(GridState<TaxClassVM> state)
    {
        var apiResponse = await _taxClassService.GetTaxClasses(state.Page, state.PageSize, _searchString);
        GridData<TaxClassVM> data = new()
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