using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.TaxClassList;

public record TaxClassListQuery : IRequest<List<TaxClassListResponse>>;