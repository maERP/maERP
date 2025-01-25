using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Product;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Products;

public partial class Products
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IProductService ProductService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<ProductListVm> _dataGrid = new();

    private async Task<GridData<ProductListVm>> LoadGridData(GridState<ProductListVm> state)
    {
        var apiResponse = await ProductService.GetProducts(state.Page, state.PageSize, _searchString);
        GridData<ProductListVm> data = new()
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