using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Extensions;
using maERP.Application.Mediator;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantPermissionService _tenantPermissionService;
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="userRepository">Repository for user data access</param>
    public UserUpdateHandler(
        IAppLogger<UserUpdateHandler> logger,
        IUserRepository userRepository,
        ITenantContext tenantContext,
        IHttpContextAccessor httpContextAccessor,
        ITenantPermissionService tenantPermissionService,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _tenantPermissionService = tenantPermissionService ?? throw new ArgumentNullException(nameof(tenantPermissionService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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
            var rawTenantId = _tenantContext.GetCurrentTenantId();
            var currentTenantId = ResolveTenantId(rawTenantId);
            var httpContext = _httpContextAccessor.HttpContext;
            var currentUser = httpContext?.User;
            var isSuperadmin = currentUser?.IsInRole("Superadmin") ?? false;
            var currentUserId = httpContext.GetUserId() ?? string.Empty;

            // Get existing user with tenant assignments
            var existingUser = await _userRepository.GetByIdWithTenantsAsync(request.Id);
            if (existingUser == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"User with ID {request.Id} not found.");
                return result;
            }

            if (!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty)
            {
                if (request.DefaultTenantId.HasValue && request.DefaultTenantId.Value != Guid.Empty)
                {
                    currentTenantId = request.DefaultTenantId.Value;
                }
            }

            if ((!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty) && existingUser.UserTenants != null && existingUser.UserTenants.Any())
            {
                currentTenantId = existingUser.UserTenants.FirstOrDefault(ut => ut.IsDefault)?.TenantId
                                  ?? existingUser.UserTenants.First().TenantId;
            }

            if (!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("Tenant context is required to update a user.");
                return result;
            }

            if (!isSuperadmin && string.IsNullOrWhiteSpace(currentUserId))
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.Unauthorized;
                result.Messages.Add("User context is required to evaluate permissions.");
                return result;
            }

            if (!isSuperadmin && currentTenantId.HasValue && (existingUser.UserTenants == null || !existingUser.UserTenants.Any(ut => ut.TenantId == currentTenantId.Value)))
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("User not found in current tenant.");
                return result;
            }

            var canManageUsers = isSuperadmin;
            if (!isSuperadmin)
            {
                canManageUsers = await _tenantPermissionService.CanManageUsersAsync(
                    currentUserId,
                    currentTenantId.Value,
                    cancellationToken);
            }

            var isSelfUpdate = !string.IsNullOrWhiteSpace(currentUserId) && currentUserId == request.Id;

            if (!isSuperadmin && !isSelfUpdate && !canManageUsers)
            {
                _logger.LogWarning("User {UserId} lacks manage permission for tenant {TenantId} when updating {TargetUserId}",
                    currentUserId,
                    currentTenantId,
                    request.Id);
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.Forbidden;
                result.Messages.Add("You do not have permission to update other users in this tenant.");
                return result;
            }

            var tenantIdsProvided = request.TenantIds != null;
            var tenantIds = request.TenantIds ?? new List<Guid>();
            var shouldUpdateTenants = tenantIdsProvided;

            if (isSelfUpdate && !canManageUsers)
            {
                var existingTenantIds = existingUser.UserTenants?.Select(ut => ut.TenantId).OrderBy(id => id).ToList() ?? new List<Guid>();
                var requestedTenantIds = tenantIds.OrderBy(id => id).ToList();
                var tenantAssignmentsChanged = shouldUpdateTenants && !existingTenantIds.SequenceEqual(requestedTenantIds);

                var existingDefaultTenantId = existingUser.UserTenants?.FirstOrDefault(ut => ut.IsDefault)?.TenantId;
                var defaultTenantChanged = request.DefaultTenantId.HasValue &&
                    existingDefaultTenantId.HasValue &&
                    request.DefaultTenantId.Value != existingDefaultTenantId.Value;

                // If no default was set previously, any attempt to set a new one counts as a change
                if (!existingDefaultTenantId.HasValue && request.DefaultTenantId.HasValue)
                {
                    defaultTenantChanged = true;
                }

                if (tenantAssignmentsChanged || defaultTenantChanged)
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.Forbidden;
                    result.Messages.Add("You are not allowed to change tenant assignments for your account.");
                    return result;
                }

                shouldUpdateTenants = false;
            }

            if (shouldUpdateTenants)
            {
                if (!await _userRepository.TenantsExistAsync(tenantIds))
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("One or more provided tenant IDs do not exist.");
                    return result;
                }

                if (!tenantIds.Contains(currentTenantId.Value))
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("User must remain assigned to the current tenant.");
                    return result;
                }

                if (request.DefaultTenantId.HasValue && request.DefaultTenantId.Value != Guid.Empty &&
                    !tenantIds.Contains(request.DefaultTenantId.Value))
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("Default tenant must be part of the tenant assignments.");
                    return result;
                }
            }

            if (!request.DefaultTenantId.HasValue || request.DefaultTenantId.Value == Guid.Empty)
            {
                request.DefaultTenantId = existingUser.UserTenants?.FirstOrDefault(ut => ut.IsDefault)?.TenantId ?? currentTenantId.Value;
            }

            var normalizedEmail = _userManager.NormalizeEmail(request.Email);
            var normalizedUserName = _userManager.NormalizeName(request.Email);

            if (!string.Equals(existingUser.NormalizedEmail, normalizedEmail, StringComparison.OrdinalIgnoreCase))
            {
                var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
                var emailInUse = userWithEmail != null && !string.Equals(userWithEmail.Id, existingUser.Id, StringComparison.OrdinalIgnoreCase);

                _logger.LogInformation("Email duplication check for user {UserId}: requested={RequestedEmail}, existsForOtherUser={Exists}",
                    request.Id,
                    normalizedEmail ?? string.Empty,
                    emailInUse);

                if (emailInUse)
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.BadRequest;
                    result.Messages.Add("Email address is already in use.");
                    return result;
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                foreach (var passwordValidator in _userManager.PasswordValidators)
                {
                    var passwordValidationResult = await passwordValidator.ValidateAsync(_userManager, existingUser, request.Password);
                    if (!passwordValidationResult.Succeeded)
                    {
                        result.Succeeded = false;
                        result.StatusCode = ResultStatusCode.BadRequest;
                        result.Messages.AddRange(passwordValidationResult.Errors.Select(e => e.Description));
                        return result;
                    }
                }

                existingUser.PasswordHash = _userManager.PasswordHasher.HashPassword(existingUser, request.Password);
            }

            // Update user properties
            existingUser.Email = request.Email;
            existingUser.NormalizedEmail = normalizedEmail;
            existingUser.UserName = request.Email;
            existingUser.NormalizedUserName = normalizedUserName;
            existingUser.Firstname = request.Firstname;
            existingUser.Lastname = request.Lastname;
            existingUser.DateModified = DateTime.UtcNow;

            // Update the user in the database
            await _userRepository.UpdateWithDetailsAsync(existingUser);

            // Update tenant assignments if provided
            if (shouldUpdateTenants)
            {
                await _userRepository.UpdateUserTenantAssignmentsAsync(
                    request.Id,
                    tenantIds,
                    request.DefaultTenantId);

                _logger.LogInformation("Updated tenant assignments for user ID: {Id}", request.Id);
            }

            // Set successful result with the updated user's ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.NoContent;
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

    private Guid? ResolveTenantId(Guid? currentTenantId)
    {
        if (currentTenantId.HasValue && currentTenantId.Value != Guid.Empty)
        {
            return currentTenantId;
        }

        var assignedTenants = _tenantContext.GetAssignedTenantIds();
        var fallback = assignedTenants.FirstOrDefault(id => id != Guid.Empty);

        return fallback == Guid.Empty ? (Guid?)null : fallback;
    }
}
