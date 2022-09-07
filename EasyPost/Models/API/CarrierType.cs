using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CarrierType : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("fields")]
        public Dictionary<string, object> fields { get; set; }
        [JsonProperty("logo")]
        public string logo { get; set; }
        [JsonProperty("readable")]
        public string readable { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }

        #endregion
    }
}
