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
    public int AiModelId { get; set; }

    private string _title = "AI Model";

    private AiModelDetailDto _aiModelDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        if (AiModelId != 0)
        {
            var result = await httpService.GetAsync<Result<AiModelDetailDto>>($"/api/v1/AiModels/{AiModelId}");
            
            if (result != null && result.Succeeded)
            {
                _title = $"AI Model - {_aiModelDetail.Name}";
                _aiModelDetail = result.Data;
            }
            else if(result != null && result.StatusCode == ResultStatusCode.NotFound)
            {
                _title = "nicht gefunden";
            }
        }
        else 
        {
            _title = "nicht gefunden";
        }
    }
}