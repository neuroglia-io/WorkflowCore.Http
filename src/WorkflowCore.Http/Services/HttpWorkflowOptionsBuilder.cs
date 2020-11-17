namespace WorkflowCore.Http.Services
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IHttpWorkflowOptionsBuilder"/> interface
    /// </summary>
    public class HttpWorkflowOptionsBuilder
        : IHttpWorkflowOptionsBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="HttpWorkflowOptionsBuilder"/>
        /// </summary>
        /// <param name="options">The <see cref="HttpWorkflowOptions"/> to configure</param>
        public HttpWorkflowOptionsBuilder(HttpWorkflowOptions options)
        {
            this.Options = options;
        }

        /// <summary>
        /// Initializes a new <see cref="HttpWorkflowOptionsBuilder"/>
        /// </summary>
        public HttpWorkflowOptionsBuilder()
            : this(new HttpWorkflowOptions())
        {

        }

        /// <summary>
        /// Gets the <see cref="HttpWorkflowOptions"/> to configure
        /// </summary>
        protected HttpWorkflowOptions Options { get; }

        /// <inheritdoc/>
        public virtual IHttpWorkflowOptionsBuilder UseHttpResponseParser<TParser>()
            where TParser : class, IHttpResponseParser
        {
            this.Options.ResponseParserType = typeof(TParser);
            return this;
        }

        /// <inheritdoc/>
        public virtual IHttpWorkflowOptionsBuilder AddHttpResponseContentParser<TParser>()
            where TParser : class, IHttpResponseContentParser
        {
            this.Options.ResponseContentParserTypes.Add(typeof(TParser));
            return this;
        }

        /// <inheritdoc/>
        public virtual IHttpWorkflowOptionsBuilder ClearHttpResponseContentParsers()
        {
            this.Options.ResponseContentParserTypes.Clear();
            return this;
        }

        /// <inheritdoc/>
        public virtual HttpWorkflowOptions Build()
        {
            return this.Options;
        }

    }

}
