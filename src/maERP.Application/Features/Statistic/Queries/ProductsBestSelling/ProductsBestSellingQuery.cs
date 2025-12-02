using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Statistic.Queries.ProductsBestSelling;

public record ProductsBestSellingQuery(int Count = 5) : IRequest<Result<ProductsBestSellingDto>>;
