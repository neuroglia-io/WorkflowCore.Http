using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;
using WorkflowCore.Http.Services;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Http.Primitives
{

    /// <summary>
    /// Represents the <see cref="IStepBody"/> used to await for an incoming <see cref="HttpRequest"/>
    /// </summary>
    public class ReceiveHttpRequest
        : StepBodyAsync
    {

        /// <summary>
        /// Initializes a new <see cref="ReceiveHttpRequest"/>
        /// </summary>
        /// <param name="httpRequestAwaiterManager">The service used to manage <see cref="IHttpRequestAwaiter"/>s</param>
        public ReceiveHttpRequest(IHttpRequestAwaiterManager httpRequestAwaiterManager)
        {
            this.HttpRequestAwaiterManager = httpRequestAwaiterManager;
        }

        /// <summary>
        /// Gets the service used to manage <see cref="IHttpRequestAwaiter"/>s
        /// </summary>
        protected IHttpRequestAwaiterManager HttpRequestAwaiterManager { get; }

        /// <summary>
        /// Gets/sets the <see cref="HttpMethod"/> of the <see cref="HttpRequest"/> to wait for
        /// </summary>
        public HttpMethod Method { get; set; }

        /// <summary>
        /// Gets/sets the path requested by the <see cref="HttpRequest"/> to wait for
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets the service used to resolve a correlation id from the incoming <see cref="HttpRequest"/>
        /// </summary>
        public IHttpRequestCorrelationIdResolver CorrelationIdResolver { get; set; }

        /// <summary>
        /// Gets/sets the correlation id of to match with the <see cref="HttpRequest"/> to wait for
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Gets the awaited <see cref="HttpRequest"/>
        /// </summary>
        public HttpRequest Request { get; private set; }

        /// <inheritdoc/>
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            this.Request = await this.HttpRequestAwaiterManager.WaitForAsync(this.Method, this.Path, this.CorrelationIdResolver, this.CorrelationId);
            return ExecutionResult.Next();
        }

    }

}
