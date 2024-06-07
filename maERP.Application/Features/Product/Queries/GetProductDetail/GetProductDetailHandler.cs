using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Product.Queries.GetProductDetail;

public class GetProductDetailHandler : IRequestHandler<GetProductDetailQuery, GetProductDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetProductDetailHandler> _logger;
    private readonly IProductRepository _productRepository;

    public GetProductDetailHandler(IMapper mapper,
        IAppLogger<GetProductDetailHandler> logger,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository;
    }
    public async Task<GetProductDetailResponse> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, true);

        if(product == null)
        {
            throw new NotFoundException("NotFoundException", "Product not found.");
        }

        var data = _mapper.Map<GetProductDetailResponse>(product);

        _logger.LogInformation("All Productes are retrieved successfully.");
        return data;
    }
}