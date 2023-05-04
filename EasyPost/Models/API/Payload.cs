using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Payload : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("request_url")]
        public string? RequestUrl { get; internal set; }
        [JsonProperty("request_headers")]
        public Dictionary<string, string>? RequestHeaders { get; internal set; }
        [JsonProperty("request_body")]
        public string? RequestBody { get; internal set; }
        [JsonProperty("response_headers")]
        public Dictionary<string, string>? ResponseHeaders { get; internal set; }
        [JsonProperty("response_body")]
        public string? ResponseBody { get; internal set; }
        [JsonProperty("response_code")]
        public int? ResponseCode { get; internal set; }
        [JsonProperty("total_time")]
        public int? TotalTime { get; internal set; }

        #endregion

        internal Payload()
        {
        }
    }
}
