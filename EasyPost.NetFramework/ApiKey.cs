using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        /// <exception cref="ResourceAlreadyCreated">Address already has an id.</exception>
        public static List<ApiKey> All()
        {
            Request request = new Request("api_keys");
            Dictionary<string, object> response = request.Execute<Dictionary<string, object>>();
            List<object> keys = (List<object>)response["keys"];
            foreach (Dictionary<string, object> key in keys)
            {
                key["created_at"] = DateTime.ParseExact((string)key["created_at"], "yyyy-MM-ddTHH:mm:ssZ",
                    CultureInfo.InvariantCulture);
            }

            return keys.Select(key => LoadFromDictionary<ApiKey>((Dictionary<string, object>)key)).ToList();
        }
    }
}
