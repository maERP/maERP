using System.Net;
using maERP.Application.Exceptions;
using maERP.Server.Models;
using Newtonsoft.Json;

namespace maERP.Server.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails problem;

        switch (ex)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails
                {
                    Title = validationException.Message,
                    Status = (int)statusCode,
                    Detail = validationException.InnerException?.Message,
                    Type = nameof(ValidationException),
                    Errors = validationException.ValidationErrors
                };
                break;

            case NotFoundException notFound:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails
                {
                    Title = notFound.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = notFound.InnerException?.Message,
                };
                break;

            default:
                _logger.LogError(ex, "Unhandled exception at {Path}", httpContext.Request.Path);
                problem = new CustomProblemDetails
                {
                    Title = "An internal server error occurred.",
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        _logger.LogError("Exception response: {StatusCode} {Title}", problem.Status, problem.Title);
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}