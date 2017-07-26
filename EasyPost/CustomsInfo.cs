﻿using RestSharp;

using System;
using System.Collections.Generic;

namespace EasyPost {
    public class CustomsInfo : Resource {
        public string id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string contents_type { get; set; }
        public string contents_explanation { get; set; }
        public string customs_certify { get; set; }
        public string customs_signer { get; set; }
        public string eel_pfc { get; set; }
        public string non_delivery_option { get; set; }
        public string restriction_type { get; set; }
        public string restriction_comments { get; set; }
        public List<CustomsItem> customs_items { get; set; }
        public string mode { get; set; }

        /// <summary>
        /// Retrieve a CustomsInfo from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsInfo. Starts with "cstinfo_".</param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.CustomsInfo instance.</returns>
        public static CustomsInfo Retrieve(string id, string apiKey = null) {
            Request request = new Request("customs_infos/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<CustomsInfo>(apiKey);
        }

        /// <summary>
        /// Create a CustomsInfo.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the customs info with. Valid pairs:
        ///   * {"customs_certify", bool}
        ///   * {"customs_signer", string}
        ///   * {"contents_type", string}
        ///   * {"contents_explanation", string}
        ///   * {"restriction_type", string}
        ///   * {"eel_pfc", string}
        ///   * {"custom_items", Dictionary<string, object>} -- Can contain the key "id" or all keys required to create a CustomsItem.
        /// All invalid keys will be ignored.
        /// </param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.CustomsInfo instance.</returns>
        public static CustomsInfo Create(Dictionary<string, object> parameters, string apiKey = null) {
            Request request = new Request("customs_infos", Method.POST);
            request.AddBody(parameters, "customs_info");

            return request.Execute<CustomsInfo>(apiKey);
        }
    }
}
