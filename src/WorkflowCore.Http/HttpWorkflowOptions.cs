using System;
using System.Collections.Generic;
using WorkflowCore.Http.Services;

namespace WorkflowCore.Http
{
    /// <summary>
    /// Represents the options used to configure http workflows
    /// </summary>
    public class HttpWorkflowOptions
    {

        /// <summary>
        /// Initializes a new <see cref="HttpWorkflowOptions"/>
        /// </summary>
        public HttpWorkflowOptions()
        {
            this.ResponseParserType = typeof(HttpResponseParser);
            this.ResponseContentParserTypes = new List<Type>()
            {
                typeof(JsonHttpResponseContentParser),
                typeof(DefaultHttpResponseContentParser)
            };
        }

        /// <summary>
        /// Gets/sets the type of <see cref="IHttpResponseParser"/> to use
        /// </summary>
        public Type ResponseParserType { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the types of the <see cref="IHttpResponseContentParser"/> to use
        /// </summary>
        public List<Type> ResponseContentParserTypes { get; set; }

    }

}
