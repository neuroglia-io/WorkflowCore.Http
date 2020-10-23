using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorkflowCore.Http.Primitives;

namespace WorkflowCore.Http.Services
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IHttpRequestAwaiterManager"/> interface
    /// </summary>
    public class HttpRequestAwaiterManager
        : IHttpRequestAwaiterManager
    {

        private object _Lock = new object();

        /// <summary>
        /// Initializes a new <see cref="HttpRequestAwaiterManager"/>
        /// </summary>
        public HttpRequestAwaiterManager()
        {
            this.Awaiters = new List<IHttpRequestAwaiter>();
        }

        /// <summary>
        /// Gets an <see cref="IList{T}"/> containing all registered <see cref="IHttpRequestAwaiter"/>s
        /// </summary>
        protected IList<IHttpRequestAwaiter> Awaiters { get; }

        /// <inheritdoc/>
        public virtual Task<HttpRequest> WaitForAsync(HttpMethod method, string path, IHttpRequestCorrelationIdResolver correlationIdResolver, string correlationId)
        {
            HttpRequestAwaiter awaiter = new HttpRequestAwaiter(method, path, correlationIdResolver, correlationId);
            awaiter.Disposed += this.OnAwaiterDisposed;
            lock (this._Lock)
            {
                this.Awaiters.Add(awaiter);
            }
            return awaiter.TaskCompletionSource.Task;
        }

        /// <inheritdoc/>
        public virtual IEnumerable<IHttpRequestAwaiter> GetAwaitersFor(HttpRequest request)
        {
            return this.Awaiters.Where(a => a.Matches(request));
        }

        protected virtual void OnAwaiterDisposed(object sender, EventArgs e)
        {
            IHttpRequestAwaiter awaiter = (IHttpRequestAwaiter)sender;
            awaiter.Disposed -= this.OnAwaiterDisposed;
            lock (this._Lock)
            {
                this.Awaiters.Remove(awaiter);
            }
        }

    }

}
