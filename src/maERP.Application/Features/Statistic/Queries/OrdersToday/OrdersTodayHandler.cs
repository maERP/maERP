using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.OrdersToday;

public class OrdersTodayHandler : IRequestHandler<OrdersTodayQuery, Result<OrdersTodayDto>>
{
    private readonly IAppLogger<OrdersTodayHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrdersTodayHandler(
        IAppLogger<OrdersTodayHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrdersTodayDto>> Handle(OrdersTodayQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle OrdersTodayQuery");

            var dto = new OrdersTodayDto();

            var now = DateTime.UtcNow;
            var todayStart = now.Date;
            var weekStart = todayStart.AddDays(-(int)todayStart.DayOfWeek);
            var lastWeekStart = weekStart.AddDays(-7);
            var lastWeekEnd = weekStart;

            var baseQuery = _orderRepository.Entities.AsQueryable();
            if (request.SalesChannelId.HasValue)
                baseQuery = baseQuery.Where(o => o.SalesChannelId == request.SalesChannelId.Value);

            // Order calculations
            dto.OrdersToday = await baseQuery
                .Where(o => o.DateOrdered >= todayStart)
                .CountAsync(cancellationToken);

            dto.OrdersPending = await baseQuery
                .Where(o => o.Status == OrderStatus.Pending || o.Status == OrderStatus.Processing)
                .CountAsync(cancellationToken);

            dto.OrdersThisWeek = await baseQuery
                .Where(o => o.DateOrdered >= weekStart)
                .CountAsync(cancellationToken);

            // Orders change compared to last week
            var ordersLastWeek = await baseQuery
                .Where(o => o.DateOrdered >= lastWeekStart && o.DateOrdered < lastWeekEnd)
                .CountAsync(cancellationToken);

            dto.OrdersChangePercent = ordersLastWeek > 0
                ? ((decimal)(dto.OrdersThisWeek - ordersLastWeek) / ordersLastWeek) * 100
                : dto.OrdersThisWeek > 0 ? 100 : 0;

            return Result<OrdersTodayDto>.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while calculating orders statistics: {0}", ex.Message);
            return Result<OrdersTodayDto>.Fail(ResultStatusCode.InternalServerError, "Error while calculating orders statistics");
        }
    }
}
