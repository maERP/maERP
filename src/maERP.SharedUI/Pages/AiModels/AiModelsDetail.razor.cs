using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.AiModel;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModelsDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IAiModelService AiModelService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    protected string Title = "AI Model";

    protected AiModelVm AiModel = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId != 0)
        {
            Title = "Bearbeiten";
            AiModel = await AiModelService.GetAiModelDetails(aiModelId);
        }
        else Title = "nicht gefunden";
    }
}