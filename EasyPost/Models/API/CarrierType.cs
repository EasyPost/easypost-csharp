using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CarrierType : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("fields")]
        public Dictionary<string, object>? Fields { get; set; }
        [JsonProperty("logo")]
        public string? Logo { get; set; }
        [JsonProperty("readable")]
        public string? Readable { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion

        internal CarrierType()
        {
        }
    }
}
