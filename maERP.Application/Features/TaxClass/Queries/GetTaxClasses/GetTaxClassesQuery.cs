using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetTaxClasses;

public record GetTaxClassesQuery : IRequest<List<GetTaxClassesResponse>>;