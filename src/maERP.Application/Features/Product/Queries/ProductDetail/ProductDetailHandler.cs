using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Dtos.Product;
using MediatR;

namespace maERP.Application.Features.Product.Queries.ProductDetail;

public class ProductDetailHandler : IRequestHandler<ProductDetailQuery, ProductDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<ProductDetailHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductDetailHandler(IMapper mapper,
        IAppLogger<ProductDetailHandler> logger,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository;
    }
    public async Task<ProductDetailDto> Handle(ProductDetailQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, true);

        if(product == null)
        {
            throw new NotFoundException("NotFoundException", "Product not found.");
        }

        var data = _mapper.Map<ProductDetailDto>(product);

        _logger.LogInformation("Product retrieved successfully.");
        return data;
    }
}