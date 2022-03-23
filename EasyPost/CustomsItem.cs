using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class CustomsItem : Resource
    {
        [JsonProperty("code")]
        public string code { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("hs_tariff_number")]
        public string hs_tariff_number { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("origin_country")]
        public string origin_country { get; set; }
        [JsonProperty("quantity")]
        public int quantity { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("value")]
        public double? value { get; set; }
        [JsonProperty("weight")]
        public double weight { get; set; }

        /// <summary>
        ///     Create a CustomsItem.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the customs item with. Valid pairs:
        ///     * {"description", string}
        ///     * {"quantity", int}
        ///     * {"weight", int}
        ///     * {"value", double}
        ///     * {"hs_tariff_number", string}
        ///     * {"origin_country", string}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        public static async Task<CustomsItem> Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("customs_items", Method.Post);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "customs_item", parameters
                }
            });

            return await request.Execute<CustomsItem>();
        }


        /// <summary>
        ///     Retrieve a CustomsItem from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsItem. Starts with "cstitem_".</param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        public static async Task<CustomsItem> Retrieve(string id)
        {
            Request request = new Request("customs_items/{id}");
            request.AddUrlSegment("id", id);

            return await request.Execute<CustomsItem>();
        }
    }
}
