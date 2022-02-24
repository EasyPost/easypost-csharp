using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class ScanForm : Resource
    {
        [JsonProperty("address")]
        public Address address { get; set; }
        [JsonProperty("batch_id")]
        public string batch_id { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("form_file_type")]
        public string form_file_type { get; set; }
        [JsonProperty("form_url")]
        public string form_url { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("tracking_codes")]
        public List<string> tracking_codes { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Create a ScanForm.
        /// </summary>
        /// <param name="shipments">Shipments to be associated with the ScanForm. Only id is required.</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        public static ScanForm Create(List<Shipment> shipments)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {
                    "shipments", shipments
                }
            };

            Request request = new Request("scan_forms", Method.POST);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "scan_form", parameters
                }
            });

            return request.Execute<ScanForm>();
        }


        /// <summary>
        ///     Get a paginated list of scan forms.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>Instance of EasyPost.ScanForm.</returns>
        public static ScanFormList All(Dictionary<string, object> parameters = null)
        {
            Request request = new Request("scan_forms");
            request.AddQueryString(parameters ?? new Dictionary<string, object>());

            ScanFormList scanFormList = request.Execute<ScanFormList>();
            scanFormList.filters = parameters;
            return scanFormList;
        }

        /// <summary>
        ///     Retrieve a ScanForm from its id.
        /// </summary>
        /// <param name="id">String representing a scan form, starts with "sf_".</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        public static ScanForm Retrieve(string id)
        {
            Request request = new Request("scan_forms/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<ScanForm>();
        }
    }
}
