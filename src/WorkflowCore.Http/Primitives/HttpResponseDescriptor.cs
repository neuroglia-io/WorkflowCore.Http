using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace WorkflowCore.Http.Primitives
{

    /// <summary>
    /// Represents the object used to describe an <see cref="HttpResponseMessage"/>
    /// </summary>
    public class HttpResponseDescriptor
        : IHttpResponseDescriptor
    {

        /// <summary>
        /// Initializes a new <see cref="HttpResponseDescriptor"/>
        /// </summary>
        protected HttpResponseDescriptor()
        {

        }

        /// <summary>
        /// Initializes a new <see cref="HttpResponseDescriptor"/>
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpResponseMessage"/>'s <see cref="HttpStatusCode"/></param>
        /// <param name="headers">An <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="HttpResponseMessage"/>'s headers</param>
        /// <param name="content">The <see cref="HttpResponseMessage"/>'s content</param>
        public HttpResponseDescriptor(HttpStatusCode statusCode, IDictionary<string, IEnumerable<string>> headers, object content)
        {
            this.StatusCode = statusCode;
            this.Headers = headers;
            this.Content = content;
        }

        /// <inheritdoc/>
        public HttpStatusCode StatusCode { get; }

        /// <inheritdoc/>
        public IDictionary<string, IEnumerable<string>> Headers { get; }

        /// <inheritdoc/>
        public object Content { get; }

    }

}
