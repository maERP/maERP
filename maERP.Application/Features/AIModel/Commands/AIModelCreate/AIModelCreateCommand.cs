using MediatR;

namespace maERP.Application.Features.AIModel.Commands.AIModelCreate;

public class AIModelCreateCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;     
}