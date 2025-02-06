using maERP.Domain.Dtos.AiModel;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModelsEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService httpService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    public AIModelDetailDto AiModelDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId != 0)
        {
            Title = "Bearbeiten";
            AiModelDetail = await httpService.GetAsync<AIModelDetailDto>("/api/v1/AiModels/" + aiModelId) ?? new AIModelDetailDto();
        }
    }

    protected async Task Save()
    {
        if (aiModelId != 0)
        {
            if (AiModelDetail != null)
            {    
               await httpService.PostAsync<AIModelDetailDto, AIModelDetailDto>("/api/v1/AiModels/" + aiModelId, AiModelDetail);
            }
        }
        else
        {
            if (AiModelDetail != null) await httpService.PutAsync<AIModelDetailDto, AIModelDetailDto>("/api/v1/AiModels", AiModelDetail);
        }

        NavigateToList();
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/AiModels");
    }
}