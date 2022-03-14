using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Report : Resource
    {
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("end_date")]
        public DateTime? end_date { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("include_children")]
        public bool include_children { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("start_date")]
        public DateTime? start_date { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("url")]
        public string url { get; set; }
        [JsonProperty("url_expires_at")]
        public DateTime? url_expires_at { get; set; }

        /// <summary>
        ///     Create a Report.
        /// </summary>
        /// <param name="type">
        ///     The type of report, e.g. "shipment", "tracker", "payment_log", etc.
        /// </param>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * {"start_date", string} Date to start the report at.
        ///     * {"end_date", string} Date to end the report at.
        ///     * {"include_children", string} Whether or not to include child objects in the report.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Report instance.</returns>
        public static async Task<Report> Create(string type, Dictionary<string, object> parameters = null)
        {
            Request request = new Request("reports/{type}", Method.Post);
            request.AddUrlSegment("type", type);
            request.AddQueryString(parameters ?? new Dictionary<string, object>());

            return await request.Execute<Report>();
        }

        /// <summary>
        ///     Get a paginated list of reports.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing a Report ID. Only retrieve ScanForms created before this id. Takes
        ///     precedence over after_id.
        ///     * {"after_id", string} String representing a Report ID. Only retrieve ScanForms created after this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <param name="type">The type of report, e.g. "shipment", "tracker", "payment_log", etc.</param>
        /// <returns>An EasyPost.ReportCollection instance.</returns>
        public static async Task<ReportCollection> All(string type, Dictionary<string, object> parameters = null)
        {
            Request request = new Request("reports/{type}");
            request.AddUrlSegment("type", type);
            request.AddQueryString(parameters ?? new Dictionary<string, object>());

            ReportCollection reportCollection = await request.Execute<ReportCollection>();
            reportCollection.filters = parameters;
            reportCollection.type = type;
            return reportCollection;
        }


        /// <summary>
        ///     Retrieve a Report from its id.
        /// </summary>
        /// <param name="id">String representing a report.</param>
        /// <returns>EasyPost.Report instance.</returns>
        public static async Task<Report> Retrieve(string id)
        {
            Request request = new Request("reports/{id}");
            request.AddUrlSegment("id", id);

            return await request.Execute<Report>();
        }

        /// <summary>
        ///     Retrieve a Report from its id and type.
        /// </summary>
        /// <param name="type">Type of report, e.g. shipment, tracker, payment_log, etc.</param>
        /// <param name="id">String representing a report.</param>
        /// <returns>EasyPost.Report instance.</returns>
        public static async Task<Report> Retrieve(string type, string id)
        {
            Request request = new Request("reports/{type}/{id}");
            request.AddUrlSegment("id", id);
            request.AddUrlSegment("type", type);

            return await request.Execute<Report>();
        }
    }
}
