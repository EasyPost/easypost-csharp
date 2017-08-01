using RestSharp;

using System;
using System.Collections.Generic;

namespace EasyPost {
    public class Order : Resource {
        public string id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string mode { get; set; }
        public string reference { get; set; }
        public bool? is_return { get; set; }
        public List<Message> messages { get; set; }
        public Address from_address { get; set; }
        public Address return_address { get; set; }
        public Address to_address { get; set; }
        public Address buyer_address { get; set; }
        public CustomsInfo customs_info { get; set; }
        public List<Shipment> shipments { get; set; }
        public List<CarrierAccount> carrier_accounts { get; set; }
        public List<Rate> rates { get; set; }
        public List<Container> containers { get; set; }
        public List<Item> items { get; set; }

        /// <summary>
        /// Retrieve a Order from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Order. Starts with "order_" if passing an id.</param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.Order instance.</returns>
        public static Order Retrieve(string id, string apiKey = null) {
            Request request = new Request("orders/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Order>(apiKey);
        }

        /// <summary>
        /// Create a Order.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the order with. Valid pairs:
        ///   * {"from_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"to_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"buyer_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"return_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"customs_info", Dictionary<string, object>} See CustomsInfo.Create for list of valid keys.
        ///   * {"is_return", bool}
        ///   * {"reference", string}
        ///   * {"shipments", IEnumerable<Shipment>} See Shipment.Create for list of valid keys.
        ///   * {"carrier_accounts", IEnumerable<CarrierAccount>}
        ///   * {"containers", IEnumerable<Container>} See Container.Create for list of valid keys.
        ///   * {"items", IEnumerable<Item>} See Item.Create for list of valid keys.
        /// All invalid keys will be ignored.
        /// </param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.Order instance.</returns>
        public static Order Create(Dictionary<string, object> parameters, string apiKey = null) {
            Request request = new Request("orders", Method.POST);
            request.AddBody(parameters, "order");

            return request.Execute<Order>(apiKey);
        }

        /// <summary>
        /// Create this Order.
        /// </summary>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <exception cref="ResourceAlreadyCreated">Order already has an id.</exception>
        public void Create(string apiKey = null) {
            if (id != null)
                throw new ResourceAlreadyCreated();
            Merge(sendCreate(this.AsDictionary(), apiKey));
        }

        /// <summary>
        /// </summary>
        /// <param name="parameters">
        /// </param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        private static Order sendCreate(Dictionary<string, object> parameters, string apiKey = null) {
            Request request = new Request("orders", Method.POST);
            request.AddBody(parameters, "order");

            return request.Execute<Order>(apiKey);
        }

        /// <summary>
        /// Purchase the shipments within this order with a carrier and service.
        /// </summary>
        /// <param name="carrier">The carrier to purchase a shipment from.</param>
        /// <param name="service">The service to purchase.</param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        public void Buy(string carrier, string service, string apiKey = null) {
            Request request = new Request("orders/{id}/buy", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddBody(new List<Tuple<string, string>>() { new Tuple<string, string>("carrier", carrier), new Tuple<string, string>("service", service) });

            Merge(request.Execute<Order>(apiKey));
        }

        /// <summary>
        /// Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object to puchase the shipment with.</param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        public void Buy(Rate rate, string apiKey = null) {
            Buy(rate.carrier, rate.service, apiKey);
        }
    }
}
