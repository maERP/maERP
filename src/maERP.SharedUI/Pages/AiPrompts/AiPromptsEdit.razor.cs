using System.Net.Http.Json;
using maERP.Domain.Dtos.AiModel;
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
    public required AiPromptInputValidator Validator { get; set; }

    [Parameter]
    public int aiPromptId { get; set; }
    
    public MudForm? Form;

    protected string Title = string.Empty;

    public AiPromptInputDto AiPrompt = new();
    public List<AiModelListDto> AiModels = new();

    protected override async Task OnInitializedAsync()
    {
        var modelResult = await HttpService.GetAsync<PaginatedResult<AiModelListDto>>("/api/v1/AiModels") ?? throw new Exception();
        AiModels = modelResult.Data;

        if (aiPromptId == 0)
        {
            Title = "AI Prompt hinzufügen";
        }
        else
        {
            Title = "AI Prompt bearbeiten";
            
            var result = await HttpService.GetAsync<Result<AiPromptInputDto>>($"/api/v1/AiPrompts/{aiPromptId}");
            
            if (result != null && result.Succeeded)
            {
                AiPrompt = result.Data;
            }
        }
        
        StateHasChanged();
    }

    protected async Task Save()
    {
        HttpResponseMessage httpResponseMessage;

        if (aiPromptId == 0)
        {
            httpResponseMessage = await HttpService.PostAsJsonAsync("/api/v1/AiPrompts", AiPrompt);
        }
        else
        {
            httpResponseMessage = await HttpService.PutAsJsonAsync($"/api/v1/AiPrompts/{aiPromptId}", AiPrompt);
        }

        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>() ?? null;
        
        if (result != null)
        {
            if (result.Succeeded)
            {
                Snackbar.Add("AI Prompt gespeichert", Severity.Success);
                NavigateToList();
            }
            else
            {
                foreach (var errorMessage in result.Messages)
                {
                    Snackbar.Add("SERVER: " + errorMessage, Severity.Error);
                }
            }
        }
        else
        {
            Snackbar.Add("AI Prompt konnte nicht gespeichert werden", Severity.Error);
        }
    }

    protected async Task OnValidSubmit()
    {
        if (Form is not null)
        {
            await Form.Validate();
            
            if (Form.IsValid)
            {
                await Save();
            }
        }
    }
    
    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/AiPrompts");
    }
}