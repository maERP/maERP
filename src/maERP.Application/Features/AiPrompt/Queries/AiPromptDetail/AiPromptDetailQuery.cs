using maERP.Domain.Dtos.AiPrompt;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Queries.AiPromptDetail;

public class AiPromptDetailQuery : IRequest<AiPromptDetailDto>
{
    public int Id { get; set; }
}