using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiModel.Queries.AiModelDetail;

public class AiModelDetailQuery : IRequest<Result<AiModelDetailDto>>
{
    public int Id { get; set; }
}