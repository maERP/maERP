using maERP.Application.Contracts.Logging;
using maERP.Application.Extensions;
using maERP.Application.Mediator;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace maERP.Application.Features.Account.Commands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Result<string>>
{
    private readonly IAppLogger<ChangePasswordHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public ChangePasswordHandler(
        IAppLogger<ChangePasswordHandler> logger,
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<Result<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<string>();

        var validator = new ChangePasswordValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            return result;
        }

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
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Current user not found.");
                return result;
            }

            var changeResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!changeResult.Succeeded)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.AddRange(changeResult.Errors.Select(e => e.Description));
                _logger.LogWarning("Password change failed for user {UserId}: {Errors}", userId,
                    string.Join(", ", result.Messages));
                return result;
            }

            user.DateModified = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.NoContent;
            result.Data = user.Id;

            _logger.LogInformation("User {UserId} changed own password", user.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while changing the password: {ex.Message}");
            _logger.LogError("Error changing password: {Message}", ex.Message);
        }

        return result;
    }
}
