using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

/// <summary>
/// Represents a custom attribute for Swagger headers.
/// </summary>
public class SwaggerHeaderAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the name of the header.
    /// </summary>
    public enum HeaderNameEnum
    {

        /// <summary>
        /// Represents the header names for pagination.
        /// </summary>
        Pagination
    }

    /// <summary>
    /// Gets the name of the header.
    /// </summary>
    public HeaderNameEnum HeaderName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SwaggerHeaderAttribute"/> class with the specified header name.
    /// </summary>
    /// <param name="headerName">The name of the header.</param>
    public SwaggerHeaderAttribute(HeaderNameEnum headerName)
    {
        HeaderName = headerName;
    }
}

/// <summary>
/// Represents a filter for adding response headers to an operation.
/// </summary>
public class AddResponseHeadersFilter : IOperationFilter
{
    /// <summary>
    /// Applies the filter to the specified operation.
    /// </summary>
    /// <param name="operation">The operation to apply the filter to.</param>
    /// <param name="context">The context of the operation filter.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.GetCustomAttributes(typeof(SwaggerHeaderAttribute), false).FirstOrDefault() is SwaggerHeaderAttribute attribute)
        {
            switch (attribute.HeaderName)
            {
                case SwaggerHeaderAttribute.HeaderNameEnum.Pagination:
                    AddPaginationHeader(operation);
                    break;
            }
        }
    }

    private void AddPaginationHeader(OpenApiOperation operation)
    {
        if (operation.Responses.ContainsKey("200"))
        {
            var response = operation.Responses["200"];
            if (response.Headers == null)
            {
                response.Headers = new Dictionary<string, OpenApiHeader>();
            }

            if (!response.Headers.ContainsKey("X-Pagination"))
            {
                response.Headers.Add("X-Pagination", new OpenApiHeader
                {
                    Description = "Pagination header",
                    Schema = new OpenApiSchema
                    {
                        Type = "Object",
                        Properties = new Dictionary<string, OpenApiSchema>
                        {
                            ["TotalItemCount"] = new OpenApiSchema
                            {
                                Type = "integer",
                                Title = "TotalItemCount",
                                Description = "The total number of items in the result set"
                            },
                            ["TotalPageCount"] = new OpenApiSchema
                            {
                                Type = "integer",
                                Title = "TotalPageCount",
                                Description = "The total number of pages based on the page selected page size"
                            },
                            ["PageSize"] = new OpenApiSchema
                            {
                                Type = "integer",
                                Title = "PageSize",
                                Description = "The number of items in a page"
                            },
                            ["CurrentPage"] = new OpenApiSchema
                            {
                                Type = "integer",
                                Title = "CurrentPage",
                                Description = "The current page number"
                            }
                        }
                    }
                });
            }
        }
    }
}