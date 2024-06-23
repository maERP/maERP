using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Shared.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.CustomerList;

public class CustomerListHandler : IRequestHandler<CustomerListQuery, PaginatedResult<CustomerListResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CustomerListHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomerListHandler(IMapper mapper,
        IAppLogger<CustomerListHandler> logger, 
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _customerRepository = customerRepository; 
    }
    public async Task<PaginatedResult<CustomerListResponse>> Handle(CustomerListQuery request, CancellationToken cancellationToken)
    {
        var customerFilterSpec = new CustomerFilterSpecification(request.SearchString);
        
        _logger.LogInformation("CustomerListHandler.Handle: Retrieving customers.");

        if (request.OrderBy.Any() != true)
        {
            return await _customerRepository.Entities
               .Specify(customerFilterSpec)
               .ProjectTo<CustomerListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            var ordering = string.Join(",", request.OrderBy);

            return await _customerRepository.Entities
               .Specify(customerFilterSpec)
               .OrderBy(ordering)
               .ProjectTo<CustomerListResponse>(_mapper.ConfigurationProvider)
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}