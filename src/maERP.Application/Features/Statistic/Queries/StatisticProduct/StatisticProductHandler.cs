﻿using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.StatisticProduct;

public class StatisticProductHandler : IRequestHandler<StatisticProductQuery, StatisticProductDto>
{
    private readonly IAppLogger<StatisticProductHandler> _logger;
    private readonly IProductRepository _productRepository;

    public StatisticProductHandler(IAppLogger<StatisticProductHandler> logger,
        IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<StatisticProductDto> Handle(StatisticProductQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handle StatisticProductQuery: {0}", request);
        
        var response = new StatisticProductDto();

        response.ProductTotal = await _productRepository.Entities.CountAsync();

        response.ProductInStock = await _productRepository.Entities
            .Where(p => p.ProductStocks.Any(w => w.Stock > 0))
            .CountAsync();

        response.ProductInWarehouse = await _productRepository.Entities
            .Where(p => p.ProductStocks.Any(w => w.Stock > 0))
            .SumAsync(p => p.ProductStocks.Sum(w => w.Stock));
           
        return response;
    }
}