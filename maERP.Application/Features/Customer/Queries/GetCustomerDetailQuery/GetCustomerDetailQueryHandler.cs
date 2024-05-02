using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.Customer;
using maERP.Application.Exceptions;
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
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if(customer == null)
        {
            throw new NotFoundException("NotFoundException", "Customer not found.");
        }

        var data = _mapper.Map<CustomerDetailDto>(customer);

        _logger.LogInformation("All Customeres are retrieved successfully.");
        return data;
    }
}