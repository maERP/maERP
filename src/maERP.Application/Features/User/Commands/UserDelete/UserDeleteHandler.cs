using maERP.Application.Contracts.Logging;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace maERP.Application.Features.User.Commands.UserDelete;

public class UserDeleteHandler : IRequestHandler<UserDeleteCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAppLogger<UserDeleteHandler> _logger;
    
    public UserDeleteHandler(
        UserManager<ApplicationUser> userManager,
        IAppLogger<UserDeleteHandler> logger)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<string>> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting user with ID: {Id}", request.Id);
        
        var result = new Result<string>();
        
        // Validate incoming data
        var validator = new UserDeleteValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in delete request for {0}: {1}", 
                nameof(UserDeleteCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Find user
            var userToDelete = await _userManager.FindByIdAsync(request.Id);
            if (userToDelete == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"User with ID {request.Id} not found.");
                
                _logger.LogWarning("User with ID {0} not found", request.Id);
                return result;
            }

            // Delete user
            var deleteResult = await _userManager.DeleteAsync(userToDelete);
            if (!deleteResult.Succeeded)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.InternalServerError;
                result.Messages.AddRange(deleteResult.Errors.Select(e => e.Description));
                
                _logger.LogError("Error deleting user {0}: {1}", 
                    request.Id, 
                    string.Join(", ", deleteResult.Errors.Select(e => e.Description)));
                    
                return result;
            }

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = userToDelete.Id;
            
            _logger.LogInformation("User {0} deleted successfully", userToDelete.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the user: {ex.Message}");
            
            _logger.LogError("Error deleting user: {Message}", ex.Message);
        }

        return result;
    }
}
