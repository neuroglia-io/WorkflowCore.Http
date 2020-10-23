using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Http.Primitives;

namespace WorkflowCore.Http.Services
{

    /// <summary>
    /// Represents the middleware used to await for incoming <see cref="HttpRequest"/>s
    /// </summary>
    public class HttpRequestAwaiterMiddleware
    {

        /// <summary>
        /// Initializes a new <see cref="HttpRequestAwaiterMiddleware"/>
        /// </summary>
        /// <param name="httpRequestAwaiterManager">The service used to manage <see cref="IHttpRequestAwaiter"/>s</param>
        /// <param name="next">The next <see cref="RequestDelegate"/> in the pipeline</param>
        public HttpRequestAwaiterMiddleware(IHttpRequestAwaiterManager httpRequestAwaiterManager, RequestDelegate next)
        {
            this.HttpRequestAwaiterManager = httpRequestAwaiterManager;
            this.Next = next;
        }

        /// <summary>
        /// Gets the service used to manage <see cref="IHttpRequestAwaiter"/>s
        /// </summary>
        protected IHttpRequestAwaiterManager HttpRequestAwaiterManager { get; }

        /// <summary>
        /// Gets the next <see cref="RequestDelegate"/> in the pipeline
        /// </summary>
        protected RequestDelegate Next { get; }

        /// <inheritdoc/>
        public virtual async Task InvokeAsync(HttpContext httpContext)
        {
            IEnumerable<IHttpRequestAwaiter> awaiters = this.HttpRequestAwaiterManager.GetAwaitersFor(httpContext.Request);
            if (!awaiters.Any())
            {
                await this.Next(httpContext);
                return;
            }
            foreach(IHttpRequestAwaiter awaiter in awaiters)
            {
                awaiter.Set(httpContext.Request);
            }
        }

    }

}
