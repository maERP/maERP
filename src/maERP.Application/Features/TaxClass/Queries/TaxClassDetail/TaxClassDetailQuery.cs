using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.TaxClassDetail;

public class TaxClassDetailQuery : IRequest<TaxClassDetailResponse>
{
    public int Id { get; set; }
}