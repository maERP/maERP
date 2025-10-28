using maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;
using maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for AI prompt operations
/// </summary>
public interface IAIPromptsApiClient
{
    /// <summary>
    /// Get paginated list of AI prompts
    /// </summary>
    Task<PaginatedResult<AiPromptListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get AI prompt details by ID
    /// </summary>
    Task<AiPromptDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new AI prompt
    /// </summary>
    Task<HttpResponseMessage> CreateAsync(AiPromptCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing AI prompt
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, AiPromptUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an AI prompt
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
