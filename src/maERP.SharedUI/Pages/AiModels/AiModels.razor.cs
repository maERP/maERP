using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiModel;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModels
{
    [Inject]
    public required NavigationManager? navigationManager { get; set; }

    [Inject]
    public required IAiModelService AiModelService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<AiModelListVm> _dataGrid = new();

    private async Task<GridData<AiModelListVm>> LoadGridData(GridState<AiModelListVm> state)
    {
        var apiResponse = await AiModelService.GetAiModels(state.Page, state.PageSize, _searchString);
        GridData<AiModelListVm> data = new()
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