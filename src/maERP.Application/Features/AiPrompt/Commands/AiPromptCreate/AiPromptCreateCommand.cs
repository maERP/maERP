using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;

public class AiPromptCreateCommand : AiPromptInputDto, IRequest<Result<Guid>>
{
}