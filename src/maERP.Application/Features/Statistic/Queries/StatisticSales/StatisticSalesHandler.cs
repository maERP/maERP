using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.StatisticSales;

public class StatisticSalesHandler : IRequestHandler<StatisticSalesQuery, Result<StatisticSalesDto>>
{
    private readonly IAppLogger<StatisticSalesHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public StatisticSalesHandler(IAppLogger<StatisticSalesHandler> logger,
        IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task<Result<StatisticSalesDto>> Handle(StatisticSalesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle StatisticSalesQuery: {0}", request);
            
            var statisticDto = new StatisticSalesDto();
            
            var now = DateTime.UtcNow;
            var hours24Ago = now.AddHours(-24);
            var days7Ago = now.AddDays(-7);
            var days30Ago = now.AddDays(-30);
            var days365Ago = now.AddDays(-365);

            // Berechnung des Umsatzes der letzten 24 Stunden
            statisticDto.Sales24Hours = await _orderRepository.Entities
                .Where(o => o.DateOrdered >= hours24Ago)
                .SumAsync(o => o.Total, cancellationToken);

            // Berechnung des Umsatzes der letzten 7 Tage
            statisticDto.Sales7Days = await _orderRepository.Entities
                .Where(o => o.DateOrdered >= days7Ago)
                .SumAsync(o => o.Total, cancellationToken);

            // Berechnung des Umsatzes der letzten 30 Tage
            statisticDto.Sales30Days = await _orderRepository.Entities
                .Where(o => o.DateOrdered >= days30Ago)
                .SumAsync(o => o.Total, cancellationToken);

            // Berechnung des Umsatzes der letzten 365 Tage
            statisticDto.Sales365Days = await _orderRepository.Entities
                .Where(o => o.DateOrdered >= days365Ago)
                .SumAsync(o => o.Total, cancellationToken);
            
            return Result<StatisticSalesDto>.Success(statisticDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fehler beim Ermitteln der Umsatzstatistik: {0}", ex.Message);
            return Result<StatisticSalesDto>.Fail(ResultStatusCode.InternalServerError, "Fehler beim Ermitteln der Umsatzstatistik");
        }
    }
} 