using MediatR;

namespace maERP.Application.Features.Ai.Commands.AiMessage;

public class AiMessageCommand : IRequest<string>
{
    public string PromptText { get; set; } = string.Empty;
}