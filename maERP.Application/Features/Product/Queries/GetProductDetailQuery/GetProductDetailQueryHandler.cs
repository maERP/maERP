using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.Product;
using MediatR;

namespace maERP.Application.Features.Product.Queries.GetProductDetailQuery;

public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProductDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetProductDetailQueryHandler> _logger;
    private readonly IProductRepository _productRepository;

    public GetProductDetailQueryHandler(IMapper mapper,
        IAppLogger<GetProductDetailQueryHandler> logger,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository;
    }
    public async Task<ProductDetailDto> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var product = await _productRepository.GetByIdAsync(request.Id);

        // Convert data objects to DTO objects
        var data = _mapper.Map<ProductDetailDto>(product);

        // Return list of DTO objects
        _logger.LogInformation("All Productes are retrieved successfully.");
        return data;
    }
}