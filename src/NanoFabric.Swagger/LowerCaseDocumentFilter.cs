using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NanoFabric.Swagger
{
    /// <summary>
    /// Converts Swagger document paths to lower case.
    /// </summary>
    public sealed class LowerCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            //swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary(x => x.Key.ToLower(), x => x.Value);

            //swaggerDoc.Paths = swaggerDoc.Paths.Select(t=>new OpenApiPaths() 
            //{
            //    { new OpenApiPathItem{ } }
            //}) ;
            swaggerDoc.Paths = swaggerDoc.Paths;

        }
    }
}
