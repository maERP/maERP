using maERP.Domain.Dtos.AiPrompt;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPromptsEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int aiPromptId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    protected AiPromptDetailDto AiPromptDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiPromptId != 0)
        {
            Title = "Bearbeiten";
            AiPromptDetail = await HttpService.GetAsync<AiPromptDetailDto>("/api/v1/AiPrompts/" + aiPromptId) ?? new AiPromptDetailDto();
        }
    }

    protected async Task Save()
    {
        if (aiPromptId != 0)
        {
            await HttpService.PutAsync<AiPromptDetailDto, AiPromptDetailDto>( "/api/v1/AiPrompts/" + aiPromptId, AiPromptDetail);
        }
        else
        {
            await HttpService.PostAsync<AiPromptDetailDto, AiPromptDetailDto>("/api/v1/AiPrompts", AiPromptDetail);
        }

        NavigateToList();
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/AiPrompts");
    }
}