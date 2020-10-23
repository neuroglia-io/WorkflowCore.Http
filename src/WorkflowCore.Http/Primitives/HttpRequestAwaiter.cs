using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WorkflowCore.Http.Services;

namespace WorkflowCore.Http.Primitives
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IHttpRequestAwaiter"/> interface
    /// </summary>
    public class HttpRequestAwaiter
        : IHttpRequestAwaiter
    {

        /// <inheritdoc/>
        public event EventHandler Disposed;

        /// <summary>
        /// Initializes a new <see cref="HttpRequestAwaiter"/>
        /// </summary>
        /// <param name="method">The <see cref="HttpMethod"/> of the <see cref="HttpRequest"/> to await</param>
        /// <param name="path">The path of the <see cref="HttpRequest"/> to await</param>
        /// <param name="correlationIdResolver"></param>
        /// <param name="correlationId"></param>
        public HttpRequestAwaiter(HttpMethod method, string path, IHttpRequestCorrelationIdResolver correlationIdResolver, string correlationId)
        {
            this.Method = method;
            this.Path = path;
            this.CorrelationIdResolver = correlationIdResolver;
            this.CorrelationId = correlationId;
            this.TaskCompletionSource = new TaskCompletionSource<HttpRequest>();
        }

        /// <inheritdoc/>
        public HttpMethod Method { get; }

        /// <inheritdoc/>
        public string Path { get; }

        /// <inheritdoc/>
        public IHttpRequestCorrelationIdResolver CorrelationIdResolver { get; }

        /// <inheritdoc/>
        public string CorrelationId { get; }

        /// <summary>
        /// Gets the <see cref="TaskCompletionSource{TResult}"/> used to await incoming <see cref="HttpRequest"/>s
        /// </summary>
        public TaskCompletionSource<HttpRequest> TaskCompletionSource { get; }

        /// <inheritdoc/>
        public virtual bool Matches(HttpRequest request)
        {
            if (request.Method == this.Method.ToString() && request.Path.Value == this.Path)
            {
                if (this.CorrelationIdResolver == null
                    || this.CorrelationIdResolver.Resolve(request) == this.CorrelationId)
                    return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public virtual void Set(HttpRequest request)
        {
            this.TaskCompletionSource.SetResult(request);
            this.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Disposed?.Invoke(this, new EventArgs());
        }

    }

}
