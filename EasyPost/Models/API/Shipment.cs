using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Shipment : EasyPostObject, IShipmentParameter
    {
        #region JSON Properties

        [JsonProperty("batch_id")]
        public string? BatchId { get; internal set; }
        [JsonProperty("batch_message")]
        public string? BatchMessage { get; internal set; }
        [JsonProperty("batch_status")]
        public string? BatchStatus { get; internal set; }
        [JsonProperty("buyer_address")]
        public Address? BuyerAddress { get; internal set; }
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount>? CarrierAccounts { get; internal set; }
        [JsonProperty("customs_info")]
        public CustomsInfo? CustomsInfo { get; internal set; }
        [JsonProperty("fees")]
        public List<Fee>? Fees { get; internal set; }
        [JsonProperty("forms")]
        public List<Form>? Forms { get; internal set; }
        [JsonProperty("from_address")]
        public Address? FromAddress { get; internal set; }
        [JsonProperty("insurance")]
        public string? Insurance { get; internal set; }
        [JsonProperty("is_return")]
        public bool? IsReturn { get; internal set; }
        [JsonProperty("messages")]
        public List<Message>? Messages { get; internal set; }
        [JsonProperty("options")]
        public Options? Options { get; internal set; }
        [JsonProperty("order_id")]
        public string? OrderId { get; internal set; }
        [JsonProperty("parcel")]
        public Parcel? Parcel { get; internal set; }
        [JsonProperty("postage_label")]
        public PostageLabel? PostageLabel { get; internal set; }
        [JsonProperty("rates")]
        public List<Rate>? Rates { get; internal set; }
        [JsonProperty("reference")]
        public string? Reference { get; internal set; }
        [JsonProperty("refund_status")]
        public string? RefundStatus { get; internal set; }
        [JsonProperty("return_address")]
        public Address? ReturnAddress { get; internal set; }
        [JsonProperty("scan_form")]
        public ScanForm? ScanForm { get; internal set; }
        [JsonProperty("selected_rate")]
        public Rate? SelectedRate { get; internal set; }
        [JsonProperty("service")]
        public string? Service { get; internal set; }
        [JsonProperty("status")]
        public string? Status { get; internal set; }
        [JsonProperty("tax_identifiers")]
        public List<TaxIdentifier>? TaxIdentifiers { get; internal set; }
        [JsonProperty("to_address")]
        public Address? ToAddress { get; internal set; }
        [JsonProperty("tracker")]
        public Tracker? Tracker { get; internal set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; internal set; }
        [JsonProperty("usps_zone")]
        public string? UspsZone { get; internal set; }

        #endregion

        internal Shipment()
        {
        }

        /// <summary>
        ///     Get the lowest rate for this Shipment.
        /// </summary>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.Rate object instance.</returns>
        public Rate LowestRate(List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
#pragma warning disable IDE0046
            if (Rates == null)
#pragma warning restore IDE0046
            {
                throw new MissingPropertyError(this, nameof(Rates));
            }

            return Utilities.Rates.GetLowestRate(Rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }
    }

    public class ShipmentCollection : PaginatedCollection<Shipment>
    {
        #region JSON Properties

        [JsonProperty("shipments")]
        public List<Shipment>? Shipments { get; internal set; }

        #endregion

        internal ShipmentCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Shipment> entries, int? pageSize = null)
        {
            BetaFeatures.Parameters.Shipments.All parameters = Filters != null ? (BetaFeatures.Parameters.Shipments.All)Filters : new BetaFeatures.Parameters.Shipments.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
