using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Products;

public partial class Products
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<ProductListDto> _dataGrid = new();

    private async Task<GridData<ProductListDto>> LoadGridData(GridState<ProductListDto> state)
    {
        var apiResponse = await HttpService.GetAsync<PaginatedResult<ProductListDto>>("/api/v1/Products")
                          ?? new PaginatedResult<ProductListDto>(new List<ProductListDto>());
                
        GridData<ProductListDto> data = new()
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