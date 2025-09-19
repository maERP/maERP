using System;
using System.Linq;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using maERP.Application.Extensions;
using Microsoft.AspNetCore.Http;

namespace maERP.Application.Features.User.Commands.UserDelete;

/// <summary>
/// Handler for processing user deletion commands.
/// Implements IRequestHandler from MediatR to handle UserDeleteCommand requests
/// and return the ID of the deleted user wrapped in a Result.
/// </summary>
public class UserDeleteHandler : IRequestHandler<UserDeleteCommand, Result<string>>
{
    /// <summary>
    private readonly IUserRepository _userRepository;
    private readonly ITenantContext _tenantContext;
    private readonly IAppLogger<UserDeleteHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantPermissionService _tenantPermissionService;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="userManager">ASP.NET Identity UserManager for user data access</param>
    /// <param name="logger">Logger for recording operations</param>
    public UserDeleteHandler(
        IUserRepository userRepository,
        ITenantContext tenantContext,
        IAppLogger<UserDeleteHandler> logger,
        IHttpContextAccessor httpContextAccessor,
        ITenantPermissionService tenantPermissionService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _tenantPermissionService = tenantPermissionService ?? throw new ArgumentNullException(nameof(tenantPermissionService));
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
            var currentTenantId = ResolveTenantId(_tenantContext.GetCurrentTenantId());
            var httpContext = _httpContextAccessor.HttpContext;
            var currentUser = httpContext?.User;
            var isSuperadmin = currentUser?.IsInRole("Superadmin") ?? false;

            if (!isSuperadmin)
            {
                var currentUserId = httpContext.GetUserId() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(currentUserId))
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.Unauthorized;
                    result.Messages.Add("User context is required to evaluate permissions.");
                    return result;
                }

                if (!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty)
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.NotFound;
                    result.Messages.Add("User not found in current tenant.");
                    return result;
                }

                var hasPermission = await _tenantPermissionService.CanManageUsersAsync(
                    currentUserId,
                    currentTenantId.Value,
                    cancellationToken);

                if (!hasPermission)
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.Forbidden;
                    result.Messages.Add("You do not have permission to delete users for this tenant.");
                    return result;
                }
            }
            else if (!currentTenantId.HasValue || currentTenantId.Value == Guid.Empty)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("Tenant context is required to delete a user.");
                return result;
            }

            var userToDelete = await _userRepository.GetByIdWithTenantsAsync(request.Id);

            // If user not found, return a not found result
            if (userToDelete == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"User with ID {request.Id} not found.");

                _logger.LogWarning("User with ID {0} not found", request.Id);
                return result;
            }

            if (!isSuperadmin && currentTenantId.HasValue && (userToDelete.UserTenants == null || !userToDelete.UserTenants.Any(ut => ut.TenantId == currentTenantId.Value)))
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("User not found in current tenant.");
                return result;
            }

            // Delete the user using ASP.NET Identity UserManager
            var deleteResult = await _userRepository.DeleteAsync(userToDelete);

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
            result.StatusCode = ResultStatusCode.NoContent;
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

    private Guid? ResolveTenantId(Guid? currentTenantId)
    {
        if (currentTenantId.HasValue && currentTenantId.Value != Guid.Empty)
        {
            return currentTenantId;
        }

        var fallback = _tenantContext.GetAssignedTenantIds().FirstOrDefault(id => id != Guid.Empty);
        return fallback == Guid.Empty ? (Guid?)null : fallback;
    }
}
