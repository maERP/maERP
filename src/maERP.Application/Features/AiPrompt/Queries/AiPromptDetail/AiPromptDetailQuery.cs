using MediatR;

namespace maERP.Application.Features.AiPrompt.Queries.AiPromptDetail;

public class AiPromptDetailQuery : IRequest<AiPromptDetailResponse>
{
    public int Id { get; set; }
}