using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.StatisticProduct;

public class StatisticProductHandler : IRequestHandler<StatisticProductQuery, Result<StatisticProductDto>>
{
    private readonly IAppLogger<StatisticProductHandler> _logger;
    private readonly IProductRepository _productRepository;

    public StatisticProductHandler(IAppLogger<StatisticProductHandler> logger,
        IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<Result<StatisticProductDto>> Handle(StatisticProductQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle StatisticProductQuery: {0}", request);
            
            var statisticDto = new StatisticProductDto();

            statisticDto.ProductTotal = await _productRepository.Entities.CountAsync();

            statisticDto.ProductInStock = await _productRepository.Entities
                .Where(p => p.ProductStocks.Any(w => w.Stock > 0))
                .CountAsync();

            statisticDto.ProductInWarehouse = await _productRepository.Entities
                .Where(p => p.ProductStocks.Any(w => w.Stock > 0))
                .SumAsync(p => p.ProductStocks.Sum(w => w.Stock));
               
            return Result<StatisticProductDto>.Success(statisticDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fehler beim Ermitteln der Produktstatistik: {0}", ex.Message);
            return Result<StatisticProductDto>.Fail(ResultStatusCode.InternalServerError, "Fehler beim Ermitteln der Produktstatistik");
        }
    }
}