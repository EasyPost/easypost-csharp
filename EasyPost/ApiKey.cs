using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EasyPost {
    public class ApiKey : Resource {
#pragma warning disable IDE1006 // Naming Styles
        public string key { get; set; }
        public string mode { get; set; }
        public DateTime? created_at { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Get a list of all API keys.
        /// </summary>
        /// <returns>A list of EasyPost.ApiKey instances.</returns>
        /// <exception cref="ResourceAlreadyCreated">Address already has an id.</exception>
        public static List<ApiKey> All() {
            Request request = new Request("api_keys");
            Dictionary<string, object> response = request.Execute<Dictionary<string, object>>();
            List<object> keys = (List<object>)response["keys"];
            foreach (Dictionary<string, object>key in keys) {
                key["created_at"] = DateTime.ParseExact((string)key["created_at"], "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            }
            return keys.Select(key => LoadFromDictionary<ApiKey>((Dictionary<string, object>)key)).ToList();
        }
    }
}