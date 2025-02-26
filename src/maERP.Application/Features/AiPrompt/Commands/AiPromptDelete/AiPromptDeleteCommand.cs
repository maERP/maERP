using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiPrompt.Commands.AiPromptDelete;

public class AiPromptDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }     
}