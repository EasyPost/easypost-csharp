// ApiKey.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EasyPost
{
    public class ApiKey : Resource
    {
        public DateTime? created_at { get; set; }

        public string key { get; set; }

        public string mode { get; set; }


        /// <summary>
        ///     Get a list of all API keys.
        /// </summary>
        /// <returns>A list of EasyPost.ApiKey instances.</returns>
        /// <exception cref="ResourceAlreadyCreated">Address already has an id.</exception>
        public static List<ApiKey> All()
        {
            var request = new Request("api_keys");
            var response = request.Execute<Dictionary<string, object>>();
            var keys = (List<object>)response["keys"];
            foreach (Dictionary<string, object> key in keys)
            {
                key["created_at"] = DateTime.ParseExact((string)key["created_at"], "yyyy-MM-ddTHH:mm:ssZ",
                    CultureInfo.InvariantCulture);
            }

            return keys.Select(key => LoadFromDictionary<ApiKey>((Dictionary<string, object>)key)).ToList();
        }
    }
}
