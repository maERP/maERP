namespace maERP.Client.Core.Models;

/// <summary>
/// Response wrapper for paginated API results.
/// Matches the PaginatedResult structure from the server.
/// </summary>
public class PaginatedResponse<T>
{
    public List<T> Data { get; set; } = new();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public bool Succeeded { get; set; }
    public List<string> Messages { get; set; } = new();
}
