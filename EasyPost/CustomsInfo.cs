using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class CustomsInfo : Resource
    {
        [JsonProperty("contents_explanation")]
        public string contents_explanation { get; set; }
        [JsonProperty("contents_type")]
        public string contents_type { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("customs_certify")]
        public string customs_certify { get; set; }
        [JsonProperty("customs_items")]
        public List<CustomsItem> customs_items { get; set; }
        [JsonProperty("customs_signer")]
        public string customs_signer { get; set; }
        [JsonProperty("declaration")]
        public string declaration { get; set; }
        [JsonProperty("eel_pfc")]
        public string eel_pfc { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("non_delivery_option")]
        public string non_delivery_option { get; set; }
        [JsonProperty("restriction_comments")]
        public string restriction_comments { get; set; }
        [JsonProperty("restriction_type")]
        public string restriction_type { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Create a CustomsInfo.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the customs info with. Valid pairs:
        ///     * {"customs_certify", bool}
        ///     * {"customs_signer", string}
        ///     * {"contents_type", string}
        ///     * {"contents_explanation", string}
        ///     * {"restriction_type", string}
        ///     * {"eel_pfc", string}
        ///     * {"custom_items", Dictionary&lt;string, object&gt;} -- Can contain the key "id" or all keys required to create a
        ///     CustomsItem.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CustomsInfo instance.</returns>
        public static async Task<CustomsInfo> Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("customs_infos", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "customs_info", parameters
                }
            });

            return await request.Execute<CustomsInfo>();
        }


        /// <summary>
        ///     Retrieve a CustomsInfo from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsInfo. Starts with "cstinfo_".</param>
        /// <returns>EasyPost.CustomsInfo instance.</returns>
        public static async Task<CustomsInfo> Retrieve(string id)
        {
            Request request = new Request("customs_infos/{id}", Method.Get);
            request.AddUrlSegment("id", id);

            return await request.Execute<CustomsInfo>();
        }
    }
}
