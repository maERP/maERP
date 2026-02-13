using System.Net;
using maERP.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();
        problemDetails.Instance = httpContext.Request.Path;
        
        if (exception is BaseException e)
        {
            httpContext.Response.StatusCode = (int)e.StatusCode;
            problemDetails.Title = e.Message;
        }
        else if (exception is NotFoundException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            problemDetails.Title = exception.Message;
        }
        else
        {
            logger.LogError(exception, "Unhandled exception at {Path}", httpContext.Request.Path);
            problemDetails.Title = "An internal server error occurred.";
        }
        logger.LogError("Exception response: {StatusCode} {Title}", problemDetails.Status, problemDetails.Title);
        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}

public class BaseException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
    }
}