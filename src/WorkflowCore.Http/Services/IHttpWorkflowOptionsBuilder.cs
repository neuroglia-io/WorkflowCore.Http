namespace WorkflowCore.Http.Services
{
    /// <summary>
    /// Defines the fundamentals of a service used to build the options used to configure http workflows
    /// </summary>
    public interface IHttpWorkflowOptionsBuilder
    {

        /// <summary>
        /// Uses the specified <see cref="IHttpResponseParser"/>
        /// </summary>
        /// <typeparam name="TParser">The type of <see cref="IHttpResponseParser"/> to use</typeparam>
        /// <returns>The configured <see cref="IHttpWorkflowOptionsBuilder"/></returns>
        IHttpWorkflowOptionsBuilder UseHttpResponseParser<TParser>()
            where TParser : class, IHttpResponseParser;

        /// <summary>
        /// Removes all configured <see cref="IHttpResponseContentParser"/>s
        /// </summary>
        /// <returns>The configured <see cref="IHttpWorkflowOptionsBuilder"/></returns>
        IHttpWorkflowOptionsBuilder ClearHttpResponseContentParsers();

        /// <summary>
        /// Adds the specified <see cref="IHttpResponseContentParser"/><para></para>
        /// The order in which <see cref="IHttpResponseContentParser"/>s are added defines their priority
        /// </summary>
        /// <typeparam name="TParser">The type of <see cref="IHttpResponseContentParser"/> to add</typeparam>
        /// <returns>The configured <see cref="IHttpWorkflowOptionsBuilder"/></returns>
        IHttpWorkflowOptionsBuilder AddHttpResponseContentParser<TParser>()
            where TParser : class, IHttpResponseContentParser;

        /// <summary>
        /// Builds the <see cref="HttpWorkflowOptions"/>
        /// </summary>
        /// <returns>A new <see cref="HttpWorkflowOptions"/></returns>
        HttpWorkflowOptions Build();

    }

}
