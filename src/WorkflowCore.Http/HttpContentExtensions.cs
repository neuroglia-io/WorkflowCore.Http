using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WorkflowCore.Http
{

    /// <summary>
    /// Defines extensions for <see cref="HttpContent"/>s
    /// </summary>
    public static class HttpContentExtensions
    {

        /// <summary>
        /// Clones the <see cref="HttpContent"/>
        /// </summary>
        /// <param name="source">The <see cref="HttpContent"/> to clone</param>
        /// <returns>A new clone of the <see cref="HttpContent"/></returns>
        public static async Task<HttpContent> CloneAsync(this HttpContent source)
        {
            if (source == null)
                return null;
            MemoryStream stream = new MemoryStream();
            await source.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Position = 0;
            HttpContent result = new StreamContent(stream);
            result.Headers.ContentType = source.Headers.ContentType;
            return result;
        }

    }

}
