using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Refund : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; set; }
        [JsonProperty("confirmation_number")]
        public string? ConfirmationNumber { get; set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }

        #endregion

        internal Refund()
        {
        }
    }
}
