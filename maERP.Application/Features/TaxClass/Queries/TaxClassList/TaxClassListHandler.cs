using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Shared.Wrapper;
using MediatR;


namespace maERP.Application.Features.TaxClass.Queries.TaxClassList;

public class TaxClassListHandler : IRequestHandler<TaxClassListQuery, PaginatedResult<TaxClassListResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<TaxClassListHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;

    public TaxClassListHandler(IMapper mapper,
        IAppLogger<TaxClassListHandler> logger, 
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository; 
    }
    public async Task<PaginatedResult<TaxClassListResponse>> Handle(TaxClassListQuery request, CancellationToken cancellationToken)
    {
        var orderFilterSpec = new TaxClassFilterSpecification(request.SearchString);

        if (request.OrderBy?.Any() != true)
        {
            return await _taxClassRepository.Entities
               .Specify(orderFilterSpec)
               .ProjectTo<TaxClassListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            var ordering = string.Join(",", request.OrderBy);

            return await _taxClassRepository.Entities
               .Specify(orderFilterSpec)
               .OrderBy(ordering)
               .ProjectTo<TaxClassListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}