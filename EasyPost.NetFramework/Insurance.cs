using System.Collections.Generic;
using RestSharp;

namespace EasyPost
{
    public class Insurance : Resource
    {
        public string amount { get; set; }
        public Address from_address { get; set; }
        public string id { get; set; }
        public List<string> messages { get; set; }
        public string mode { get; set; }
        public string provider { get; set; }
        public string provider_id { get; set; }
        public string reference { get; set; }
        public string shipment_id { get; set; }
        public string status { get; set; }
        public Address to_address { get; set; }
        public Tracker tracker { get; set; }
        public string tracking_code { get; set; }

        /// <summary>
        ///     Refresh this Insurance.
        /// </summary>
        /// <param name="parameters">Optional dictionary of parameters to use when refreshing this insurance.</param>
        /// <returns>This refreshed EasyPost.Insurance object.</returns>
        public Insurance Refresh(Dictionary<string, object> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();
            Request request = new Request("insurances/{id}");
            request.AddUrlSegment("id", id);
            request.AddQueryString(parameters);

            Insurance refreshedInsurance = request.Execute<Insurance>();
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
        /// <returns>EasyPost.InsuranceCollection instance.</returns>
        public static InsuranceCollection All(Dictionary<string, object> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();
            Request request = new Request("insurances");
            request.AddQueryString(parameters);

            return request.Execute<InsuranceCollection>();
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
        public static Insurance Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("insurances", Method.POST);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "insurance", parameters
                }
            });

            return request.Execute<Insurance>();
        }

        /// <summary>
        ///     Retrieve an Insurance from its id.
        /// </summary>
        /// <param name="id">String representing an Insurance. Starts with "ins_".</param>
        /// <returns>EasyPost.Insurance instance.</returns>
        public static Insurance Retrieve(string id)
        {
            Request request = new Request("insurances/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Insurance>();
        }

        private static Insurance SendCreate(Dictionary<string, object> parameters)
        {
            Request request = new Request("insurances", Method.POST);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "insurance", parameters
                }
            });

            return request.Execute<Insurance>();
        }
    }
}
