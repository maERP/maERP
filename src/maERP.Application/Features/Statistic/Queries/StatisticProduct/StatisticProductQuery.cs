using maERP.Domain.Dtos.Statistic;
using MediatR;

namespace maERP.Application.Features.Statistic.Queries.StatisticProduct;

public record StatisticProductQuery : IRequest<StatisticProductDto>;