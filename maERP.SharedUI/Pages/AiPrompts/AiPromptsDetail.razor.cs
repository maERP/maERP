using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiPrompt;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPromptsDetail
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IAiPromptService _aiPromptService { get; set; }

    [Parameter]
    public int aiPromptId { get; set; }

    protected string Title = "AI Model";

    protected AiPromptVM aiPrompt = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiPromptId != 0)
        {
            Title = "Bearbeiten";
            aiPrompt = await _aiPromptService.GetAiPromptDetails(aiPromptId);
        }
        else Title = "nicht gefunden";
    }
}