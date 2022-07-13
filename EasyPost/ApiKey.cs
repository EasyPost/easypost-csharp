﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class ApiKey : Resource
    {
        #region JSON Properties

        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("key")]
        public string key { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }

        #endregion

        /// <summary>
        ///     Get a list of all API keys.
        /// </summary>
        /// <returns>A list of EasyPost.ApiKey instances.</returns>
        public static async Task<List<ApiKey>> All()
        {
            Request request = new Request("api_keys", Method.Get)
            {
                RootElement = "keys"
            };
            return await request.Execute<List<ApiKey>>();
        }
    }
}
