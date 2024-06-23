using MediatR;

namespace maERP.Application.Features.AIModel.Queries.AIModelDetail;

public class AIModelDetailQuery : IRequest<AIModelDetailResponse>
{
    public int Id { get; set; }
}