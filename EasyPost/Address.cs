using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class Address : IResource {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool residential { get; set; }
        public string mode { get; set; }
        public string error { get; set; }
        public string message { get; set; }

        private static Client client = new Client();

        /// <summary>
        /// Retrieve an Address from its id.
        /// </summary>
        /// <param name="id">String representing an Address. Starts with "adr_".</param>
        /// <returns>EasyPost.Address instance.</returns>
        public static Address Retrieve(string id) {
            Request request = new Request("addresses/{id}");
            request.AddUrlSegment("id", id);

            return client.Execute<Address>(request);
        }

        /// <summary>
        /// Create an Address.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the address with. Valid pairs:
        ///   * {"name", string}
        ///   * {"company", string}
        ///   * {"stree1", string}
        ///   * {"street2", string}
        ///   * {"city", string}
        ///   * {"state", string}
        ///   * {"zip", string}
        ///   * {"country", string}
        ///   * {"phone", string}
        ///   * {"email", string}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Address instance.</returns>
        public static Address Create(IDictionary<string, object> parameters = null) {
            return sendCreate(parameters ?? new Dictionary<string, object>());
        }

        /// <summary>
        /// Create this Address.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Address already has an id.</exception>
        public void Create() {
            if (id != null) throw new ResourceAlreadyCreated();
            this.Merge(sendCreate(this.AsDictionary()));
        }

        private static Address sendCreate(IDictionary<string, object> parameters) {
            Request request = new Request("addresses", Method.POST);
            request.addBody(parameters, "address");

            return client.Execute<Address>(request);
        }

        /// <summary>
        /// Create and verify an Address.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the address with. Valid pairs:
        ///   * {"name", string}
        ///   * {"company", string}
        ///   * {"stree1", string}
        ///   * {"street2", string}
        ///   * {"city", string}
        ///   * {"state", string}
        ///   * {"zip", string}
        ///   * {"country", string}
        ///   * {"phone", string}
        ///   * {"email", string}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Address instance.</returns>
        public static Address CreateAndVerify(IDictionary<string, object> parameters = null) {
            parameters = parameters ?? new Dictionary<string, object>();

            Request request = new Request("addresses/create_and_verify", Method.POST);
            request.RootElement = "address";
            request.addBody(parameters, "address");

            return client.Execute<Address>(request);
        }

        /// <summary>
        /// Verify an address.
        /// </summary>
        /// <returns>EasyPost.Address instance. Check message for verification failures.</returns>
        public void Verify() {
            if (id == null) Create();

            Request request = new Request("addresses/{id}/verify");
            request.RootElement = "address";
            request.AddUrlSegment("id", id);

            this.Merge(client.Execute<Address>(request));
        }
    }
}