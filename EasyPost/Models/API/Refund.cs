using System.Collections.Generic;
using System.Linq;
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
        public string? Carrier { get; internal set; }
        [JsonProperty("confirmation_number")]
        public string? ConfirmationNumber { get; internal set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; internal set; }
        [JsonProperty("status")]
        public string? Status { get; internal set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; internal set; }

        #endregion

        internal Refund()
        {
        }
    }

    public class RefundCollection : PaginatedCollection<Refund>
    {
        #region JSON Properties

        [JsonProperty("refunds")]
        public List<Refund>? Refunds { get; internal set; }

        #endregion

        internal RefundCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Refund> entries, int? pageSize = null)
        {
            BetaFeatures.Parameters.Refunds.All parameters = Filters != null ? (BetaFeatures.Parameters.Refunds.All)Filters : new BetaFeatures.Parameters.Refunds.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
