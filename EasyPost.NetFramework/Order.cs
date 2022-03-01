using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
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
        public void Buy(string carrier, string service)
        {
            Request request = new Request("orders/{id}/buy", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddQueryString(new Dictionary<string, object>
            {
                {
                    "carrier", carrier
                },
                {
                    "service", service
                }
            });

            Merge(request.Execute<Order>());
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object instance to purchase the shipment with.</param>
        public void Buy(Rate rate) => Buy(rate.carrier, rate.service);

        /// <summary>
        ///     Create this Order.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Order already has an id.</exception>
        public void Create()
        {
            if (id != null)
            {
                throw new ResourceAlreadyCreated();
            }

            Merge(SendCreate(AsDictionary()));
        }

        /// <summary>
        ///     Populate the rates property for this Order.
        /// </summary>
        public void GetRates()
        {
            if (id == null)
            {
                Create();
            }

            Request request = new Request("orders/{id}/rates");
            request.AddUrlSegment("id", id);

            rates = request.Execute<Order>().rates;
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
        public static Order Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("orders", Method.POST);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "order", parameters
                }
            });

            return request.Execute<Order>();
        }


        /// <summary>
        ///     Retrieve a Order from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Order. Starts with "order_" if passing an id.</param>
        /// <returns>EasyPost.Order instance.</returns>
        public static Order Retrieve(string id)
        {
            Request request = new Request("orders/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Order>();
        }

        private static Order SendCreate(Dictionary<string, object> parameters)
        {
            Request request = new Request("orders", Method.POST);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "order", parameters
                }
            });

            return request.Execute<Order>();
        }
    }
}
