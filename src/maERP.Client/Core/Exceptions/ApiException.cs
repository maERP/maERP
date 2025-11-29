using System.Net;

namespace maERP.Client.Core.Exceptions;

/// <summary>
/// Exception thrown when an API request fails with error messages from the server.
/// This exception carries the structured error messages from the API response.
/// </summary>
public class ApiException : Exception
{
    /// <summary>
    /// HTTP status code of the failed response.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Error messages returned by the API.
    /// These are typically validation errors or business logic error messages.
    /// </summary>
    public IReadOnlyList<string> Messages { get; }

    /// <summary>
    /// Creates a new ApiException with status code and error messages.
    /// </summary>
    /// <param name="statusCode">HTTP status code of the response.</param>
    /// <param name="messages">Error messages from the API.</param>
    public ApiException(HttpStatusCode statusCode, IEnumerable<string> messages)
        : base(FormatMessage(statusCode, messages))
    {
        StatusCode = statusCode;
        Messages = messages?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly();
    }

    /// <summary>
    /// Creates a new ApiException with status code and a single error message.
    /// </summary>
    /// <param name="statusCode">HTTP status code of the response.</param>
    /// <param name="message">Single error message.</param>
    public ApiException(HttpStatusCode statusCode, string message)
        : this(statusCode, new[] { message })
    {
    }

    /// <summary>
    /// Creates a new ApiException from an inner exception.
    /// </summary>
    /// <param name="statusCode">HTTP status code of the response.</param>
    /// <param name="messages">Error messages from the API.</param>
    /// <param name="innerException">Inner exception that caused this exception.</param>
    public ApiException(HttpStatusCode statusCode, IEnumerable<string> messages, Exception innerException)
        : base(FormatMessage(statusCode, messages), innerException)
    {
        StatusCode = statusCode;
        Messages = messages?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly();
    }

    /// <summary>
    /// Gets a combined error message string suitable for display.
    /// Multiple messages are joined with newlines.
    /// </summary>
    public string CombinedMessage => Messages.Count > 0
        ? string.Join(Environment.NewLine, Messages)
        : $"API request failed with status code {(int)StatusCode}";

    private static string FormatMessage(HttpStatusCode statusCode, IEnumerable<string> messages)
    {
        var messageList = messages?.ToList() ?? new List<string>();
        return messageList.Count > 0
            ? string.Join("; ", messageList)
            : $"API request failed with status code {(int)statusCode}";
    }
}
