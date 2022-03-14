using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyPost
{
    public class Rate : Resource
    {
        [JsonProperty("carrier")]
        public string carrier { get; set; }
        [JsonProperty("carrier_account_id")]
        public string carrier_account_id { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("delivery_date")]
        public DateTime? delivery_date { get; set; }
        [JsonProperty("delivery_date_guaranteed")]
        public bool delivery_date_guaranteed { get; set; }
        [JsonProperty("delivery_days")]
        public int? delivery_days { get; set; }
        [JsonProperty("est_delivery_days")]
        public int? est_delivery_days { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("list_currency")]
        public string list_currency { get; set; }
        [JsonProperty("list_rate")]
        public string list_rate { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("rate")]
        public string rate { get; set; }
        [JsonProperty("retail_currency")]
        public string retail_currency { get; set; }
        [JsonProperty("retail_rate")]
        public string retail_rate { get; set; }
        [JsonProperty("service")]
        public string service { get; set; }
        [JsonProperty("shipment_id")]
        public string shipment_id { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Retrieve a Rate from its id.
        /// </summary>
        /// <param name="id">String representing a Rate. Starts with "rate_".</param>
        /// <returns>EasyPost.Rate instance.</returns>
        public static async Task<Rate> Retrieve(string id)
        {
            Request request = new Request("rates/{id}");
            request.AddUrlSegment("id", id);

            return await request.Execute<Rate>();
        }
    }
}
