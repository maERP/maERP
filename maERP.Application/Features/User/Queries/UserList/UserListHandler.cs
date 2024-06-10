using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.User.Queries.UserList;

public class UserListHandler : IRequestHandler<UserListQuery, List<UserListResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UserListHandler> _logger;
    private readonly IUserRepository _userRepository;

    public UserListHandler(IMapper mapper,
        IAppLogger<UserListHandler> logger, 
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository; 
    }

    public async Task<List<UserListResponse>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var useres = await _userRepository.GetAllAsync();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<UserListResponse>>(useres);

        // Return list of DTO objects
        _logger.LogInformation("All Useres are retrieved successfully.");
        return data;
    }
}