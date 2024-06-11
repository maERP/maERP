using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Shared.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Queries.ProductList;

public class ProductListHandler : IRequestHandler<ProductListQuery, PaginatedResult<ProductListResponse>>
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
    public async Task<PaginatedResult<ProductListResponse>> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        var orderFilterSpec = new ProductFilterSpecification(request.SearchString);

        if (request.OrderBy?.Any() != true)
        {
            return await _productRepository.Entities
               .Specify(orderFilterSpec)
               .ProjectTo<ProductListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            var ordering = string.Join(",", request.OrderBy);

            return await _productRepository.Entities
               .Specify(orderFilterSpec)
               .OrderBy(ordering)
               .ProjectTo<ProductListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}