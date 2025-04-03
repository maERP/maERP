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

    protected AiModelDetailDto AiModel = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiModelId != 0)
        {
            var result = await httpService.GetAsync<Result<AiModelDetailDto>>($"/api/v1/AiModels/{aiModelId}");
            
            if (result != null && result.Succeeded)
            {
                AiModel = result.Data;
            }
            else if(result != null && result.StatusCode == ResultStatusCode.NotFound)
            {
                Title = "nicht gefunden";
            }
        }
        else 
        {
            Title = "nicht gefunden";
        }
    }
}