using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Insurance : Resource
    {
        [JsonProperty("amount")]
        public string amount { get; set; }
        [JsonProperty("from_address")]
        public Address from_address { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("messages")]
        public List<string> messages { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("provider")]
        public string provider { get; set; }
        [JsonProperty("provider_id")]
        public string provider_id { get; set; }
        [JsonProperty("reference")]
        public string reference { get; set; }
        [JsonProperty("shipment_id")]
        public string shipment_id { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("to_address")]
        public Address to_address { get; set; }
        [JsonProperty("tracker")]
        public Tracker tracker { get; set; }
        [JsonProperty("tracking_code")]
        public string tracking_code { get; set; }

        /// <summary>
        ///     Refresh this Insurance.
        /// </summary>
        /// <param name="parameters">Optional dictionary of parameters to use when refreshing this insurance.</param>
        /// <returns>This refreshed EasyPost.Insurance object.</returns>
        public async Task<Insurance> Refresh(Dictionary<string, object> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();
            Request request = new Request("insurances/{id}");
            request.AddUrlSegment("id", id);
            request.AddQueryString(parameters);

            Insurance refreshedInsurance = await request.Execute<Insurance>();
            Merge(refreshedInsurance);
            return this;
        }

        /// <summary>
        ///     List all Insurance objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing an Insurance ID. Starts with "ins_". Only retrieve insurances created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing an Insurance ID. Starts with "ins_". Only retrieve insurances created after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve insurances created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve insurances created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.InsuranceCollection instance.</returns>
        public static async Task<InsuranceCollection> All(Dictionary<string, object> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();
            Request request = new Request("insurances");
            request.AddQueryString(parameters);

            return await request.Execute<InsuranceCollection>();
        }

        /// <summary>
        ///     Create an Insurance.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the insurance with. Valid pairs:
        ///     * {"to_address", Address} The actual destination of the package to be insured
        ///     * {"from_address", Address} The actual origin of the package to be insured
        ///     * {"tracking_code", string} The tracking code associated with the non-EasyPost-purchased package you'd like to insure
        ///     * {"reference", string} Optional. A unique value that may be used in place of ID when doing Retrieve calls for this insurance
        ///     * {"amount", decimal} The USD value of contents you would like to insure. Currently the maximum is $5000
        ///     * {"carrier", string} Optional. The carrier associated with the tracking_code you provided. The carrier will get auto-detected if none is provided
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Insurance instance.</returns>
        public static async Task<Insurance> Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("insurances", Method.Post);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "insurance", parameters
                }
            });

            return await request.Execute<Insurance>();
        }

        /// <summary>
        ///     Retrieve an Insurance from its id.
        /// </summary>
        /// <param name="id">String representing an Insurance. Starts with "ins_".</param>
        /// <returns>EasyPost.Insurance instance.</returns>
        public static async Task<Insurance> Retrieve(string id)
        {
            Request request = new Request("insurances/{id}");
            request.AddUrlSegment("id", id);

            return await request.Execute<Insurance>();
        }

        private static async Task<Insurance> SendCreate(Dictionary<string, object> parameters)
        {
            Request request = new Request("insurances", Method.Post);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "insurance", parameters
                }
            });

            return await request.Execute<Insurance>();
        }
    }
}
