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
        public string? Carrier { get; internal set; }
        [JsonProperty("carrier_detail")]
        public CarrierDetail? CarrierDetail { get; internal set; }
        [JsonProperty("est_delivery_date")]
        public DateTime? EstDeliveryDate { get; internal set; }
        [JsonProperty("public_url")]
        public string? PublicUrl { get; internal set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; internal set; }
        [JsonProperty("signed_by")]
        public string? SignedBy { get; internal set; }
        [JsonProperty("status")]
        public string? Status { get; internal set; }
        [JsonProperty("status_detail")]
        public string? StatusDetail { get; internal set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; internal set; }
        [JsonProperty("tracking_details")]
        public List<TrackingDetail>? TrackingDetails { get; internal set; }
        [JsonProperty("tracking_updated_at")]
        public DateTime? TrackingUpdatedAt { get; internal set; }
        [JsonProperty("weight")]
        public double? Weight { get; internal set; }

        #endregion

        internal Tracker()
        {
        }
    }

    public class TrackerCollection : PaginatedCollection<Tracker>
    {
        #region JSON Properties

        [JsonProperty("trackers")]
        public List<Tracker>? Trackers { get; internal set; }

        #endregion

        internal TrackerCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Tracker> entries, int? pageSize = null)
        {
            BetaFeatures.Parameters.Trackers.All parameters = Filters != null ? (BetaFeatures.Parameters.Trackers.All)Filters : new BetaFeatures.Parameters.Trackers.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
