using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.Product;
using MediatR;

namespace maERP.Application.Features.Product.Queries.GetProductsQuery;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<ProductListDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetProductsHandler> _logger;
    private readonly IProductRepository _productRepository;

    public GetProductsHandler(IMapper mapper,
        IAppLogger<GetProductsHandler> logger, 
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository; 
    }
    public async Task<List<ProductListDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var products = await _productRepository.GetAllAsync();

        // Sort by DateCreated
        products = products.OrderByDescending(o => o.DateCreated).ToList();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<ProductListDto>>(products);

        // Return list of DTO objects
        _logger.LogInformation("All Productes are retrieved successfully.");
        return data;
    }
}