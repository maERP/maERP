using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetTaxClassDetail;

public class GetTaxClassDetailQuery : IRequest<GetTaxClassDetailResponse>
{
    public int Id { get; set; }
}