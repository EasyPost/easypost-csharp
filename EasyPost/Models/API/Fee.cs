using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Fee : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("amount")]
        public double? Amount { get; set; }
        [JsonProperty("charged")]
        public bool? Charged { get; set; }
        [JsonProperty("refunded")]
        public bool? Refunded { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion

        internal Fee()
        {
        }
    }
}
