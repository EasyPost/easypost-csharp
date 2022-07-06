using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public List<Message> messages { get; set; }
        [JsonProperty("min_datetime")]
        public DateTime min_datetime { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("pickup_rates")]
        public List<PickupRate> pickup_rates { get; set; }
        [JsonProperty("reference")]
        public string reference { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Get the pickup rates as a list of Rate objects.
        /// </summary>
        /// <returns>List of Rate objects.</returns>
        internal List<Rate> Rates
        {
            get
            {
                List<Rate> rates = new List<Rate>();
                foreach (PickupRate pickupRate in pickup_rates)
                {
                    rates.Add(pickupRate);
                }

                return rates;
            }
        }

        /// <summary>
        ///     Purchase this pickup.
        /// </summary>
        /// <param name="carrier">The name of the carrier to purchase with.</param>
        /// <param name="service">The name of the service to purchase.</param>
        public async Task Buy(string carrier, string service)
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            Request request = new Request("pickups/{id}/buy", Method.Post);
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

            Merge(await request.Execute<Pickup>());
        }

        /// <summary>
        ///     Cancel this pickup.
        /// </summary>
        public async Task Cancel()
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            Request request = new Request("pickups/{id}/cancel", Method.Post);
            request.AddUrlSegment("id", id);

            Merge(await request.Execute<Pickup>());
        }

        /// <summary>
        ///     Get the lowest rate for this Pickup.
        /// </summary>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.PickupRate object instance.</returns>
        public PickupRate LowestRate(List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            return (PickupRate)Utilities.Rates.GetLowestObjectRate(Rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
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
        public static async Task<Pickup> Create(Dictionary<string, object>? parameters = null) => await SendCreate(parameters ?? new Dictionary<string, object>());


        /// <summary>
        ///     Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static async Task<Pickup> Retrieve(string id)
        {
            Request request = new Request("pickups/{id}", Method.Get);
            request.AddUrlSegment("id", id);

            return await request.Execute<Pickup>();
        }

        private static async Task<Pickup> SendCreate(Dictionary<string, object> parameters)
        {
            Request request = new Request("pickups", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "pickup", parameters
                }
            });

            return await request.Execute<Pickup>();
        }
    }
}
