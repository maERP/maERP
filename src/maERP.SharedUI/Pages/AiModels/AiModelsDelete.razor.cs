using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModelsDelete
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IAiModelService AiModelService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId > 0)
        {
            await AiModelService.DeleteAiModel(aiModelId);
            NavigationManager.NavigateTo("/AiModels");
        }
    }
}