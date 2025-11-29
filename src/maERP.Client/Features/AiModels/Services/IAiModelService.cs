using maERP.Client.Core.Models;
using maERP.Domain.Dtos.AiModel;

namespace maERP.Client.Features.AiModels.Services;

/// <summary>
/// Service interface for AI model-related API operations.
/// </summary>
public interface IAiModelService
{
    /// <summary>
    /// Gets a paginated list of AI models with full pagination metadata.
    /// </summary>
    Task<PaginatedResponse<AiModelListDto>> GetAiModelsAsync(
        QueryParameters parameters,
        CancellationToken ct = default);
}
