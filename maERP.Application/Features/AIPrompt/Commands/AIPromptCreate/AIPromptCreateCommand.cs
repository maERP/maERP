using maERP.Domain.Enums;
using MediatR;

namespace maERP.Application.Features.AIPrompt.Commands.AIPromptCreate;

public class AIPromptCreateCommand : IRequest<int>
{
    public AIModelType AiModelType { get; set; } = AIModelType.None;
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}