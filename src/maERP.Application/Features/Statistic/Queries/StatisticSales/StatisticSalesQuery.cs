using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Statistic.Queries.StatisticSales;

public record StatisticSalesQuery : IRequest<Result<StatisticSalesDto>>;