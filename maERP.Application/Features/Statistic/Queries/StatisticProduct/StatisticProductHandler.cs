using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.StatisticProduct;

public class StatisticProductHandler : IRequestHandler<StatisticProductQuery, StatisticProductResponse>
{
    private readonly IAppLogger<StatisticProductHandler> _logger;
    private readonly IProductRepository _productRepository;

    public StatisticProductHandler(IAppLogger<StatisticProductHandler> logger,
        IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<StatisticProductResponse> Handle(StatisticProductQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handle StatisticProductQuery: {0}", request);
        
        var response = new StatisticProductResponse();

        response.ProductTotal = await _productRepository.Entities.CountAsync();
        response.ProductInStock = await _productRepository.Entities.CountAsync();
        
        return response;
    }
}