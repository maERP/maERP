using maERP.Application.Dtos;
using maERP.Application.Dtos.TaxClass;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetAllTaxClassesQuery;

public record GetAllTaxClassesQuery : IRequest<List<TaxClassListDto>>;