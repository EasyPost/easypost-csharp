using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CarrierType : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("fields")]
        public Dictionary<string, object>? Fields { get; internal set; }
        [JsonProperty("logo")]
        public string? Logo { get; internal set; }
        [JsonProperty("readable")]
        public string? Readable { get; internal set; }
        [JsonProperty("type")]
        public string? Type { get; internal set; }

        #endregion

        internal CarrierType()
        {
        }
    }
}
