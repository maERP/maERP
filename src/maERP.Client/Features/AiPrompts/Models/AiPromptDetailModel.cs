using maERP.Client.Features.AiPrompts.Services;
using maERP.Domain.Dtos.AiPrompt;

namespace maERP.Client.Features.AiPrompts.Models;

/// <summary>
/// Data record for passing AI prompt ID to the detail page.
/// </summary>
public record AiPromptDetailData(Guid AiPromptId);

/// <summary>
/// Model for AI prompt detail page using MVUX pattern.
/// Receives AI prompt ID from navigation data.
/// </summary>
public partial record AiPromptDetailModel
{
    private readonly IAiPromptService _aiPromptService;
    private readonly INavigator _navigator;
    private readonly Guid _aiPromptId;

    public AiPromptDetailModel(
        IAiPromptService aiPromptService,
        INavigator navigator,
        AiPromptDetailData data)
    {
        _aiPromptService = aiPromptService;
        _navigator = navigator;
        _aiPromptId = data.AiPromptId;
    }

    /// <summary>
    /// Feed that loads the AI prompt details.
    /// </summary>
    public IFeed<AiPromptDetailDto> AiPrompt => Feed.Async(async ct =>
    {
        var aiPrompt = await _aiPromptService.GetAiPromptAsync(_aiPromptId, ct);
        return aiPrompt ?? throw new InvalidOperationException($"AI prompt {_aiPromptId} not found");
    });

    /// <summary>
    /// Navigate to edit AI prompt page.
    /// </summary>
    public async Task EditAiPrompt()
    {
        await _navigator.NavigateDataAsync(this, new AiPromptEditData(_aiPromptId));
    }

    /// <summary>
    /// Navigate back to AI prompt list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
