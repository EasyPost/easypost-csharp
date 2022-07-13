using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Utilities;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Order : Resource
    {
        #region JSON Properties

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

        #endregion

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
            return Rates.GetLowestObjectRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }

        /// <summary>
        ///     Create a Order.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the order with. Valid pairs:
        ///     * {"from_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"to_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"buyer_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"return_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"customs_info", Dictionary&lt;string, object&gt;} See CustomsInfo.Create for list of valid keys.
        ///     * {"is_return", bool}
        ///     * {"reference", string}
        ///     * {"shipments", IEnumerable&lt;Shipment&gt;} See Shipment.Create for list of valid keys.
        ///     * {"carrier_accounts", IEnumerable&lt;CarrierAccount&gt;}
        ///     * {"containers", IEnumerable&lt;Container&gt;} See Container.Create for list of valid keys.
        ///     * {"items", IEnumerable&lt;Item&gt;} See Item.Create for list of valid keys.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Order instance.</returns>
        public static async Task<Order> Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("orders", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "order", parameters
                }
            });

            return await request.Execute<Order>();
        }


        /// <summary>
        ///     Retrieve a Order from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Order. Starts with "order_" if passing an id.</param>
        /// <returns>EasyPost.Order instance.</returns>
        public static async Task<Order> Retrieve(string id)
        {
            Request request = new Request("orders/{id}", Method.Get);
            request.AddUrlSegment("id", id);

            return await request.Execute<Order>();
        }
    }
}
