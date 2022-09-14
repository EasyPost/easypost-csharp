using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Shipment : EasyPostObject
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

        #region CRUD Operations

        /// <summary>
        ///     Get the Smartrates for this shipment.
        /// </summary>
        /// <returns>A list of EasyPost.Smartrate instances.</returns>
        [CrudOperations.Read]
        public async Task<List<Smartrate>> GetSmartrates()
        {
            if (Id == null)
            {
                throw new MissingParameterError("Id");
            }

            return await Request<List<Smartrate>>(Method.Get, $"shipments/{Id}/smartrate", null, "result");
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rateId">The id of the rate to purchase the shipment with.</param>
        /// <param name="insuranceValue">The value to insure the shipment for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        [CrudOperations.Update]
        public async Task Buy(string? rateId, string? insuranceValue = null, bool withCarbonOffset = false)
        {
            // TODO: Should this function return the updated Shipment like Order.Buy?
            if (Id == null)
            {
                throw new MissingParameterError("Id");
            }

            if (rateId == null)
            {
                throw new MissingParameterError("RateId");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "rate", new Dictionary<string, object> { { "id", rateId } } },
                { "insurance", insuranceValue ?? string.Empty },
                { "carbon_offset", withCarbonOffset }
            };

            await Update<Shipment>(Method.Post, $"shipments/{Id}/buy", parameters);
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">The Rate to purchase the shipment with.</param>
        /// <param name="insuranceValue">The value to insure the shipment for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        [CrudOperations.Update]
        public async Task Buy(Rate rate, string? insuranceValue = null, bool withCarbonOffset = false) => await Buy(rate.Id, insuranceValue, withCarbonOffset);

        /// <summary>
        ///     Generate a postage label for this shipment.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        [CrudOperations.Update]
        public async Task<Shipment> GenerateLabel(string fileFormat)
        {
            if (Id == null)
            {
                throw new MissingParameterError("Id");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object> { { "file_format", fileFormat } };

            await Update<Shipment>(Method.Get, $"shipments/{Id}/label", parameters);
            return this;
        }

        /// <summary>
        ///     Insure shipment for the given amount.
        /// </summary>
        /// <param name="amount">The amount to insure the shipment for. Currency is provided when creating a shipment.</param>
        [CrudOperations.Update]
        public async Task<Shipment> Insure(double amount)
        {
            if (Id == null)
            {
                throw new MissingParameterError("Id");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object> { { "amount", amount } };

            await Update<Shipment>(Method.Post, $"shipments/{Id}/insure", parameters);
            return this;
        }

        /// <summary>
        ///     Send a refund request to the carrier the shipment was purchased from.
        /// </summary>
        [CrudOperations.Update]
        public async Task<Shipment> Refund()
        {
            if (Id == null)
            {
                throw new MissingParameterError("Id");
            }

            await Update<Shipment>(Method.Get, $"shipments/{Id}/refund");
            return this;
        }

        /// <summary>
        ///     Refresh the rates for this Shipment.
        /// </summary>
        /// <param name="parameters">Optional dictionary of parameters for the API request.</param>
        /// <param name="withCarbonOffset">Whether to use carbon offset when re-rating the shipment.</param>
        [CrudOperations.Update]
        public async Task RegenerateRates(Dictionary<string, object>? parameters = null, bool withCarbonOffset = false)
        {
            parameters ??= new Dictionary<string, object>();

            if (Id == null)
            {
                throw new MissingParameterError("Id");
            }

            parameters.Add("carbon_offset", withCarbonOffset);

            Shipment shipment = await Request<Shipment>(Method.Post, $"shipments/{Id}/rerate", parameters);
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
            if (Rates == null)
            {
                throw new FilteringError("rates is null");
            }

            return Calculation.Rates.GetLowestObjectRate(Rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }

        /// <summary>
        ///     Get the lowest smartrate for this Shipment.
        /// </summary>
        /// <param name="deliveryDays">Delivery days restriction to use when filtering.</param>
        /// <param name="deliveryAccuracy">Delivery days accuracy restriction to use when filtering.</param>
        /// <returns>Lowest EasyPost.Smartrate object instance.</returns>
        public async Task<Smartrate?> LowestSmartrate(int deliveryDays, SmartrateAccuracy deliveryAccuracy)
        {
            List<Smartrate> smartrates = await GetSmartrates();
            return Calculation.Rates.GetLowestShipmentSmartrate(smartrates, deliveryDays, deliveryAccuracy);
        }
    }
}
