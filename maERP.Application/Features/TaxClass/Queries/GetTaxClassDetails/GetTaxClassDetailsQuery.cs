using maERP.Application.Dtos.TaxClass;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetTaxClassDetailQuery;

public class GetTaxClassDetailsQuery : IRequest<TaxClassDetailDto>
{
    public int Id { get; set; }
}