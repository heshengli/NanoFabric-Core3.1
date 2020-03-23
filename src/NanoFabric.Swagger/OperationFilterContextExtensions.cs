using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NanoFabric.Swagger
{
    internal static class OperationFilterContextExtensions
    {
        internal static bool HasAuthorize(this OperationFilterContext context)
        {
            return context.ApiDescription.CustomAttributes().OfType<AuthorizeAttribute>().Any();
        }
    }
}