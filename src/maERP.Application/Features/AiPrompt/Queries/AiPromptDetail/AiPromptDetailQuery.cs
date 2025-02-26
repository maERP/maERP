using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Queries.AiPromptDetail;

public class AiPromptDetailQuery : IRequest<Result<AiPromptDetailDto>>
{
    public int Id { get; set; }
}