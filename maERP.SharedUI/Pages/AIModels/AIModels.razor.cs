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

    private MudDataGrid<AIModelListVM> _dataGrid = new();

    private async Task<GridData<AIModelListVM>> LoadGridData(GridState<AIModelListVM> state)
    {
        var apiResponse = await _aiModelService.GetAIModels(state.Page, state.PageSize, _searchString);
        GridData<AIModelListVM> data = new()
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