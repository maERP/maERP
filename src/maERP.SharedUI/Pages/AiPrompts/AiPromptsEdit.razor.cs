using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiPrompt;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPromptsEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IAiPromptService AiPromptService { get; set; }

    [Parameter]
    public int aiPromptId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    protected AiPromptVm AiPrompt = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiPromptId != 0)
        {
            Title = "Bearbeiten";
            AiPrompt = await AiPromptService.GetAiPromptDetails(aiPromptId);
        }
    }

    protected async Task Save()
    {
        if (aiPromptId != 0)
        {
            await AiPromptService.UpdateAiPrompt(aiPromptId, AiPrompt);
        }
        else
        {
            await AiPromptService.CreateAiPrompt(AiPrompt);
        }

        NavigateToList();
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/AiPrompts");
    }
}