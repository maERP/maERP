using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.GetCustomerDetail;

public class GetCustomerDetailHandler : IRequestHandler<GetCustomerDetailQuery, GetCustomerDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetCustomerDetailHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerDetailHandler(IMapper mapper,
        IAppLogger<GetCustomerDetailHandler> logger,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _customerRepository = customerRepository;
    }
    public async Task<GetCustomerDetailResponse> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerWithDetails(request.Id);

        if(customer == null)
        {
            throw new NotFoundException("NotFoundException", "Customer not found.");
        }

        var data = _mapper.Map<GetCustomerDetailResponse>(customer);

        _logger.LogInformation("All Customeres are retrieved successfully.");
        return data;
    }
}