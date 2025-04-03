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
    public int aiPromptId { get; set; }

    protected string Title = "AI Prompt";

    protected AiPromptDetailDto AiPromptDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiPromptId != 0)
        {
            var result = await HttpService.GetAsync<Result<AiPromptDetailDto>>($"/api/v1/AiPrompts/{aiPromptId}");
            
            if (result != null && result.Succeeded)
            {
                AiPromptDetail = result.Data;
            }
            else if(result != null && result.StatusCode == ResultStatusCode.NotFound)
            {
                Title = "nicht gefunden";
            }
        }
        else 
        {
            Title = "nicht gefunden";
        }
    }
}