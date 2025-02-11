using maERP.Application.Contracts.Logging;
using maERP.Application.Exceptions;
using maERP.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace maERP.Application.Features.User.Commands.UserDelete;

public class UserDeleteHandler : IRequestHandler<UserDeleteCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAppLogger<UserDeleteHandler> _logger;
    
    public UserDeleteHandler(
        UserManager<ApplicationUser> userManager,
        IAppLogger<UserDeleteHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<string> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UserDeleteValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(UserDeleteCommand), request.Id);
            throw new ValidationException("Invalid User Delete Request", validationResult);
        }

        // Find user
        var userToDelete = await _userManager.FindByIdAsync(request.Id.ToString());
        if (userToDelete == null)
        {
            throw new NotFoundException($"User with ID {request.Id} not found.", request.Id);
        }

        // Delete user
        var result = await _userManager.DeleteAsync(userToDelete);
        if (!result.Succeeded)
        {
            _logger.LogError("Error deleting user {0}", request.Id);
            throw new Exception($"Error deleting user {request.Id}");
        }

        _logger.LogInformation("User {0} deleted successfully", request.Id);

        return userToDelete.Id;
    }
}
