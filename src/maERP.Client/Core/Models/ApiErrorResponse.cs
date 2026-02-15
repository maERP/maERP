namespace maERP.Client.Core.Models;

/// <summary>
/// Model to deserialize API error responses.
/// Supports both the standard ApiResponse format and RFC 7807 ProblemDetails.
/// </summary>
internal class ApiErrorResponse
{
    // Standard API response format
    public int StatusCode { get; set; }
    public List<string> Messages { get; set; } = new();
    public bool Succeeded { get; set; }

    // RFC 7807 ProblemDetails format
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
}
