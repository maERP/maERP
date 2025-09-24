using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace maERP.Server.Filters;

public class TenantHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Tenant-Id",
            In = ParameterLocation.Header,
            Required = false,
            Description = "The tenant identifier for multi-tenant operations",
            Schema = new OpenApiSchema
            {
                Type = "string",
                Format = "uuid",
                Default = new Microsoft.OpenApi.Any.OpenApiString("11111111-1111-1111-1111-111111111111")
            }
        });
    }
}