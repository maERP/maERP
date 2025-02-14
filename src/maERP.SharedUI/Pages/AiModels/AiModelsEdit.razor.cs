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

    public AiModelDetailDto AiModelDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId != 0)
        {
            Title = "Bearbeiten";
            AiModelDetail = await httpService.GetAsync<AiModelDetailDto>("/api/v1/AiModels/" + aiModelId) ?? new AiModelDetailDto();
        }
    }

    protected async Task Save()
    {
        if (aiModelId != 0)
        {
            await httpService.PostAsync<AiModelDetailDto, AiModelDetailDto>("/api/v1/AiModels/" + aiModelId, AiModelDetail);
        }
        else
        {
            await httpService.PutAsync<AiModelDetailDto, AiModelDetailDto>("/api/v1/AiModels", AiModelDetail);
        }

        NavigateToList();
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/AiModels");
    }
}