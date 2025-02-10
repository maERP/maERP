using maERP.Domain.Dtos.AiModel;
using MediatR;

namespace maERP.Application.Features.AiModel.Queries.AiModelDetail;

public class AiModelDetailQuery : IRequest<AiModelDetailDto>
{
    public int Id { get; set; }
}