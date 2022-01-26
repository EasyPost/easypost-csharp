using System;
using System.Collections.Generic;
using RestSharp;

namespace EasyPost
{
    public class ScanForm : Resource
    {
#pragma warning disable IDE1006 // Naming Styles
        public string id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public List<string> tracking_codes { get; set; }
        public Address address { get; set; }
        public string form_url { get; set; }
        public string form_file_type { get; set; }
        public string mode { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string batch_id { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Get a paginated list of scan forms.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///   * {"before_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created before this id. Takes precedence over after_id.
        ///   * {"after_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created after this id.
        ///   * {"start_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created after this datetime.
        ///   * {"end_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created before this datetime.
        ///   * {"page_size", int} Max size of list. Default to 20.
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>Instance of EasyPost.ScanForm.</returns>
        public static ScanFormList List(Dictionary<string, object> parameters = null)
        {
            Request request = new Request("v2/scan_forms");
            request.AddQueryString(parameters ?? new Dictionary<string, object>());

            ScanFormList scanFormList = request.Execute<ScanFormList>();
            scanFormList.filters = parameters;
            return scanFormList;
        }

        /// <summary>
        /// Create a ScanForm.
        /// </summary>
        /// <param name="shipments">Shipments to be associated with the ScanForm. Only id is required.</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        public static ScanForm Create(List<Shipment> shipments)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "shipments", shipments }
            };

            Request request = new Request("v2/scan_forms", Method.POST);
            request.AddBody(new Dictionary<string, object>() { { "scan_form", parameters } });

            return request.Execute<ScanForm>();
        }

        /// <summary>
        /// Retrieve a ScanForm from its id.
        /// </summary>
        /// <param name="id">String representing a scan form, starts with "sf_".</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        public static ScanForm Retrieve(string id)
        {
            Request request = new Request("v2/scan_forms/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<ScanForm>();
        }
    }
}