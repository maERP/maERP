using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiModel;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModelsEdit
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IAiModelService _aiModelService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    protected AiModelVM aiModel = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId != 0)
        {
            Title = "Bearbeiten";
            aiModel = await _aiModelService.GetAiModelDetails(aiModelId);
        }
    }

    protected async Task Save()
    {
        if (aiModelId != 0)
        {
            await _aiModelService.UpdateAiModel(aiModelId, aiModel);
        }
        else
        {
            await _aiModelService.CreateAiModel(aiModel);
        }

        NavigateToList();
    }

    public void NavigateToList()
    {
        StateHasChanged();
        _navigationManager.NavigateTo("/AiModels");
    }
}