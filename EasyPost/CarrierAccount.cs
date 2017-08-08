﻿using RestSharp;

using System;
using System.Collections.Generic;

namespace EasyPost {
    public class CarrierAccount : Resource {
        public string id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string reference { get; set; }
        public string readable { get; set; }
        public Dictionary<string, object> credentials { get; set; }
        public Dictionary<string, object> test_credentials { get; set; }

        public static List<CarrierAccount> List() {
            Request request = new Request("v2/carrier_accounts");
            return request.Execute<List<CarrierAccount>>();
        }

        /// <summary>
        /// Retrieve a CarrierAccount from its id.
        /// </summary>
        /// <param name="id">String representing a carrier account. Starts with "ca_".</param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        public static CarrierAccount Retrieve(string id) {
            Request request = new Request("v2/carrier_accounts/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<CarrierAccount>();
        }

        /// <summary>
        /// Create a CarrierAccount.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///   * {"type", string} Required (e.g. EndiciaAccount, UPSAccount, etc.).
        ///   * {"reference", string} External reference for carrier account.
        ///   * {"description", string} Description of carrier account.
        ///   * {"credentials", Dictionary<string, string>}
        ///   * {"test_credentials", Dictionary<string, string>}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        public static CarrierAccount Create(Dictionary<string, object> parameters) {
            Request request = new Request("v2/carrier_accounts", Method.POST);
            request.AddBody(new Dictionary<string, object>() { { "carrier_account", parameters } });

            return request.Execute<CarrierAccount>();
        }

        /// <summary>
        /// Remove this CarrierAccount from your account.
        /// </summary>
        public void Destroy() {
            Request request = new Request("v2/carrier_accounts/{id}", Method.DELETE);
            request.AddUrlSegment("id", id);

            request.Execute();
        }

        /// <summary>
        /// Update this CarrierAccount.
        /// </summary>
        /// <param name="parameters">See CarrierAccount.Create for more details.</param>
        public void Update(Dictionary<string, object> parameters) {
            Request request = new Request("v2/carrier_accounts/{id}", Method.PUT);
            request.AddUrlSegment("id", id);
            request.AddBody(new Dictionary<string, object>() { { "carrier_account", parameters } });

            Merge(request.Execute<CarrierAccount>());
        }
    }
}
