using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiModels;

public partial class AiModelsDetail
{
    [Inject]
    public required NavigationManager navigationManager { get; set; }

    [Inject]
    public required IHttpService httpService { get; set; }

    [Parameter]
    public int aiModelId { get; set; }

    protected string Title = "AI Model";

    protected AIModelDetailDto? aiModel = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId != 0)
        {
            Title = "Bearbeiten";
            aiModel = await httpService.GetAsync<AIModelDetailDto>("/api/v1/AiModels/" + aiModelId);
        }
        else Title = "nicht gefunden";
    }
}