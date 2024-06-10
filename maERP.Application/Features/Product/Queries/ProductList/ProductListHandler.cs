using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Product.Queries.ProductList;

public class ProductListHandler : IRequestHandler<ProductListQuery, List<ProductListResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<ProductListHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductListHandler(IMapper mapper,
        IAppLogger<ProductListHandler> logger, 
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository; 
    }
    public async Task<List<ProductListResponse>> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var products = await _productRepository.GetAllAsync();

        // Sort by DateCreated
        products = products.OrderByDescending(o => o.DateCreated).ToList();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<ProductListResponse>>(products);

        // Return list of DTO objects
        _logger.LogInformation("All Productes are retrieved successfully.");
        return data;
    }
}