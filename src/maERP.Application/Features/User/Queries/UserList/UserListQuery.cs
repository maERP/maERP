using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.User.Queries.UserList;

/// <summary>
/// Query for retrieving a paginated list of users with optional filtering and sorting.
/// Implements IRequest to work with MediatR, returning a paginated list of users wrapped in a PaginatedResult.
/// </summary>
public class UserListQuery : IRequest<PaginatedResult<UserListDto>>
{
    /// <summary>
    /// The page number to retrieve (1-based indexing)
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// The number of items per page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Optional search string to filter users
    /// </summary>
    public string SearchString { get; set; }

    /// <summary>
    /// Optional array of properties to order the results by
    /// </summary>
    public string[] OrderBy { get; set; }

    /// <summary>
    /// Constructor that initializes the query with pagination, search, and ordering parameters
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (default: 1)</param>
    /// <param name="pageSize">The number of items per page (default: 10)</param>
    /// <param name="searchString">Optional search string to filter users (default: empty string)</param>
    /// <param name="orderBy">Optional comma-separated list of properties to order by (default: empty string)</param>
    public UserListQuery(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        SearchString = searchString;

        // Parse the orderBy string into an array of property names
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            OrderBy = orderBy.Split(',');
        }
        else OrderBy = new string[] { };
    }
}