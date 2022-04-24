using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models
{
    public class Order : Resource
    {
        [JsonProperty("buyer_address")]
        public Address buyer_address { get; set; }
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount> carrier_accounts { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("customs_info")]
        public CustomsInfo customs_info { get; set; }
        [JsonProperty("from_address")]
        public Address from_address { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("is_return")]
        public bool? is_return { get; set; }
        [JsonProperty("messages")]
        public List<Message> messages { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
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
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Purchase the shipments within this order with a carrier and service.
        /// </summary>
        /// <param name="carrier">The carrier to purchase a shipment from.</param>
        /// <param name="service">The service to purchase.</param>
        public async Task Buy(string carrier, string service)
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            Request request = new Request("orders/{id}/buy", Method.Post);
            request.AddUrlSegment("id", id);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "carrier", carrier
                },
                {
                    "service", service
                }
            });

            Merge(await request.Execute<Order>());
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object instance to purchase the shipment with.</param>
        public async Task Buy(Rate rate) => await Buy(rate.carrier, rate.service);

        /// <summary>
        ///     Populate the rates property for this Order.
        /// </summary>
        public async Task GetRates()
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            Request request = new Request("orders/{id}/rates", Method.Get);
            request.AddUrlSegment("id", id);

            rates = (await request.Execute<Order>()).rates;
        }
    }
}
