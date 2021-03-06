// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.DataFactory.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Sftp read settings.
    /// </summary>
    public partial class HttpReadSetting : ConnectorReadSetting
    {
        /// <summary>
        /// Initializes a new instance of the HttpReadSetting class.
        /// </summary>
        public HttpReadSetting()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the HttpReadSetting class.
        /// </summary>
        /// <param name="type">The read setting type.</param>
        /// <param name="additionalProperties">Unmatched properties from the
        /// message are deserialized this collection</param>
        /// <param name="maxConcurrentConnections">The maximum concurrent
        /// connection count for the source data store. Type: integer (or
        /// Expression with resultType integer).</param>
        /// <param name="requestMethod">The HTTP method used to call the
        /// RESTful API. The default is GET. Type: string (or Expression with
        /// resultType string).</param>
        /// <param name="requestBody">The HTTP request body to the RESTful API
        /// if requestMethod is POST. Type: string (or Expression with
        /// resultType string).</param>
        /// <param name="additionalHeaders">The additional HTTP headers in the
        /// request to the RESTful API. Type: string (or Expression with
        /// resultType string).</param>
        /// <param name="requestTimeout">Specifies the timeout for a HTTP
        /// client to get HTTP response from HTTP server.</param>
        public HttpReadSetting(string type, IDictionary<string, object> additionalProperties = default(IDictionary<string, object>), object maxConcurrentConnections = default(object), object requestMethod = default(object), object requestBody = default(object), object additionalHeaders = default(object), object requestTimeout = default(object))
            : base(type, additionalProperties, maxConcurrentConnections)
        {
            RequestMethod = requestMethod;
            RequestBody = requestBody;
            AdditionalHeaders = additionalHeaders;
            RequestTimeout = requestTimeout;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the HTTP method used to call the RESTful API. The
        /// default is GET. Type: string (or Expression with resultType
        /// string).
        /// </summary>
        [JsonProperty(PropertyName = "requestMethod")]
        public object RequestMethod { get; set; }

        /// <summary>
        /// Gets or sets the HTTP request body to the RESTful API if
        /// requestMethod is POST. Type: string (or Expression with resultType
        /// string).
        /// </summary>
        [JsonProperty(PropertyName = "requestBody")]
        public object RequestBody { get; set; }

        /// <summary>
        /// Gets or sets the additional HTTP headers in the request to the
        /// RESTful API. Type: string (or Expression with resultType string).
        /// </summary>
        [JsonProperty(PropertyName = "additionalHeaders")]
        public object AdditionalHeaders { get; set; }

        /// <summary>
        /// Gets or sets specifies the timeout for a HTTP client to get HTTP
        /// response from HTTP server.
        /// </summary>
        [JsonProperty(PropertyName = "requestTimeout")]
        public object RequestTimeout { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public override void Validate()
        {
            base.Validate();
        }
    }
}
