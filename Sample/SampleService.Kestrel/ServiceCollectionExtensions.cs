using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NanoFabric.Mediatr;

namespace SampleService.Kestrel
{
    /// <summary>
    /// 应用扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequestManager"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication<TRequestManager>(
            this IServiceCollection services, IConfiguration configuration
        ) where TRequestManager : class, IRequestManager
        {
            return services
                .AddSingleton<IRequestManager, TRequestManager>()
                .AddSingleton(ApiInfo.Instantiate(configuration));
        }
    }
}
