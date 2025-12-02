using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.ProductsToday;

public class ProductsTodayHandler : IRequestHandler<ProductsTodayQuery, Result<ProductsTodayDto>>
{
    private readonly IAppLogger<ProductsTodayHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductsTodayHandler(
        IAppLogger<ProductsTodayHandler> logger,
        IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<Result<ProductsTodayDto>> Handle(ProductsTodayQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle ProductsTodayQuery");

            var dto = new ProductsTodayDto();

            var now = DateTime.UtcNow;
            var todayStart = now.Date;
            var weekStart = todayStart.AddDays(-(int)todayStart.DayOfWeek);
            var lastWeekStart = weekStart.AddDays(-7);
            var lastWeekEnd = weekStart;

            // Product calculations
            dto.ProductsTotal = await _productRepository.Entities.CountAsync(cancellationToken);

            // Products in stock - products that have any stock > 0 in any warehouse
            dto.ProductsInStock = await _productRepository.Entities
                .Where(p => p.ProductStocks.Any(ps => ps.Stock > 0))
                .CountAsync(cancellationToken);

            // Products with low stock (below minimum stock level in any warehouse)
            dto.ProductsLowStock = await _productRepository.Entities
                .Where(p => p.ProductStocks.Any(ps => ps.Stock > 0 && ps.Stock <= ps.StockMin))
                .CountAsync(cancellationToken);

            // Products change - compare this week's new products to last week
            var productsThisWeek = await _productRepository.Entities
                .Where(p => p.DateCreated >= weekStart)
                .CountAsync(cancellationToken);

            var productsLastWeek = await _productRepository.Entities
                .Where(p => p.DateCreated >= lastWeekStart && p.DateCreated < lastWeekEnd)
                .CountAsync(cancellationToken);

            dto.ProductsChangePercent = productsLastWeek > 0
                ? ((decimal)(productsThisWeek - productsLastWeek) / productsLastWeek) * 100
                : productsThisWeek > 0 ? 100 : 0;

            return Result<ProductsTodayDto>.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while calculating product statistics: {0}", ex.Message);
            return Result<ProductsTodayDto>.Fail(ResultStatusCode.InternalServerError, "Error while calculating product statistics");
        }
    }
}
