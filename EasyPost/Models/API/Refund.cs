using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Refund : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string carrier { get; set; }
        [JsonProperty("confirmation_number")]
        public string confirmation_number { get; set; }
        [JsonProperty("shipment_id")]
        public string shipment_id { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("tracking_code")]
        public string tracking_code { get; set; }

        #endregion
    }
}
