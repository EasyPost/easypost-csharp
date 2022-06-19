using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.V2
{
    public class Order : EasyPostObject
    {
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


        /// <summary>
        ///     Purchase the shipments within this order with a carrier and service.
        /// </summary>
        /// <param name="withCarrier">The carrier to purchase a shipment from.</param>
        /// <param name="withService">The service to purchase.</param>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Order> Buy(string withCarrier, string withService)
        {
            if (Id == null)
            {
                throw new PropertyMissing("id");
            }

            return await Update<Order>(Method.Post, $"orders/{Id}/buy",
                new Dictionary<string, object>
                {
                    {
                        "carrier", withCarrier
                    },
                    {
                        "service", withService
                    }
                });
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object instance to purchase the shipment with.</param>
        [ApiCompatibility(ApiVersion.Latest)]
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
                    throw new PropertyMissing("service");
                }
            }
            else
            {
                throw new PropertyMissing("carrier");
            }
        }

        /// <summary>
        ///     Populate the rates property for this Order.
        /// </summary>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task GetRates()
        {
            if (Id == null)
            {
                throw new PropertyMissing("id");
            }

            Order order = await Request<Order>(Method.Get, $"orders/{Id}/rates");
            Rates = order.Rates;
        }

        /// <summary>
        ///     Get the lowest rate for this Order.
        /// </summary>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.Rate object instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public Rate LowestRate(List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            if (Rates == null)
            {
                throw new PropertyMissing("rates");
            }

            return Calculation.Rates.GetLowestObjectRate(Rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }
    }
}
