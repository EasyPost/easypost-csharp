using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Message : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; internal set; }
        [JsonProperty("carrier_account_id")]
        public string? CarrierAccountId { get; internal set; }
        [JsonProperty("message")]
        public string? Text { get; internal set; } // "Message" is the enclosing class name
        [JsonProperty("type")]
        public string? Type { get; internal set; }

        #endregion

        internal Message()
        {
        }
    }
}
