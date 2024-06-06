using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.User;
using MediatR;

namespace maERP.Application.Features.User.Queries.GetUserDetailQuery;

public class GetUserDetailsHandler : IRequestHandler<GetUserDetailsQuery, UserDetailDto>
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
    
    public async Task<UserDetailDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var user = await _userRepository.GetByIdAsync(request.Id);

        // Convert data objects to DTO objects
        var data = _mapper.Map<UserDetailDto>(user);

        // Return list of DTO objects
        _logger.LogInformation("All Users retrieved successfully.");
        return data;
    }
}