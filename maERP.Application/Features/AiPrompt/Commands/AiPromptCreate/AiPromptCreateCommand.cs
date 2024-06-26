using maERP.Domain.Enums;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;

public class AiPromptCreateCommand : IRequest<int>
{
    public AiModelType AiModelType { get; set; } = AiModelType.None;
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}