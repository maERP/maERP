using maERP.Application.Features.AiModel.Commands.AiModelCreate;
using maERP.Application.Features.AiModel.Commands.AiModelUpdate;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for AI model operations
/// </summary>
public interface IAIModelsApiClient
{
    /// <summary>
    /// Get paginated list of AI models
    /// </summary>
    Task<PaginatedResult<AiModelListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get AI model details by ID
    /// </summary>
    Task<AiModelDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new AI model
    /// </summary>
    Task<HttpResponseMessage> CreateAsync(AiModelCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing AI model
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, AiModelUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an AI model
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
