using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowCore.Http.Services
{
    /// <summary>
    /// Defines the fundamentals of a service used to parse the content of <see cref="HttpResponseMessage"/>s
    /// </summary>
    public interface IHttpResponseContentParser
    {

        /// <summary>
        /// Determines whether or not the <see cref="IHttpResponseContentParser"/> can parse the specified content type
        /// </summary>
        /// <param name="contentType">The content type to parse</param>
        /// <returns>A boolean indicating whether or not the <see cref="IHttpResponseContentParser"/> can parse the specified content type</returns>
        bool CanParse(string contentType);

        /// <summary>
        /// Parses the content of the specified <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="httpResponse">The <see cref="HttpResponseMessage"/> to parse the content of</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The parsed content</returns>
        Task<object> ParseAsync(HttpResponseMessage httpResponse, CancellationToken cancellationToken = default);

    }

}
