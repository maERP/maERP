using maERP.Application.Dtos.TaxClass;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetTaxClassQuery;

public class GetTaxClassQuery : IRequest<TaxClassDetailDto>
{
    public int Id { get; set; }
}