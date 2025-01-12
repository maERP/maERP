using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptDelete;

public class AiPromptDeleteCommand : IRequest<int>
{
    public int Id { get; set; }     
}