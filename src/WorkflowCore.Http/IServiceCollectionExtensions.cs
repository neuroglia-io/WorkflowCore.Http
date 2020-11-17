using Microsoft.Extensions.DependencyInjection;
using System;
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
        /// <param name="configurationAction">An <see cref="Action{T}"/> used to configure http workflows</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddHttpWorkflow(this IServiceCollection services, Action<IHttpWorkflowOptionsBuilder> configurationAction)
        {
            IHttpWorkflowOptionsBuilder builder = new HttpWorkflowOptionsBuilder();
            configurationAction(builder);
            HttpWorkflowOptions options = builder.Build();
            services.AddTransient(typeof(IHttpResponseParser), options.ResponseParserType);
            foreach(Type responseContentParserType in options.ResponseContentParserTypes)
            {
                services.AddTransient(typeof(IHttpResponseContentParser), responseContentParserType);
            }
            services.AddTransient<SendHttpRequest>();
            services.AddTransient<ReceiveHttpRequest>();
            services.AddSingleton<IHttpRequestAwaiterManager, HttpRequestAwaiterManager>();
            return services;
        }

    }

}
