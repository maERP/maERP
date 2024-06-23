using MediatR;

namespace maERP.Application.Features.AIModel.Commands.AIModelDelete;

public class AIModelDeleteCommand : IRequest<int>
{
    public int Id { get; set; }     
}