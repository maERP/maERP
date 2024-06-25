using maERP.Domain.Enums;
using MediatR;

namespace maERP.Application.Features.AIPrompt.Commands.AIPromptUpdate;

public class AIPromptUpdateCommand : IRequest<int>
{
    public int Id { get; set; }
    public AIModelType AiModelType { get; set; } = AIModelType.None;
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}