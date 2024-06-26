using MediatR;

namespace maERP.Application.Features.AiModel.Queries.AiModelDetail;

public class AiModelDetailQuery : IRequest<AiModelDetailResponse>
{
    public int Id { get; set; }
}