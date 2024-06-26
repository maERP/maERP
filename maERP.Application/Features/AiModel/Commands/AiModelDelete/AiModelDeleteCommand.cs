using MediatR;

namespace maERP.Application.Features.AiModel.Commands.AiModelDelete;

public class AiModelDeleteCommand : IRequest<int>
{
    public int Id { get; set; }     
}