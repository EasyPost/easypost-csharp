using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Http;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models
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
        public async Task Buy(string carrier, string service)
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            await Update<Pickup>(Method.Post, $"pickups/{id}/buy",
                new Dictionary<string, object>
                {
                    {
                        "carrier", carrier
                    },
                    {
                        "service", service
                    }
                });
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

            await Update<Pickup>(Method.Post, $"pickups/{id}/cancel");
        }
    }
}
