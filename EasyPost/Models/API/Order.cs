using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Calculation;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Order : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("buyer_address")]
        public Address buyer_address { get; set; }
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount> carrier_accounts { get; set; }

        [JsonProperty("customs_info")]
        public CustomsInfo customs_info { get; set; }
        [JsonProperty("from_address")]
        public Address from_address { get; set; }

        [JsonProperty("is_return")]
        public bool? is_return { get; set; }
        [JsonProperty("messages")]
        public List<Message> messages { get; set; }
        [JsonProperty("mode")]
        public new string mode { get; set; }
        [JsonProperty("rates")]
        public List<Rate> rates { get; set; }
        [JsonProperty("reference")]
        public string reference { get; set; }
        [JsonProperty("return_address")]
        public Address return_address { get; set; }
        [JsonProperty("service")]
        public string service { get; set; }
        [JsonProperty("shipments")]
        public List<Shipment> shipments { get; set; }
        [JsonProperty("to_address")]
        public Address to_address { get; set; }

        #endregion

        /// <summary>
        ///     Purchase the shipments within this order with a carrier and service.
        /// </summary>
        /// <param name="withCarrier">The carrier to purchase a shipment from.</param>
        /// <param name="withService">The service to purchase.</param>
        public async Task<Order> Buy(string withCarrier, string withService)
        {
            if (id == null)
            {
                throw new Exception("id is null");
            }

            Dictionary<string, object?> parameters = new Dictionary<string, object?>
            {
                {
                    "carrier", withCarrier
                },
                {
                    "service", withService
                }
            };

            return await Update<Order>(Method.Post, $"orders/{id}/buy", parameters);
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object instance to purchase the shipment with.</param>
        public async Task Buy(Rate rate)
        {
            if (rate.carrier != null)
            {
                if (rate.service != null)
                {
                    await Buy(rate.carrier, rate.service);
                }
                else
                {
                    throw new Exception("service is required");
                }
            }
            else
            {
                throw new Exception("carrier is required");
            }
        }

        /// <summary>
        ///     Populate the rates property for this Order.
        /// </summary>
        public async Task GetRates()
        {
            if (id == null)
            {
                throw new Exception("id is null");
            }

            Order order = await Request<Order>(Method.Get, $"orders/{id}/rates");
            rates = order.rates;
        }

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
            if (rates == null)
            {
                throw new Exception("Rates not populated.  Call GetRates() first.");
            }

            return Rates.GetLowestObjectRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }
    }
}
