using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPromptsDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int AiPromptId { get; set; }

    private string _title = "AI Prompt";

    private AiPromptDetailDto _aiPromptDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        if (AiPromptId != 0)
        {
            var result = await HttpService.GetAsync<Result<AiPromptDetailDto>>($"/api/v1/AiPrompts/{AiPromptId}");
            
            if (result != null && result.Succeeded)
            {
                _title = $"AI Prompt - {_aiPromptDetail.Identifier}";
                _aiPromptDetail = result.Data;
            }
            else if(result != null && result.StatusCode == ResultStatusCode.NotFound)
            {
                _title = "nicht gefunden";
            }
        }
        else 
        {
            _title = "nicht gefunden";
        }
    }
}