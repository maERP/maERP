using Microsoft.OpenApi.Models;
using maERP.Server.Filters;
using System.Reflection;

namespace maERP.Server.ServiceRegistrations;

public static class SwaggerRegistrationService
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "maERP.Server API", 
                Version = "v1",
                Description = "RESTful API for maERP - Enterprise Resource Planning System",
                Contact = new OpenApiContact
                {
                    Name = "maERP",
                    Email = "support@maerp.de",
                    Url = new Uri("https://github.com/maERP")
                },
                License = new OpenApiLicense
                {
                    Name = "GPL 3.0 License",
                    Url = new Uri("https://opensource.org/license/gpl-3-0")
                }
            });

            // Include XML comments for enhanced documentation
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }

            // JWT Bearer Authentication
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement 
            {
                {
                    new OpenApiSecurityScheme 
                    {
                        Reference = new OpenApiReference 
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            // Add RFC 7807 Problem Details schema support
            options.MapType<Microsoft.AspNetCore.Mvc.ProblemDetails>(() => new OpenApiSchema
            {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    ["type"] = new OpenApiSchema { Type = "string", Format = "uri" },
                    ["title"] = new OpenApiSchema { Type = "string" },
                    ["status"] = new OpenApiSchema { Type = "integer", Format = "int32" },
                    ["detail"] = new OpenApiSchema { Type = "string" },
                    ["instance"] = new OpenApiSchema { Type = "string", Format = "uri" },
                    ["traceId"] = new OpenApiSchema { Type = "string" }
                },
                AdditionalProperties = new OpenApiSchema { Type = "object" }
            });

            // Add custom filters for Problem Details
            options.OperationFilter<ProblemDetailsOperationFilter>();
            options.SchemaFilter<ProblemDetailsSchemaFilter>();

            // Custom schema IDs for better documentation
            options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
        });

        return services;
    }
}