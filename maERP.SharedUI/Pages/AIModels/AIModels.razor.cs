using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AIModel;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AIModels;

public partial class AIModels
{
    [Inject]
    public required NavigationManager? navigationManager { get; set; }

    [Inject]
    public required IAIModelService _aiModelService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<AIModelVM> _dataGrid = new();

    private async Task<GridData<AIModelVM>> LoadGridData(GridState<AIModelVM> state)
    {
        var apiResponse = await _aiModelService.GetAIModels(state.Page, state.PageSize, _searchString);
        GridData<AIModelVM> data = new()
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