using System;
using System.Collections.Generic;

using RestSharp;

namespace EasyPost {
    public class Report : Resource {
        public string id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string mode { get; set; }
        public string status { get; set; }
        public Boolean include_children { get; set; }
        public string url { get; set; }
        public DateTime? url_expires_at { get; set; }

        /// <summary>
        /// Retrieve a Report from its id and type.
        /// </summary>
        /// <param name="type">Type of report, e.g. shipment, tracker, payment_log, etc.</param>
        /// <param name="id">String representing a report.</param>
        /// <returns>EasyPost.Report instance.</returns>
        public static Report Retrieve(string type, string id) {
            Request request = new Request("reports/{type}/{id}");
            request.AddUrlSegment("id", id);
            request.AddUrlSegment("type", type);

            return request.Execute<Report>();
        }

        /// <summary>
        /// Create a Report.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///   * {"start_date", string} Date to start the report at.
        ///   * {"end_date", string} Date to end the report at.
        ///   * {"include_children", string} Whether or not to include child objects in the report.
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Report instance.</returns>
        public static Report Create(string type, Dictionary<string, object> parameters = null) {
            Request request = new Request("reports/{type}", Method.POST);
            request.AddUrlSegment("type", type);
            request.AddQueryString(parameters ?? new Dictionary<string, object>());

            return request.Execute<Report>();
        }

        /// <summary>
        /// Get a paginated list of reports.
        /// </summary>
        /// Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///   * {"before_id", string} String representing a Report ID. Only retrieve ScanForms created before this id. Takes precedence over after_id.
        ///   * {"after_id", string} String representing a Report ID. Only retrieve ScanForms created after this id.
        ///   * {"start_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created after this datetime.
        ///   * {"end_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created before this datetime.
        ///   * {"page_size", int} Max size of list. Default to 20.
        /// All invalid keys will be ignored.
        /// <param name="parameters">
        /// </param>
        /// <returns>Instance of EasyPost.ScanForm</returns>
        public static ReportList List(string type, Dictionary<string, object> parameters = null) {
            Request request = new Request("reports/{type}");
            request.AddUrlSegment("type", type);
            request.AddQueryString(parameters ?? new Dictionary<string, object>());

            ReportList reportList = request.Execute<ReportList>();
            reportList.filters = parameters;
            reportList.type = type;
            return reportList;
        }
    }
}
