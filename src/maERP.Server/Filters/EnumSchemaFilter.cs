using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace maERP.Server.Filters
{
    /// <summary>
    /// Adds enum descriptions to the Swagger schema by extracting the [Description] attribute from enum members.
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var enumDescriptions = new List<string>();
                foreach (var name in Enum.GetNames(context.Type))
                {
                    var member = context.Type.GetMember(name).FirstOrDefault();
                    var descriptionAttribute = member?.GetCustomAttribute<DescriptionAttribute>();
                    if (descriptionAttribute != null)
                        enumDescriptions.Add($"{name} - {descriptionAttribute.Description}");
                    else
                        enumDescriptions.Add(name);
                }

                if (!string.IsNullOrEmpty(schema.Description))
                {
                    schema.Description += "\n";
                }
                schema.Description += "Enum values: " + string.Join(", ", enumDescriptions);
            }
        }
    }
}
