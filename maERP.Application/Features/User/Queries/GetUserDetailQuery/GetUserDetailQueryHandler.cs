using MediatR;
using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.User;

namespace maERP.Application.Features.User.Queries.GetUserDetailQuery;
/*
public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetUserDetailQueryHandler> _logger;
    private readonly IUserRepository _userRepository;

    public GetUserDetailQueryHandler(IMapper mapper,
        IAppLogger<GetUserDetailQueryHandler> logger,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository;
    }
    
    public async Task<UserDetailDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
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
*/