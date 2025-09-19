using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptDelete;

public class AiPromptDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}