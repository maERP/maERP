using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiPrompt;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPromptsEdit
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IAiPromptService _aiPromptService { get; set; }

    [Parameter]
    public int aiPromptId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    protected AiPromptVM aiPrompt = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiPromptId != 0)
        {
            Title = "Bearbeiten";
            aiPrompt = await _aiPromptService.GetAiPromptDetails(aiPromptId);
        }
    }

    protected async Task Save()
    {
        if (aiPromptId != 0)
        {
            await _aiPromptService.UpdateAiPrompt(aiPromptId, aiPrompt);
        }
        else
        {
            await _aiPromptService.CreateAiPrompt(aiPrompt);
        }

        NavigateToList();
    }

    public void NavigateToList()
    {
        StateHasChanged();
        _navigationManager.NavigateTo("/AiPrompts");
    }
}