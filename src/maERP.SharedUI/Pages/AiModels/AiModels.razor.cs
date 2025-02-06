using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModels
{
    [Inject]
    public required NavigationManager? navigationManager { get; set; }

    [Inject]
    public required IHttpService httpService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<AIModelListDto> _dataGrid = new();

    private async Task<GridData<AIModelListDto>> LoadGridData(GridState<AIModelListDto> state)
    {
        var apiResponse = await httpService.GetAsync<PaginatedResult<AIModelListDto>>("/api/v1/AiModels");
        GridData<AIModelListDto> data = new()
        {
            Items = apiResponse!.Data,
            TotalItems = apiResponse.TotalCount
        };

        return data;
    }

    private async Task Search()
    {
        await _dataGrid.ReloadServerData();
    }
}