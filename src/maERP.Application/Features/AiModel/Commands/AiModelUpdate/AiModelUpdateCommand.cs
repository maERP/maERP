using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiModel.Commands.AiModelUpdate;

public class AiModelUpdateCommand : AiModelUpdateDto, IRequest<Result<int>>
{
}