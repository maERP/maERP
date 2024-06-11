using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Product;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Products;

public partial class Products
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IProductService _productService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<ProductListVM> _dataGrid = new();

    private async Task<GridData<ProductListVM>> LoadGridData(GridState<ProductListVM> state)
    {
        var apiResponse = await _productService.GetProducts(state.Page, state.PageSize, _searchString);
        GridData<ProductListVM> data = new()
        {
            Items = apiResponse.Data,
            TotalItems = apiResponse.TotalCount
        };

        return data;
    }

    private async Task Search()
    {
        if (_dataGrid is not null)
        {
            await _dataGrid!.ReloadServerData();
        }
    }
}