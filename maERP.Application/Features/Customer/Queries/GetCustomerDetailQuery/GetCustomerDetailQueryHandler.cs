using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.Customer;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.GetCustomerDetailQuery;

public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, CustomerDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetCustomerDetailQueryHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerDetailQueryHandler(IMapper mapper,
        IAppLogger<GetCustomerDetailQueryHandler> logger,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _customerRepository = customerRepository;
    }
    public async Task<CustomerDetailDto> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        // Convert data objects to DTO objects
        var data = _mapper.Map<CustomerDetailDto>(customer);

        // Return list of DTO objects
        _logger.LogInformation("All Customeres are retrieved successfully.");
        return data;
    }
}