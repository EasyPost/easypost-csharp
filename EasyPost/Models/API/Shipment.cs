using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Shipment
    /// <summary>
    ///     Class representing an <a href="https://docs.easypost.com/docs/shipments#shipment-object">EasyPost shipment</a>.
    /// </summary>
    public class Shipment : EasyPostObject, Parameters.IShipmentParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The ID of the <see cref="EasyPost.Models.API.Batch"/> that contains this Shipment, if applicable.
        /// </summary>
        [JsonProperty("batch_id")]
        public string? BatchId { get; set; }

        /// <inheritdoc cref="EasyPost.Models.API.BatchShipment.BatchMessage"/>.
        [JsonProperty("batch_message")]
        public string? BatchMessage { get; set; }

        /// <inheritdoc cref="EasyPost.Models.API.BatchShipment.BatchStatus"/>.
        [JsonProperty("batch_status")]
        public string? BatchStatus { get; set; }

        /// <summary>
        ///     The <see cref="Address"/> of the buyer. Defaults to the <see cref="ToAddress"/>.
        /// </summary>
        [JsonProperty("buyer_address")]
        public Address? BuyerAddress { get; set; }

        /// <summary>
        ///     The <see cref="EasyPost.Models.API.CarrierAccount"/>s associated with the shipment.
        /// </summary>
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount>? CarrierAccounts { get; set; }

        /// <summary>
        ///     The <see cref="CustomsInfo"/> associated with the shipment.
        /// </summary>
        [JsonProperty("customs_info")]
        public CustomsInfo? CustomsInfo { get; set; }

        /// <summary>
        ///     A list of <see cref="Fee"/>s associated with the shipment charged to the billing user's account.
        /// </summary>
        [JsonProperty("fees")]
        public List<Fee>? Fees { get; set; }

        /// <summary>
        ///     A list of <see cref="Form"/>s associated with the shipment.
        /// </summary>
        [JsonProperty("forms")]
        public List<Form>? Forms { get; set; }

        /// <summary>
        ///     The origin <see cref="Address"/> of the shipment.
        /// </summary>
        [JsonProperty("from_address")]
        public Address? FromAddress { get; set; }

        /// <summary>
        ///     The <see cref="Insurance"/> associated with the shipment.
        /// </summary>
        [JsonProperty("insurance")]
        public string? Insurance { get; set; }

        /// <summary>
        ///     Whether the shipment is a return shipment.
        /// </summary>
        [JsonProperty("is_return")]
        public bool? IsReturn { get; set; }

        /// <summary>
        ///     A list of <see cref="LineItem"/>s associated with the shipment.
        /// </summary>
        [JsonProperty("line_items")]
        public List<LineItem>? LineItems { get; set; }

        /// <summary>
        ///     A list of any carrier errors that occurred during rating or purchasing.
        /// </summary>
        [JsonProperty("messages")]
        public List<Message>? Messages { get; set; }

        /// <summary>
        ///     The <see cref="Options"/> used for the shipment.
        /// </summary>
        [JsonProperty("options")]
        public Options? Options { get; set; }

        /// <summary>
        ///     The ID of the <see cref="EasyPost.Models.API.Order"/> associated with the shipment.
        /// </summary>
        [JsonProperty("order_id")]
        public string? OrderId { get; set; }

        /// <summary>
        ///     The <see cref="Parcel"/> associated with the shipment.
        /// </summary>
        [JsonProperty("parcel")]
        public Parcel? Parcel { get; set; }

        /// <summary>
        ///     The <see cref="EasyPost.Models.API.PostageLabel"/> associated with the shipment.
        /// </summary>
        [JsonProperty("postage_label")]
        public PostageLabel? PostageLabel { get; set; }

        /// <summary>
        ///     A list of <see cref="Rate"/>s available for the shipment.
        /// </summary>
        [JsonProperty("rates")]
        public List<Rate>? Rates { get; set; }

        /// <summary>
        ///     An optional field that may be used in place of ID in some API endpoints.
        /// </summary>
        [JsonProperty("reference")]
        public string? Reference { get; set; }

        /// <inheritdoc cref="EasyPost.Models.API.Refund.Status"/>.
        [JsonProperty("refund_status")]
        public string? RefundStatus { get; set; }

        /// <summary>
        ///     The <see cref="Address"/> of the shipper. Defaults to the <see cref="FromAddress"/>.
        /// </summary>
        [JsonProperty("return_address")]
        public Address? ReturnAddress { get; set; }

        /// <summary>
        ///     The <see cref="ScanForm"/> associated with the shipment.
        /// </summary>
        [JsonProperty("scan_form")]
        public ScanForm? ScanForm { get; set; }

        /// <summary>
        ///     The <see cref="EasyPost.Models.API.Rate"/> selected for the shipment.
        /// </summary>
        [JsonProperty("selected_rate")]
        public Rate? SelectedRate { get; set; }

        /// <summary>
        ///     The service level of the <see cref="SelectedRate"/>.
        /// </summary>
        [JsonProperty("service")]
        public string? Service { get; set; }

        /// <inheritdoc cref="EasyPost.Models.API.Tracker.Status"/>.
        [JsonProperty("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     A list of <see cref="TaxIdentifier"/>s associated with the shipment.
        /// </summary>
        [JsonProperty("tax_identifiers")]
        public List<TaxIdentifier>? TaxIdentifiers { get; set; }

        /// <summary>
        ///     The destination <see cref="Address"/> of the shipment.
        /// </summary>
        [JsonProperty("to_address")]
        public Address? ToAddress { get; set; }

        /// <summary>
        ///     The <see cref="Tracker"/> associated with the shipment.
        /// </summary>
        [JsonProperty("tracker")]
        public Tracker? Tracker { get; set; }

        /// <inheritdoc cref="EasyPost.Models.API.Tracker.TrackingCode"/>.
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }

        /// <summary>
        ///     The USPS zone number for the shipment.
        /// </summary>
        [JsonProperty("usps_zone")]
        public string? UspsZone { get; set; }

        #endregion

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
#pragma warning restore CA1724 // Naming conflicts with Parameters.Shipment

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="Shipment"/>s.
    /// </summary>
    public class ShipmentCollection : PaginatedCollection<Shipment>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Shipment"/>s in the collection.
        /// </summary>
        [JsonProperty("shipments")]
        public List<Shipment>? Shipments { get; set; }

        #endregion

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        public override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Shipment> entries, int? pageSize = null)
        {
            Parameters.Shipment.All parameters = Filters != null ? (Parameters.Shipment.All)Filters : new Parameters.Shipment.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
