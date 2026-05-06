using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Product.Queries.ProductList;

public class ProductListHandler : IRequestHandler<ProductListQuery, PaginatedResult<ProductListDto>>
{
    private readonly IAppLogger<ProductListHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductListHandler(
        IAppLogger<ProductListHandler> logger,
        IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<PaginatedResult<ProductListDto>> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        var salesFilterSpec = new ProductFilterSpecification(request.SearchString);

        _logger.LogInformation("Handle ProductListQuery: {0}", request);

        if (request.SalesBy.Any() != true)
        {
            var products = await _productRepository.Entities
               .Include(p => p.Manufacturer)
               .Include(p => p.TaxClass)
               .Specify(salesFilterSpec)
               .Select(p => MapToProductListDto(p))
               .AsNoTracking() // Ensure no EF caching
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return products;
        }

        var salesing = string.Join(",", request.SalesBy);

        var salesedProducts = await _productRepository.Entities
            .Include(p => p.Manufacturer)
            .Include(p => p.TaxClass)
            .Specify(salesFilterSpec)
            .OrderBy(salesing)
            .Select(p => MapToProductListDto(p))
            .AsNoTracking() // Ensure no EF caching
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return salesedProducts;
    }

    private static ProductListDto MapToProductListDto(Domain.Entities.Product product)
    {
        return new ProductListDto
        {
            Id = product.Id,
            Sku = product.Sku,
            Name = product.Name,
            Description = product.Description,
            Ean = product.Ean,
            Price = product.Price,
            Msrp = product.Msrp,
            TaxRate = product.TaxClass?.TaxRate ?? 0,
            Manufacturer = product.Manufacturer != null ? new ManufacturerListDto
            {
                Id = product.Manufacturer.Id,
                Name = product.Manufacturer.Name,
                City = product.Manufacturer.City,
                Country = product.Manufacturer.Country
            } : null
        };
    }
}