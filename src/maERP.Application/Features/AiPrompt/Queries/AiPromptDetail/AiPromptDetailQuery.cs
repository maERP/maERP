using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiPrompt.Queries.AiPromptDetail;

public class AiPromptDetailQuery : IRequest<Result<AiPromptDetailDto>>
{
    public Guid Id { get; set; }
}