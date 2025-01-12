using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.CustomerDetail;

public class CustomerDetailHandler : IRequestHandler<CustomerDetailQuery, CustomerDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CustomerDetailHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomerDetailHandler(IMapper mapper,
        IAppLogger<CustomerDetailHandler> logger,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _customerRepository = customerRepository;
    }
    public async Task<CustomerDetailResponse> Handle(CustomerDetailQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerWithDetails(request.Id);

        if(customer == null)
        {
            throw new NotFoundException("NotFoundException", "Customer not found.");
        }

        var data = _mapper.Map<CustomerDetailResponse>(customer);

        _logger.LogInformation("Customere retrieved successfully.");
        return data;
    }
}