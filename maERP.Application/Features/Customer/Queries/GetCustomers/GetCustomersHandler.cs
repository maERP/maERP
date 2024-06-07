using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.GetCustomers;

public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, List<GetCustomersResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetCustomersHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersHandler(IMapper mapper,
        IAppLogger<GetCustomersHandler> logger, 
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _customerRepository = customerRepository; 
    }
    public async Task<List<GetCustomersResponse>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var customers = await _customerRepository.GetAllAsync();

        // Sort by DateEnrollment
        customers = customers.OrderByDescending(o => o.DateEnrollment).ToList();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<GetCustomersResponse>>(customers);

        // Return list of DTO objects
        _logger.LogInformation("All Customeres are retrieved successfully.");
        return data;
    }
}