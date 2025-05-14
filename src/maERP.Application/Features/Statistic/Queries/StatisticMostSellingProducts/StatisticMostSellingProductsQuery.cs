using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Statistic.Queries.StatisticMostSellingProducts;

public record StatisticMostSellingProductsQuery : IRequest<Result<StatisticMostSellingProductsDto>>;