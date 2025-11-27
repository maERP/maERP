namespace maERP.Client.Core.Models;

/// <summary>
/// Parameters for paginated API queries.
/// Uses the same naming convention as the server API.
/// </summary>
public record QueryParameters
{
    /// <summary>
    /// Page number (0-based).
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PageSize { get; init; } = 20;

    /// <summary>
    /// Search term to filter results.
    /// </summary>
    public string? SearchString { get; init; }

    /// <summary>
    /// Sort order (e.g., "Name Ascending", "DateCreated Descending").
    /// Multiple sort fields can be separated by commas.
    /// </summary>
    public string? OrderBy { get; init; }

    /// <summary>
    /// Builds the query string for API requests.
    /// </summary>
    public string ToQueryString()
    {
        var parameters = new List<string>
        {
            $"pageNumber={PageNumber}",
            $"pageSize={PageSize}"
        };

        if (!string.IsNullOrWhiteSpace(SearchString))
        {
            parameters.Add($"searchString={Uri.EscapeDataString(SearchString)}");
        }

        if (!string.IsNullOrWhiteSpace(OrderBy))
        {
            parameters.Add($"orderBy={Uri.EscapeDataString(OrderBy)}");
        }

        return string.Join("&", parameters);
    }

    /// <summary>
    /// Creates a new QueryParameters with the next page.
    /// </summary>
    public QueryParameters NextPage() => this with { PageNumber = PageNumber + 1 };

    /// <summary>
    /// Creates a new QueryParameters with the previous page.
    /// </summary>
    public QueryParameters PreviousPage() => this with { PageNumber = Math.Max(0, PageNumber - 1) };

    /// <summary>
    /// Creates a new QueryParameters with a specific page.
    /// </summary>
    public QueryParameters WithPage(int page) => this with { PageNumber = Math.Max(0, page) };

    /// <summary>
    /// Creates a new QueryParameters with a search term.
    /// </summary>
    public QueryParameters WithSearch(string? search) => this with { SearchString = search, PageNumber = 0 };

    /// <summary>
    /// Creates a new QueryParameters with a sort order.
    /// </summary>
    public QueryParameters WithOrderBy(string? orderBy) => this with { OrderBy = orderBy, PageNumber = 0 };
}
