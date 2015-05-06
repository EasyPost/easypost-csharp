using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class CarrierAccount : IResource {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string description { get; set; }
        public string reference { get; set; }
        public string readable { get; set; }
        public Dictionary<string, object> credentials { get; set; }
        public Dictionary<string, object> test_credentials { get; set; }

        private static Client client = new Client();

        public static List<CarrierAccount> List() {
            Request request = new Request("carrier_accounts");
            return client.Execute<List<CarrierAccount>>(request);
        }

        /// <summary>
        /// Retrieve a CarrierAccount from its id.
        /// </summary>
        /// <param name="id">String representing a carrier account. Starts with "ca_".</param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        public static CarrierAccount Retrieve(string id) {
            Request request = new Request("carrier_accounts/{id}");
            request.AddUrlSegment("id", id);

            return client.Execute<CarrierAccount>(request);
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
        public static CarrierAccount Create(IDictionary<string, object> parameters) {
            Request request = new Request("carrier_accounts", Method.POST);
            request.addBody(parameters, "carrier_account");

            return client.Execute<CarrierAccount>(request);
        }

        /// <summary>
        /// Remove this CarrierAccount from your account.
        /// </summary>
        public void Destroy() {
            Request request = new Request("carrier_accounts/{id}", Method.DELETE);
            request.AddUrlSegment("id", id);

            client.Execute(request);
        }

        /// <summary>
        /// Update this CarrierAccount.
        /// </summary>
        /// <param name="parameters">See CarrierAccount.Create for more details.</param>
        public void Update(IDictionary<string, object> parameters) {
            Request request = new Request("carrier_accounts/{id}", Method.PUT);
            request.AddUrlSegment("id", id);
            request.addBody(parameters, "carrier_account");

            this.Merge(client.Execute<CarrierAccount>(request));
        }
    }
}
