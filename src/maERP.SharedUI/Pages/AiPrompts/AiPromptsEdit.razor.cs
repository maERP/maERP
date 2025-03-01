using System.Net.Http.Json;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPromptsEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required AiPromptUpdateValidator Validator { get; set; }

    [Parameter]
    public int aiPromptId { get; set; }
    
    public MudForm? _form;
    
    protected string Title = "Bearbeiten";

    public AiPromptUpdateDto AiPrompt = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiPromptId != 0)
        {
            var result = await HttpService.GetAsync<Result<AiPromptUpdateDto>>($"/api/v1/AiPrompts/{aiPromptId}");
            
            if (result != null && result.Succeeded)
            {
                AiPrompt = result.Data;
            }
            else
            {
                // Handle error case
                AiPrompt = new();
            }
        }
    }

    protected async Task Save()
    {
        var httpResponseMessage = await HttpService.PutAsJsonAsync<AiPromptUpdateDto>($"/api/v1/AiModels/{aiPromptId}", AiPrompt);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>() ?? null;

        if (result != null)
        {
            if (result.Succeeded)
            {
                NavigateToList();
                Snackbar.Add("AI Prompt gespeichert", Severity.Success);
            }
            else
            {
                foreach (var errorMessage in result.Messages)
                {
                    Snackbar.Add(errorMessage, Severity.Error);
                }
            }
        }
        else
        {
            Snackbar.Add("AI Prompt konnte nicht gespeichert werden", Severity.Error);
        }
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/AiPrompts");
    }
}