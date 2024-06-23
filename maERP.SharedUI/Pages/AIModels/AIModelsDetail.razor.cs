using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AIModel;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AIModels;

public partial class AIModelsDetail
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IAIModelService _aiModelService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    protected string Title = "AI Model";

    protected AIModelVM aiModel = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId != 0)
        {
            Title = "Bearbeiten";
            aiModel = await _aiModelService.GetAIModelDetails(aiModelId);
        }
        else Title = "nicht gefunden";
    }
}