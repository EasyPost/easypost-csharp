using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Order : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("buyer_address")]
        public Address? BuyerAddress { get; set; }
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount>? CarrierAccounts { get; set; }
        [JsonProperty("customs_info")]
        public CustomsInfo? CustomsInfo { get; set; }
        [JsonProperty("from_address")]
        public Address? FromAddress { get; set; }
        [JsonProperty("is_return")]
        public bool? IsReturn { get; set; }
        [JsonProperty("messages")]
        public List<Message>? Messages { get; set; }
        [JsonProperty("rates")]
        public List<Rate>? Rates { get; set; }
        [JsonProperty("reference")]
        public string? Reference { get; set; }
        [JsonProperty("return_address")]
        public Address? ReturnAddress { get; set; }
        [JsonProperty("service")]
        public string? Service { get; set; }
        [JsonProperty("shipments")]
        public List<Shipment>? Shipments { get; set; }
        [JsonProperty("to_address")]
        public Address? ToAddress { get; set; }

        #endregion

        #region CRUD Operations

        /// <summary>
        ///     Purchase the shipments within this order with a carrier and service.
        /// </summary>
        /// <param name="withCarrier">The carrier to purchase a shipment from.</param>
        /// <param name="withService">The service to purchase.</param>
        [CrudOperations.Update]
        public async Task<Order> Buy(string withCarrier, string withService)
        {
            if (Id == null)
            {
                throw new MissingParameterError("Id");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "carrier", withCarrier },
                { "service", withService }
            };

            await Update<Order>(Method.Post, $"orders/{Id}/buy", parameters);
            return this;
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object instance to purchase the shipment with.</param>
        [CrudOperations.Update]
        public async Task Buy(Rate rate)
        {
            if (rate.Carrier != null)
            {
                if (rate.Service != null)
                {
                    await Buy(rate.Carrier, rate.Service);
                }
                else
                {
                    throw new MissingParameterError("service is required");
                }
            }
            else
            {
                throw new MissingParameterError("carrier is required");
            }
        }

        /// <summary>
        ///     Populate the rates property for this Order.
        /// </summary>
        [CrudOperations.Update]
        public async Task GetRates()
        {
            // TODO: Should this return the updated Order object?
            if (Id == null)
            {
                throw new MissingParameterError("Id");
            }

            await Update<Order>(Method.Get, $"orders/{Id}/rates");
        }

        #endregion

        /// <summary>
        ///     Get the lowest rate for this Order.
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
                throw new FilteringError("Rates not populated. Call GetRates() first.");
            }

            return Calculation.Rates.GetLowestObjectRate(Rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }
    }
}
