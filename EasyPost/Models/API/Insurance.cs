using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Insurance
    public class Insurance : EasyPostObject, IInsuranceParameter
    {
        #region JSON Properties

        [JsonProperty("amount")]
        public string? Amount { get; internal set; }
        [JsonProperty("from_address")]
        public Address? FromAddress { get; internal set; }
        [JsonProperty("messages")]
        public List<string>? Messages { get; internal set; }
        [JsonProperty("provider")]
        public string? Provider { get; internal set; }
        [JsonProperty("provider_id")]
        public string? ProviderId { get; internal set; }
        [JsonProperty("reference")]
        public string? Reference { get; internal set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; internal set; }
        [JsonProperty("status")]
        public string? Status { get; internal set; }
        [JsonProperty("to_address")]
        public Address? ToAddress { get; internal set; }
        [JsonProperty("tracker")]
        public Tracker? Tracker { get; internal set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; internal set; }
        [JsonProperty("fee")]
        public Fee? Fee { get; internal set; }
        #endregion

        internal Insurance()
        {
        }
    }
#pragma warning restore CA1724 // Naming conflicts with Parameters.Insurance

    public class InsuranceCollection : PaginatedCollection<Insurance>
    {
        #region JSON Properties

        [JsonProperty("insurances")]
        public List<Insurance>? Insurances { get; internal set; }

        #endregion

        internal InsuranceCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Insurance> entries, int? pageSize = null)
        {
            BetaFeatures.Parameters.Insurance.All parameters = Filters != null ? (BetaFeatures.Parameters.Insurance.All)Filters : new BetaFeatures.Parameters.Insurance.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
