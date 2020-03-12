using Microsoft.OpenApi.Models;
using NanoFabric.Core;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace NanoFabric.Swagger
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        private readonly IApiInfo _apiInfo;

        public AuthorizeCheckOperationFilter(IApiInfo apiInfo)
        {
            _apiInfo = apiInfo;
        }


        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!context.HasAuthorize()) return;

            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement()
                {
                    { new OpenApiSecurityScheme { Name = "oauth2" }, _apiInfo.Scopes.Keys.ToList()}
                }
            };
        }
    }
}
