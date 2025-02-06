using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModelsDelete
{
    [Inject]
    public required NavigationManager navigationManager { get; set; }

    [Inject]
    public required IHttpService httpService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId > 0)
        {
            await httpService.DeleteAsync("/api/v1/AiModels/" + aiModelId);
            navigationManager.NavigateTo("/AiModels");
        }
    }
}