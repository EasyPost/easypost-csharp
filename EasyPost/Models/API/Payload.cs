using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#payload-object">EasyPost event payload</a>.
    /// </summary>
    public class Payload : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The URL that the request was sent to (the <see cref="Webhook"/> URL).
        /// </summary>
        [JsonProperty("request_url")]
        public string? RequestUrl { get; set; }

        /// <summary>
        ///     The HTTP headers that were included in the request made to the <see cref="RequestUrl"/>.
        /// </summary>
        [JsonProperty("request_headers")]
        public Dictionary<string, string>? RequestHeaders { get; set; }

        /// <summary>
        ///     The HTTP body of the request made to the <see cref="RequestUrl"/>.
        /// </summary>
        [JsonProperty("request_body")]
        public string? RequestBody { get; set; }

        /// <summary>
        ///     The HTTP headers that were received in response to the request made to the <see cref="RequestUrl"/>.
        /// </summary>
        [JsonProperty("response_headers")]
        public Dictionary<string, string>? ResponseHeaders { get; set; }

        /// <summary>
        ///     The HTTP body that was received in response to the request made to the <see cref="RequestUrl"/>.
        /// </summary>
        [JsonProperty("response_body")]
        public string? ResponseBody { get; set; }

        /// <summary>
        ///     The HTTP status code that was received in response to the request made to the <see cref="RequestUrl"/>.
        /// </summary>
        [JsonProperty("response_code")]
        public int? ResponseCode { get; set; }

        /// <summary>
        ///     The total time, in milliseconds, that it took to complete the request made to the <see cref="RequestUrl"/>.
        /// </summary>
        [JsonProperty("total_time")]
        public int? TotalTime { get; set; }

        #endregion

    }
}
