using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.SalessToday;

public class SalessTodayHandler : IRequestHandler<SalessTodayQuery, Result<SalessTodayDto>>
{
    private readonly IAppLogger<SalessTodayHandler> _logger;
    private readonly ISalesRepository _salesRepository;

    public SalessTodayHandler(
        IAppLogger<SalessTodayHandler> logger,
        ISalesRepository salesRepository)
    {
        _logger = logger;
        _salesRepository = salesRepository;
    }

    public async Task<Result<SalessTodayDto>> Handle(SalessTodayQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle SalessTodayQuery");

            var dto = new SalessTodayDto();

            var now = DateTime.UtcNow;
            var todayStart = now.Date;
            var weekStart = todayStart.AddDays(-(int)todayStart.DayOfWeek);
            var lastWeekStart = weekStart.AddDays(-7);
            var lastWeekEnd = weekStart;

            var baseQuery = _salesRepository.Entities.AsQueryable();
            if (request.SalesChannelId.HasValue)
                baseQuery = baseQuery.Where(o => o.SalesChannelId == request.SalesChannelId.Value);

            // Sales calculations
            dto.SalessToday = await baseQuery
                .Where(o => o.DateSalesed >= todayStart)
                .CountAsync(cancellationToken);

            dto.SalessPending = await baseQuery
                .Where(o => o.Status == SalesStatus.Pending || o.Status == SalesStatus.Processing)
                .CountAsync(cancellationToken);

            dto.SalessThisWeek = await baseQuery
                .Where(o => o.DateSalesed >= weekStart)
                .CountAsync(cancellationToken);

            // Saless change compared to last week
            var salessLastWeek = await baseQuery
                .Where(o => o.DateSalesed >= lastWeekStart && o.DateSalesed < lastWeekEnd)
                .CountAsync(cancellationToken);

            dto.SalessChangePercent = salessLastWeek > 0
                ? ((decimal)(dto.SalessThisWeek - salessLastWeek) / salessLastWeek) * 100
                : dto.SalessThisWeek > 0 ? 100 : 0;

            return Result<SalessTodayDto>.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while calculating saless statistics: {0}", ex.Message);
            return Result<SalessTodayDto>.Fail(ResultStatusCode.InternalServerError, "Error while calculating saless statistics");
        }
    }
}
