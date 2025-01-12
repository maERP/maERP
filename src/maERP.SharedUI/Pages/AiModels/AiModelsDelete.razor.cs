using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModelsDelete
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IAiModelService _aiModelService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId > 0)
        {
            await _aiModelService.DeleteAiModel(aiModelId);
            _navigationManager.NavigateTo("/AiModels");
        }
    }
}