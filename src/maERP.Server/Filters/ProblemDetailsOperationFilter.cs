using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace maERP.Server.Filters;

/// <summary>
/// Swagger operation filter that automatically adds RFC 7807 Problem Details responses
/// to all endpoints that can return error responses.
/// </summary>
public class ProblemDetailsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Get all ProducesResponseType attributes from the method
        var responseTypes = context.MethodInfo
            .GetCustomAttributes<ProducesResponseTypeAttribute>()
            .ToList();

        // Add Problem Details schema for common error status codes
        AddProblemDetailsResponse(operation, "400", "Bad Request");
        AddProblemDetailsResponse(operation, "401", "Unauthorized");
        AddProblemDetailsResponse(operation, "403", "Forbidden");
        AddProblemDetailsResponse(operation, "404", "Not Found");
        AddProblemDetailsResponse(operation, "500", "Internal Server Error");

        // Check if operation already has these responses defined and preserve existing ones
        foreach (var responseType in responseTypes)
        {
            var statusCode = responseType.StatusCode.ToString();
            if (IsErrorStatusCode(responseType.StatusCode) && !operation.Responses.ContainsKey(statusCode))
            {
                AddProblemDetailsResponse(operation, statusCode, GetStatusCodeDescription(responseType.StatusCode));
            }
        }
    }

    private static void AddProblemDetailsResponse(OpenApiOperation operation, string statusCode, string description)
    {
        if (operation.Responses.ContainsKey(statusCode))
        {
            // If response already exists, ensure it includes Problem Details content type
            var existingResponse = operation.Responses[statusCode];
            if (!existingResponse.Content.ContainsKey("application/problem+json"))
            {
                existingResponse.Content.Add("application/problem+json", new OpenApiMediaType
                {
                    Schema = CreateProblemDetailsSchema()
                });
            }
        }
        else
        {
            // Add new Problem Details response
            operation.Responses.Add(statusCode, new OpenApiResponse
            {
                Description = description,
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/problem+json"] = new OpenApiMediaType
                    {
                        Schema = CreateProblemDetailsSchema(),
                        Examples = CreateProblemDetailsExamples(statusCode, description)
                    }
                }
            });
        }
    }

    private static OpenApiSchema CreateProblemDetailsSchema()
    {
        return new OpenApiSchema
        {
            Type = "object",
            Properties = new Dictionary<string, OpenApiSchema>
            {
                ["type"] = new OpenApiSchema
                {
                    Type = "string",
                    Format = "uri",
                    Description = "A URI reference that identifies the problem type"
                },
                ["title"] = new OpenApiSchema
                {
                    Type = "string",
                    Description = "A short, human-readable summary of the problem type"
                },
                ["status"] = new OpenApiSchema
                {
                    Type = "integer",
                    Format = "int32",
                    Description = "The HTTP status code"
                },
                ["detail"] = new OpenApiSchema
                {
                    Type = "string",
                    Description = "A human-readable explanation specific to this occurrence"
                },
                ["instance"] = new OpenApiSchema
                {
                    Type = "string",
                    Format = "uri",
                    Description = "A URI reference that identifies the specific occurrence"
                },
                ["traceId"] = new OpenApiSchema
                {
                    Type = "string",
                    Description = "The correlation ID for tracing this request"
                }
            },
            AdditionalProperties = new OpenApiSchema
            {
                Type = "object",
                Description = "Additional problem-specific extension data"
            },
            Example = new Microsoft.OpenApi.Any.OpenApiObject
            {
                ["type"] = new Microsoft.OpenApi.Any.OpenApiString("https://tools.ietf.org/html/rfc9110#section-15.5.1"),
                ["title"] = new Microsoft.OpenApi.Any.OpenApiString("Bad Request"),
                ["status"] = new Microsoft.OpenApi.Any.OpenApiInteger(400),
                ["detail"] = new Microsoft.OpenApi.Any.OpenApiString("The request could not be processed due to invalid input"),
                ["instance"] = new Microsoft.OpenApi.Any.OpenApiString("/api/v1/products/123"),
                ["traceId"] = new Microsoft.OpenApi.Any.OpenApiString("0HN7KBGV5C3QD:00000001")
            }
        };
    }

    private static Dictionary<string, OpenApiExample> CreateProblemDetailsExamples(string statusCode, string description)
    {
        return new Dictionary<string, OpenApiExample>
        {
            ["default"] = new OpenApiExample
            {
                Summary = $"{description} example",
                Description = $"Example of RFC 7807 Problem Details response for {statusCode} {description}",
                Value = new Microsoft.OpenApi.Any.OpenApiObject
                {
                    ["type"] = new Microsoft.OpenApi.Any.OpenApiString(GetTypeForStatusCode(statusCode)),
                    ["title"] = new Microsoft.OpenApi.Any.OpenApiString(description),
                    ["status"] = new Microsoft.OpenApi.Any.OpenApiInteger(int.Parse(statusCode)),
                    ["detail"] = new Microsoft.OpenApi.Any.OpenApiString(GetDetailForStatusCode(statusCode)),
                    ["instance"] = new Microsoft.OpenApi.Any.OpenApiString("/api/v1/endpoint"),
                    ["traceId"] = new Microsoft.OpenApi.Any.OpenApiString("0HN7KBGV5C3QD:00000001")
                }
            }
        };
    }

    private static string GetTypeForStatusCode(string statusCode)
    {
        return statusCode switch
        {
            "400" => "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            "401" => "https://tools.ietf.org/html/rfc9110#section-15.5.2", 
            "403" => "https://tools.ietf.org/html/rfc9110#section-15.5.4",
            "404" => "https://tools.ietf.org/html/rfc9110#section-15.5.5",
            "500" => "https://tools.ietf.org/html/rfc9110#section-15.6.1",
            _ => "https://tools.ietf.org/html/rfc9110"
        };
    }

    private static string GetDetailForStatusCode(string statusCode)
    {
        return statusCode switch
        {
            "400" => "The request could not be processed due to invalid input or missing required fields",
            "401" => "Authentication is required to access this resource",
            "403" => "You do not have permission to access this resource",
            "404" => "The requested resource was not found",
            "500" => "An internal server error occurred while processing the request",
            _ => "An error occurred while processing the request"
        };
    }

    private static bool IsErrorStatusCode(int statusCode)
    {
        return statusCode >= 400;
    }

    private static string GetStatusCodeDescription(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            422 => "Unprocessable Entity",
            500 => "Internal Server Error",
            _ => "Error"
        };
    }
}