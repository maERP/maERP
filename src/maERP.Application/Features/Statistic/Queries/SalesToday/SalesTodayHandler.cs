using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.SalesToday;

public class SalesTodayHandler : IRequestHandler<SalesTodayQuery, Result<SalesTodayDto>>
{
    private readonly IAppLogger<SalesTodayHandler> _logger;
    private readonly ISalesRepository _salesRepository;

    public SalesTodayHandler(
        IAppLogger<SalesTodayHandler> logger,
        ISalesRepository salesRepository)
    {
        _logger = logger;
        _salesRepository = salesRepository;
    }

    public async Task<Result<SalesTodayDto>> Handle(SalesTodayQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle SalesTodayQuery");

            var dto = new SalesTodayDto();

            var now = DateTime.UtcNow;
            var todayStart = now.Date;
            var weekStart = todayStart.AddDays(-(int)todayStart.DayOfWeek);
            var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            var baseQuery = _salesRepository.Entities.AsQueryable();
            if (request.SalesChannelId.HasValue)
                baseQuery = baseQuery.Where(o => o.SalesChannelId == request.SalesChannelId.Value);

            // Revenue calculations
            dto.RevenueToday = await baseQuery
                .Where(o => o.DateSalesed >= todayStart)
                .SumAsync(o => o.Total, cancellationToken);

            dto.RevenueThisWeek = await baseQuery
                .Where(o => o.DateSalesed >= weekStart)
                .SumAsync(o => o.Total, cancellationToken);

            dto.RevenueThisMonth = await baseQuery
                .Where(o => o.DateSalesed >= monthStart)
                .SumAsync(o => o.Total, cancellationToken);

            // Calculate revenue change compared to last week's same day
            var lastWeekSameDayStart = todayStart.AddDays(-7);
            var lastWeekSameDayEnd = lastWeekSameDayStart.AddDays(1);
            var revenueLastWeekSameDay = await baseQuery
                .Where(o => o.DateSalesed >= lastWeekSameDayStart && o.DateSalesed < lastWeekSameDayEnd)
                .SumAsync(o => o.Total, cancellationToken);

            dto.RevenueChangePercent = revenueLastWeekSameDay > 0
                ? ((dto.RevenueToday - revenueLastWeekSameDay) / revenueLastWeekSameDay) * 100
                : dto.RevenueToday > 0 ? 100 : 0;

            return Result<SalesTodayDto>.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while calculating sales statistics: {0}", ex.Message);
            return Result<SalesTodayDto>.Fail(ResultStatusCode.InternalServerError, "Error while calculating sales statistics");
        }
    }
}
