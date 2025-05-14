using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.StatisticMostSellingProducts;

public class StatisticMostSellingProductsHandler : IRequestHandler<StatisticMostSellingProductsQuery, Result<StatisticMostSellingProductsDto>>
{
    private readonly IAppLogger<StatisticMostSellingProductsHandler> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public StatisticMostSellingProductsHandler(
        IAppLogger<StatisticMostSellingProductsHandler> logger,
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<StatisticMostSellingProductsDto>> Handle(StatisticMostSellingProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle StatisticMostSellingProductsQuery: {0}", request);
            
            var statisticDto = new StatisticMostSellingProductsDto();
            
            // Zeiträume definieren
            var today = DateTime.UtcNow.Date;
            var sevenDaysAgo = today.AddDays(-7);
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var firstDayOfYear = new DateTime(today.Year, 1, 1);
            
            // Die 10 meistverkauften Produkte von heute abrufen
            statisticDto.TopProductsToday = await GetTopSellingProducts(today, today.AddDays(1), cancellationToken);
            
            // Die 10 meistverkauften Produkte der letzten 7 Tage abrufen
            statisticDto.TopProductsLastSevenDays = await GetTopSellingProducts(sevenDaysAgo, today.AddDays(1), cancellationToken);
            
            // Die 10 meistverkauften Produkte dieses Monats abrufen
            statisticDto.TopProductsThisMonth = await GetTopSellingProducts(firstDayOfMonth, today.AddDays(1), cancellationToken);
            
            // Die 10 meistverkauften Produkte dieses Jahres abrufen
            statisticDto.TopProductsThisYear = await GetTopSellingProducts(firstDayOfYear, today.AddDays(1), cancellationToken);
            
            // Die 10 meistverkauften Produkte aller Zeiten abrufen
            statisticDto.TopProductsAllTime = await GetTopSellingProducts(null, null, cancellationToken);
            
            return Result<StatisticMostSellingProductsDto>.Success(statisticDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fehler beim Ermitteln der meistverkauften Produkte: {0}", ex.Message);
            return Result<StatisticMostSellingProductsDto>.Fail(
                ResultStatusCode.InternalServerError, 
                "Fehler beim Ermitteln der meistverkauften Produkte");
        }
    }
    
    private async Task<List<MostSellingProductItem>> GetTopSellingProducts(
        DateTime? startDate, 
        DateTime? endDate, 
        CancellationToken cancellationToken)
    {
        var query = _orderRepository.Entities
            .Include(o => o.OrderItems)
            .Where(o => o.OrderItems.Any());
        
        // Zeitraum-Filter anwenden, falls vorhanden
        if (startDate.HasValue)
        {
            query = query.Where(o => o.DateOrdered >= startDate.Value);
        }
        
        if (endDate.HasValue)
        {
            query = query.Where(o => o.DateOrdered < endDate.Value);
        }
        
        // Bestellungen mit ihren Positionen abrufen und nach Produkt gruppieren
        var topProducts = await query
            .SelectMany(o => o.OrderItems)
            .GroupBy(oi => new { oi.ProductId })
            .Select(g => new
            {
                ProductId = g.Key.ProductId,
                TotalQuantity = g.Sum(oi => oi.Quantity)
            })
            .OrderByDescending(x => x.TotalQuantity)
            .Take(10)
            .ToListAsync(cancellationToken);
        
        // Produkt-Informationen anreichern
        var result = new List<MostSellingProductItem>();
        
        foreach (var product in topProducts)
        {
            var productInfo = await _productRepository.GetByIdAsync(product.ProductId);
            
            if (productInfo != null)
            {
                result.Add(new MostSellingProductItem
                {
                    ProductId = product.ProductId,
                    ProductName = productInfo.Name,
                    ProductSku = productInfo.Sku,
                    TotalQuantity = product.TotalQuantity
                });
            }
        }
        
        return result;
    }
} 