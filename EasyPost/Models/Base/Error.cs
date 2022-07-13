using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.Base
{
    public class Error : EasyPostObject
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
    }
}
