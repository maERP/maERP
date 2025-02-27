using System.Net.Http.Json;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModelsAdd
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;

    public AiModelDetailDto AiModelDetail = new();

    protected async Task Save()
    {
        var httpResponseMessage = await HttpService.PostAsJsonAsync<AiModelDetailDto>("/api/v1/AiModels", AiModelDetail);
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
                    Snackbar.Add(errorMessage, Severity.Error);
                }
            }
        }
        else
        {
            Snackbar.Add("AI Model konnte nicht gespeichert werden", Severity.Error);
        }
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/AiModels");
    }
}
