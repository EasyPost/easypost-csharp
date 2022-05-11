using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Address : Resource
    {
        [JsonProperty("carrier_facility")]
        public string carrier_facility { get; set; }
        [JsonProperty("city")]
        public string city { get; set; }
        [JsonProperty("company")]
        public string company { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("error")]
        public string error { get; set; }
        [JsonProperty("federal_tax_id")]
        public string federal_tax_id { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("phone")]
        public string phone { get; set; }
        [JsonProperty("residential")]
        public bool? residential { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
        [JsonProperty("state_tax_id")]
        public string state_tax_id { get; set; }
        [JsonProperty("street1")]
        public string street1 { get; set; }
        [JsonProperty("street2")]
        public string street2 { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("verifications")]
        public Verifications verifications { get; set; }
        [JsonProperty("verify")]
        public List<string> verify { get; set; }
        [JsonProperty("verify_strict")]
        public List<string> verify_strict { get; set; }
        [JsonProperty("zip")]
        public string zip { get; set; }

        /// <summary>
        ///     Verify an address.
        /// </summary>
        /// <returns>EasyPost.Address instance. Check message for verification failures.</returns>
        public async Task Verify(string? carrier = null)
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            Request request = new Request("addresses/{id}/verify", Method.Get)
            {
                RootElement = "address"
            };
            request.AddUrlSegment("id", id);

            if (carrier != null)
            {
                request.AddParameter("carrier", carrier);
            }

            Merge(await request.Execute<Address>());
        }

        /// <summary>
        ///     List all Address objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing an Address ID. Starts with "adr_". Only retrieve addresses created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing an Address ID. Starts with "adr". Only retrieve addresses created after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve addresses created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve addresses created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.AddressCollection instance.</returns>
        public static async Task<AddressCollection> All(Dictionary<string, object>? parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();
            Request request = new Request("addresses", Method.Get, parameters);

            return await request.Execute<AddressCollection>();
        }

        /// <summary>
        ///     Create an Address.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the address with. Valid pairs:
        ///     * {"name", string}
        ///     * {"company", string}
        ///     * {"street1", string}
        ///     * {"street2", string}
        ///     * {"city", string}
        ///     * {"state", string}
        ///     * {"zip", string}
        ///     * {"country", string}
        ///     * {"phone", string}
        ///     * {"email", string}
        ///     * {"verifications", List&lt;string&gt;} Possible items are "delivery" and "zip4".
        ///     * {"strict_verifications", List&lt;string&gt;} Possible items are "delivery" and "zip4".
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Address instance.</returns>
        public static async Task<Address> Create(Dictionary<string, object>? parameters = null)
        {
            Request request = new Request("addresses", Method.Post);
            return await SendCreate(request, parameters);
        }

        /// <summary>
        ///     Create and verify an Address.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the address with. Valid pairs:
        ///     * {"name", string}
        ///     * {"company", string}
        ///     * {"street1", string}
        ///     * {"street2", string}
        ///     * {"city", string}
        ///     * {"state", string}
        ///     * {"zip", string}
        ///     * {"country", string}
        ///     * {"phone", string}
        ///     * {"email", string}
        ///     All invalid keys will be ignored.
        /// </param>
        public static async Task<Address> CreateAndVerify(Dictionary<string, object>? parameters = null)
        {
            Request request = new Request("addresses/create_and_verify", Method.Post)
            {
                RootElement = "address"
            };
            return await SendCreate(request, parameters);
        }

        /// <summary>
        ///     Retrieve an Address from its id.
        /// </summary>
        /// <param name="id">String representing an Address. Starts with "adr_".</param>
        /// <returns>EasyPost.Address instance.</returns>
        public static async Task<Address> Retrieve(string id)
        {
            Request request = new Request("addresses/{id}", Method.Get);
            request.AddUrlSegment("id", id);

            return await request.Execute<Address>();
        }

        private static async Task<Address> SendCreate(Request request, Dictionary<string, object>? parameters = null)
        {
            parameters ??= new Dictionary<string, object>();
            Dictionary<string, object> body = new Dictionary<string, object>();

            if (parameters.ContainsKey("verify"))
            {
                body.Add("verify", parameters["verify"]);
                // removing verify from the address data parameters, since it needs to be one key above
                parameters.Remove("verify");
            }

            if (parameters.ContainsKey("verify_strict"))
            {
                body.Add("verify_strict", parameters["verify_strict"]);
                // removing verify_strict from the address data parameters, since it needs to be one key above
                parameters.Remove("verify_strict");
            }

            body.Add("address", parameters);

            request.AddParameters(body);

            return await request.Execute<Address>();
        }
    }
}
