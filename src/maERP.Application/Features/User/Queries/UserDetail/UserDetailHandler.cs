using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.User.Queries.UserDetail;

public class UserDetailHandler : IRequestHandler<UserDetailQuery, Result<UserDetailDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UserDetailHandler> _logger;
    private readonly IUserRepository _userRepository;

    public UserDetailHandler(IMapper mapper,
        IAppLogger<UserDetailHandler> logger,
        IUserRepository userRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    
    public async Task<Result<UserDetailDto>> Handle(UserDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving user details for ID: {Id}", request.Id);
        
        var result = new Result<UserDetailDto>();
        
        try
        {
            // Query the database
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"User with ID {request.Id} not found");
                
                _logger.LogWarning("User with ID {Id} not found", request.Id);
                return result;
            }

            // Convert data objects to DTO objects
            var data = _mapper.Map<UserDetailDto>(user);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;
            
            _logger.LogInformation("User with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the user: {ex.Message}");
            
            _logger.LogError("Error retrieving user: {Message}", ex.Message);
        }
        
        return result;
    }
}