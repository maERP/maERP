using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClasses
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;

    private readonly MudDataGrid<TaxClassListDto> _dataGrid = new();

    private async Task<GridData<TaxClassListDto>> LoadGridData(GridState<TaxClassListDto> state)
    {
        var apiResponse = await HttpService.GetAsync<PaginatedResult<TaxClassListDto>>("/api/v1/TaxClasses")
                          ?? new PaginatedResult<TaxClassListDto>(new List<TaxClassListDto>());
            
        GridData<TaxClassListDto> data = new()
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