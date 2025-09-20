using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RestaurantManagement.Api.Filters;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check if this is a file upload endpoint
        if (context.ApiDescription.RelativePath?.Contains("files/upload") == true)
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["file"] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary",
                                    Description = "Upload file"
                                },
                                ["category"] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Description = "File category (user, restaurant, dish, etc.)",
                                    Example = new Microsoft.OpenApi.Any.OpenApiString("user")
                                }
                            },
                            Required = new HashSet<string> { "file", "category" }
                        }
                    }
                }
            };

            // Clear parameters to avoid conflicts
            operation.Parameters?.Clear();
        }
    }
}
