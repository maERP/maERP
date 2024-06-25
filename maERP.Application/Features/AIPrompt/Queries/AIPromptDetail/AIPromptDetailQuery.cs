using MediatR;

namespace maERP.Application.Features.AIPrompt.Queries.AIPromptDetail;

public class AIPromptDetailQuery : IRequest<AIPromptDetailResponse>
{
    public int Id { get; set; }
}