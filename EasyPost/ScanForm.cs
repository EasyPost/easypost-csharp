using System;
using System.Collections.Generic;

namespace EasyPost {
    public class ScanForm : IResource {
        public string id { get; set; }
        public Nullable<DateTime> created_at { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
        public List<string> tracking_codes { get; set; }
        public Address address { get; set; }
        public string form_url { get; set; }
        public string form_file_type { get; set; }
        public string mode { get; set; }
        public string status { get; set; }
        public string message { get; set; }

        /// <summary>
        /// Get a paginated list of scan forms.
        /// </summary>
        /// Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///   * {"before_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created before this id. Takes precedence over after_id.
        ///   * {"after_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created after this id.
        ///   * {"start_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created after this datetime.
        ///   * {"end_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created before this datetime.
        ///   * {"page_size", int} Max size of list. Default to 20.
        /// All invalid keys will be ignored.
        /// <param name="parameters">
        /// </param>
        /// <returns>Instance of EasyPost.ScanForm</returns>
        public static ScanFormList List(Dictionary<string, object> parameters = null) {
            Request request = new Request("scan_forms");
            request.AddQueryString(parameters ?? new Dictionary<string, object>());

            ScanFormList scanFormList = request.Execute<ScanFormList>();
            scanFormList.filters = parameters;
            return scanFormList;
        }
    }
}
