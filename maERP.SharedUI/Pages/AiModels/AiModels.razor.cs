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
    public required IAiModelService _aiModelService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<AiModelListVM> _dataGrid = new();

    private async Task<GridData<AiModelListVM>> LoadGridData(GridState<AiModelListVM> state)
    {
        var apiResponse = await _aiModelService.GetAiModels(state.Page, state.PageSize, _searchString);
        GridData<AiModelListVM> data = new()
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