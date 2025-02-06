using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPrompts
{
    [Inject]
    public required NavigationManager? NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;
    private MudDataGrid<AiPromptListDto> _dataGrid = new();

    private async Task<GridData<AiPromptListDto>> LoadGridData(GridState<AiPromptListDto> state)
    {
        var apiResponse = await HttpService.GetAsync<PaginatedResult<AiPromptListDto>>("/api/v1/AiPrompts") ??
                          new PaginatedResult<AiPromptListDto>(new List<AiPromptListDto>());

        GridData<AiPromptListDto> data = new()
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