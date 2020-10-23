using Microsoft.AspNetCore.Builder;
using WorkflowCore.Http.Primitives;
using WorkflowCore.Http.Services;
using WorkflowCore.Interface;

namespace WorkflowCore.Http
{
    /// <summary>
    /// Defines extensions for <see cref="IApplicationBuilder"/>s
    /// </summary>
    public static class IApplicationBuilderExtensions
    {

        /// <summary>
        /// Uses the <see cref="HttpRequestAwaiterMiddleware"/> to enable the use of the <see cref="ReceiveHttpRequest"/> <see cref="IStepBody"/>
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHttpWorkflow(this IApplicationBuilder app)
        {
            app.UseMiddleware<HttpRequestAwaiterMiddleware>();
            return app;
        }

    }

}
