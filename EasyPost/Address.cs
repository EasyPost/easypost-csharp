using RestSharp;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost {
    public class Address : IResource {
        public string id { get; set; }
        public Nullable<DateTime> created_at { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
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
        public Verifications verifications { get; set; }

        /// <summary>
        /// Retrieve an Address from its id.
        /// </summary>
        /// <param name="id">String representing an Address. Starts with "adr_".</param>
        /// <returns>EasyPost.Address instance.</returns>
        public static Address Retrieve(string id) {
            Request request = new Request("addresses/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Address>();
        }

        public static async Task<Address> RetrieveTaskAsync(string id) {
            Request request = new Request("addresses/{id}");
            request.AddUrlSegment("id", id);

            return await request.ExecuteTaskAsync<Address>();
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
        /// <param name="verifications">
        /// A list of verifications to perform on the address.
        /// Possible items are "delivery" and "zip4".
        /// </param>
        /// <param name="strict_verifications">
        /// A list of verifications to perform on the address.
        /// Will cause an HttpException to be raised if unsucessful.
        /// Possible items are "delivery" and "zip4".
        /// </param>
        /// <returns>EasyPost.Address instance.</returns>
        public static Address Create(Dictionary<string, object> parameters = null) {
            List<string> verifications = null, strictVerifications = null;
            parameters = parameters ?? new Dictionary<string, object>();

            if (parameters.ContainsKey("verifications")) {
                verifications = (List<string>)parameters["verifications"];
                parameters.Remove("verifications");
            }

            if (parameters.ContainsKey("strict_verifications")) {
                strictVerifications = (List<string>)parameters["strict_verifications"];
                parameters.Remove("strict_verifications");
            }

            return sendCreate(parameters, verifications, strictVerifications);
        }

        public static async Task<Address> CreateTaskAsync(Dictionary<string, object> parameters = null) {
            List<string> verifications = null, strictVerifications = null;
            parameters = parameters ?? new Dictionary<string, object>();

            if (parameters.ContainsKey("verifications"))
            {
                verifications = (List<string>)parameters["verifications"];
                parameters.Remove("verifications");
            }

            if (parameters.ContainsKey("strict_verifications"))
            {
                strictVerifications = (List<string>)parameters["strict_verifications"];
                parameters.Remove("strict_verifications");
            }

            return await sendCreateTaskAsync(parameters, verifications, strictVerifications);
        }

        /// <summary>
        /// Create this Address.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Address already has an id.</exception>
        public void Create() {
            Create(null, null);
        }

        public async Task CreateTaskAsync() {
            await CreateTaskAsync(null, null);
        }

        /// <summary>
        /// Create this Address.
        /// </summary>
        /// <param name="verifications">
        /// A list of verifications to perform on the address.
        /// Possible items are "delivery" and "zip4".
        /// </param>
        /// <param name="strict_verifications">
        /// A list of verifications to perform on the address.
        /// Will cause an HttpException to be raised if unsucessful.
        /// Possible items are "delivery" and "zip4".
        /// </param>
        /// <exception cref="ResourceAlreadyCreated">Address already has an id.</exception>
        public void Create(List<string> verifications = null, List<string> strictVerifications = null) {
            if (id != null)
                throw new ResourceAlreadyCreated();
            this.Merge(sendCreate(this.AsDictionary(), verifications, strictVerifications));
        }

        public async Task CreateTaskAsync(List<string> verifications = null, List<string> strictVerifications = null) {
            if (id != null)
                throw new ResourceAlreadyCreated();
            this.Merge(await sendCreateTaskAsync(this.AsDictionary(), verifications, strictVerifications));
        }

        private static Address sendCreate(Dictionary<string, object> parameters, List<string> verifications = null, List<string> strictVerifications = null) {
            Request request = new Request("addresses", Method.POST);
            request.AddBody(parameters, "address");

            foreach (string verification in verifications ?? new List<string>()) {
                request.AddParameter("verify[]", verification, ParameterType.QueryString);
            }

            foreach (string verification in strictVerifications ?? new List<string>()) {
                request.AddParameter("verify_strict[]", verification, ParameterType.QueryString);
            }

            return request.Execute<Address>();
        }

        private static async Task<Address> sendCreateTaskAsync(Dictionary<string, object> parameters, List<string> verifications = null, List<string> strictVerifications = null) {
            Request request = new Request("addresses", Method.POST);
            request.AddBody(parameters, "address");

            foreach (string verification in verifications ?? new List<string>())
            {
                request.AddParameter("verify[]", verification, ParameterType.QueryString);
            }

            foreach (string verification in strictVerifications ?? new List<string>())
            {
                request.AddParameter("verify_strict[]", verification, ParameterType.QueryString);
            }

            return await request.ExecuteTaskAsync<Address>();
        }

        /// <summary>
        /// Verify an address.
        /// </summary>
        /// <returns>EasyPost.Address instance. Check message for verification failures.</returns>
        public void Verify(string carrier = null) {
            if (id == null)
                Create();

            Request request = new Request("addresses/{id}/verify");
            request.RootElement = "address";
            request.AddUrlSegment("id", id);

            if (carrier != null)
                request.AddParameter("carrier", carrier, ParameterType.QueryString);

            this.Merge(request.Execute<Address>());
        }

        public async Task VerifyTaskAsync(string carrier = null) {
            if (id == null)
                await CreateTaskAsync();

            Request request = new Request("addresses/{id}/verify");
            request.RootElement = "address";
            request.AddUrlSegment("id", id);

            if (carrier != null)
                request.AddParameter("carrier", carrier, ParameterType.QueryString);

            this.Merge(await request.ExecuteTaskAsync<Address>());
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
        public static Address CreateAndVerify(Dictionary<string, object> parameters = null) {
            parameters["strict_verifications"] = new List<string>() { "delivery" };
            return Address.Create(parameters);
        }

        public static async Task<Address> CreateAndVerifyTaskAsync(Dictionary<string, object> parameters = null) {
            parameters["strict_verifications"] = new List<string>() { "delivery" };
            return await Address.CreateTaskAsync(parameters);
        }
    }
}