using maERP.Application.Contracts.Logging;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Identity;

namespace maERP.Application.Features.User.Commands.UserDelete;

/// <summary>
/// Handler for processing user deletion commands.
/// Implements IRequestHandler from MediatR to handle UserDeleteCommand requests
/// and return the ID of the deleted user wrapped in a Result.
/// </summary>
public class UserDeleteHandler : IRequestHandler<UserDeleteCommand, Result<string>>
{
    /// <summary>
    /// ASP.NET Identity UserManager for user data operations
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<UserDeleteHandler> _logger;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="userManager">ASP.NET Identity UserManager for user data access</param>
    /// <param name="logger">Logger for recording operations</param>
    public UserDeleteHandler(
        UserManager<ApplicationUser> userManager,
        IAppLogger<UserDeleteHandler> logger)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Handles the user deletion command request
    /// </summary>
    /// <param name="request">The command containing the user ID to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the deleted user if successful</returns>
    public async Task<Result<string>> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting user with ID: {Id}", request.Id);

        var result = new Result<string>();

        // Validate incoming data using FluentValidation
        var validator = new UserDeleteValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation errors
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
            // Find the user to delete by ID
            var userToDelete = await _userManager.FindByIdAsync(request.Id);

            // If user not found, return a not found result
            if (userToDelete == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"User with ID {request.Id} not found.");

                _logger.LogWarning("User with ID {0} not found", request.Id);
                return result;
            }

            // Delete the user using ASP.NET Identity UserManager
            var deleteResult = await _userManager.DeleteAsync(userToDelete);

            // If deletion fails, return an error result with the error descriptions
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

            // Set successful result with the deleted user's ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = userToDelete.Id;

            _logger.LogInformation("User {0} deleted successfully", userToDelete.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during user deletion
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the user: {ex.Message}");

            _logger.LogError("Error deleting user: {Message}", ex.Message);
        }

        return result;
    }
}
