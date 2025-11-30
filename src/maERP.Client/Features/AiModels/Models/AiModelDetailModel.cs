using maERP.Client.Features.AiModels.Services;
using maERP.Domain.Dtos.AiModel;

namespace maERP.Client.Features.AiModels.Models;

/// <summary>
/// Data record for passing AI model ID to the detail page.
/// </summary>
public record AiModelDetailData(Guid AiModelId);

/// <summary>
/// Model for AI model detail page using MVUX pattern.
/// Receives AI model ID from navigation data.
/// </summary>
public partial record AiModelDetailModel
{
    private readonly IAiModelService _aiModelService;
    private readonly INavigator _navigator;
    private readonly Guid _aiModelId;

    public AiModelDetailModel(
        IAiModelService aiModelService,
        INavigator navigator,
        AiModelDetailData data)
    {
        _aiModelService = aiModelService;
        _navigator = navigator;
        _aiModelId = data.AiModelId;
    }

    /// <summary>
    /// Feed that loads the AI model details.
    /// </summary>
    public IFeed<AiModelDetailDto> AiModel => Feed.Async(async ct =>
    {
        var aiModel = await _aiModelService.GetAiModelAsync(_aiModelId, ct);
        return aiModel ?? throw new InvalidOperationException($"AI model {_aiModelId} not found");
    });

    /// <summary>
    /// Navigate to edit AI model page.
    /// </summary>
    public async Task EditAiModel()
    {
        await _navigator.NavigateDataAsync(this, new AiModelEditData(_aiModelId));
    }

    /// <summary>
    /// Navigate back to AI model list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
