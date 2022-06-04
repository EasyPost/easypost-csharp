using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Exceptions;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.V2
{
    public class Pickup : Resource
    {
        [JsonProperty("address")]
        public Address? address { get; set; }
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount>? carrier_accounts { get; set; }
        [JsonProperty("confirmation")]
        public string? confirmation { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("id")]
        public string? id { get; set; }
        [JsonProperty("instructions")]
        public string? instructions { get; set; }
        [JsonProperty("is_account_address")]
        public bool is_account_address { get; set; }
        [JsonProperty("max_datetime")]
        public DateTime max_datetime { get; set; }
        [JsonProperty("messages")]
        public List<Message>? messages { get; set; }
        [JsonProperty("min_datetime")]
        public DateTime min_datetime { get; set; }
        [JsonProperty("mode")]
        public string? mode { get; set; }
        [JsonProperty("name")]
        public string? name { get; set; }
        [JsonProperty("pickup_rates")]
        public List<PickupRate>? pickup_rates { get; set; }
        [JsonProperty("reference")]
        public string? reference { get; set; }
        [JsonProperty("status")]
        public string? status { get; set; }
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

        /// <summary>
        ///     Get the pickup rates as a list of Rate objects.
        /// </summary>
        /// <returns>List of Rate objects.</returns>
        internal IEnumerable<Rate> Rates
        {
            get { return pickup_rates != null ? pickup_rates.Cast<Rate>().ToList() : new List<Rate>(); }
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
            return (PickupRate)Calculation.Rates.GetLowestObjectRate(Rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }
    }
}
