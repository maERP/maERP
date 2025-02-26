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

    protected AiModelDetailDto? AiModel = new();
    
    protected Result<AiModelDetailDto> AiModelResult = new();
    
    protected bool IsLoading = true;
    protected string? ErrorMessage;

    protected override async Task OnParametersSetAsync()
    {
        IsLoading = true;
        ErrorMessage = null;
        
        if (aiModelId != 0)
        {
            var result = await httpService.GetAsync<Result<AiModelDetailDto>>($"/api/v1/AiModels/{aiModelId}");
            
            if (result != null && result.Succeeded)
            {
                AiModel = result.Data;
            }
            else if(result != null && result.StatusCode == ResultStatusCode.NotFound)
            {
                ErrorMessage = $"AI Model with ID {aiModelId} not found";
                Title = "nicht gefunden"; 
            }
        }
        else 
        {
            Title = "nicht gefunden";
            ErrorMessage = "Invalid AI Model ID";
        }
        
        IsLoading = false;
    }
}