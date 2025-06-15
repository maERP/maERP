using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.User.Commands.UserCreate;

/// <summary>
/// Handler for processing user creation commands.
/// Implements IRequestHandler from MediatR to handle UserCreateCommand requests
/// and return the ID of the newly created user wrapped in a Result.
/// </summary>
public class UserCreateHandler : IRequestHandler<UserCreateCommand, Result<string>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<UserCreateHandler> _logger;

    /// <summary>
    /// Repository for user data operations
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="userRepository">Repository for user data access</param>
    public UserCreateHandler(
        IAppLogger<UserCreateHandler> logger,
        IUserRepository userRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Handles the user creation command request
    /// </summary>
    /// <param name="request">The command containing user creation data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created user if successful</returns>
    public async Task<Result<string>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new user with email: {Email}", request.Email);

        var result = new Result<string>();

        // Validate incoming data using FluentValidation
        var validator = new UserCreateValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation errors
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(UserCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Manual mapping from command to entity (instead of using AutoMapper)
            var userToCreate = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            // Add the new user to the database with the provided password
            await _userRepository.CreateAsync(userToCreate, request.Password);

            // Set successful result with the new user's ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = userToCreate.Id;

            _logger.LogInformation("Successfully created user with ID: {Id}", userToCreate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during user creation
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the user: {ex.Message}");

            _logger.LogError("Error creating user: {Message}", ex.Message);
        }

        return result;
    }
}