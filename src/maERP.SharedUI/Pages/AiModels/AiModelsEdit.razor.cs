using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiModel;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModelsEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IAiModelService AiModelService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    protected AiModelVm AiModel = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId != 0)
        {
            Title = "Bearbeiten";
            AiModel = await AiModelService.GetAiModelDetails(aiModelId);
        }
    }

    protected async Task Save()
    {
        if (aiModelId != 0)
        {
            await AiModelService.UpdateAiModel(aiModelId, AiModel);
        }
        else
        {
            await AiModelService.CreateAiModel(AiModel);
        }

        NavigateToList();
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/AiModels");
    }
}