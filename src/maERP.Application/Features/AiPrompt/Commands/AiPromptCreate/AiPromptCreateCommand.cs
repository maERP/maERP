using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;

public class AiPromptCreateCommand : AiPromptCreateDto, IRequest<Result<int>>
{
}