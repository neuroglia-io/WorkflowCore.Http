using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace WorkflowCore.Http.Services
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IHttpRequestCorrelationIdResolver"/><para></para>
    /// The correlation id is resolved by looking up for an 'X-Correlation-Id' header in incoming <see cref="HttpRequest"/>s
    /// </summary>
    public class HttpRequestCorrelationIdResolver
        : IHttpRequestCorrelationIdResolver
    {

        public const string CorrelationIdHeader = "X-Correlation-Id";

        /// <inheritdoc/>
        public virtual string Resolve(HttpRequest request)
        {
            if (request.Headers.TryGetValue(CorrelationIdHeader, out StringValues correlationId))
                return correlationId.FirstOrDefault();
            else
                return null;
        }

    }
}
