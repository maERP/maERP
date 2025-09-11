using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.AiModel.Commands.AiModelDelete;

public class AiModelDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}