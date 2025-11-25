using Microsoft.OpenApi;
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

            options.AddSecurityRequirement(_ => new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference("Bearer"),
                    new List<string>()
                }
            });

            // Add RFC 7807 Problem Details schema support
            options.MapType<Microsoft.AspNetCore.Mvc.ProblemDetails>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.Object,
                Properties = new Dictionary<string, IOpenApiSchema>
                {
                    ["type"] = new OpenApiSchema { Type = JsonSchemaType.String, Format = "uri" },
                    ["title"] = new OpenApiSchema { Type = JsonSchemaType.String },
                    ["status"] = new OpenApiSchema { Type = JsonSchemaType.Integer, Format = "int32" },
                    ["detail"] = new OpenApiSchema { Type = JsonSchemaType.String },
                    ["instance"] = new OpenApiSchema { Type = JsonSchemaType.String, Format = "uri" },
                    ["traceId"] = new OpenApiSchema { Type = JsonSchemaType.String }
                },
                AdditionalProperties = new OpenApiSchema { Type = JsonSchemaType.Object }
            });

            // Add custom filters for Problem Details
            options.OperationFilter<ProblemDetailsOperationFilter>();
            options.SchemaFilter<ProblemDetailsSchemaFilter>();

            // Add tenant header support
            options.OperationFilter<TenantHeaderOperationFilter>();

            // Custom schema IDs for better documentation
            options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
        });

        return services;
    }
}
