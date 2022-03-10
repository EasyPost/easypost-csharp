using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Pickup : Resource
    {
        [JsonProperty("address")]
        public Address address { get; set; }
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount> carrier_accounts { get; set; }
        [JsonProperty("confirmation")]
        public string confirmation { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("instructions")]
        public string instructions { get; set; }
        [JsonProperty("is_account_address")]
        public bool is_account_address { get; set; }
        [JsonProperty("max_datetime")]
        public DateTime max_datetime { get; set; }
        [JsonProperty("messages")]
        public List<string> messages { get; set; }
        [JsonProperty("min_datetime")]
        public DateTime min_datetime { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("pickup_rates")]
        public List<Rate> pickup_rates { get; set; }
        [JsonProperty("reference")]
        public string reference { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Purchase this pickup.
        /// </summary>
        /// <param name="carrier">The name of the carrier to purchase with.</param>
        /// <param name="service">The name of the service to purchase.</param>
        public void Buy(string carrier, string service)
        {
            Request request = new Request("pickups/{id}/buy", Method.POST);
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

            Merge(request.Execute<Pickup>());
        }

        /// <summary>
        ///     Cancel this pickup.
        /// </summary>
        public void Cancel()
        {
            Request request = new Request("pickups/{id}/cancel", Method.POST);
            request.AddUrlSegment("id", id);

            Merge(request.Execute<Pickup>());
        }

        /// <summary>
        ///     Create a Pickup.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///     * {"is_account_address", bool}
        ///     * {"min_datetime", DateTime}
        ///     * {"max_datetime", DateTime}
        ///     * {"reference", string}
        ///     * {"instructions", string}
        ///     * {"carrier_accounts", List&lt;CarrierAccount&gt;}
        ///     * {"address", Address}
        ///     * {"shipment", Shipment}
        ///     * {"batch", Batch}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static Pickup Create(Dictionary<string, object> parameters = null) => SendCreate(parameters ?? new Dictionary<string, object>());


        /// <summary>
        ///     Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static Pickup Retrieve(string id)
        {
            Request request = new Request("pickups/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Pickup>();
        }

        private static Pickup SendCreate(Dictionary<string, object> parameters)
        {
            Request request = new Request("pickups", Method.POST);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "pickup", parameters
                }
            });

            return request.Execute<Pickup>();
        }
    }
}
