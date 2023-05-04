using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Fee : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("amount")]
        public double? Amount { get; internal set; }
        [JsonProperty("charged")]
        public bool? Charged { get; internal set; }
        [JsonProperty("refunded")]
        public bool? Refunded { get; internal set; }
        [JsonProperty("type")]
        public string? Type { get; internal set; }

        #endregion

        internal Fee()
        {
        }
    }
}
