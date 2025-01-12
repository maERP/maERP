using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiPrompt;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPrompts
{
    [Inject]
    public required NavigationManager? navigationManager { get; set; }

    [Inject]
    public required IAiPromptService _aiPromptService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<AiPromptListVM> _dataGrid = new();

    private async Task<GridData<AiPromptListVM>> LoadGridData(GridState<AiPromptListVM> state)
    {
        var apiResponse = await _aiPromptService.GetAiPrompts(state.Page, state.PageSize, _searchString);
        GridData<AiPromptListVM> data = new()
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