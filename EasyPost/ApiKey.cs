using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyPost
{
    public class ApiKey : Resource
    {
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("key")]
        public string key { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }

        /// <summary>
        ///     Get a list of all API keys.
        /// </summary>
        /// <returns>A list of EasyPost.ApiKey instances.</returns>
        public static async Task<List<ApiKey>> All()
        {
            Request request = new Request("api_keys")
            {
                RootElement = "keys"
            };
            return await request.Execute<List<ApiKey>>();
        }
    }
}
