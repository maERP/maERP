using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiPrompt;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPromptsDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IAiPromptService AiPromptService { get; set; }

    [Parameter]
    public int aiPromptId { get; set; }

    protected string Title = "AI Model";

    protected AiPromptVm AiPrompt = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiPromptId != 0)
        {
            Title = "Bearbeiten";
            AiPrompt = await AiPromptService.GetAiPromptDetails(aiPromptId);
        }
        else Title = "nicht gefunden";
    }
}