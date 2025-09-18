using System;
using System.Linq;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.User.Commands.UserUpdate;

/// <summary>
/// Handler for processing user update commands.
/// Implements IRequestHandler from MediatR to handle UserUpdateCommand requests
/// and return the ID of the updated user wrapped in a Result.
/// </summary>
public class UserUpdateHandler : IRequestHandler<UserUpdateCommand, Result<string>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<UserUpdateHandler> _logger;

    /// <summary>
    /// Repository for user data operations
    /// </summary>
    private readonly IUserRepository _userRepository;

    private readonly ITenantContext _tenantContext;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="userRepository">Repository for user data access</param>
    public UserUpdateHandler(
        IAppLogger<UserUpdateHandler> logger,
        IUserRepository userRepository,
        ITenantContext tenantContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
    }

    /// <summary>
    /// Handles the user update command request
    /// </summary>
    /// <param name="request">The command containing the user update data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the updated user if successful</returns>
    public async Task<Result<string>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating user with ID: {Id}", request.Id);

        var result = new Result<string>();

        // Validate incoming data using FluentValidation
        var validator = new UserUpdateValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation errors
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(UserUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            var currentTenantId = _tenantContext.GetCurrentTenantId();
            if (!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("Tenant context is required to update a user.");
                return result;
            }

            // Get existing user with tenant assignments
            var existingUser = await _userRepository.GetByIdWithTenantsAsync(request.Id);
            if (existingUser == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"User with ID {request.Id} not found.");
                return result;
            }

            if (existingUser.UserTenants == null || !existingUser.UserTenants.Any(ut => ut.TenantId == currentTenantId.Value))
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("User not found in current tenant.");
                return result;
            }

            if (request.TenantIds != null && request.TenantIds.Any())
            {
                if (!await _userRepository.TenantsExistAsync(request.TenantIds))
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("One or more provided tenant IDs do not exist.");
                    return result;
                }

                if (!request.TenantIds.Contains(currentTenantId.Value))
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("User must remain assigned to the current tenant.");
                    return result;
                }

                if (request.DefaultTenantId.HasValue && request.DefaultTenantId.Value != Guid.Empty &&
                    !request.TenantIds.Contains(request.DefaultTenantId.Value))
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("Default tenant must be part of the tenant assignments.");
                    return result;
                }
            }

            if (!request.DefaultTenantId.HasValue || request.DefaultTenantId.Value == Guid.Empty)
            {
                request.DefaultTenantId = existingUser.UserTenants.FirstOrDefault(ut => ut.IsDefault)?.TenantId ?? currentTenantId.Value;
            }

            // Update user properties
            existingUser.Email = request.Email;
            existingUser.UserName = request.Email;
            existingUser.Firstname = request.Firstname;
            existingUser.Lastname = request.Lastname;
            existingUser.DateModified = DateTime.UtcNow;

            // Update the user in the database
            await _userRepository.UpdateWithDetailsAsync(existingUser);

            // Update tenant assignments if provided
            if (request.TenantIds != null && request.TenantIds.Any())
            {
                await _userRepository.UpdateUserTenantAssignmentsAsync(
                    request.Id,
                    request.TenantIds,
                    request.DefaultTenantId);

                _logger.LogInformation("Updated tenant assignments for user ID: {Id}", request.Id);
            }

            // Set successful result with the updated user's ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = existingUser.Id;

            _logger.LogInformation("Successfully updated user with ID: {Id}", existingUser.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during user update
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the user: {ex.Message}");

            _logger.LogError("Error updating user: {Message}", ex.Message);
        }

        return result;
    }
}
