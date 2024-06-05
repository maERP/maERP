using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.Customer;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.GetCustomersQuery;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerListDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetCustomersQueryHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersQueryHandler(IMapper mapper,
        IAppLogger<GetCustomersQueryHandler> logger, 
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _customerRepository = customerRepository; 
    }
    public async Task<List<CustomerListDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var customers = await _customerRepository.GetAllAsync();

        // Sort by DateEnrollment
        customers = customers.OrderByDescending(o => o.DateEnrollment).ToList();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<CustomerListDto>>(customers);

        // Return list of DTO objects
        _logger.LogInformation("All Customeres are retrieved successfully.");
        return data;
    }
}