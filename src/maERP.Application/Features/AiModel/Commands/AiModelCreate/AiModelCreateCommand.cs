using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiModel.Commands.AiModelCreate;

/// <summary>
/// Command for creating a new AI model in the system.
/// Inherits from AiModelInputDto to get all AI model properties and implements IRequest
/// to work with MediatR, returning the ID of the newly created AI model wrapped in a Result.
/// </summary>
public class AiModelCreateCommand : AiModelInputDto, IRequest<Result<int>>
{
}