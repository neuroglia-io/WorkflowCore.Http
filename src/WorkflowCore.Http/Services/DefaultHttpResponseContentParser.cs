using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowCore.Http.Services
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IHttpResponseContentParser"/>
    /// </summary>
    public class DefaultHttpResponseContentParser
        : IHttpResponseContentParser
    {

        /// <inheritdoc/>
        public virtual bool CanParse(string contentType)
        {
            return true;
        }

        /// <inheritdoc/>
        public virtual async Task<object> ParseAsync(HttpResponseMessage httpResponse, CancellationToken cancellationToken = default)
        {
            return await httpResponse.Content.ReadAsStringAsync();
        }

    }

}
