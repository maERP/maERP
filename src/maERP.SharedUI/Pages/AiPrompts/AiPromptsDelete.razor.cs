using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPromptsDelete
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IAiPromptService AiPromptService { get; set; }

    [Parameter]
    public int aiPromptId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (aiPromptId > 0)
        {
            await AiPromptService.DeleteAiPrompt(aiPromptId);
            NavigationManager.NavigateTo("/AiPrompts");
        }
    }
}