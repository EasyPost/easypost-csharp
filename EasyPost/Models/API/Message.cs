using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Message : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; set; }
        [JsonProperty("carrier_account_id")]
        public string? CarrierAccountId { get; set; }
        [JsonProperty("message")]
        public string? Text { get; set; } // "Message" is the enclosing class name
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion
    }
}
