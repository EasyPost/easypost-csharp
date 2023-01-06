using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1716
    public class Error : EasyPostObject
#pragma warning restore CA1716
    {
        #region JSON Properties

        [JsonProperty("code")]
        public string? Code { get; set; }
        [JsonProperty("errors")]
        public List<Error>? Errors { get; set; }
        [JsonProperty("field")]
        public string? Field { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("suggestion")]
        public string? Suggestion { get; set; }

        #endregion

        internal Error()
        {
        }
    }
}
