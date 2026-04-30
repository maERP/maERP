using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.Account;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Http;

namespace maERP.Application.Features.Account.Queries.GetCurrentUser;

public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, Result<CurrentUserProfileDto>>
{
    private readonly IAppLogger<GetCurrentUserHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetCurrentUserHandler(
        IAppLogger<GetCurrentUserHandler> logger,
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<Result<CurrentUserProfileDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<CurrentUserProfileDto>();

        var userId = _httpContextAccessor.HttpContext.GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.Unauthorized;
            result.Messages.Add("Authenticated user context is required.");
            return result;
        }

        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Current user not found.");
                _logger.LogWarning("Authenticated user {UserId} not found in database", userId);
                return result;
            }

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = new CurrentUserProfileDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                PhoneNumber = user.PhoneNumber ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the current user: {ex.Message}");
            _logger.LogError("Error retrieving current user: {Message}", ex.Message);
        }

        return result;
    }
}
