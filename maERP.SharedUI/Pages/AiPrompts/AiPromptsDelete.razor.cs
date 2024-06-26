using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPromptsDelete
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IAiPromptService _aiPromptService { get; set; }

    [Parameter]
    public int aiPromptId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (aiPromptId > 0)
        {
            await _aiPromptService.DeleteAiPrompt(aiPromptId);
            _navigationManager.NavigateTo("/AiPrompts");
        }
    }
}