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

    /// <summary>
    /// Gets a single AI model by its ID.
    /// </summary>
    Task<AiModelDetailDto?> GetAiModelAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Creates a new AI model.
    /// </summary>
    Task<Guid> CreateAiModelAsync(AiModelInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing AI model.
    /// </summary>
    Task UpdateAiModelAsync(Guid id, AiModelInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Deletes an AI model by its ID.
    /// </summary>
    Task DeleteAiModelAsync(Guid id, CancellationToken ct = default);
}
