using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Fee : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("charged")]
        public bool Charged { get; set; }
        [JsonProperty("refunded")]
        public bool Refunded { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion
    }
}
