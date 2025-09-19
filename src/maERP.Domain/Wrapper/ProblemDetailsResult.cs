namespace maERP.Domain.Wrapper;

/// <summary>
/// RFC 7807 Problem Details implementation for structured error responses.
/// Extends the base Result class with standardized error information format.
/// </summary>
public class ProblemDetailsResult : Result
{
    /// <summary>
    /// A URI reference that identifies the problem type.
    /// This specification encourages that, when dereferenced,
    /// it provide human-readable documentation for the problem type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// A short, human-readable summary of the problem type.
    /// It SHOULD NOT change from occurrence to occurrence of the problem.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// A human-readable explanation specific to this occurrence of the problem.
    /// </summary>
    public string? Detail { get; set; }

    /// <summary>
    /// A URI reference that identifies the specific occurrence of the problem.
    /// It may or may not yield further information if dereferenced.
    /// </summary>
    public string? Instance { get; set; }

    /// <summary>
    /// Additional members for problem-specific extension data.
    /// </summary>
    public Dictionary<string, object>? Extensions { get; set; }

    public static ProblemDetailsResult BadRequest(string title, string detail, string? type = null, string? instance = null)
    {
        return new ProblemDetailsResult
        {
            Succeeded = false,
            StatusCode = ResultStatusCode.BadRequest,
            Type = type ?? "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = title,
            Detail = detail,
            Instance = instance,
            Messages = new List<string> { detail }
        };
    }

    public static ProblemDetailsResult NotFound(string title, string detail, string? type = null, string? instance = null)
    {
        return new ProblemDetailsResult
        {
            Succeeded = false,
            StatusCode = ResultStatusCode.NotFound,
            Type = type ?? "https://tools.ietf.org/html/rfc9110#section-15.5.5",
            Title = title,
            Detail = detail,
            Instance = instance,
            Messages = new List<string> { detail }
        };
    }

    public static ProblemDetailsResult Unauthorized(string title, string detail, string? type = null, string? instance = null)
    {
        return new ProblemDetailsResult
        {
            Succeeded = false,
            StatusCode = ResultStatusCode.Unauthorized,
            Type = type ?? "https://tools.ietf.org/html/rfc9110#section-15.5.2",
            Title = title,
            Detail = detail,
            Instance = instance,
            Messages = new List<string> { detail }
        };
    }

    public static ProblemDetailsResult Forbidden(string title, string detail, string? type = null, string? instance = null)
    {
        return new ProblemDetailsResult
        {
            Succeeded = false,
            StatusCode = ResultStatusCode.Forbidden,
            Type = type ?? "https://tools.ietf.org/html/rfc9110#section-15.5.4",
            Title = title,
            Detail = detail,
            Instance = instance,
            Messages = new List<string> { detail }
        };
    }

    public static ProblemDetailsResult InternalServerError(string title, string detail, string? type = null, string? instance = null)
    {
        return new ProblemDetailsResult
        {
            Succeeded = false,
            StatusCode = ResultStatusCode.InternalServerError,
            Type = type ?? "https://tools.ietf.org/html/rfc9110#section-15.6.1",
            Title = title,
            Detail = detail,
            Instance = instance,
            Messages = new List<string> { detail }
        };
    }

    public static Task<ProblemDetailsResult> BadRequestAsync(string title, string detail, string? type = null, string? instance = null)
    {
        return Task.FromResult(BadRequest(title, detail, type, instance));
    }

    public static Task<ProblemDetailsResult> NotFoundAsync(string title, string detail, string? type = null, string? instance = null)
    {
        return Task.FromResult(NotFound(title, detail, type, instance));
    }

    public static Task<ProblemDetailsResult> UnauthorizedAsync(string title, string detail, string? type = null, string? instance = null)
    {
        return Task.FromResult(Unauthorized(title, detail, type, instance));
    }

    public static Task<ProblemDetailsResult> ForbiddenAsync(string title, string detail, string? type = null, string? instance = null)
    {
        return Task.FromResult(Forbidden(title, detail, type, instance));
    }

    public static Task<ProblemDetailsResult> InternalServerErrorAsync(string title, string detail, string? type = null, string? instance = null)
    {
        return Task.FromResult(InternalServerError(title, detail, type, instance));
    }

    /// <summary>
    /// Adds extension data to the problem details.
    /// </summary>
    public ProblemDetailsResult WithExtension(string key, object value)
    {
        Extensions ??= new Dictionary<string, object>();
        Extensions[key] = value;
        return this;
    }

    /// <summary>
    /// Sets the instance URI for this specific problem occurrence.
    /// </summary>
    public ProblemDetailsResult WithInstance(string instance)
    {
        Instance = instance;
        return this;
    }
}

/// <summary>
/// Generic Problem Details result that includes typed data.
/// </summary>
public class ProblemDetailsResult<T> : ProblemDetailsResult, IResult<T>
{
    public T Data { get; set; } = default!;

    public static ProblemDetailsResult<T> BadRequest(string title, string detail, T? data = default, string? type = null, string? instance = null)
    {
        return new ProblemDetailsResult<T>
        {
            Succeeded = false,
            StatusCode = ResultStatusCode.BadRequest,
            Type = type ?? "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = title,
            Detail = detail,
            Instance = instance,
            Data = data!,
            Messages = new List<string> { detail }
        };
    }

    public static ProblemDetailsResult<T> NotFound(string title, string detail, T? data = default, string? type = null, string? instance = null)
    {
        return new ProblemDetailsResult<T>
        {
            Succeeded = false,
            StatusCode = ResultStatusCode.NotFound,
            Type = type ?? "https://tools.ietf.org/html/rfc9110#section-15.5.5",
            Title = title,
            Detail = detail,
            Instance = instance,
            Data = data!,
            Messages = new List<string> { detail }
        };
    }

    public static Task<ProblemDetailsResult<T>> BadRequestAsync(string title, string detail, T? data = default, string? type = null, string? instance = null)
    {
        return Task.FromResult(BadRequest(title, detail, data, type, instance));
    }

    public static Task<ProblemDetailsResult<T>> NotFoundAsync(string title, string detail, T? data = default, string? type = null, string? instance = null)
    {
        return Task.FromResult(NotFound(title, detail, data, type, instance));
    }
}