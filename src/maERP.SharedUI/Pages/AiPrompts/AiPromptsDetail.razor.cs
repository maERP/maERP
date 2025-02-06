using maERP.Domain.Dtos.AiPrompt;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.AiPrompts;

public partial class AiPromptsDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int aiPromptId { get; set; }

    protected string Title = "AI Model";

    protected AiPromptDetailDto aiPromptDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        if (aiPromptId != 0)
        {
            Title = "Bearbeiten";
            aiPromptDetail = await HttpService.GetAsync<AiPromptDetailDto>("/api/v1/AiPrompts/" + aiPromptId) ?? new AiPromptDetailDto();
        }
        else Title = "nicht gefunden";
    }
}