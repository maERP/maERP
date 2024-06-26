using maERP.Domain.Enums;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;

public class AiPromptUpdateCommand : IRequest<int>
{
    public int Id { get; set; }
    public AiModelType AiModelType { get; set; } = AiModelType.None;
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}