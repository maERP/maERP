using maERP.Application.Dtos.TaxClass;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetTaxClassesQuery;

public record GetTaxClassesQuery : IRequest<List<TaxClassListDto>>;