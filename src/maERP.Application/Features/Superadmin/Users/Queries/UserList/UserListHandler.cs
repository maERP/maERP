using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Extensions;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.User;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Identity;

namespace maERP.Application.Features.Superadmin.Users.Queries.UserList;

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
        _logger.LogInformation("Handle UserListQuery: {Request}", request);

        var sanitizedPageSize = request.PageSize <= 0 ? 10 : request.PageSize;
        var sanitizedPage = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageIndex = sanitizedPage - 1;

        // Superadmin: Show ALL users (with and without tenant assignments)
        // No tenant filtering - this endpoint is protected by [Authorize(Roles = "Superadmin")]
        var query = _userManager.Users
            .Select(u => new UserListDto
            {
                Id = u.Id,
                Email = u.Email ?? string.Empty,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                DateCreated = u.DateCreated
            });

        if (request.OrderBy.Any())
        {
            var ordering = string.Join(",", request.OrderBy);
            try
            {
                query = query.OrderBy(ordering);
            }
            catch (ParseException ex)
            {
                _logger.LogWarning("Invalid orderBy value '{0}' supplied. {1}", ordering, ex.Message);
            }
        }

        var result = await query.ToPaginatedListAsync(pageIndex, sanitizedPageSize);
        result.CurrentPage = sanitizedPage;
        result.PageSize = sanitizedPageSize;
        return result;
    }
}
