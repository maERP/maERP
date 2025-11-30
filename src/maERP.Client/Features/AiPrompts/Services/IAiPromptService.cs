using maERP.Client.Core.Models;
using maERP.Domain.Dtos.AiPrompt;

namespace maERP.Client.Features.AiPrompts.Services;

/// <summary>
/// Service interface for AI prompt-related API operations.
/// </summary>
public interface IAiPromptService
{
    /// <summary>
    /// Gets a paginated list of AI prompts with full pagination metadata.
    /// </summary>
    Task<PaginatedResponse<AiPromptListDto>> GetAiPromptsAsync(
        QueryParameters parameters,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a single AI prompt by its ID.
    /// </summary>
    Task<AiPromptDetailDto?> GetAiPromptAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Creates a new AI prompt.
    /// </summary>
    Task<Guid> CreateAiPromptAsync(AiPromptInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing AI prompt.
    /// </summary>
    Task UpdateAiPromptAsync(Guid id, AiPromptInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Deletes an AI prompt by its ID.
    /// </summary>
    Task DeleteAiPromptAsync(Guid id, CancellationToken ct = default);
}
