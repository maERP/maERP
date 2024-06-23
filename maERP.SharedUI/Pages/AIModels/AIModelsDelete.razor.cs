using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AIModels;

public partial class AIModelsDelete
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IAIModelService _aiModelService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId > 0)
        {
            await _aiModelService.DeleteAIModel(aiModelId);
            _navigationManager.NavigateTo("/AIModels");
        }
    }
}