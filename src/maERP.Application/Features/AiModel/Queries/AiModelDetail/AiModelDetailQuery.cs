using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiModel.Queries.AiModelDetail;

/// <summary>
/// Query for retrieving detailed information about a specific AI model.
/// Implements IRequest to work with MediatR, returning AI model details wrapped in a Result.
/// </summary>
public class AiModelDetailQuery : IRequest<Result<AiModelDetailDto>>
{
    /// <summary>
    /// The unique identifier of the AI model to retrieve
    /// </summary>
    public int Id { get; set; }
}