using System;
using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Tracker : EasyPostObject, ITrackerParameter
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; set; }
        [JsonProperty("carrier_detail")]
        public CarrierDetail? CarrierDetail { get; set; }
        [JsonProperty("est_delivery_date")]
        public DateTime? EstDeliveryDate { get; set; }
        [JsonProperty("public_url")]
        public string? PublicUrl { get; set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }
        [JsonProperty("signed_by")]
        public string? SignedBy { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("status_detail")]
        public string? StatusDetail { get; set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }
        [JsonProperty("tracking_details")]
        public List<TrackingDetail>? TrackingDetails { get; set; }
        [JsonProperty("tracking_updated_at")]
        public DateTime? TrackingUpdatedAt { get; set; }
        [JsonProperty("weight")]
        public double? Weight { get; set; }

        #endregion

        internal Tracker()
        {
        }
    }

    public class TrackerCollection : PaginatedCollection<Tracker>
    {
        #region JSON Properties

        [JsonProperty("trackers")]
        public List<Tracker>? Trackers { get; set; }

        internal string? TrackingCode { get; set; }

        internal string? Carrier { get; set; }

        #endregion

        internal TrackerCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Tracker> entries, int? pageSize = null)
        {
            string? lastId = entries.Last().Id;

            BetaFeatures.Parameters.Trackers.All parameters = new()
            {
                BeforeId = lastId,
                // TrackingCode and Carrier won't be included in the request if they are null.
                TrackingCode = TrackingCode,
                Carrier = Carrier,
            };

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
