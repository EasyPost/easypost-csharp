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
        public string? BatchId { get; set; }
        [JsonProperty("batch_message")]
        public string? BatchMessage { get; set; }
        [JsonProperty("batch_status")]
        public string? BatchStatus { get; set; }
        [JsonProperty("buyer_address")]
        public Address? BuyerAddress { get; set; }
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount>? CarrierAccounts { get; set; }
        [JsonProperty("customs_info")]
        public CustomsInfo? CustomsInfo { get; set; }
        [JsonProperty("fees")]
        public List<Fee>? Fees { get; set; }
        [JsonProperty("forms")]
        public List<Form>? Forms { get; set; }
        [JsonProperty("from_address")]
        public Address? FromAddress { get; set; }
        [JsonProperty("insurance")]
        public string? Insurance { get; set; }
        [JsonProperty("is_return")]
        public bool? IsReturn { get; set; }
        [JsonProperty("messages")]
        public List<Message>? Messages { get; set; }
        [JsonProperty("options")]
        public Options? Options { get; set; }
        [JsonProperty("order_id")]
        public string? OrderId { get; set; }
        [JsonProperty("parcel")]
        public Parcel? Parcel { get; set; }
        [JsonProperty("postage_label")]
        public PostageLabel? PostageLabel { get; set; }
        [JsonProperty("rates")]
        public List<Rate>? Rates { get; set; }
        [JsonProperty("reference")]
        public string? Reference { get; set; }
        [JsonProperty("refund_status")]
        public string? RefundStatus { get; set; }
        [JsonProperty("return_address")]
        public Address? ReturnAddress { get; set; }
        [JsonProperty("scan_form")]
        public ScanForm? ScanForm { get; set; }
        [JsonProperty("selected_rate")]
        public Rate? SelectedRate { get; set; }
        [JsonProperty("service")]
        public string? Service { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("tax_identifiers")]
        public List<TaxIdentifier>? TaxIdentifiers { get; set; }
        [JsonProperty("to_address")]
        public Address? ToAddress { get; set; }
        [JsonProperty("tracker")]
        public Tracker? Tracker { get; set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }
        [JsonProperty("usps_zone")]
        public string? UspsZone { get; set; }

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
        public List<Shipment>? Shipments { get; set; }

        #endregion

        internal ShipmentCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Shipment> entries, int? pageSize = null)
        {
            string? lastId = entries.Last().Id;

            BetaFeatures.Parameters.Shipments.All parameters;

            if (Filters != null)
            {
                parameters = (BetaFeatures.Parameters.Shipments.All)Filters;
            }
            else
            {
                parameters = new BetaFeatures.Parameters.Shipments.All();
            }

            parameters.BeforeId = lastId;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
