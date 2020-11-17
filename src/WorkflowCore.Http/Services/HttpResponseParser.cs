using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCore.Http.Primitives;

namespace WorkflowCore.Http.Services
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IHttpResponseParser"/> interface
    /// </summary>
    public class HttpResponseParser
        : IHttpResponseParser
    {

        /// <summary>
        /// Initializes a new <see cref="HttpResponseParser"/>
        /// </summary>
        /// <param name="logger">The service used to perform logging</param>
        /// <param name="contentParsers">An <see cref="IEnumerable{T}"/> containing all available <see cref="IHttpResponseContentParser"/>s</param>
        public HttpResponseParser(ILogger<HttpResponseParser> logger, IEnumerable<IHttpResponseContentParser> contentParsers)
        {
            this.Logger = logger;
            this.ContentParsers = contentParsers;
        }

        /// <summary>
        /// Gets the service used to perform logging
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing all available <see cref="IHttpResponseContentParser"/>s
        /// </summary>
        protected IEnumerable<IHttpResponseContentParser> ContentParsers { get; }

        /// <inheritdoc/>
        public virtual async Task<IHttpResponseDescriptor> ParseAsync(HttpResponseMessage httpResponse, CancellationToken cancellationToken = default)
        {
            object content = null;
            IHttpResponseContentParser contentParser = this.ContentParsers.FirstOrDefault(p => p.CanParse(httpResponse.Content.Headers.ContentType.MediaType));
            if (contentParser == null)
                this.Logger.LogInformation($"Failed to find an {nameof(IHttpResponseContentParser)} able to parse the specified content type '{{contentType}}'", httpResponse.Content.Headers.ContentType.MediaType);
            content = await contentParser.ParseAsync(httpResponse, cancellationToken);
            return new HttpResponseDescriptor(httpResponse.StatusCode, httpResponse.Headers.ToDictionary(h => h.Key, h => h.Value), content);
        }

    }

}
