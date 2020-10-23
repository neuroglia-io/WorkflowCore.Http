using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WorkflowCore.Http.Primitives;

namespace WorkflowCore.Http.Services
{

    /// <summary>
    /// Defines the fundamentals of a service used to manage <see cref="IHttpRequestAwaiter"/>s
    /// </summary>
    public interface IHttpRequestAwaiterManager
    {

        /// <summary>
        /// Waits for an incoming <see cref="HttpRequest"/>
        /// </summary>
        /// <param name="method">The <see cref="HttpMethod"/> of the <see cref="HttpRequest"/> to await</param>
        /// <param name="path">The path requested by the <see cref="HttpRequest"/> to await</param>
        /// <param name="correlationIdResolver">The service used to resolve the correlation id from incoming <see cref="HttpRequest"/>s</param>
        /// <param name="correlationId">The correlation id to match</param>
        /// <returns>The awaited <see cref="HttpRequest"/></returns>
        Task<HttpRequest> WaitForAsync(HttpMethod method, string path, IHttpRequestCorrelationIdResolver correlationIdResolver, string correlationId);

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing all the <see cref="IHttpRequestAwaiter"/>s that match the specified <see cref="HttpRequest"/>
        /// </summary>
        /// <param name="request">The <see cref="HttpRequest"/> to match</param>
        /// <returns>A new <see cref="IEnumerable{T}"/> containing all the <see cref="IHttpRequestAwaiter"/>s that match the specified <see cref="HttpRequest"/></returns>
        IEnumerable<IHttpRequestAwaiter> GetAwaitersFor(HttpRequest request);

    }

}
