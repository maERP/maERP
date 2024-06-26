using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiModel;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModelsDetail
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IAiModelService _aiModelService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    protected string Title = "AI Model";

    protected AiModelVM aiModel = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId != 0)
        {
            Title = "Bearbeiten";
            aiModel = await _aiModelService.GetAiModelDetails(aiModelId);
        }
        else Title = "nicht gefunden";
    }
}