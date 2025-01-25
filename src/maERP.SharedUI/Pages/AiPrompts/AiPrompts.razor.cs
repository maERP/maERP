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
    public required IAiPromptService AiPromptService { get; set; }

    private string _searchString = string.Empty;

    private MudDataGrid<AiPromptListVm> _dataGrid = new();

    private async Task<GridData<AiPromptListVm>> LoadGridData(GridState<AiPromptListVm> state)
    {
        var apiResponse = await AiPromptService.GetAiPrompts(state.Page, state.PageSize, _searchString);
        GridData<AiPromptListVm> data = new()
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