using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiModel.Commands.AiModelCreate;

public class AiModelCreateCommand : AiModelInputDto, IRequest<Result<int>>
{
}