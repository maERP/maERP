using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptUpdate;

public class AiPromptUpdateCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public AiModelType AiModelType { get; set; } = AiModelType.None;
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}