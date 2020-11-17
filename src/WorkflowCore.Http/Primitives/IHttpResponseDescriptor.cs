using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace WorkflowCore.Http.Primitives
{
    /// <summary>
    /// Defines the fundamentals of an object used to describe an <see cref="HttpResponseMessage"/>
    /// </summary>
    public interface IHttpResponseDescriptor
    {

        /// <summary>
        /// Gets the <see cref="HttpResponseMessage"/>'s <see cref="HttpStatusCode"/>
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="HttpResponseMessage"/>'s headers 
        /// </summary>
        IDictionary<string, IEnumerable<string>> Headers { get; }

        /// <summary>
        /// Gets the <see cref="HttpResponseMessage"/>'s content
        /// </summary>
        object Content { get; }

    }

}
