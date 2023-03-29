using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json;
using RestSharp;

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

        #region CRUD Operations

        /// <summary>
        ///     Get the SmartRates for this shipment.
        /// </summary>
        /// <returns>A list of EasyPost.Smartrate instances.</returns>
        [CrudOperations.Read]
        public async Task<List<Smartrate>> GetSmartrates()
        {
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            return await Request<List<Smartrate>>(Method.Get, $"shipments/{Id}/smartrate", null, "result");
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rateId">The id of the rate to purchase the shipment with.</param>
        /// <param name="insuranceValue">The value to insure the shipment for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        /// <param name="endShipperId">The id of the end shipper to use for this purchase.</param>
        /// <returns>The task to buy this Shipment.</returns>
        [CrudOperations.Update]
        public async Task Buy(string? rateId, string? insuranceValue = null, bool withCarbonOffset = false, string? endShipperId = null)
        {
            // TODO: Should this function return the updated Shipment like Order.Buy?
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            if (rateId == null)
            {
                throw new MissingParameterError("rateId");
            }

            Dictionary<string, object> parameters = new()
            {
                { "rate", new Dictionary<string, object> { { "id", rateId } } },
                { "insurance", insuranceValue ?? string.Empty },
                { "carbon_offset", withCarbonOffset },
            };

            if (endShipperId != null)
            {
                parameters.Add("end_shipper", endShipperId);
            }

            await Update<Shipment>(Method.Post, $"shipments/{Id}/buy", parameters);
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">The Rate to purchase the shipment with.</param>
        /// <param name="insuranceValue">The value to insure the shipment for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        /// <param name="endShipperId">The id of the end shipper to use for this purchase.</param>
        /// <returns>The task to buy this Shipment.</returns>
        [CrudOperations.Update]
        public async Task Buy(Rate rate, string? insuranceValue = null, bool withCarbonOffset = false, string? endShipperId = null)
        {
            if (rate == null)
            {
                throw new MissingParameterError("rate");
            }

            await Buy(rate.Id, insuranceValue, withCarbonOffset, endShipperId);
        }

        /// <summary>
        ///     Purchase a label for this <see cref="Shipment"/> with the given <see cref="Rate"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.Buy"/> parameters set.</param>
        /// <returns>This updated <see cref="Shipment"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Buy(BetaFeatures.Parameters.Shipments.Buy parameters)
        {
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            await Update<Shipment>(Method.Post, $"shipments/{Id}/buy", parameters.ToDictionary());
            return this;
        }

        /// <summary>
        ///     Generate a postage label for this shipment.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <returns>The updated Shipment.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> GenerateLabel(string fileFormat)
        {
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            Dictionary<string, object> parameters = new() { { "file_format", fileFormat } };

            await Update<Shipment>(Method.Get, $"shipments/{Id}/label", parameters);
            return this;
        }

        /// <summary>
        ///     Generate a postage label for this <see cref="Shipment"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.GenerateLabel"/> parameter set.</param>
        /// <returns>This updated <see cref="Shipment"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> GenerateLabel(BetaFeatures.Parameters.Shipments.GenerateLabel parameters)
        {
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            await Update<Shipment>(Method.Get, $"shipments/{Id}/label", parameters.ToDictionary());
            return this;
        }

        /// <summary>
        ///     Insure shipment for the given amount.
        /// </summary>
        /// <param name="amount">The amount to insure the shipment for. Currency is provided when creating a shipment.</param>
        /// <returns>The updated Shipment.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Insure(double amount)
        {
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            Dictionary<string, object> parameters = new() { { "amount", amount } };

            await Update<Shipment>(Method.Post, $"shipments/{Id}/insure", parameters);
            return this;
        }

        /// <summary>
        ///     Insure this <see cref="Shipment"/> for the given amount.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.Insure"/> parameters set.</param>
        /// <returns>This updated <see cref="Shipment"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Insure(BetaFeatures.Parameters.Shipments.Insure parameters)
        {
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            await Update<Shipment>(Method.Post, $"shipments/{Id}/insure", parameters.ToDictionary());
            return this;
        }

        /// <summary>
        ///     Send a refund request to the carrier the shipment was purchased from.
        /// </summary>
        /// <returns>The updated Shipment.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Refund()
        {
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            await Update<Shipment>(Method.Post, $"shipments/{Id}/refund");
            return this;
        }

        /// <summary>
        ///     Refresh the rates for this Shipment.
        /// </summary>
        /// <param name="parameters">Optional dictionary of parameters for the API request.</param>
        /// <param name="withCarbonOffset">Whether to use carbon offset when re-rating the shipment.</param>
        /// <returns>The task to regenerate this Shipment's rates.</returns>
        [CrudOperations.Update]
        public async Task RegenerateRates(Dictionary<string, object>? parameters = null, bool withCarbonOffset = false)
        {
            parameters ??= new Dictionary<string, object>();

            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            parameters.Add("carbon_offset", withCarbonOffset);

            Shipment shipment = await Request<Shipment>(Method.Post, $"shipments/{Id}/rerate", parameters);
            Rates = shipment.Rates;
        }

        /// <summary>
        ///     Refresh <see cref="Rates"/> for this <see cref="Shipment"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.RegenerateRates"/> parameter set.</param>
        /// <returns>This updated <see cref="Shipment"/> instance.</returns>
        [CrudOperations.Update]
        public async Task RegenerateRates(BetaFeatures.Parameters.Shipments.RegenerateRates parameters)
        {
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            Shipment shipment = await Request<Shipment>(Method.Post, $"shipments/{Id}/rerate", parameters.ToDictionary());
            Rates = shipment.Rates;
        }

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

        /// <summary>
        ///     Get the lowest SmartRate for this Shipment.
        /// </summary>
        /// <param name="deliveryDays">Delivery days restriction to use when filtering.</param>
        /// <param name="deliveryAccuracy">Delivery days accuracy restriction to use when filtering.</param>
        /// <returns>Lowest EasyPost.Smartrate object instance.</returns>
        public async Task<Smartrate?> LowestSmartrate(int deliveryDays, SmartrateAccuracy deliveryAccuracy)
        {
            List<Smartrate> smartRates = await GetSmartrates();
            return Utilities.Rates.GetLowestSmartRate(smartRates, deliveryDays, deliveryAccuracy);
        }
    }

    public class ShipmentCollection : PaginatedCollection<Shipment>
    {
        #region JSON Properties

        [JsonProperty("shipments")]
        public List<Shipment>? Shipments { get; set; }

        internal bool? Purchased { get; set; }

        internal bool? IncludeChildren { get; set; }

        #endregion

        internal ShipmentCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Shipment> entries, int? pageSize = null)
        {
            string? lastId = entries.Last().Id;

            BetaFeatures.Parameters.Shipments.All parameters = new()
            {
                BeforeId = lastId,
                // Purchased and IncludeChildren won't be included in the request if they are null.
                Purchased = Purchased,
                IncludeChildren = IncludeChildren,
            };

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
