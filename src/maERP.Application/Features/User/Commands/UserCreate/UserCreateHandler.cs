using System;
using System.Collections.Generic;
using System.Linq;
using maERP.Application.Extensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Http;

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

    private readonly ITenantContext _tenantContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantPermissionService _tenantPermissionService;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="userRepository">Repository for user data access</param>
    public UserCreateHandler(
        IAppLogger<UserCreateHandler> logger,
        IUserRepository userRepository,
        ITenantContext tenantContext,
        IHttpContextAccessor httpContextAccessor,
        ITenantPermissionService tenantPermissionService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _tenantPermissionService = tenantPermissionService ?? throw new ArgumentNullException(nameof(tenantPermissionService));
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
        var validator = new UserCreateValidator(_userRepository);
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
            var httpContext = _httpContextAccessor.HttpContext;
            var currentUser = httpContext?.User;
            var currentUserId = httpContext.GetUserId() ?? string.Empty;
            var isSuperadmin = currentUser?.IsInRole("Superadmin") ?? false;

            var resolvedTenantId = ResolveTenantId(_tenantContext.GetCurrentTenantId());
            var desiredDefaultTenantId = request.DefaultTenantId != Guid.Empty
                ? request.DefaultTenantId
                : resolvedTenantId;

            if (!desiredDefaultTenantId.HasValue || desiredDefaultTenantId.Value == Guid.Empty)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("Default tenant is required to create a user.");
                return result;
            }

            request.DefaultTenantId = desiredDefaultTenantId.Value;

            var normalizedAdditionalIds = request.AdditionalTenantIds?
                .Where(id => id != Guid.Empty && id != request.DefaultTenantId)
                .Distinct()
                .ToList() ?? new List<Guid>();

            var allTenantIds = new List<Guid> { request.DefaultTenantId };
            allTenantIds.AddRange(normalizedAdditionalIds);

            if (!await _userRepository.TenantsExistAsync(allTenantIds))
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("One or more provided tenant IDs do not exist.");
                return result;
            }

            if (!isSuperadmin)
            {
                if (string.IsNullOrWhiteSpace(currentUserId))
                {
                    result.Succeeded = false;
                    result.StatusCode = ResultStatusCode.Unauthorized;
                    result.Messages.Add("User context is required to evaluate permissions.");
                    return result;
                }

                foreach (var tenantId in allTenantIds.Distinct())
                {
                    var hasPermission = await _tenantPermissionService.CanManageUsersAsync(
                        currentUserId,
                        tenantId,
                        cancellationToken);

                    if (!hasPermission)
                    {
                        result.Succeeded = false;
                        result.StatusCode = ResultStatusCode.Forbidden;
                        result.Messages.Add("You do not have permission to create users for this tenant.");
                        return result;
                    }
                }
            }

            request.AdditionalTenantIds = normalizedAdditionalIds;

            // Manual mapping from command to entity (instead of using AutoMapper)
            var userToCreate = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            // Add the new user to the database with the provided password
            var createResult = await _userRepository.CreateAsync(userToCreate, request.Password);

            // Check if user creation was successful
            if (createResult.Any())
            {
                // Creation failed, return errors
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.AddRange(createResult.Select(e => e.Description));
                return result;
            }

            // Assign user to tenants
            await _userRepository.AssignUserToTenantsAsync(
                userToCreate.Id,
                allTenantIds,
                request.DefaultTenantId);

            // Set successful result with the new user's ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = userToCreate.Id;

            _logger.LogInformation("Successfully created user with ID: {Id} and assigned to {TenantCount} tenants",
                userToCreate.Id, allTenantIds.Count);
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
