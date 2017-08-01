using RestSharp;

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

        /// <summary>
        /// </summary>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        public static List<CarrierAccount> List(string apiKey = null) {
            Request request = new Request("carrier_accounts");
            return request.Execute<List<CarrierAccount>>(apiKey);
        }

        /// <summary>
        /// Retrieve a CarrierAccount from its id.
        /// </summary>
        /// <param name="id">String representing a carrier account. Starts with "ca_".</param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        public static CarrierAccount Retrieve(string id, string apiKey = null) {
            Request request = new Request("carrier_accounts/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<CarrierAccount>(apiKey);
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
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        public static CarrierAccount Create(Dictionary<string, object> parameters, string apiKey = null) {
            Request request = new Request("carrier_accounts", Method.POST);
            request.AddBody(parameters, "carrier_account");

            return request.Execute<CarrierAccount>(apiKey);
        }

        /// <summary>
        /// Remove this CarrierAccount from your account.
        /// </summary>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        public void Destroy(string apiKey = null) {
            Request request = new Request("carrier_accounts/{id}", Method.DELETE);
            request.AddUrlSegment("id", id);

            request.Execute<CarrierAccount>(apiKey);
        }

        /// <summary>
        /// Update this CarrierAccount.
        /// </summary>
        /// <param name="parameters">See CarrierAccount.Create for more details.</param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        public void Update(Dictionary<string, object> parameters, string apiKey = null) {
            Request request = new Request("carrier_accounts/{id}", Method.PUT);
            request.AddUrlSegment("id", id);
            request.AddBody(parameters, "carrier_account");

            Merge(request.Execute<CarrierAccount>(apiKey));
        }
    }
}
