using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;

public class AiPromptUpdateCommand : AiPromptUpdateDto, IRequest<Result<int>>
{
}