using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowCore.Http.Services
{
    /// <summary>
    /// Represents an <see cref="IHttpResponseContentParser"/> implementation used to parse JSON contents
    /// </summary>
    public class JsonHttpResponseContentParser
        : IHttpResponseContentParser
    {

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> containing the content types supported by the <see cref="JsonHttpResponseContentParser"/>
        /// </summary>
        public static IEnumerable<string> SupportedContentTypes => new[] { "application/json", "text/json" };

        /// <inheritdoc/>
        public virtual bool CanParse(string contentType)
        {
            return SupportedContentTypes.Contains(contentType);
        }

        /// <inheritdoc/>
        public virtual async Task<object> ParseAsync(HttpResponseMessage httpResponse, CancellationToken cancellationToken = default)
        {
            string json = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JObject>(json);
        }

    }

}
