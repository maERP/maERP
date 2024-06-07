using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.User.Queries.GetUserDetails;

public class GetUserDetailsHandler : IRequestHandler<GetUserDetailsQuery, GetUserDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetUserDetailsHandler> _logger;
    private readonly IUserRepository _userRepository;

    public GetUserDetailsHandler(IMapper mapper,
        IAppLogger<GetUserDetailsHandler> logger,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository;
    }
    
    public async Task<GetUserDetailResponse> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var user = await _userRepository.GetByIdAsync(request.Id);

        // Convert data objects to DTO objects
        var data = _mapper.Map<GetUserDetailResponse>(user);

        // Return list of DTO objects
        _logger.LogInformation("All Users retrieved successfully.");
        return data;
    }
}