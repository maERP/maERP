using MediatR;

namespace maERP.Application.Features.AIPrompt.Commands.AIPromptDelete;

public class AIPromptDeleteCommand : IRequest<int>
{
    public int Id { get; set; }     
}