using System.Net.Http.Json;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModelsEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required AiModelsInputValidator Validator { get; set; }

    [Parameter]
    public int aiModelId { get; set; }
    
    public MudForm? Form;

    protected string Title = string.Empty;

    public AiModelInputDto AiModel = new();

    protected override async Task OnInitializedAsync()
    {
        if (aiModelId == 0)
        {
            Title = "AI Model hinzufügen";
        }
        else
        {
            Title = "AI Model bearbeiten";
            
            var result = await HttpService.GetAsync<Result<AiModelInputDto>>($"/api/v1/AiModels/{aiModelId}");
            
            if (result != null && result.Succeeded)
            {
                AiModel = result.Data;
            }
        }
        
        StateHasChanged();
    }

    protected async Task Save()
    {
        HttpResponseMessage httpResponseMessage;

        if (aiModelId == 0)
        {
            httpResponseMessage = await HttpService.PostAsJsonAsync("/api/v1/AiModels", AiModel);
        }
        else
        {
            httpResponseMessage = await HttpService.PutAsJsonAsync($"/api/v1/AiModels/{aiModelId}", AiModel);
        }

        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>() ?? null;
        
        if (result != null)
        {
            if (result.Succeeded)
            {
                Snackbar.Add("AI Model gespeichert", Severity.Success);
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
            Snackbar.Add("AI Model konnte nicht gespeichert werden", Severity.Error);
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
        NavigationManager.NavigateTo("/AiModels");
    }
}