using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.User.Queries.UserDetail;

public class UserDetailHandler : IRequestHandler<UserDetailQuery, UserDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UserDetailHandler> _logger;
    private readonly IUserRepository _userRepository;

    public UserDetailHandler(IMapper mapper,
        IAppLogger<UserDetailHandler> logger,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository;
    }
    
    public async Task<UserDetailResponse> Handle(UserDetailQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var user = await _userRepository.GetByIdAsync(request.Id);

        // Convert data objects to DTO objects
        var data = _mapper.Map<UserDetailResponse>(user);

        // Return list of DTO objects
        _logger.LogInformation("User retrieved successfully.");
        return data;
    }
}