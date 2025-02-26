using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptCreate;

public class AiPromptCreateCommand : IRequest<Result<int>>
{
    public AiModelType AiModelType { get; set; } = AiModelType.None;
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}