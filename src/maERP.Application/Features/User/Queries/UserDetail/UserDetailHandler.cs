using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using System.Collections.Generic;
using System.Linq;

namespace maERP.Application.Features.User.Queries.UserDetail;

/// <summary>
/// Handler for processing user detail queries.
/// Implements IRequestHandler from MediatR to handle UserDetailQuery requests
/// and return detailed user information wrapped in a Result.
/// </summary>
public class UserDetailHandler : IRequestHandler<UserDetailQuery, Result<UserDetailDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<UserDetailHandler> _logger;

    /// <summary>
    /// Repository for user data operations
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="userRepository">Repository for user data access</param>
    public UserDetailHandler(
        IAppLogger<UserDetailHandler> logger,
        IUserRepository userRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Handles the user detail query request
    /// </summary>
    /// <param name="request">The query containing the user ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed user information if successful</returns>
    public async Task<Result<UserDetailDto>> Handle(UserDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving user details for ID: {Id}", request.Id);

        var result = new Result<UserDetailDto>();

        try
        {
            // Retrieve user from the repository by ID
            var user = await _userRepository.GetByIdAsync(request.Id);

            // If user not found, return a not found result
            if (user == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"User with ID {request.Id} not found");

                _logger.LogWarning("User with ID {Id} not found", request.Id);
                return result;
            }
            
            // Get user tenant assignments
            var userTenantAssignments = await _userRepository.GetUserTenantAssignmentsAsync(request.Id);
            var tenantAssignments = new List<UserTenantAssignmentDto>();
            
            // Map tenant assignments to DTOs
            if (userTenantAssignments != null && userTenantAssignments.Any())
            {
                foreach (var assignment in userTenantAssignments)
                {
                    if (assignment.Tenant != null)
                    {
                        tenantAssignments.Add(new UserTenantAssignmentDto
                        {
                            TenantId = assignment.TenantId,
                            TenantName = assignment.Tenant.Name,
                            TenantCode = assignment.Tenant.TenantCode,
                            IsDefault = assignment.IsDefault
                        });
                    }
                }
            }

            // Manual mapping from entity to DTO (instead of using AutoMapper)
            var data = new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                DefaultTenantId = user.DefaultTenantId,
                TenantAssignments = tenantAssignments
            };

            // Set successful result with the user details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;

            _logger.LogInformation("User with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during user retrieval
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the user: {ex.Message}");

            _logger.LogError("Error retrieving user: {Message}", ex.Message);
        }

        return result;
    }
}