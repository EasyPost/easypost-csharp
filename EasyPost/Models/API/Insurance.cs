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
        public string? Amount { get; set; }
        [JsonProperty("from_address")]
        public Address? FromAddress { get; set; }
        [JsonProperty("messages")]
        public List<string>? Messages { get; set; }
        [JsonProperty("provider")]
        public string? Provider { get; set; }
        [JsonProperty("provider_id")]
        public string? ProviderId { get; set; }
        [JsonProperty("reference")]
        public string? Reference { get; set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("to_address")]
        public Address? ToAddress { get; set; }
        [JsonProperty("tracker")]
        public Tracker? Tracker { get; set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }
        [JsonProperty("fee")]
        public Fee? Fee { get; set; }
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
        public List<Insurance>? Insurances { get; set; }

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
