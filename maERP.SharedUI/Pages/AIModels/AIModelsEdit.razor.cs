using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AIModel;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AIModels;

public partial class AIModelsEdit
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IAIModelService _aiModelService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    protected AIModelVM aiModel = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId != 0)
        {
            Title = "Bearbeiten";
            aiModel = await _aiModelService.GetAIModelDetails(aiModelId);
        }
    }

    protected async Task Save()
    {
        if (aiModelId != 0)
        {
            await _aiModelService.UpdateAIModel(aiModelId, aiModel);
        }
        else
        {
            await _aiModelService.CreateAIModel(aiModel);
        }

        NavigateToList();
    }

    public void NavigateToList()
    {
        StateHasChanged();
        _navigationManager.NavigateTo("/AIModels");
    }
}