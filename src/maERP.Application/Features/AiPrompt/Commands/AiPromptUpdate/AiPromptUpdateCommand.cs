using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;

public class AiPromptUpdateCommand : AiPromptInputDto, IRequest<Result<Guid>>
{
}