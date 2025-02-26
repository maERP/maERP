using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiModel.Commands.AiModelDelete;

public class AiModelDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }     
}