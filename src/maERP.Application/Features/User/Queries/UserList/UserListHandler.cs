using maERP.Application.Contracts.Logging;
using maERP.Application.Extensions;
using maERP.Domain.Dtos.User;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;

namespace maERP.Application.Features.User.Queries.UserList;

/// <summary>
/// Handler for processing user list queries.
/// Implements IRequestHandler from MediatR to handle UserListQuery requests
/// and return a paginated list of users wrapped in a PaginatedResult.
/// </summary>
public class UserListHandler : IRequestHandler<UserListQuery, PaginatedResult<UserListDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<UserListHandler> _logger;

    /// <summary>
    /// ASP.NET Identity UserManager for user data operations
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="userManager">ASP.NET Identity UserManager for user data access</param>
    public UserListHandler(
        IAppLogger<UserListHandler> logger,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    /// <summary>
    /// Handles the user list query request
    /// </summary>
    /// <param name="request">The query containing pagination, search, and ordering parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>PaginatedResult containing a list of users based on the query parameters</returns>
    public async Task<PaginatedResult<UserListDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement filter specification if needed
        // var userFilterSpec = new UserFilterSpecification(request.SearchString);

        _logger.LogInformation("Handle UserListQuery: {0}", request);

        // Manual mapping using LINQ projection instead of AutoMapper
        // This creates a query that selects only the needed properties for the DTO
        var query = _userManager.Users
            //.Specify(userFilterSpec) // Uncomment when filter specification is implemented
            .Select(u => new UserListDto
            {
                Id = u.Id,
                Email = u.Email ?? string.Empty,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                DateCreated = u.DateCreated
            });

        // If no ordering is specified, return paginated results without ordering
        if (request.OrderBy.Any() != true)
        {
            return await query
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        // Join the ordering properties into a string format that can be used by Dynamic LINQ
        var ordering = string.Join(",", request.OrderBy);

        // Apply dynamic ordering and pagination to the query
        return await query
            .OrderBy(ordering)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}