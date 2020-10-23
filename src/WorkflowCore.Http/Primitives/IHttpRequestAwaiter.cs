using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using WorkflowCore.Http.Services;

namespace WorkflowCore.Http.Primitives
{

    /// <summary>
    /// Defines the fundamentals of an object used to await an <see cref="HttpRequest"/>
    /// </summary>
    public interface IHttpRequestAwaiter
        : IDisposable
    {

        /// <summary>
        /// Represents the event fired whenever the <see cref="IHttpRequestAwaiter"/> has been disposed of
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// Gets the <see cref="HttpMethod"/> of the <see cref="HttpRequest"/> to await
        /// </summary>
        HttpMethod Method { get; }

        /// <summary>
        /// Gets the path of the <see cref="HttpRequest"/> to await
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Gets the service used to resolve a correlation id from incoming <see cref="HttpRequest"/>s
        /// </summary>
        IHttpRequestCorrelationIdResolver CorrelationIdResolver { get; }

        /// <summary>
        /// Gets the correlation id to match, if any
        /// </summary>
        string CorrelationId { get; }

        /// <summary>
        /// Gets a boolean indicating whether or not the specified <see cref="HttpRequest"/> matches the <see cref="IHttpRequestAwaiter"/>
        /// </summary>
        /// <param name="request">The <see cref="HttpRequest"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="HttpRequest"/> matches the <see cref="IHttpRequestAwaiter"/></returns>
        bool Matches(HttpRequest request);

        /// <summary>
        /// Sets the awaited <see cref="HttpRequest"/>, thus completing the associated <see cref="ReceiveHttpRequest"/> step
        /// </summary>
        /// <param name="request">The received <see cref="HttpRequest"/></param>
        void Set(HttpRequest request);

    }

}
