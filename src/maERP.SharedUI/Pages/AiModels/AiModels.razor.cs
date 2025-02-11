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

    private readonly MudDataGrid<AiModelListDto> _dataGrid = new();

    private async Task<GridData<AiModelListDto>> LoadGridData(GridState<AiModelListDto> state)
    {
        var apiResponse = await httpService.GetAsync<PaginatedResult<AiModelListDto>>("/api/v1/AiModels");
        GridData<AiModelListDto> data = new()
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