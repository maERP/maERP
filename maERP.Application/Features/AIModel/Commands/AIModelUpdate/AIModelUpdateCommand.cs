using MediatR;

namespace maERP.Application.Features.AIModel.Commands.AIModelUpdate;

public class AIModelUpdateCommand : IRequest<int>
{
    public int Id { get; set; }     
    public string Name { get; set; } = string.Empty;
}