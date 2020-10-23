using Polly;
using Polly.Retry;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Http.Primitives
{

    /// <summary>
    /// Represents an <see cref="IStepBody"/> used to send an <see cref="HttpRequestMessage"/>
    /// </summary>
    public class SendHttpRequest
       : StepBodyAsync, IDisposable
    {

        /// <summary>
        /// Initializes a new <see cref="SendHttpRequest"/>
        /// </summary>
        /// <param name="httpClientFactory">The service used to create <see cref="System.Net.Http.HttpClient"/>s</param>
        public SendHttpRequest(IHttpClientFactory httpClientFactory)
        {
            this.HttpClient = httpClientFactory.CreateClient();
            this.EnsureSuccessStatusCode = true;
        }

        /// <summary>
        /// Gets the <see cref="System.Net.Http.HttpClient"/> used to send <see cref="HttpRequestMessage"/>s
        /// </summary>
        protected HttpClient HttpClient { get; }

        /// <summary>
        /// Gets/sets the <see cref="HttpMethod"/> of the <see cref="HttpRequestMessage"/> to send
        /// </summary>
        public HttpMethod Method { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="System.Uri"/> to request
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="Action{T}"/> used to configure the headers of the <see cref="HttpRequestMessage"/> to send
        /// </summary>
        public Action<HttpRequestHeaders> Headers { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="HttpContent"/> of the <see cref="HttpRequestMessage"/> to send
        /// </summary>
        public HttpContent Content { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="AsyncRetryPolicy"/> to used when sending the <see cref="HttpRequestMessage"/>
        /// </summary>
        public AsyncRetryPolicy RetryPolicy { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to ensure that the response to the request has a success status code
        /// </summary>
        public bool EnsureSuccessStatusCode { get; set; }

        /// <inheritdoc/>
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            int statusCode;
            if (this.RetryPolicy == null)
                statusCode = await this.RequestAsync();
            else
                statusCode = await this.RetryPolicy.ExecuteAsync(() => this.RequestAsync());
            return ExecutionResult.Outcome(statusCode);
        }

        /// <summary>
        /// Sends the <see cref="HttpRequestMessage"/>
        /// </summary>
        /// <returns>The resulting <see cref="HttpStatusCode"/></returns>
        protected async Task<int> RequestAsync()
        {
            using (HttpRequestMessage request = new HttpRequestMessage(this.Method, this.Uri))
            {
                this.Headers?.Invoke(request.Headers);
                request.Content = await this.Content?.CloneAsync();
                using (HttpResponseMessage response = await this.HttpClient.SendAsync(request))
                {
                    if (this.EnsureSuccessStatusCode)
                        response.EnsureSuccessStatusCode();
                    return (int)response.StatusCode;
                }
            }
        }

        /// <summary>
        /// Disposes of the <see cref="SendHttpRequest"/>
        /// </summary>
        public void Dispose()
        {
            this.Content?.Dispose();
        }

    }

}
