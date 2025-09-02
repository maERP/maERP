using maERP.Domain.Wrapper;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Extensions;

/// <summary>
/// Extension methods for converting Result objects to ActionResult responses.
/// Provides consistent mapping from Result patterns to HTTP responses.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts a generic Result to an ActionResult with proper HTTP status code mapping.
    /// </summary>
    /// <typeparam name="T">The type of data contained in the result</typeparam>
    /// <param name="result">The result to convert</param>
    /// <returns>An ActionResult with appropriate HTTP status code and response body</returns>
    public static ActionResult ToActionResult<T>(this Result<T> result)
    {
        return new ObjectResult(result)
        {
            StatusCode = (int)result.StatusCode
        };
    }

    /// <summary>
    /// Converts a non-generic Result to an ActionResult with proper HTTP status code mapping.
    /// </summary>
    /// <param name="result">The result to convert</param>
    /// <returns>An ActionResult with appropriate HTTP status code and response body</returns>
    public static ActionResult ToActionResult(this Domain.Wrapper.IResult result)
    {
        return new ObjectResult(result)
        {
            StatusCode = (int)result.StatusCode
        };
    }

    /// <summary>
    /// Converts a ProblemDetailsResult to an ActionResult that follows RFC 7807.
    /// </summary>
    /// <param name="result">The problem details result to convert</param>
    /// <returns>An ObjectResult configured for RFC 7807 Problem Details format</returns>
    public static ActionResult ToActionResult(this ProblemDetailsResult result)
    {
        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Type = result.Type,
            Title = result.Title,
            Detail = result.Detail,
            Status = (int)result.StatusCode,
            Instance = result.Instance
        };

        // Add extensions if they exist
        if (result.Extensions != null)
        {
            foreach (var extension in result.Extensions)
            {
                problemDetails.Extensions[extension.Key] = extension.Value;
            }
        }

        return new ObjectResult(problemDetails)
        {
            StatusCode = (int)result.StatusCode,
            ContentTypes = { "application/problem+json" }
        };
    }

    /// <summary>
    /// Converts a generic ProblemDetailsResult to an ActionResult that follows RFC 7807.
    /// </summary>
    /// <typeparam name="T">The type of data contained in the result</typeparam>
    /// <param name="result">The problem details result to convert</param>
    /// <returns>An ObjectResult configured for RFC 7807 Problem Details format</returns>
    public static ActionResult ToActionResult<T>(this ProblemDetailsResult<T> result)
    {
        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Type = result.Type,
            Title = result.Title,
            Detail = result.Detail,
            Status = (int)result.StatusCode,
            Instance = result.Instance
        };

        // Add extensions if they exist
        if (result.Extensions != null)
        {
            foreach (var extension in result.Extensions)
            {
                problemDetails.Extensions[extension.Key] = extension.Value;
            }
        }

        // Add data if it exists and is not null
        if (result.Data != null)
        {
            problemDetails.Extensions["data"] = result.Data;
        }

        return new ObjectResult(problemDetails)
        {
            StatusCode = (int)result.StatusCode,
            ContentTypes = { "application/problem+json" }
        };
    }
}