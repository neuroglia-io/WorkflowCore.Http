using Microsoft.AspNetCore.Http;

namespace WorkflowCore.Http.Services
{
    /// <summary>
    /// Defines the fundamentals of a service used to resolve a correlation id from an <see cref="HttpRequest"/>
    /// </summary>
    public interface IHttpRequestCorrelationIdResolver
    {

        /// <summary>
        /// Resolves a correlation id from the specified <see cref="HttpRequest"/>
        /// </summary>
        /// <param name="request">The <see cref="HttpRequest"/> to resolve the correlation id from</param>
        /// <returns>The resolved correlation id</returns>
        string Resolve(HttpRequest request);

    }

}
