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
}
