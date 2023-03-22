using System.Collections.Generic;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Refund : EasyPostObject, IRefundParameter
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

    public class RefundCollection : PaginatedCollection<Refund>
    {
        #region JSON Properties

        [JsonProperty("refunds")]
        public List<Refund>? Refunds { get; set; }

        #endregion

        internal RefundCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Refund> entries, int? pageSize = null) => throw new System.NotImplementedException();
    }
}
