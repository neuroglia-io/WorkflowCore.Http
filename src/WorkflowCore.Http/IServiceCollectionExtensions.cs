using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Http.Primitives;
using WorkflowCore.Http.Services;

namespace WorkflowCore.Http
{

    /// <summary>
    /// Defines extensions for <see cref="IServiceCollection"/>s
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Adds and configures all http services for the WorkflowCore workflow engine
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddHttpWorkflow(this IServiceCollection services)
        {
            services.AddTransient<SendHttpRequest>();
            services.AddTransient<ReceiveHttpRequest>();
            services.AddSingleton<IHttpRequestAwaiterManager, HttpRequestAwaiterManager>();
            return services;
        }

    }

}
