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
    private readonly IOrderRepository _orderRepository;

    public SalesTodayHandler(
        IAppLogger<SalesTodayHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
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

            // Revenue calculations
            dto.RevenueToday = await _orderRepository.Entities
                .Where(o => o.DateOrdered >= todayStart)
                .SumAsync(o => o.Total, cancellationToken);

            dto.RevenueThisWeek = await _orderRepository.Entities
                .Where(o => o.DateOrdered >= weekStart)
                .SumAsync(o => o.Total, cancellationToken);

            dto.RevenueThisMonth = await _orderRepository.Entities
                .Where(o => o.DateOrdered >= monthStart)
                .SumAsync(o => o.Total, cancellationToken);

            // Calculate revenue change compared to last week's same day
            var lastWeekSameDayStart = todayStart.AddDays(-7);
            var lastWeekSameDayEnd = lastWeekSameDayStart.AddDays(1);
            var revenueLastWeekSameDay = await _orderRepository.Entities
                .Where(o => o.DateOrdered >= lastWeekSameDayStart && o.DateOrdered < lastWeekSameDayEnd)
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
