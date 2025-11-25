using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace maERP.Server.Filters;

public class TenantHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Tenant-Id",
            In = ParameterLocation.Header,
            Required = false,
            Description = "The tenant identifier for multi-tenant operations",
            Schema = new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Format = "uuid",
                Default = JsonValue.Create("11111111-1111-1111-1111-111111111111")
            }
        });
    }
}