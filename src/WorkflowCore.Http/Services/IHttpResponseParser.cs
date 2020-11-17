using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCore.Http.Primitives;

namespace WorkflowCore.Http.Services
{

    /// <summary>
    /// Defines the fundamentals of a service used to parse <see cref="HttpResponseMessage"/>s
    /// </summary>
    public interface IHttpResponseParser
    {

        /// <summary>
        /// Parses the specified <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="httpResponse">The <see cref="HttpResponseMessage"/> to parse</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="IHttpResponseDescriptor"/> describing the specified <see cref="HttpResponseMessage"/></returns>
        Task<IHttpResponseDescriptor> ParseAsync(HttpResponseMessage httpResponse, CancellationToken cancellationToken = default);

    }

}
